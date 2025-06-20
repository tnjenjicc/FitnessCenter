using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using Projekat_A.Views;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Input;

namespace Projekat_A.ViewModels
{
    public class ViewMembersViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly GroupService _groupService;
        private readonly Storage _storage;
        private readonly CustomMessageBoxService _customMessageBoxService;
        private Group _currentGroup;
        
        private ObservableCollection<Member> _groupMembers;
        private ObservableCollection<Member> _availableMembers;
        private Member _selectedGroupMember;
        private Member _selectedAvailableMember;
        private string _groupName;
        private string _groupDescription;
        private int _currentMemberCount;
        private int _maxMemberCount;
        private string _trainerName;
        private bool _isAddingMember;


        public ObservableCollection<Member> GroupMembers
        {
            get => _groupMembers;
            set
            {
                _groupMembers = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<Member> AvailableMembers
        {
            get => _availableMembers;
            set
            {
                _availableMembers = value;
                OnPropertyChanged();
            }
        }

        public Member SelectedGroupMember
        {
            get => _selectedGroupMember;
            set
            {
                _selectedGroupMember = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasSelectedMember));
            }
        }

        public Member SelectedAvailableMember
        {
            get => _selectedAvailableMember;
            set
            {
                _selectedAvailableMember = value;
                OnPropertyChanged();
            }
        }

        public string GroupName
        {
            get => _groupName;
            set
            {
                _groupName = value;
                OnPropertyChanged();
            }
        }

        public string GroupDescription
        {
            get => _groupDescription;
            set
            {
                _groupDescription = value;
                OnPropertyChanged();
            }
        }

        public int CurrentMemberCount
        {
            get => _currentMemberCount;
            set
            {
                _currentMemberCount = value;
                OnPropertyChanged();
            }
        }

        public int MaxMemberCount
        {
            get => _maxMemberCount;
            set
            {
                _maxMemberCount = value;
                OnPropertyChanged();
            }
        }

        public string TrainerName
        {
            get => _trainerName;
            set
            {
                _trainerName = value;
                OnPropertyChanged();
            }
        }

        public bool IsAddingMember
        {
            get => _isAddingMember;
            set
            {
                _isAddingMember = value;
                OnPropertyChanged();
            }
        }

       
        public bool HasSelectedMember => SelectedGroupMember != null;

        public ICommand AddMemberCommand { get; private set; }
        public ICommand RemoveMemberCommand { get; private set; }
        public ICommand ShowAddMemberCommand { get; private set; }
        public ICommand CancelAddCommand { get; private set; }
        public ICommand RefreshCommand { get; private set; }

        public ViewMembersViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _groupService = serviceProvider.GetRequiredService<GroupService>();
            _storage = serviceProvider.GetRequiredService<Storage>();
            _customMessageBoxService = _serviceProvider.GetRequiredService<CustomMessageBoxService>();

            GroupMembers = new ObservableCollection<Member>();
            AvailableMembers = new ObservableCollection<Member>();

