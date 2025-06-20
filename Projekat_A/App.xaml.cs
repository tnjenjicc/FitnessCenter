using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Services;
using Projekat_A.ViewModels;
using Projekat_A.Views;
using Projekat_A.UserControls;
using System.Windows.Threading;
using System.Diagnostics;

namespace Projekat_A
{
    public partial class App : Application
    {
        public static ServiceProvider _servicesProvider;

        public App()
        {
            AppDomain.CurrentDomain.UnhandledException += CurrentDomain_UnhandledException;
            this.DispatcherUnhandledException += App_DispatcherUnhandledException;

            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<INavigationService, Projekat_A.Services.NavigationService>();

            services.AddSingleton<LoginViewModel>();
            services.AddSingleton<LoginWindow>(provider => new LoginWindow
            {
                DataContext = provider.GetRequiredService<LoginViewModel>()
            });

            services.AddSingleton<RegistrationViewModel>();
            services.AddSingleton<RegistrationWindow>();

            services.AddTransient<CustomMessageBoxViewModel>();
            services.AddTransient<CustomMessageBox>();
            services.AddSingleton<CustomMessageBoxService>();
            services.AddSingleton<Storage>();
            services.AddSingleton<LoadingViewModel>();
            services.AddSingleton<LoadingUserControl>();

            services.AddSingleton<UserWindow>();
            services.AddSingleton<UserViewModel>();
            services.AddSingleton<UserService>();
            services.AddSingleton<SettingsViewModel>();
            services.AddSingleton<SettingsUserControl>();

            services.AddSingleton<TrainerService>();
            services.AddSingleton<TrainerAppointmentsViewModel>();
            services.AddSingleton<TrainerAppointmentsUserControl>();

            services.AddSingleton<AddAppointmentViewModel>();
            services.AddSingleton<AddAppointmentUserControl>();

            services.AddSingleton<ViewHallsViewModel>();
            services.AddSingleton<ViewHallsUserControl>();

            services.AddSingleton<MemberService>();

            services.AddSingleton<ReservationViewModel>();
            services.AddSingleton<ReservationUserControl>();

            services.AddSingleton<ViewMembershipUserControl>();
            services.AddSingleton<ViewMembershipViewModel>();

            services.AddSingleton<MembershipPaymentUserControl>();
            services.AddSingleton<MembershipPaymentViewModel>();

            services.AddSingleton<GroupService>();

            services.AddSingleton<ViewMembersUserControl>();
            services.AddSingleton<ViewMembersViewModel>();

            services.AddSingleton<Func<Type, BaseViewModel>>(serviceProvider => viewModelType => (BaseViewModel)serviceProvider.GetRequiredService(viewModelType));

            _servicesProvider = services.BuildServiceProvider();
        }

        protected override void OnStartup(StartupEventArgs e)
        {
            var loginWindow = _servicesProvider.GetRequiredService<LoginWindow>();
            _servicesProvider.GetRequiredService<INavigationService>().NavigateTo<LoginViewModel>();
            loginWindow.Show();

            base.OnStartup(e);
        }
        private void CurrentDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            if (e.ExceptionObject is Exception ex)
            {
                Console.WriteLine($"Caught unhandled exception: {ex.Message}");
                Console.WriteLine($"StackTrace: {ex.StackTrace}");
            }
        }

        private void App_DispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
        {
            Console.WriteLine($"Caught dispatcher unhandled exception: {e.Exception.Message}");
            Console.WriteLine($"StackTrace: {e.Exception.StackTrace}");
            e.Handled = true; 
        }
    }

}
