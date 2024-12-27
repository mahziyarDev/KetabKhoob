using Common.Application;
using Microsoft.AspNetCore.Http;

namespace Shop.Application.Products.RemoveImage;

/// <summary></summary>
/// <param name="ImageFile"></param>
/// <param name="ProductId"></param>
/// <param name="Sequence"></param>
public record RemoveProductImageCommand 
    (
        long ProductId,
        long ImageId   
    )
:IBaseCommand;