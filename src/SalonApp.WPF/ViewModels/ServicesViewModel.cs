using SalonApp.Modules.Services.Application;
using SalonApp.Modules.Services.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.WPF.ViewModels;

public class ServicesViewModel : INotifyPropertyChanged
{
    private readonly ServiceManager _serviceManager;

    public ObservableCollection<Service> Services { get; set; } = new();

    private string _name = string.Empty;
    public string Name
    {
        get => _name;
        set { _name = value; OnPropertyChanged(nameof(Name)); }
    }

    private int _durationInMinutes;
    public int DurationInMinutes
    {
        get => _durationInMinutes;
        set { _durationInMinutes = value; OnPropertyChanged(nameof(DurationInMinutes)); }
    }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set { _price = value; OnPropertyChanged(nameof(Price)); }
    }

    private Service? _selectedService;
    public Service? SelectedService
    {
        get => _selectedService;
        set
        {
            _selectedService = value;
            OnPropertyChanged(nameof(SelectedService));
            if (value != null)
            {
                Name = value.Name;
                DurationInMinutes = value.DurationInMinutes;
                Price = value.Price;
            }
        }
    }

    public ServicesViewModel(ServiceManager serviceManager)
    {
        _serviceManager = serviceManager;
        LoadServicesAsync();
    }

    private async void LoadServicesAsync()
    {
        var services = await _serviceManager.GetAllServicesAsync();
        Services.Clear();
        foreach (var service in services)
        {
            Services.Add(service);
        }
    }

    public async Task AddServiceAsync()
    {
        try
        {
            var service = new Service
            {
                Name = Name,
                DurationInMinutes = DurationInMinutes,
                Price = Price
            };

            await _serviceManager.AddServiceAsync(service);

            Name = string.Empty;
            DurationInMinutes = 0;
            Price = 0;

            LoadServicesAsync();
        }
        catch (InvalidOperationException ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Greška");
        }

    }

    public async Task DeleteServiceAsync(Service service)
    {
        await _serviceManager.DeleteServiceAsync(service.Id);

        SelectedService = null;
        Name = string.Empty;
        DurationInMinutes = 0;
        Price = 0;

        LoadServicesAsync();
    }

    public async Task UpdateServiceAsync()
    {
        if (SelectedService == null) return;

        try
        {
            SelectedService.Name = Name;
            SelectedService.DurationInMinutes = DurationInMinutes;
            SelectedService.Price = Price;

            await _serviceManager.UpdateServiceAsync(SelectedService);

            SelectedService = null;
            Name = string.Empty;
            DurationInMinutes = 0;
            Price = 0;

            LoadServicesAsync();
        }
        catch (InvalidOperationException ex)
        {
            System.Windows.MessageBox.Show(ex.Message, "Greška");
        }
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}