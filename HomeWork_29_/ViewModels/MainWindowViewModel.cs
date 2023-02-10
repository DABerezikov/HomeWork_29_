using System.Linq;
using HomeWork_29_.Services.Interfaces;
using HomeWork_29_.ViewModels.Base;
using HomeWork_29_DB.Entityes;
using HW_29.Interfaces;

namespace HomeWork_29_.ViewModels
{
    internal class MainWindowViewModel : ViewModel
    {
        private readonly IRepository<Product> _productsRepository;
        private readonly IRepository<Buyer> _buyerRepository;
        private readonly ISalesService _salesService;

        #region Title : string - Заголовок окна

        /// <summary>Заголовок окна</summary>
        private string _Title = "Главное окно";

        /// <summary>Заголовок окна</summary>
        public string Title { get => _Title; set => Set(ref _Title, value); }

        #endregion

        #region Status : string - Статус

        /// <summary>Статус</summary>
        private string _Status = "Готов!";

        /// <summary>Статус</summary>
        public string Status { get => _Status; set => Set(ref _Status, value); }

        #endregion

        public MainWindowViewModel(
            IRepository<Product> ProductsRepository,
            IRepository<Buyer> BuyerRepository,
            ISalesService SalesService)
        {
            _productsRepository = ProductsRepository;
            _buyerRepository = BuyerRepository;
            _salesService = SalesService;

            var deals_count = _salesService.Deals.Count();
            var product = _productsRepository.Get(5);
            var buyer = _buyerRepository.Get(7);

            var deal = SalesService.MakeADeal(product.Name, buyer);
            var deals_count2 = _salesService.Deals.Count();
        }
    }
}
