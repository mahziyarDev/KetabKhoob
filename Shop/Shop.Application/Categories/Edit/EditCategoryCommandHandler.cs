using Common.Application;
using Shop.Domain.CategoryAgg.Repository;
using Shop.Domain.CategoryAgg.Services;

namespace Shop.Application.Categories.Edit;

public class EditCategoryCommandHandler : IBaseCommandHandler<EditCategoryCommand>
{
    private readonly ICategoryRepository _categoryRepository;
    private readonly ICategoryDomainService _categoryDomainService;

    /// <summary></summary>
    /// <param name="categoryRepository"></param>
    /// <param name="categoryDomainService"></param>
    public EditCategoryCommandHandler(ICategoryRepository categoryRepository, ICategoryDomainService categoryDomainService)
    {
        _categoryRepository = categoryRepository;
        _categoryDomainService = categoryDomainService;
    }
    
    public async Task<OperationResult> Handle(EditCategoryCommand request, CancellationToken cancellationToken)
    {
        var category = await _categoryRepository.GetAsync(request.Id,cancellationToken);
        if (category == null)
            return OperationResult.NotFound();
        category.Edit(request.Title,request.Slug,request.SeoData,_categoryDomainService);
        await _categoryRepository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}