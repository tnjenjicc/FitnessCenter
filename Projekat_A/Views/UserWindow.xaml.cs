using Projekat_A.ViewModels;

namespace Projekat_A.Views
{
    public partial class UserWindow
    {
        private readonly UserViewModel _userViewModel;
        public UserWindow(UserViewModel _viewModel)
        {
            InitializeComponent();
            _userViewModel = _viewModel;
            DataContext = _userViewModel;
        }
    }
}

