using Microsoft.Extensions.DependencyInjection;
using Projekat_A.Commands;
using Projekat_A.Models;
using Projekat_A.Services;
using Projekat_A.Util;
using Projekat_A.Views;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat_A.ViewModels
{
    public class MembershipPaymentViewModel : BaseViewModel
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly MemberService _memberService;
        private readonly Storage _storage;
        private readonly CustomMessageBoxService _customMessageBoxService;

        private ObservableCollection<MembershipPayment> _unpaidPayments;
        private ObservableCollection<MembershipPayment> _paymentHistory;
        private ObservableCollection<Promotion> _availablePromotions;
        private MembershipPayment _selectedPayment;
        private Promotion _selectedPromotion;
        private string _selectedPaymentMethod;
        private decimal _originalPrice;
        private decimal _finalPrice;
        private decimal _totalDebt;
        private bool _isLoading;

        private Dictionary<int, string> _usedPromotions = new Dictionary<int, string>();

        public ObservableCollection<MembershipPayment> UnpaidPayments
        {
            get => _unpaidPayments ?? new ObservableCollection<MembershipPayment>();
            set
            {
                _unpaidPayments = value;
                OnPropertyChanged();
            }
        }

        public ObservableCollection<MembershipPayment> PaymentHistory
        {
            get => _paymentHistory ?? new ObservableCollection<MembershipPayment>();
            set
            {
                _paymentHistory = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasPaymentHistory));
            }
        }
        public ObservableCollection<Promotion> AvailablePromotions
        {
            get => _availablePromotions ?? new ObservableCollection<Promotion>();
            set
            {
                _availablePromotions = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasAvailablePromotions));
            }
        }

        public MembershipPayment SelectedPayment
        {
            get => _selectedPayment;
            set
            {
                _selectedPayment = value;
                OnPropertyChanged();
                OnPropertyChanged(nameof(HasSelectedPayment));
                OnPaymentSelectionChanged();
            }
        }

        public Promotion SelectedPromotion
        {
            get => _selectedPromotion;
            set
            {
                _selectedPromotion = value;
                OnPropertyChanged();
                CalculateFinalPrice();
            }
        }

        public string SelectedPaymentMethod
        {
            get => _selectedPaymentMethod ?? "Cash";
            set
            {
                _selectedPaymentMethod = value;
                OnPropertyChanged();
            }
        }

        public decimal OriginalPrice
        {
            get => _originalPrice;
            set
            {
                _originalPrice = value;
                OnPropertyChanged();
            }
        }

        public decimal FinalPrice
        {
            get => _finalPrice;
            set
            {
                _finalPrice = value;
                OnPropertyChanged();
            }
        }

        public decimal TotalDebt
        {
            get => _totalDebt;
            set
            {
                _totalDebt = value;
                OnPropertyChanged();
            }
        }

        public bool IsLoading
        {
            get => _isLoading;
            set
            {
                _isLoading = value;
                OnPropertyChanged();
                ProcessPaymentCommand.RaiseCanExecuteChanged();
                RefreshDataCommand.RaiseCanExecuteChanged();
            }
        }

        public ObservableCollection<string> PaymentMethods { get; set; }

        public RelayCommand ProcessPaymentCommand { get; set; }
        public RelayCommand RefreshDataCommand { get; set; }
        public RelayCommand ClearPromotionCommand { get; set; }

        public MembershipPaymentViewModel(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
            _memberService = _serviceProvider.GetRequiredService<MemberService>();
            _storage = _serviceProvider.GetRequiredService<Storage>();
            _customMessageBoxService = _serviceProvider.GetRequiredService<CustomMessageBoxService>();

            InitializeCollections();
            InitializeCommands();

            Application.Current.Dispatcher.BeginInvoke(new Action(async () => await LoadDataAsync()));
        }

        private void InitializeCollections()
        {
            UnpaidPayments = new ObservableCollection<MembershipPayment>();
            PaymentHistory = new ObservableCollection<MembershipPayment>();
            AvailablePromotions = new ObservableCollection<Promotion>();

            PaymentMethods = new ObservableCollection<string>
            {
                (string)Application.Current.Resources["paymentMethodCash"],
                (string)Application.Current.Resources["paymentMethodCard"],
                (string)Application.Current.Resources["paymentMethodBankTransfer"],
                (string)Application.Current.Resources["paymentMethodOnline"]
            };
        }

        private void InitializeCommands()
        {
            ProcessPaymentCommand = new RelayCommand(
                async o => await ProcessPaymentAsync(),
                o => CanProcessPayment()
            );

            RefreshDataCommand = new RelayCommand(
                async o => await LoadDataAsync(),
                o => !IsLoading
            );

            ClearPromotionCommand = new RelayCommand(
                o => ClearPromotion(),
                o => SelectedPromotion != null
            );
        }
        public async Task LoadDataAsync()
        {
            if (_storage.User == null)
            {
                _customMessageBoxService.Show("errorTitle", "notLoggedInFailed", MessageBoxButton.OK);
                return;
            }

            IsLoading = true;

            var currentSelectedPaymentId = SelectedPayment?.IdPayment;

            try
            {
                var memberId = _storage.User.Id;

                System.Diagnostics.Debug.WriteLine($"Loading data for member ID: {memberId}");

                await _memberService.CreateTestPromotionsAsync(memberId);

                var unpaidPayments = await _memberService.GetUnpaidPaymentsAsync(memberId);
                System.Diagnostics.Debug.WriteLine($"Found {unpaidPayments?.Count() ?? 0} unpaid payments");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    UnpaidPayments.Clear();
                    if (unpaidPayments != null)
                    {
                        foreach (var payment in unpaidPayments)
                        {
                            UnpaidPayments.Add(payment);
                        }

                        if (currentSelectedPaymentId.HasValue)
                        {
                            var previouslySelected = UnpaidPayments.FirstOrDefault(p => p.IdPayment == currentSelectedPaymentId.Value);
                            if (previouslySelected != null)
                            {
                                SelectedPayment = previouslySelected;
                                System.Diagnostics.Debug.WriteLine($"Restored selection to payment ID: {currentSelectedPaymentId.Value}");
                            }
                        }
                    }
                });

                var paymentHistory = await _memberService.GetPaymentHistoryAsync(memberId);
                System.Diagnostics.Debug.WriteLine($"Found {paymentHistory?.Count() ?? 0} payment history records");

                Application.Current.Dispatcher.Invoke(() =>
                {
                    PaymentHistory.Clear();
                    if (paymentHistory != null)
                    {
                        foreach (var payment in paymentHistory)
                        {
                            PaymentHistory.Add(payment);
                        }
                    }
                    OnPropertyChanged(nameof(HasPaymentHistory));
                });

                TotalDebt = await _memberService.GetTotalDebtAsync(memberId);
                System.Diagnostics.Debug.WriteLine($"Total debt: {TotalDebt}");

                System.Diagnostics.Debug.WriteLine("Data loading completed successfully");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error in LoadDataAsync: {ex.Message}");
                _customMessageBoxService.Show("errorTitle", "loadDataFailed", MessageBoxButton.OK);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private async void OnPaymentSelectionChanged()
        {
            if (SelectedPayment == null)
            {
                System.Diagnostics.Debug.WriteLine("OnPaymentSelectionChanged: SelectedPayment is null.");
                OriginalPrice = 0;
                FinalPrice = 0;
                AvailablePromotions.Clear();
                SelectedPromotion = null;
                ProcessPaymentCommand.RaiseCanExecuteChanged();
                return;
            }

            try
            {
                OriginalPrice = SelectedPayment.Price;
                FinalPrice = SelectedPayment.Price;


                var promotions = await _memberService.GetAvailablePromotionsAsync(
                    _storage.User.Id,
                    SelectedPayment.MembershipIdMembership);


                if (promotions != null)
                {
                    foreach (var promo in promotions)
                    {
                        System.Diagnostics.Debug.WriteLine($"  Received promotion: {promo.Description}, Discount: {promo.Discount}%");
                    }
                }

                AvailablePromotions.Clear();
                if (promotions != null)
                {
                    foreach (var promotion in promotions)
                    {
                        AvailablePromotions.Add(promotion);
                        System.Diagnostics.Debug.WriteLine($"OnPaymentSelectionChanged: Added promotion to UI: {promotion.Description}");
                    }
                }

                SelectedPromotion = null;
                ProcessPaymentCommand.RaiseCanExecuteChanged();

                OnPropertyChanged(nameof(AvailablePromotions));
                OnPropertyChanged(nameof(HasAvailablePromotions));
            }
            catch (Exception ex)
            {
                _customMessageBoxService.Show("errorTitle", "loadPromotionsFailed", MessageBoxButton.OK);
            }
        }

        
        private async void CalculateFinalPrice()
        {
            if (SelectedPayment == null) return;

            try
            {
                var promotionId = SelectedPromotion?.IdPromotion;
                FinalPrice = await _memberService.CalculatePriceWithPromotionAsync(
                    SelectedPayment.IdPayment, promotionId);
            }
            catch (Exception ex)
            {
                _customMessageBoxService.Show("errorTitle", "calculatePriceFailed", MessageBoxButton.OK);
            }
        }
        private async Task ProcessPaymentAsync()
        {
            if (SelectedPayment == null) return;

            try
            {
                IsLoading = true;

                var result = _customMessageBoxService.Show(
                    "confirmPaymentTitle",
                    string.Format("confirmPaymentText", FinalPrice.ToString("C"), SelectedPaymentMethod),
                    MessageBoxButton.YesNo
                );


                if (result == true)
                {
                    var success = await _memberService.ProcessPaymentAsync(
                        SelectedPayment.IdPayment,
                        SelectedPaymentMethod,
                        SelectedPromotion?.IdPromotion);

                    if (success)
                    {
                        _customMessageBoxService.Show("successTitle", "successPayment", MessageBoxButton.OK);

                        await LoadDataAsync();
                        ClearSelection();
                    }
                    else
                    {
                        _customMessageBoxService.Show("errorTitle", "processPaymentFailed", MessageBoxButton.OK);
                    }
                }
            }
            catch (Exception ex)
            {
                _customMessageBoxService.Show("errorTitle", "processPaymentFailedUnknown", MessageBoxButton.OK);
            }
            finally
            {
                IsLoading = false;
            }
        }
        private bool CanProcessPayment()
        {
            return SelectedPayment != null && !IsLoading && !string.IsNullOrEmpty(SelectedPaymentMethod);
        }

        private void ClearPromotion()
        {
            SelectedPromotion = null;
            CalculateFinalPrice();
        }

        private void ClearSelection()
        {
            SelectedPayment = null;
            SelectedPromotion = null;
            OriginalPrice = 0;
            FinalPrice = 0;
            AvailablePromotions.Clear();
        }

        public bool HasPaymentHistory
        {
            get => PaymentHistory != null && PaymentHistory.Count > 0;
        }

        public bool HasSelectedPayment
        {
            get => SelectedPayment != null;
        }

        public bool HasAvailablePromotions
        {
            get => AvailablePromotions != null && AvailablePromotions.Count > 0;
        }


    }
}