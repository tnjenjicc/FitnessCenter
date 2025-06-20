using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;

namespace Projekat_A.ViewModels
{
    public class ViewHallsViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly TrainerService _trainerService;

        private ObservableCollection<Hall> _halls;
        private Hall _selectedHall;

        public ObservableCollection<Hall> Halls
        {
            get => _halls;
            set
            {
                _halls = value;
                OnPropertyChanged(nameof(Halls));
            }
        }

        public Hall SelectedHall
        {
            get => _selectedHall;
            set
            {
                _selectedHall = value;
                OnPropertyChanged(nameof(SelectedHall));
            }
        }


        public ViewHallsViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _trainerService = _serviceProvider.GetRequiredService<TrainerService>();

            LoadHalls();
        }

        private void LoadHalls()
        {
            var hallsList = _trainerService.GetAllHalls();
            Halls = new ObservableCollection<Hall>(hallsList);
        }

    }
}