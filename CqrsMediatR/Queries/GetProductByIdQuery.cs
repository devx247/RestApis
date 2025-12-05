using CqrsMediatR.Models;
using MediatR;

namespace CqrsMediatR.Queries
{
    public record GetProductByIdQuery(int Id) : IRequest<Product?>;
}
