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

    private int _durationMinutes;
    public int DurationMinutes
    {
        get => _durationMinutes;
        set { _durationMinutes = value; OnPropertyChanged(nameof(DurationMinutes)); }
    }

    private decimal _price;
    public decimal Price
    {
        get => _price;
        set { _price = value; OnPropertyChanged(nameof(Price)); }
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
                DurationInMinutes = DurationMinutes,
                Price = Price
            };

            await _serviceManager.AddServiceAsync(service);

            Name = string.Empty;
            DurationMinutes = 0;
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
        LoadServicesAsync();
    }

    public event PropertyChangedEventHandler? PropertyChanged;

    protected void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }
}