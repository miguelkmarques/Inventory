﻿using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Add
{
    public class AddStockCommandHandler(IStockRepository stockRepository, IMediator mediator) : IRequestHandler<AddStockCommand, bool>
    {
        public Task<bool> Handle(AddStockCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
