using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.AddChild;

/// <summary></summary>
/// <param name="ParentId"></param>
/// <param name="Title"></param>
/// <param name="Slug"></param>
/// <param name="SeoData"></param>
public record AddChildCategoryCommand
(
    long ParentId,
    string Title,
    string Slug,
    SeoData SeoData
) : IBaseCommand;