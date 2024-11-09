using System.ComponentModel.DataAnnotations;
using Common.Application;
using Common.Domain.ValueObjects;

namespace Shop.Application.Categories.Create;

/// <summary></summary>
/// <param name="Title"></param>
/// <param name="Slug"></param>
/// <param name="SeoData"></param>
public record CreateCategoryCommand
(
    string Title,
    string Slug,   
    SeoData SeoData
    
) : IBaseCommand;