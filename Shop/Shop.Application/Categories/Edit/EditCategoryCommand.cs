using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.Edit;

/// <summary></summary>
/// <param name="Id"></param>
/// <param name="Title"></param>
/// <param name="Slug"></param>
/// <param name="SeoData"></param>
public record EditCategoryCommand
(
    long Id,
    string Title,
    string Slug,
    SeoData SeoData
): IBaseCommand;