            InitializeCommands();
            LoadData();
        }

        private void InitializeCommands()
        {
            AddMemberCommand = new RelayCommand(
                async param => await AddMemberToGroupAsync(),
                param => CanAddMember()
            );

            RemoveMemberCommand = new RelayCommand(
                async param => await RemoveMemberFromGroupAsync(),
                param => CanRemoveMember()
            );

            ShowAddMemberCommand = new RelayCommand(
                param => ShowAddMember(),
                param => !IsAddingMember
            );

            CancelAddCommand = new RelayCommand(
                param => CancelAdd(),
                param => IsAddingMember
            );

            RefreshCommand = new RelayCommand(
                param => LoadData(),
                param => true
            );
        }

        public async void LoadData()
        {
            await LoadCurrentGroupAsync();
            await LoadGroupMembersAsync();
        }

        private async Task LoadCurrentGroupAsync()
        {
            try
            {
                if (_storage.User?.AccountType != "Group")
                    return;

                _currentGroup = _groupService.GetGroupById(_storage.User.Id);

                if (_currentGroup != null)
                {
                    GroupName = _currentGroup.Name;
                    GroupDescription = _currentGroup.Description;
                    MaxMemberCount = _currentGroup.MaxNumberOfMembers;
                    TrainerName = $"{_currentGroup.TrainerUser?.Name} {_currentGroup.TrainerUser?.Surname}";
                }
            }
            catch (Exception ex)
            {
                _customMessageBoxService.Show("errorTitle", "loadCurrentGroupFailed", MessageBoxButton.OK);
                await HideStatusMessageAfterDelay();
            }
        }

        private async Task LoadGroupMembersAsync()
        {
            try
            {
                if (_currentGroup == null) return;

                var members = _groupService.GetGroupMembers(_currentGroup.UserId);

                GroupMembers.Clear();
                foreach (var member in members)
                {
                    GroupMembers.Add(member);
                }
                CurrentMemberCount = GroupMembers.Count;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading group members: {ex.Message}");
                _customMessageBoxService.Show("errorTitle", "loadGroupMembersFailed", MessageBoxButton.OK);
                await HideStatusMessageAfterDelay();
            }
        }

        private async Task LoadAvailableMembersAsync()
        {
            try
            {
                if (_currentGroup == null) return;

                var availableMembers = _groupService.GetAvailableMembers(_currentGroup.UserId);

                AvailableMembers.Clear();
                foreach (var member in availableMembers)
                {
                    AvailableMembers.Add(member);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error loading available members: {ex.Message}");
                _customMessageBoxService.Show("errorTitle", "loadAvailableMembersFailed", MessageBoxButton.OK);
                await HideStatusMessageAfterDelay();
            }
        }

        private void ShowAddMember()
        {
            IsAddingMember = true;
            LoadAvailableMembersAsync();
        }

        private void CancelAdd()
        {
            IsAddingMember = false;
            SelectedAvailableMember = null;
        }

        private async Task AddMemberToGroupAsync()
        {
            try
            {
                if (SelectedAvailableMember == null || _currentGroup == null)
                    return;

                var messageBoxViewModel = new CustomMessageBoxViewModel();
                var customMessageBox = new CustomMessageBox(messageBoxViewModel);
                customMessageBox.Initialize(
                    "confirmAddition", 
                    "confirmAddMemberMessage", 
                    MessageBoxButton.YesNo
                );

                messageBoxViewModel.Message = $"Da li ste sigurni da želite dodati {SelectedAvailableMember.Name} {SelectedAvailableMember.Surname} u grupu?";

                var result = customMessageBox.ShowDialog();

                if (result == true)
                {
                    bool success = _groupService.AddMemberToGroup(_currentGroup.UserId, SelectedAvailableMember.UserId);

                    if (success)
                    {
                        await LoadGroupMembersAsync();
                        CancelAdd();

                        _customMessageBoxService.Show("infoTitle", "memberAddedSuccess", MessageBoxButton.OK);
                        await HideStatusMessageAfterDelay();
                    }
                    else
                    {
                        _customMessageBoxService.Show("errorTitle", "memberAddedFailed", MessageBoxButton.OK);
                        await HideStatusMessageAfterDelay();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error adding member to group: {ex.Message}");

                _customMessageBoxService.Show("errorTitle", "memberAddedFailedUnknown", MessageBoxButton.OK);
                await HideStatusMessageAfterDelay();
            }
        }

        private async Task RemoveMemberFromGroupAsync()
        {
            try
            {
                if (SelectedGroupMember == null || _currentGroup == null)
                    return;

                var messageBoxViewModel = new CustomMessageBoxViewModel();
                var customMessageBox = new CustomMessageBox(messageBoxViewModel);
                customMessageBox.Initialize(
                    "confirmRemoval",
                    "confirmRemoveMemberMessage",
                    MessageBoxButton.YesNo
                );

                messageBoxViewModel.Message = $"Da li ste sigurni da želite ukloniti {SelectedGroupMember.Name} {SelectedGroupMember.Surname} iz grupe?";

                var result = customMessageBox.ShowDialog();

                if (result == true) 
                {
                    bool success = _groupService.RemoveMemberFromGroup(_currentGroup.UserId, SelectedGroupMember.UserId);

                    if (success)
                    {
                        await LoadGroupMembersAsync();
                        SelectedGroupMember = null;

                        _customMessageBoxService.Show("infoTitle", "memberRemoveSuccess", MessageBoxButton.OK);
                        await HideStatusMessageAfterDelay();
                    }
                    else
                    {
                        _customMessageBoxService.Show("errorTitle", "memberRemoveFailed", MessageBoxButton.OK);
                        await HideStatusMessageAfterDelay();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error removing member from group: {ex.Message}");
                _customMessageBoxService.Show("errorTitle", "memberRemoveFailedUnknown", MessageBoxButton.OK);
                await HideStatusMessageAfterDelay();
            }
        }

        private bool CanAddMember()
        {
            return SelectedAvailableMember != null && 
                   _currentGroup != null && 
                   !_groupService.IsGroupFull(_currentGroup.UserId);
        }

        private bool CanRemoveMember()
        {
            return SelectedGroupMember != null;
        }

        private async Task HideStatusMessageAfterDelay()
        {
            await Task.Delay(3000);
        }
    }
}