using System.Windows.Input;
using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.AddImage;

/// <summary></summary>
/// <param name="ImageFile"></param>
/// <param name="ProductId"></param>
/// <param name="Sequence"></param>
public record AddProductImageCommand 
    (
        IFormFile ImageFile,
        long ProductId,
        int Sequence
    )
:IBaseCommand;