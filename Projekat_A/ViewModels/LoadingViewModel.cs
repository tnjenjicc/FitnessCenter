namespace Projekat_A.ViewModels
{
    public class LoadingViewModel : BaseViewModel
    {
        private bool _loading = false;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                _loading = value;
                OnPropertyChanged(nameof(Loading));
            }
        }
    }
}