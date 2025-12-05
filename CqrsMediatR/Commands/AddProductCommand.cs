using CqrsMediatR.Models;
using MediatR;

namespace CqrsMediatR.Commands
{
    public record AddProductCommand(Product Product): IRequest<Product>;
}
