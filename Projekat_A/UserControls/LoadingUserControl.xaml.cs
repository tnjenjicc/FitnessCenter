using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;

namespace Projekat_A.UserControls
{
    public partial class LoadingUserControl : UserControl, INotifyPropertyChanged
    {
        private bool _loading;

        public bool Loading
        {
            get { return _loading; }
            set
            {
                if (_loading != value)
                {
                    _loading = value;
                    OnPropertyChanged(nameof(Loading));
                }
            }
        }

        public LoadingUserControl()
        {
            InitializeComponent();
            DataContext = this;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}