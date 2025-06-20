using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Services;
using Projekat_A.Util;
using System.Collections.ObjectModel;
using System.Windows;


namespace Projekat_A.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        private INavigationService? _navigation;
        private IServiceProvider _serviceProvider;
        private String? _username;
        private String? _accountType;

        public INavigationService Navigation
        {
            get => _navigation;
            set
            {
                _navigation = value;
                OnPropertyChanged();
            }
        }

        public String Username
        {
            get => _username ?? string.Empty;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public String AccountType
        {
            get => _accountType ?? string.Empty;
            set
            {
                _accountType = value;
                OnPropertyChanged(nameof(AccountType));
            }
        }
        public RelayCommand SettingsPage { get; set; }
        public ObservableCollection<MenuItem> Menu { get; set; }
        public UserViewModel(INavigationService navService, IServiceProvider serviceProvider)
        {
            Menu = new ObservableCollection<MenuItem>();
            _serviceProvider = serviceProvider;

            var user = _serviceProvider.GetRequiredService<Storage>().User ?? new Models.User();
            var userService = _serviceProvider.GetRequiredService<UserService>();

            Username = user.Username;
            AccountType = user.AccountType;
            userService.logUser((sbyte)1);

            Navigation = navService;

            _serviceProvider.GetRequiredService<SettingsViewModel>().loadUserPreferences();

            if (AccountType.Equals("Member"))
            {
                Console.WriteLine("Member");
                Navigation.NavigateTo<ReservationViewModel>();
                 Menu.Add(new MenuItem
                 {
                     Text = (string)App.Current.Resources["reservation"],
                     Command = new RelayCommand(o => {
                         Navigation.NavigateTo<ReservationViewModel>();
                         var reservationVM = _serviceProvider.GetRequiredService<ReservationViewModel>();
                         Task.Run(async () => await reservationVM.InitAsync());
                     }, o => true),
                     Icon = "Reservation"
                 });
                Menu.Add(new MenuItem
                {
                    Text = (string)App.Current.Resources["viewMemberships"],
                    Command = new RelayCommand(o => {
                        Navigation.NavigateTo<ViewMembershipViewModel>();
                        _serviceProvider.GetRequiredService<ViewMembershipViewModel>().LoadData();
                    }, o => true),
                    Icon = "CardMembership"
                });
                Menu.Add(new MenuItem
                {
                    Text = (string)App.Current.Resources["membershipPayment"],
                    Command = new RelayCommand(async o => {
                        Navigation.NavigateTo<MembershipPaymentViewModel>();
                        var membershipPaymentVM = _serviceProvider.GetRequiredService<MembershipPaymentViewModel>();

                        await Application.Current.Dispatcher.InvokeAsync(async () => {
                            await membershipPaymentVM.LoadDataAsync();
                        });
                    }, o => true),
                    Icon = "Cash"
                });

            }
            else if (AccountType.Equals("Trainer"))
            {
                Console.WriteLine("Trainer");
                Navigation.NavigateTo<TrainerAppointmentsViewModel>();
                 Menu.Add(new MenuItem
                 {
                     Text = (string)App.Current.Resources["viewTrainerAppointment"],
                     Command = new RelayCommand(o => {
                         _serviceProvider.GetRequiredService<TrainerAppointmentsViewModel>().setFilters();
                         Navigation.NavigateTo<TrainerAppointmentsViewModel>();
                     }, o => true),
                     Icon = "CalendarAccount"
                 });
                 Menu.Add(new MenuItem
                 {
                     Text = (string)App.Current.Resources["addAppointment"],
                     Command = new RelayCommand(o => { Navigation.NavigateTo<AddAppointmentViewModel>(); }, o => true),
                     Icon = "CalendarPlus"
                 });
                 Menu.Add(new MenuItem
                 {
                     Text = (string)App.Current.Resources["viewHalls"],
                     Command = new RelayCommand(o => { Navigation.NavigateTo<ViewHallsViewModel>(); }, o => true),
                     Icon = "Domain"
                 });

            }
            else if (AccountType.Equals("Group"))
            {
                Console.WriteLine("Group");
                Navigation.NavigateTo<ViewMembersViewModel>();
                Menu.Add(new MenuItem
                {
                    Text = (string)App.Current.Resources["viewMembers"],
                    Command = new RelayCommand(o => {
                        _serviceProvider.GetRequiredService<ViewMembersViewModel>().LoadData();
                        Navigation.NavigateTo<ViewMembersViewModel>();
                    }, o => true),
                    Icon = "People"
                });
            }

             Menu.Add(new MenuItem
             {
                Text = (string)App.Current.Resources["settings"],
                Command = new RelayCommand(o => { Navigation.NavigateTo<SettingsViewModel>(); }, o => true),
                Icon = "Settings"
             });
            Menu.Add(new MenuItem
            {
                Text = (string)App.Current.Resources["shutdown"],
                Command = new RelayCommand(o => { userService.logUser((sbyte)0); App.Current.Shutdown(); }, o => true),
                Icon = "Power"
            });
        }
    }
}
