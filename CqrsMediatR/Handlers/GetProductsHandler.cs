using CqrsMediatR.Contracts;
using CqrsMediatR.Models;
using CqrsMediatR.Queries;
using MediatR;

namespace CqrsMediatR.Handlers
{
    public class GetProductsHandler : IRequestHandler<GetProductsQuery, IEnumerable<Product>>
    {
        public IDataStore DataStore { get; }

        public GetProductsHandler(IDataStore dataStore)
        {
            DataStore = dataStore;
        }

        public async Task<IEnumerable<Product>> Handle(GetProductsQuery request, CancellationToken cancellationToken)
        {
            return await  DataStore.GetAllProducts();
        }
    }
}