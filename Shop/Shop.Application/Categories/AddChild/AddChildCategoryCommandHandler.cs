using Common.Application;
using Shop.Domain.CategoryAgg.Repository;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.AddChild;

public class AddChildCategoryCommandHandler : IBaseCommandHandler<AddChildCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    /// <summary></summary>
    /// <param name="categoryDomainService"></param>
    /// <param name="categoryRepository"></param>
    public AddChildCategoryCommandHandler(ICategoryDomainService categoryDomainService, ICategoryRepository categoryRepository)
    {
        _categoryDomainService = categoryDomainService;
        _categoryRepository = categoryRepository;
    }
    
    public async Task<OperationResult> Handle(AddChildCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetTracking(request.ParentId);
        if (category == null)
            return OperationResult.NotFound();
        
        category.AddChild(request.Title,request.Slug,request.SeoData,_categoryDomainService);
        await _categoryRepository.Save();
        return OperationResult.Success();
    }
}