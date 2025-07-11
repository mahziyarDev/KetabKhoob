﻿using Common.Application;
using Shop.Domain.SiteEntities.Repository;


namespace Shop.Application.SiteEntities.ShippingMethods.Edit;

internal class EditShippingMethodCommandHandler : IBaseCommandHandler<EditShippingMethodCommand>
{
    private readonly IShippingMethodRepository _repository;

    public EditShippingMethodCommandHandler(IShippingMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(EditShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _repository.GetTrackingAsync(request.Id, cancellationToken);

        if (shipping == null)
            return OperationResult.NotFound();


        shipping.Edit(request.Cost, request.Title);
        await _repository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}