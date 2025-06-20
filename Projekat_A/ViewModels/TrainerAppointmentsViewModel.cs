using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using Projekat_A.Views;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows;

namespace Projekat_A.ViewModels
{
    public class TrainerAppointmentsViewModel : BaseViewModel
    {
        private readonly INavigationService _navigationService;
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainerService _trainerService;
        private readonly CustomMessageBoxService _customMessageBoxService;
        private readonly Storage _storage;

        private ObservableCollection<TrainingSession> _trainingSessions;
        private TrainingSession _selectedSession;
        private ObservableCollection<Hall> _halls;
        private Hall _selectedHall;
        private string _filterText;
        private bool _isFiltering;
        private bool _isEditMode;
        private TrainingSession _editSession;
        private Hall _editHall;

        public ObservableCollection<TrainingSession> TrainingSessions
        {
            get => _trainingSessions;
            set
            {
                _trainingSessions = value;
                OnPropertyChanged();
            }
        }

        public TrainingSession SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Hall> Halls
        {
            get => _halls;
            set
            {
                _halls = value;
                OnPropertyChanged();
            }
        }

        public Hall SelectedHall
        {
            get => _selectedHall;
            set
            {
                _selectedHall = value;
                OnPropertyChanged();
                if (_selectedHall != null && !_isFiltering)
                {
                    FilterSessionsByHall();
                }
            }
        }

        public string FilterText
        {
            get => _filterText;
            set
            {
                _filterText = value;
                OnPropertyChanged();
            }
        }

        public bool IsEditMode
        {
            get => _isEditMode;
            set
            {
                _isEditMode = value;
                OnPropertyChanged();
            }
        }

        public TrainingSession EditSession
        {
            get => _editSession;
            set
            {
                _editSession = value;
                OnPropertyChanged();
            }
        }

        public Hall EditHall
        {
            get => _editHall;
            set
            {
                _editHall = value;
                OnPropertyChanged();
            }
        }

        public RelayCommand EditSessionCommand { get; private set; }
        public RelayCommand DeleteSessionCommand { get; private set; }
        public RelayCommand ResetFilterCommand { get; private set; }
        public RelayCommand ApplyFilterCommand { get; private set; }
        public RelayCommand SaveSessionCommand { get; private set; }
        public RelayCommand CancelEditCommand { get; private set; }

        public TrainerAppointmentsViewModel(INavigationService navigationService, IServiceProvider serviceProvider)
        {
            _navigationService = navigationService;
            _serviceProvider = serviceProvider;
            _trainerService = _serviceProvider.GetRequiredService<TrainerService>();
            _storage = _serviceProvider.GetRequiredService<Storage>();
            _customMessageBoxService = _serviceProvider.GetRequiredService<CustomMessageBoxService>();


            TrainingSessions = new ObservableCollection<TrainingSession>();
            Halls = new ObservableCollection<Hall>();
            _isFiltering = false;
            IsEditMode = false;

            EditSessionCommand = new RelayCommand(StartEditSession, CanEditSession);
            DeleteSessionCommand = new RelayCommand(DeleteSession, CanDeleteSession);
            ResetFilterCommand = new RelayCommand(ResetFilter, CanResetFilter);
            ApplyFilterCommand = new RelayCommand(ApplyFilter, CanApplyFilter);
            SaveSessionCommand = new RelayCommand(SaveSession, CanSaveSession);
            CancelEditCommand = new RelayCommand(CancelEdit, CanCancelEdit);

            LoadTrainerSessions();
            LoadHalls();
        }

        public void setFilters()
        {
            LoadTrainerSessions();
            LoadHalls();
        }

        private void LoadTrainerSessions()
        {
            TrainingSessions.Clear();
            var trainerId = _storage.User.Id;
            var sessions = _trainerService.GetTrainerSessions(trainerId);

            foreach (var session in sessions)
            {
                TrainingSessions.Add(session);
            }
        }

        private void LoadHalls()
        {
            Halls.Clear();
            var halls = _trainerService.GetAllHalls();

            foreach (var hall in halls)
            {
                Halls.Add(hall);
            }
        }

        private void FilterSessionsByHall()
        {
            if (SelectedHall == null) return;

            _isFiltering = true;
            TrainingSessions.Clear();

            var sessions = _trainerService.GetSessionsByHall(SelectedHall.IdHall)
                .Where(s => s.TrainerUserId == _storage.User.Id);

            foreach (var session in sessions)
            {
                TrainingSessions.Add(session);
            }
            _isFiltering = false;
        }

