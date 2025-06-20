using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Services;
using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Controls;
using MaterialDesignThemes.Wpf;
using Projekat_A.Data;

namespace Projekat_A.ViewModels
{
    public class RegistrationViewModel : BaseViewModel
    {
        private String? _username;
        private String? _password;
        private String? _repeatedPassword;
        private String? _emailAddress;
        private String? _phoneNumber;
        private string? _accountType;
        private bool _isDarkTheme;

        private string? _firstName;
        private string? _lastName;
        private DateTime? _birthDate;
        private DateTime? _enrollmentDate;

        private string? _specialization;
        private string? _workHours;

        private string? _groupName;
        private string? _description;
        private string? _maxMembers;
        private int _trainerUserId;

        private IServiceProvider _serviceProvider;

        public String Username
        {
            get => _username ?? string.Empty;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        public String Password
        {
            get => _password ?? string.Empty;
            set
            {
                _password = value;
                OnPropertyChanged(nameof(Password));
            }
        }

        public String RepeatedPassword
        {
            get => _repeatedPassword ?? string.Empty;
            set
            {
                _repeatedPassword = value;
                OnPropertyChanged(nameof(RepeatedPassword));
            }
        }

        public String EmailAddress
        {
            get => _emailAddress ?? string.Empty;
            set
            {
                _emailAddress = value;
                OnPropertyChanged(nameof(EmailAddress));
            }
        }

        public String PhoneNumber
        {
            get => _phoneNumber ?? string.Empty;
            set
            {
                _phoneNumber = value;
                OnPropertyChanged(nameof(PhoneNumber));
            }
        }

        public string AccountType
        {
            get => _accountType ?? string.Empty;
            set
            {
                _accountType = value;
                OnPropertyChanged(nameof(AccountType));
            }
        }

        public bool isDarkTheme
        {
            get => _isDarkTheme;
            set
            {
                _isDarkTheme = value;
                OnPropertyChanged(nameof(isDarkTheme));

                var paletteHelper = new MaterialDesignThemes.Wpf.PaletteHelper();
                var theme = paletteHelper.GetTheme();

                theme.SetBaseTheme(value ?
                    MaterialDesignThemes.Wpf.Theme.Dark :
                    MaterialDesignThemes.Wpf.Theme.Light);

                paletteHelper.SetTheme(theme);
            }
        }

        public string FirstName
        {
            get => _firstName ?? string.Empty;
            set
            {
                _firstName = value;
                OnPropertyChanged(nameof(FirstName));
            }
        }

        public string LastName
        {
            get => _lastName ?? string.Empty;
            set
            {
                _lastName = value;
                OnPropertyChanged(nameof(LastName));
            }
        }

        public DateTime? BirthDate
        {
            get => _birthDate;
            set
            {
                _birthDate = value;
                OnPropertyChanged(nameof(BirthDate));
            }
        }

        public DateTime? EnrollmentDate
        {
            get => _enrollmentDate;
            set
            {
                _enrollmentDate = value;
                OnPropertyChanged(nameof(EnrollmentDate));
            }
        }

        public string Specialization
        {
            get => _specialization ?? string.Empty;
            set
            {
                _specialization = value;
                OnPropertyChanged(nameof(Specialization));
            }
        }

        public string WorkHours
        {
            get => _workHours ?? string.Empty;
            set
            {
                _workHours = value;
                OnPropertyChanged(nameof(WorkHours));
            }
        }

        public string GroupName
        {
            get => _groupName ?? string.Empty;
            set
            {
                _groupName = value;
                OnPropertyChanged(nameof(GroupName));
            }
        }

        public string Description
        {
            get => _description ?? string.Empty;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        public string MaxMembers
        {
            get => _maxMembers ?? string.Empty;
            set
            {
                _maxMembers = value;
                OnPropertyChanged(nameof(MaxMembers));
            }
        }

        public int TrainerUserId
        {
            get => _trainerUserId;
            set
            {
                _trainerUserId = value;
                OnPropertyChanged(nameof(TrainerUserId));
            }
        }

        public event Action? CloseAction;
        public RelayCommand RegisterUser { get; set; }
        public RelayCommand CloseWindow { get; set; }
        public RelayCommand DarkThemeCommand { get; set; }
        public RelayCommand CloseApplicationCommand { get; set; }

        public RegistrationViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            EnrollmentDate = DateTime.Today;

            RegisterUser = new RelayCommand(o =>
            {

                if (string.IsNullOrEmpty(Username) || string.IsNullOrEmpty(Password) ||
                    string.IsNullOrEmpty(RepeatedPassword) || string.IsNullOrEmpty(EmailAddress) ||
                    string.IsNullOrEmpty(PhoneNumber) || string.IsNullOrEmpty(AccountType))
                {
                    var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                    cmbs.Show("errorTitle", "allFieldsRequired", System.Windows.MessageBoxButton.OK);
                    return;
                }

                if (AccountType == "Član" || AccountType == "Member")
                {
                    if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                        BirthDate == null || EnrollmentDate == null)
                    {
                        var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                        cmbs.Show("errorTitle", "allFieldsRequired", System.Windows.MessageBoxButton.OK);
                        return;
                    }
                }
                else if (AccountType == "Trener" || AccountType == "Trainer")
                {
                    if (string.IsNullOrEmpty(FirstName) || string.IsNullOrEmpty(LastName) ||
                        string.IsNullOrEmpty(Specialization) || string.IsNullOrEmpty(WorkHours))
                    {
                        var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                        cmbs.Show("errorTitle", "allFieldsRequired", System.Windows.MessageBoxButton.OK);
                        return;
                    }
                }
                else if (AccountType == "Grupa" || AccountType == "Group")
                {
                    if (string.IsNullOrEmpty(GroupName) || string.IsNullOrEmpty(Description) ||
                        string.IsNullOrEmpty(MaxMembers) || TrainerUserId <= 0)
                    {
                        var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                        cmbs.Show("errorTitle", "allFieldsRequired", System.Windows.MessageBoxButton.OK);
                        return;
                    }

                    // Verify trainer exists
                    using (var context = new FitnessCenterContext())
                    {
                        if (!context.Trainers.Any(t => t.UserId == TrainerUserId))
                        {
                            var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                            cmbs.Show("errorTitle", "trainerNotFound", System.Windows.MessageBoxButton.OK);
                            return;
                        }
                    }
                }

                if (!Password.Equals(RepeatedPassword))
                {
                    var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                    cmbs.Show("errorTitle", "passwordsDontMatch", System.Windows.MessageBoxButton.OK);
                    return;
                }

                RegistrationService service = new RegistrationService();
                bool result = false;

                try
                {
                    if (AccountType == "Član" || AccountType == "Member")
                    {
                        result = service.RegisterMember(Username, Password, EmailAddress, PhoneNumber,
                            FirstName, LastName, BirthDate.Value, EnrollmentDate.Value);
                    }
                    else if (AccountType == "Trener" || AccountType == "Trainer")
                    {
                        result = service.RegisterTrainer(Username, Password, EmailAddress, PhoneNumber,
                            FirstName, LastName, Specialization, WorkHours);
                    }
                    else if (AccountType == "Grupa" || AccountType == "Group")
                    {
                        if (int.TryParse(MaxMembers, out int maxMembersCount))
                        {
                            result = service.RegisterGroup(Username, Password, EmailAddress, PhoneNumber,
                                GroupName, Description, maxMembersCount, TrainerUserId);
                        }
                        else
                        {
                            var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                            cmbs.Show("errorTitle", "invalidMaxMembers", System.Windows.MessageBoxButton.OK);
                            return;
                        }
                    }
                }
                catch (DbUpdateException dbEx)
                {
                    var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                    cmbs.Show("DB Update Error!", dbEx.InnerException?.Message ?? dbEx.Message, MessageBoxButton.OK);
                    Console.WriteLine("Caught DbUpdateException: " + dbEx.Message);
                }
                catch (Exception ex)
                {
                    var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                    cmbs.Show("Exception!", ex.Message + "\n" + ex.StackTrace, MessageBoxButton.OK);
                    return;
                }


                if (result)
                {
                    var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                    cmbs.Show("successTitle", "registrationSuccedeed", System.Windows.MessageBoxButton.OK);
                    CloseAction?.Invoke();
                }
                else
                {
                    var cmbs = _serviceProvider.GetRequiredService<CustomMessageBoxService>();
                    cmbs.Show("errorTitle", "errorWhileRegister", System.Windows.MessageBoxButton.OK);
                }
            }, o => true);

            CloseWindow = new RelayCommand(o =>
            {
                CloseAction?.Invoke();
            }, o => true);

            DarkThemeCommand = new RelayCommand(o =>
            {
                isDarkTheme = !isDarkTheme;
            }, o => true);

            CloseApplicationCommand = new RelayCommand(o =>
            {
                Application.Current.Shutdown();
            }, o => true);

        }


    }
}
