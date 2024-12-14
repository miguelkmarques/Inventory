using InventoryVenturus.Repositories.Interfaces;
using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Consume
{
    public class ConsumeStockCommandHandler(IStockRepository stockRepository, IMediator mediator) : IRequestHandler<ConsumeStockCommand, bool>
    {
        public Task<bool> Handle(ConsumeStockCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