        private void StartEditSession(object parameter)
        {
            if (SelectedSession == null) return;

            EditSession = new TrainingSession
            {
                IdSession = SelectedSession.IdSession,
                Session = SelectedSession.Session,
                TrainerUserId = SelectedSession.TrainerUserId,
                TrainerUser = SelectedSession.TrainerUser,
                HallIdHall = SelectedSession.HallIdHall,
                HallIdHallNavigation = SelectedSession.HallIdHallNavigation
            };

            EditHall = Halls.FirstOrDefault(h => h.IdHall == EditSession.HallIdHall);

            IsEditMode = true;
        }

        private bool CanEditSession(object parameter)
        {
            return SelectedSession != null;
        }

        private void SaveSession(object parameter)
        {
            if (EditSession == null || EditHall == null) return;

            if (string.IsNullOrWhiteSpace(EditSession.Session))
            {
                _customMessageBoxService.Show("errorTitle", "emptySession", MessageBoxButton.OK);
                return;
            }

            try
            {
                var originalSession = _trainerService.GetTrainingSessionById(EditSession.IdSession);
                if (originalSession == null)
                {
                    _customMessageBoxService.Show("errorTitle", "sessionNotFound", MessageBoxButton.OK);
                    return;
                }

                originalSession.Session = EditSession.Session;
                originalSession.HallIdHall = EditHall.IdHall;
                bool success = _trainerService.UpdateTrainingSession(originalSession);

                if (success)
                {
                    LoadTrainerSessions();

                    SelectedSession = TrainingSessions.FirstOrDefault(s => s.IdSession == originalSession.IdSession);

                    _customMessageBoxService.Show("infoTitle", "sessionUpdated", MessageBoxButton.OK);

                    IsEditMode = false;
                }
                else
                {
                    _customMessageBoxService.Show("errorTitle", "updateFailed", MessageBoxButton.OK);
                }
            }
            catch (Exception ex)
            {
                _customMessageBoxService.Show("errorTitle", $"errorOccurred|{ex.Message}", MessageBoxButton.OK);

            }
        }

        private bool CanSaveSession(object parameter)
        {
            return EditSession != null && EditHall != null && !string.IsNullOrWhiteSpace(EditSession.Session);
        }

        private void CancelEdit(object parameter)
        {
            IsEditMode = false;
            EditSession = null;
            EditHall = null;
        }

        private bool CanCancelEdit(object parameter)
        {
            return IsEditMode;
        }

        private void DeleteSession(object parameter)
        {
            if (SelectedSession == null) return;

            var result = _customMessageBoxService.Show("confirmationTitle", "confirmationDelete", MessageBoxButton.YesNo);


            if (result == true)
            {
                bool success = _trainerService.DeleteTrainingSession(SelectedSession.IdSession);
                if (success)
                {
                    TrainingSessions.Remove(SelectedSession);
                    _customMessageBoxService.Show("infoTitle", "sessionDeleted", MessageBoxButton.OK);

                }
                else
                {
                    _customMessageBoxService.Show("errorTitle", "deleteFailed", MessageBoxButton.OK);

                }
            }
        }

        private bool CanDeleteSession(object parameter)
        {
            return SelectedSession != null;
        }

        private void ResetFilter(object parameter)
        {
            FilterText = string.Empty;
            SelectedHall = null;
            LoadTrainerSessions();
        }

        private bool CanResetFilter(object parameter)
        {
            return !string.IsNullOrEmpty(FilterText) || SelectedHall != null;
        }

        private void ApplyFilter(object parameter)
        {
            if (string.IsNullOrEmpty(FilterText) && SelectedHall == null)
            {
                LoadTrainerSessions();
                return;
            }

            _isFiltering = true;
            TrainingSessions.Clear();

            var sessions = _trainerService.GetTrainerSessions(_storage.User.Id);

            if (SelectedHall != null)
            {
                sessions = sessions.Where(s => s.HallIdHall == SelectedHall.IdHall).ToList();
            }

            if (!string.IsNullOrEmpty(FilterText))
            {
                sessions = sessions.Where(s =>
                    s.Session.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    s.HallIdHallNavigation.Name.Contains(FilterText, StringComparison.OrdinalIgnoreCase) ||
                    s.HallIdHallNavigation.Location.Contains(FilterText, StringComparison.OrdinalIgnoreCase)
                ).ToList();
            }

            foreach (var session in sessions)
            {
                TrainingSessions.Add(session);
            }
            _isFiltering = false;
        }

        private bool CanApplyFilter(object parameter)
        {
            return !string.IsNullOrEmpty(FilterText) || SelectedHall != null;
        }
    }
}