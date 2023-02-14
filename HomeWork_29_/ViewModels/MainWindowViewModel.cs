using System.Windows.Input;
using HomeWork_29_.Services.Interfaces;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;
using MathCore.WPF.Commands;
using MathCore.WPF.ViewModels;


namespace HomeWork_29_.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        private readonly IRepository<Deal> _dealRepository;
        private readonly ISalesService _salesService;
        private readonly IUserDialog _userDialog;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region CurrentModel : ViewModel - Текущая дочерняя модель-представления

        /// <summary>Текущая дочерняя модель-представления</summary>
        private ViewModel _CurrentModel;

        /// <summary>Текущая дочерняя модель-представления</summary>
        public ViewModel CurrentModel { get => _CurrentModel; private set => Set(ref _CurrentModel, value); }

        #endregion


        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        #region Command ShowProductsViewCommand - Отобразить представление продуктов

        /// <summary> Отобразить представление продуктов </summary>
        private ICommand _ShowProductsViewCommand;

        /// <summary> Отобразить представление продуктов </summary>
        public ICommand ShowProductsViewCommand => _ShowProductsViewCommand
        ??= new LambdaCommand(OnShowProductsViewCommandExecuted, CanShowProductsViewCommandExecute);

        /// <summary> Проверка возможности выполнения - Отобразить представление продуктов </summary>
        private bool CanShowProductsViewCommandExecute() => true;

        /// <summary> Логика выполнения - Отобразить представление продуктов </summary>
        private void OnShowProductsViewCommandExecuted()
        {
            CurrentModel = new ProductsViewModel(_productsRepository, _userDialog);
        }

        #endregion

        #region Command ShowBuyersViewCommand - Отобразить представление покупателей

        /// <summary> Отобразить представление покупателей </summary>
        private ICommand _ShowBuyersViewCommand;

        /// <summary> Отобразить представление покупателей </summary>
        public ICommand ShowBuyersViewCommand => _ShowBuyersViewCommand
            ??= new LambdaCommand(OnShowBuyersViewCommandExecuted, CanShowBuyersViewCommandExecute);

        /// <summary> Проверка возможности выполнения - Отобразить представление покупателей </summary>
        private bool CanShowBuyersViewCommandExecute() => true;

        /// <summary> Логика выполнения - Отобразить представление покупателей </summary>
        private void OnShowBuyersViewCommandExecuted()
        {
            CurrentModel = new BuyersViewModel(_buyerRepository);
        }

        #endregion

        #region Command ShowStatisticsViewCommand - Отобразить представление статистики

        /// <summary> Отобразить представление статистики </summary>
        private ICommand _ShowStatisticsViewCommand;

        /// <summary> Отобразить представление статистики </summary>
        public ICommand ShowStatisticsViewCommand => _ShowStatisticsViewCommand
            ??= new LambdaCommand(OnShowStatisticsViewCommandExecuted, CanShowStatisticsViewCommandExecute);

        /// <summary> Проверка возможности выполнения - Отобразить представление статистики </summary>
        private bool CanShowStatisticsViewCommandExecute() => true;

        /// <summary> Логика выполнения - Отобразить представление статистики </summary>
        private void OnShowStatisticsViewCommandExecuted()
        {
            CurrentModel = new StatisticsViewModel(_buyerRepository, _productsRepository, _dealRepository);
        }

        #endregion



        public MainWindowViewModel(
            IRepository<Product> ProductsRepository,
            IRepository<Buyer> BuyerRepository,
            IRepository<Deal> DealRepository,
            ISalesService SalesService,
            IUserDialog UserDialog)
        {
            _productsRepository = ProductsRepository;
            _buyerRepository = BuyerRepository;
            _dealRepository = DealRepository;
            _salesService = SalesService;
            _userDialog = UserDialog;
        }

        //private async void Test()
        //{
        //    var deals_count = _salesService.Deals.Count();
        //    var product = await _productsRepository.GetAsync(5);
        //    var buyer = await  _buyerRepository.GetAsync(7);

        //    var deal = _salesService.MakeADeal(product.Name, buyer);
        //    var deals_count2 = _salesService.Deals.Count();


        //}
    }
}
