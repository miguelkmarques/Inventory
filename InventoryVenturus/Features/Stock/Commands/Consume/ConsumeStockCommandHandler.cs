using MediatR;

namespace InventoryVenturus.Features.Stock.Commands.Consume
{
    public class ConsumeStockCommandHandler : IRequestHandler<ConsumeStockCommand, bool>
    {
        public Task<bool> Handle(ConsumeStockCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}
