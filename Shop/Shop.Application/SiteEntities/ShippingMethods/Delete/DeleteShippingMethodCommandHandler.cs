﻿using Common.Application;
using Shop.Domain.SiteEntities.Repository;

namespace Shop.Application.SiteEntities.ShippingMethods.Delete;

public class DeleteShippingMethodCommandHandler : IBaseCommandHandler<DeleteShippingMethodCommand>
{
    private readonly IShippingMethodRepository _repository;

    public DeleteShippingMethodCommandHandler(IShippingMethodRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(DeleteShippingMethodCommand request, CancellationToken cancellationToken)
    {
        var shipping = await _repository.GetTrackingAsync(request.Id,cancellationToken);
        if (shipping == null)
            return OperationResult.NotFound();

        _repository.Delete(shipping);
        await _repository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}