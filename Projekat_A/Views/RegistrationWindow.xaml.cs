using Projekat_A.Data;
using Projekat_A.ViewModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

namespace Projekat_A.Views
{
    public partial class RegistrationWindow : Window
    {
        private readonly RegistrationViewModel _viewModel;
        public RegistrationWindow(RegistrationViewModel vm)
        {
            _viewModel = vm;
            _viewModel.CloseAction += () => this.Close();
            DataContext = _viewModel;
            InitializeComponent();

            if (_viewModel != null)
            {
                _viewModel.TrainerUserId = 0;
            }
        }
        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            DragMove();
        }

        private void txtPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext;

            if (context is RegistrationViewModel viewModel)
            {
                viewModel.Password = txtPassword.Password;
            }
        }

        private void txtRepeatedPassword_PasswordChanged(object sender, RoutedEventArgs e)
        {
            var context = this.DataContext;

            if (context is RegistrationViewModel viewModel)
            {
                viewModel.RepeatedPassword = txtRepeatedPassword.Password;
            }
        }

        private void AccountType_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ComboBox comboBox && comboBox.SelectedItem is ComboBoxItem selectedItem)
            {
                string? content = selectedItem.Content?.ToString();
                if (DataContext is RegistrationViewModel vm)
                {
                    vm.AccountType = content ?? string.Empty;
                }

                memberFieldsPanel.Visibility = Visibility.Collapsed;
                trainerFieldsPanel.Visibility = Visibility.Collapsed;
                groupFieldsPanel.Visibility = Visibility.Collapsed;

                if (content == "Član" || content == "Member")
                {
                    memberFieldsPanel.Visibility = Visibility.Visible;
                }
                else if (content == "Trener" || content == "Trainer")
                {
                    trainerFieldsPanel.Visibility = Visibility.Visible;
                }
                else if (content == "Grupa" || content == "Group")
                {
                    LoadTrainers();
                    groupFieldsPanel.Visibility = Visibility.Visible;
                }
            }
        }

        private void LoadTrainers()
        {
            try
            {
                using (var context = new FitnessCenterContext())
                {
                    var trainers = context.Trainers
                        .Select(t => new
                        {
                            UserId = t.UserId,
                            DisplayName = t.Name + " " + t.Surname + " (" + t.Specialization + ")"
                        })
                        .ToList();

                    TrainerComboBox.ItemsSource = trainers;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading trainers: " + ex.Message);
            }
        }

        private void TrainerComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (TrainerComboBox.SelectedValue != null && DataContext is RegistrationViewModel vm)
            {
                vm.TrainerUserId = (int)TrainerComboBox.SelectedValue;
            }
        }

    }
}

