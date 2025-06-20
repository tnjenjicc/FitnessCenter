using Projekat_A.ViewModels;

namespace Projekat_A.Services
{

    public class NavigationService : ObservableObject, INavigationService
    {
        private BaseViewModel _currentView;
        private readonly Func<Type, BaseViewModel> _viewModelFactory;

        public BaseViewModel CurrentView
        {
            get => _currentView;
            private set
            {
                _currentView = value;
                OnPropertyChanged();
            }
        }

        public NavigationService(Func<Type, BaseViewModel> viewModelBase)
        {
            _viewModelFactory = viewModelBase;
        }

        public void NavigateTo<TViewModel>() where TViewModel : BaseViewModel
        {
            BaseViewModel vm = _viewModelFactory.Invoke(typeof(TViewModel));
            CurrentView = vm;
        }
    }
}
