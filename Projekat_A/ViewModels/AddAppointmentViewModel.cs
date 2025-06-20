using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using System;
using System.Collections.ObjectModel;
using System.Windows;

namespace Projekat_A.ViewModels
{
    public class AddAppointmentViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainerService _trainerService;
        private readonly Storage _storage;
        private readonly INavigationService _navigationService;
        private readonly CustomMessageBoxService _customMessageBoxService;


        private string _sessionDetails;
        private Hall _selectedHall;
        private ObservableCollection<Hall> _availableHalls;

        public string SessionDetails
        {
            get => _sessionDetails;
            set
            {
                _sessionDetails = value;
                OnPropertyChanged(nameof(SessionDetails));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public Hall SelectedHall
        {
            get => _selectedHall;
            set
            {
                _selectedHall = value;
                OnPropertyChanged(nameof(SelectedHall));
                SaveCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<Hall> AvailableHalls
        {
            get => _availableHalls;
            set
            {
                _availableHalls = value;
                OnPropertyChanged(nameof(AvailableHalls));
            }
        }

        public RelayCommand SaveCommand { get; private set; }
        public RelayCommand CancelCommand { get; private set; }

        public AddAppointmentViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _trainerService = _serviceProvider.GetRequiredService<TrainerService>();
            _storage = _serviceProvider.GetRequiredService<Storage>();
            _navigationService = _serviceProvider.GetRequiredService<INavigationService>();
            _customMessageBoxService = _serviceProvider.GetRequiredService<CustomMessageBoxService>();


            LoadHalls();
            InitializeCommands();
        }

        private void LoadHalls()
        {
            var halls = _trainerService.GetAllHalls();
            AvailableHalls = new ObservableCollection<Hall>(halls);
        }

        private void InitializeCommands()
        {
            SaveCommand = new RelayCommand(ExecuteSave, CanExecuteSave);
            CancelCommand = new RelayCommand(ExecuteCancel, _ => true); 
        }

        private bool CanExecuteSave(object parameter)
        {
            return !string.IsNullOrWhiteSpace(SessionDetails) && SelectedHall != null;
        }

        private void ExecuteSave(object parameter)
        {
            var newSession = new TrainingSession
            {
                Session = SessionDetails,
                TrainerUserId = _storage.User.Id, 
                HallIdHall = SelectedHall.IdHall
            };

            if (_trainerService.ValidateTrainingSession(newSession))
            {
                bool result = _trainerService.AddTrainingSession(newSession);

                if (result)
                {
                    _customMessageBoxService.Show("infoTitle", "sessionAddedSuccess", MessageBoxButton.OK);

                    _navigationService.NavigateTo<TrainerAppointmentsViewModel>();
                    _serviceProvider.GetRequiredService<TrainerAppointmentsViewModel>().setFilters();
                }
                else
                {
                    _customMessageBoxService.Show("errorTitle", "sessionAddFailed", MessageBoxButton.OK);
                }
            }
            else
            {
                _customMessageBoxService.Show("errorTitle", "sessionValidationError", MessageBoxButton.OK);
            }
        }

        private void ExecuteCancel(object parameter)
        {
            _navigationService.NavigateTo<TrainerAppointmentsViewModel>();
            _serviceProvider.GetRequiredService<TrainerAppointmentsViewModel>().setFilters();
        }
    }
}