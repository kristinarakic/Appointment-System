using SalonApp.Modules.Staff.Application;
using SalonApp.Modules.Staff.Domain;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SalonApp.WPF.ViewModels
{
    public class StaffViewModel : INotifyPropertyChanged
    {
        private readonly StaffService _staffService;

        public ObservableCollection<StaffMember> StaffMembers { get; set; } = new();

        private string _firstName = string.Empty;
        public string FirstName
        {
            get => _firstName;
            set { _firstName = value; OnPropertyChanged(nameof(FirstName)); }
        }

        private string _lastName = string.Empty;
        public string LastName
        {
            get => _lastName;
            set { _lastName = value; OnPropertyChanged(nameof(LastName)); }
        }

        private string _specialty = string.Empty;
        public string Specialty
        {
            get => _specialty;
            set { _specialty = value; OnPropertyChanged(nameof(Specialty)); }
        }

        public StaffViewModel(StaffService staffService)
        {
            _staffService = staffService;
            LoadStaffAsync();
        }

        private async void LoadStaffAsync()
        {
            var staff = await _staffService.GetAllStaffMembersAsync();
            StaffMembers.Clear();
            foreach (var member in staff)
            {
                StaffMembers.Add(member);
            }
        }

        public async Task AddStaffMemberAsync()
        {
            try
            {
                var member = new StaffMember
                {
                    FirstName = FirstName,
                    LastName = LastName,
                    Specialty = Specialty
                };

                await _staffService.AddStaffMemberAsync(member);

                FirstName = string.Empty;
                LastName = string.Empty;
                Specialty = string.Empty;

                LoadStaffAsync();
            } catch (InvalidOperationException ex)
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
