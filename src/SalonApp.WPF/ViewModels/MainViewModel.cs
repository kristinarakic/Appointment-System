using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.WPF.ViewModels
{
    public class MainViewModel
    {
        private object _currentViewModel;

        public object CurrentViewModel
        {
            get => _currentViewModel;
            set
            {
                _currentViewModel = value;
                OnPropertyChanged(nameof(CurrentViewModel));
            }
        }

        private readonly ClientsViewModel _clientsViewModel;

        public MainViewModel(ClientsViewModel clientsViewModel)
        {
            _clientsViewModel = clientsViewModel;
            _currentViewModel = _clientsViewModel;
        }

        public event PropertyChangedEventHandler? PropertyChanged;

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }   
    }
}
