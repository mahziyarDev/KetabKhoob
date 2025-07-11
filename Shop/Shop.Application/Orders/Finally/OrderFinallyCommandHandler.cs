﻿using Common.Application;
using Shop.Domain.OrderAgg.Repository;

namespace Shop.Application.Orders.Finally;

public class OrderFinallyCommandHandler : IBaseCommandHandler<OrderFinallyCommand>
{
    private readonly IOrderRepository _repository;
    public OrderFinallyCommandHandler(IOrderRepository repository)
    {
        _repository = repository;
    }

    public async Task<OperationResult> Handle(OrderFinallyCommand request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetTrackingAsync(request.OrderId,cancellationToken);
        if (order == null)
            return OperationResult.NotFound();

        order.Finally();
        await _repository.SaveChangeAsync(cancellationToken);
        return OperationResult.Success();
    }
}