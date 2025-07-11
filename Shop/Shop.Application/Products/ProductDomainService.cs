using Shop.Domain.ProductAgg.Repository;
using Shop.Domain.ProductAgg.Services;

namespace Shop.Application.Products
{
    public class ProductDomainService : IProductDomainService
    {
        private readonly IProductRepository _repository;
        /// <summary></summary>
        /// <param name="repository"></param>
        public ProductDomainService(IProductRepository repository)
        {
            _repository = repository;
        }

        public bool SlugIsExist(string slug)
        {
            return _repository.Exists(x => x.Slug == slug);
        }
    }
}
