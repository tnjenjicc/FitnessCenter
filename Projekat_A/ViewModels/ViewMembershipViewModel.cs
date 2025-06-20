using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using System.Threading.Tasks;
using System.Linq;
using System.Windows;

namespace Projekat_A.ViewModels
{
    public class ViewMembershipViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MemberService _memberService;
        private readonly Storage _storage;

        private ObservableCollection<MembershipDisplay> _memberships;
        private ObservableCollection<MembershipType> _membershipTypes;
        private MembershipDisplay _selectedMembership;
        private MembershipType _selectedMembershipType;
        private DateOnly _selectedExpirationDate;
        private bool _isAddingMembership;
        private readonly CustomMessageBoxService _customMessageBoxService;

        public ObservableCollection<MembershipDisplay> Memberships
        {
            get => _memberships;
            set
            {
                _memberships = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MembershipType> MembershipTypes
        {
            get => _membershipTypes;
            set
            {
                _membershipTypes = value;
                OnPropertyChanged();
            }
        }

        public MembershipDisplay SelectedMembership
        {
            get => _selectedMembership;
            set
            {
                _selectedMembership = value;
                OnPropertyChanged();
            }
        }

        public MembershipType SelectedMembershipType
        {
            get => _selectedMembershipType;
            set
            {
                _selectedMembershipType = value;
                OnPropertyChanged();
            }
        }

        public DateOnly SelectedExpirationDate
        {
            get => _selectedExpirationDate;
            set
            {
                _selectedExpirationDate = value;
                OnPropertyChanged();
            }
        }

        public bool IsAddingMembership
        {
            get => _isAddingMembership;
            set
            {
                _isAddingMembership = value;
                OnPropertyChanged();
            }
        }


        public ICommand AddMembershipCommand { get; private set; }
        public ICommand CancelAddCommand { get; private set; }
        public ICommand ShowAddMembershipCommand { get; private set; }
        public ICommand SaveMembershipCommand { get; private set; }

        public ViewMembershipViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _memberService = _serviceProvider.GetRequiredService<MemberService>();
            _storage = _serviceProvider.GetRequiredService<Storage>();
            _customMessageBoxService = _serviceProvider.GetRequiredService<CustomMessageBoxService>();

            Memberships = new ObservableCollection<MembershipDisplay>();
            MembershipTypes = new ObservableCollection<MembershipType>();
            SelectedExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));

            ShowAddMembershipCommand = new RelayCommand(param => ShowAddMembership(), param => true);
            CancelAddCommand = new RelayCommand(param => CancelAdd(), param => true);
            SaveMembershipCommand = new RelayCommand(async param => await SaveMembership(), param => CanSaveMembership());

            LoadData();
        }

        public async void LoadData()
        {
            await LoadMemberships();
            await LoadMembershipTypes();
        }

        private async Task LoadMemberships()
        {
            if (_storage.User == null)
                return;

            var member = await _memberService.GetMemberByUserIdAsync(_storage.User.Id);
            if (member == null)
                return;

            var memberships = await _memberService.GetMembershipsByMemberIdAsync(member.UserId);

            string activeText = Application.Current.TryFindResource("active")?.ToString() ?? "Active";
            string expiredText = Application.Current.TryFindResource("expired")?.ToString() ?? "Expired";

            Memberships.Clear();
            foreach (var membership in memberships)
            {
                var isActive = membership.ExpirationDate >= DateOnly.FromDateTime(DateTime.Now);
                Memberships.Add(new MembershipDisplay
                {
                    Membership = membership,
                    MembershipTypeName = membership.MembershipTypeIdTypeNavigation.Name,
                    Price = membership.MembershipTypeIdTypeNavigation.CurrentPrice,
                    ExpirationDate = membership.ExpirationDate,
                    IsActive = isActive,

                    Status = isActive ? activeText : expiredText
                });
            }
        }

        private async Task LoadMembershipTypes()
        {
            var types = await _memberService.GetAllMembershipTypesAsync();

            MembershipTypes.Clear();
            foreach (var type in types)
            {
                MembershipTypes.Add(type);
            }

            if (MembershipTypes.Count > 0)
                SelectedMembershipType = MembershipTypes[0];
        }

        private void ShowAddMembership()
        {
            IsAddingMembership = true;
            SelectedExpirationDate = DateOnly.FromDateTime(DateTime.Now.AddMonths(1));
        }

        private void CancelAdd()
        {
            IsAddingMembership = false;
        }

        private async Task SaveMembership()
        {
            if (_storage.User == null || SelectedMembershipType == null)
                return;

            var member = await _memberService.GetMemberByUserIdAsync(_storage.User.Id);
            if (member == null)
                return;

            var success = await _memberService.AddMembershipAsync(
                member.UserId,
                SelectedMembershipType.IdType,
                SelectedExpirationDate);

            if (success)
            {
                _customMessageBoxService.Show("infoTitle", "membershipAddedSuccess", MessageBoxButton.OK);
                IsAddingMembership = false;
                await LoadMemberships();
            }
            else
            {
                _customMessageBoxService.Show("errorTitle", "membershipAddFailed", MessageBoxButton.OK);
            }

            await Task.Delay(3000);
        }

        private bool CanSaveMembership()
        {
            return SelectedMembershipType != null &&
                   SelectedExpirationDate > DateOnly.FromDateTime(DateTime.Now);
        }
    }

    public class MembershipDisplay
    {
        public Membership Membership { get; set; }
        public string MembershipTypeName { get; set; }
        public decimal Price { get; set; }
        public DateOnly ExpirationDate { get; set; }
        public bool IsActive { get; set; }
        public string Status { get; set; }
    }
}