using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Projekat_A.ViewModels
{
    public class TrainingSessionViewModel
    {
        public TrainingSession Session { get; set; }
        public string TrainerName => $"{Session.TrainerUser.Name} {Session.TrainerUser.Surname}";
        public string TrainerSpecialization => Session.TrainerUser.Specialization;
        public string HallName => Session.HallIdHallNavigation.Name;
        public string HallLocation => Session.HallIdHallNavigation.Location;
        public string SessionDetails => Session.Session;
        public bool IsReserved { get; set; }

        public TrainingSessionViewModel(TrainingSession session, bool isReserved = false)
        {
            Session = session;
            IsReserved = isReserved;
        }
    }

    public class ReservationViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MemberService _memberService;
        private ObservableCollection<TrainingSessionViewModel> _availableSessions;
        private ObservableCollection<TrainingSessionViewModel> _myReservations;
        private TrainingSessionViewModel _selectedSession;
        private bool _isLoading;
        private Member _currentMember;

        public ObservableCollection<TrainingSessionViewModel> AvailableSessions
        {
            get => _availableSessions;
            set
            {
                _availableSessions = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<TrainingSessionViewModel> MyReservations
        {
            get => _myReservations;
            set
            {
                _myReservations = value;
                OnPropertyChanged();
            }
        }

        public TrainingSessionViewModel SelectedSession
        {
            get => _selectedSession;
            set
            {
                _selectedSession = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(CanReserve));
                OnPropertyChanged(nameof(CanCancel));
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
            }
        }

        public bool CanReserve => SelectedSession != null && !SelectedSession.IsReserved;
        public bool CanCancel => SelectedSession != null && SelectedSession.IsReserved;

        public ICommand ReserveSessionCommand { get; }
        public ICommand CancelReservationCommand { get; }

        public ReservationViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _memberService = new MemberService();

            AvailableSessions = new ObservableCollection<TrainingSessionViewModel>();
            MyReservations = new ObservableCollection<TrainingSessionViewModel>();

            ReserveSessionCommand = new RelayCommand(async param => await ReserveSession(),
                param => CanReserve);

            CancelReservationCommand = new RelayCommand(async param => await CancelReservation(),
                param => CanCancel);

            LoadCurrentMember();

            Task.Run(async () => await LoadData());
        }

        public async Task InitAsync()
        {
            await LoadCurrentMember();
            await LoadData();
        }


        private async Task LoadCurrentMember()
        {
            var user = _serviceProvider.GetRequiredService<Storage>().User;
            if (user != null)
            {
                _currentMember = await _memberService.GetMemberByUserIdAsync(user.Id);
            }
        }


        private async Task LoadData()
        {
            try
            {
                IsLoading = true;

                var user = _serviceProvider.GetRequiredService<Storage>().User;
                if (user == null)
                {
                    return;
                }

                if (_currentMember == null)
                {
                    _currentMember = await _memberService.GetMemberByUserIdAsync(user.Id);
                    if (_currentMember == null)
                    {
                        return;
                    }
                }

                var allSessions = await _memberService.GetAvailableTrainingSessionsAsync();

                var reservedSessions = await _memberService.GetMemberReservationsAsync(_currentMember.UserId);
                var reservedIds = reservedSessions.Select(s => s.IdSession).ToHashSet();

                Application.Current.Dispatcher.Invoke(() =>
                {
                    AvailableSessions.Clear();
                    MyReservations.Clear();

                    foreach (var session in allSessions)
                    {
                        bool isReserved = reservedIds.Contains(session.IdSession);
                        var sessionViewModel = new TrainingSessionViewModel(session, isReserved);

                        AvailableSessions.Add(sessionViewModel);

                        if (isReserved)
                        {
                            MyReservations.Add(sessionViewModel);
                        }
                    }
                });
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading data: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task ReserveSession()
        {
            if (SelectedSession == null) return;

            try
            {
                IsLoading = true;

                bool success = await _memberService.ReserveTrainingSessionAsync(
                    _currentMember.UserId,
                    SelectedSession.Session.IdSession);

                if (success)
                {
                    SelectedSession.IsReserved = true;

                    if (!MyReservations.Contains(SelectedSession))
                    {
                        MyReservations.Add(SelectedSession);
                    }

                    OnPropertyChanged(nameof(CanReserve));
                    OnPropertyChanged(nameof(CanCancel));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reserving session: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        private async Task CancelReservation()
        {
            if (SelectedSession == null) return;

            try
            {
                IsLoading = true;

                bool success = await _memberService.CancelReservationAsync(
                    _currentMember.UserId,
                    SelectedSession.Session.IdSession);

                if (success)
                {
                    SelectedSession.IsReserved = false;
                    MyReservations.Remove(SelectedSession);

                    OnPropertyChanged(nameof(CanReserve));
                    OnPropertyChanged(nameof(CanCancel));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }
    }
}