using MaterialDesignThemes.Wpf;
using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Projekat_A.ViewModels
{
    public class SettingsViewModel : BaseViewModel
    {
        private ComboBoxItem? _theme;
        private ComboBoxItem? _mode;
        private ComboBoxItem? _font;
        private readonly PaletteHelper palette = new PaletteHelper();
        private IServiceProvider _serviceProvider;
        public ComboBoxItem? Theme
        {
            get => _theme;
            set
            {
                _theme = value;
                OnPropertyChanged(nameof(Theme));
            }
        }

        public ComboBoxItem? Mode
        {
            get => _mode;
            set
            {
                _mode = value;
                OnPropertyChanged(nameof(Mode));
            }
        }

        public ComboBoxItem? Font
        {
            get => _font;
            set
            {
                _font = value;
                OnPropertyChanged(nameof(Font));
            }
        }

        public ObservableCollection<ComboBoxItem> themeOptions { get; set; }
        public ObservableCollection<ComboBoxItem> modeOptions { get; set; }
        public ObservableCollection<ComboBoxItem> fontOptions { get; set; }
        public RelayCommand SaveCommand { get; set; }
        public SettingsViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;

            themeOptions = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Content = (string) App.Current.Resources["blueTheme"] },
                new ComboBoxItem { Content = (string) App.Current.Resources["pinkTheme"] }
            };

            modeOptions = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Content = (string) App.Current.Resources["lightMode"] },
                new ComboBoxItem { Content = (string) App.Current.Resources["darkMode"] }
            };

            fontOptions = new ObservableCollection<ComboBoxItem>
            {
                new ComboBoxItem { Content = (string) App.Current.Resources["ArialFont"] },
                new ComboBoxItem { Content = (string) App.Current.Resources["ComfortaaFont"] },
                new ComboBoxItem { Content = (string) App.Current.Resources["MontserratFont"] } ,
            };

            loadUserPreferences();
            SaveCommand = new RelayCommand(o =>
            {
                var userService = _serviceProvider.GetRequiredService<UserService>();
                int mode, theme;
                string _mode = (string)Mode.Content.ToString();
                string _theme = (string)Theme.Content.ToString();
                string _font = (string)Font.Content.ToString();

                if (_mode.Equals("Light") || _mode.Equals("Svijetli"))
                    mode = 0;
                else
                    mode = 1;

                if (_mode.Equals("Blue") || _mode.Equals("Plava"))
                    theme = 0;
                else
                    theme = 1;
                userService.saveUserChanges(mode, theme, _font);

                setMode(_mode);
                setTheme(_theme);
                setFont(_font);
            }, o => true);
        }

        public void loadUserPreferences()
        {
            User? user = _serviceProvider.GetRequiredService<Services.Storage>().User;
            if (user == null) return;


            int mode = (int)user.Mode;
            Mode = modeOptions[mode];

            int theme = (int)user.Theme;
            Theme = themeOptions[theme];


            String font = user.Font;
            if (font.Equals("Arial"))
                Font = fontOptions[0];
            else if (font.Equals("Comfortaa"))
                Font = fontOptions[1];
            else
                Font = fontOptions[2];

            if (Mode != null && Mode.Content != null)
                setMode(Mode.Content.ToString());

            if (Theme != null && Theme.Content != null)
                setTheme(Theme.Content.ToString());

            if (Font != null && Font.Content != null)
                setFont(Font.Content.ToString());
        }

        private void setMode(String mode)
        {
            ITheme theme = palette.GetTheme();
            if (mode.Equals("Light") || mode.Equals("Svijetli"))
                theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Light);
            else
                theme.SetBaseTheme(MaterialDesignThemes.Wpf.Theme.Dark);

            palette.SetTheme(theme);
        }

        private void setTheme(String themee)
        {
            ITheme theme = palette.GetTheme();

            if (themee.Equals("Blue") || themee.Equals("Plava"))
            {
                theme.SetPrimaryColor((Color)ColorConverter.ConvertFromString("#0085B8"));
                theme.SetSecondaryColor((Color)ColorConverter.ConvertFromString("#E91E63"));
            }
            else
            {
                theme.SetPrimaryColor((Color)ColorConverter.ConvertFromString("#E91E63"));
                theme.SetSecondaryColor((Color)ColorConverter.ConvertFromString("#0085B8"));
            }
            palette.SetTheme(theme);
        }

        private void setFont(string font)
        {
            Application.Current.Resources["AppFont"] = new System.Windows.Media.FontFamily(font);
        }

    }
}
