using SalonApp.Modules.Clients.Application;
using SalonApp.Modules.Clients.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace SalonApp.WPF.ViewModels
{
    public class ClientsViewModel : INotifyPropertyChanged
    {
        private readonly ClientService _clientService;
        public ObservableCollection<Client> Clients { get; set; } = new();
        
        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set
            {
                
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set
            {
                 _lastName = value;
                 OnPropertyChanged(nameof(LastName));
                
            }
        }

        private string _phone = string.Empty;
        public string Phone
        {
            get => _phone;
            set 
            { 
                _phone = value; 
                OnPropertyChanged(nameof(Phone)); 
            }
        }

        private string _email = string.Empty;
        public string Email
        {
            get => _email;
            set 
            { 
                _email = value; 
                OnPropertyChanged(nameof(Email)); 
            }
        }

        private Client? _selectedClient;
        public Client? SelectedClient
        {
            get => _selectedClient;
            set
            {
                _selectedClient = value;
                OnPropertyChanged(nameof(SelectedClient));
                if (value != null)
                {
                    FirstName = value.FirstName;
                    LastName = value.LastName;
                    Phone = value.Phone;
                    Email = value.Email;
                }
            }
        }

        public ClientsViewModel(ClientService clientService)
        {
            _clientService = clientService;
            LoadClientsAsync();
        }

        private async void LoadClientsAsync()
        {
            var clients = await _clientService.GetAllClientsAsync();
            Clients.Clear();
            foreach(var client in clients)
            {
                Clients.Add(client);
            }
        }

        public async Task AddClientAsync()
        {
            try
            {
                var newClient = new Client
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Phone = Phone,
                    Email = Email
                };

                await _clientService.AddClientAsync(newClient);

                FirstName = string.Empty;
                LastName = string.Empty;
                Phone = string.Empty;
                Email = string.Empty;

                LoadClientsAsync();
            } catch (InvalidOperationException ex) 
            {
                System.Windows.MessageBox.Show(ex.Message, "Greška");
            }
           
        }

        public async Task DeleteClientAsync(Client client)
        {
            await _clientService.DeleteClientAsync(client.Id);
            LoadClientsAsync();
        }

        public async Task UpdateClientAsync()
        {
            if (SelectedClient == null) return;

            try
            {
                SelectedClient.FirstName = FirstName;
                SelectedClient.LastName = LastName;
                SelectedClient.Phone = Phone;
                SelectedClient.Email = Email;

                await _clientService.UpdateClientAsync(SelectedClient);

                SelectedClient = null;
                FirstName = string.Empty;
                LastName = string.Empty;
                Phone = string.Empty;
                Email = string.Empty;

                LoadClientsAsync();
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
}
