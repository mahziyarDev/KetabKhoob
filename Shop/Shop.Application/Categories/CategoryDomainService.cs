using Shop.Domain.CategoryAgg.Repository;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories
{
    public class CategoryDomainService : ICategoryDomainService
    {
        private readonly ICategoryRepository _categoryRepository;
        /// <summary></summary>
        /// <param name="categoryRepository"></param>
        public CategoryDomainService(ICategoryRepository categoryRepository)
        {
            _categoryRepository = categoryRepository;
        }

        public bool IsSlugExist(string slug)
        {
            return _categoryRepository.Exists(x => x.Slug == slug);
        }
    }
}
