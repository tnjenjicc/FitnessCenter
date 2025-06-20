using System.Windows.Controls;
using Projekat_A.ViewModels;
using Microsoft.Extensions.DependencyInjection;

namespace Projekat_A.UserControls
{
    public partial class SettingsUserControl : UserControl
    {
        public SettingsUserControl()
        {
            InitializeComponent();

            var viewModel = App._servicesProvider.GetRequiredService<SettingsViewModel>();

            this.DataContext = viewModel;
        }
    }
}