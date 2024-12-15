using FluentValidation;
using InventoryVenturus.Features.Common;
using InventoryVenturus.Features.Products.Dtos;
using InventoryVenturus.Features.Products.Notifications;
using MediatR;

namespace InventoryVenturus.Features.Products.Queries.Get
{
    public record GetProductQuery(Guid Id) : IRequest<ProductDto?>;

    public class GetProductQueryValidator : AbstractValidator<GetProductQuery>
    {
        public GetProductQueryValidator()
        {
            RuleFor(x => x.Id)
                .ValidateId();
        }
    }
}
