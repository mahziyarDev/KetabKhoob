using Shop.Domain.SellerAgg;
using Shop.Domain.SellerAgg.Repository;
using Shop.Domain.SellerAgg.Service;

namespace Shop.Application.Sellers
{
    public class SellerDomainService : ISellerDomainService
    {
        private readonly ISellerRepository _sellerRepository;
        /// <summary></summary>
        /// <param name="sellerRepository"></param>
        public SellerDomainService(ISellerRepository sellerRepository)
        {
            _sellerRepository = sellerRepository;
        }

        public bool IsValidSellerInformation(Seller seller)
        {
            return _sellerRepository.Exists(x => x.UserID == seller.Id || x.NationalCode == seller.NationalCode);
        }

        public bool NationalCodeExistInDataBase(string nationalCode)
        {
            return _sellerRepository.Exists(x => x.NationalCode == nationalCode);
        }
    }
}
