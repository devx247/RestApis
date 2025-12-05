using CqrsMediatR.Contracts;
using CqrsMediatR.Models;
using CqrsMediatR.Queries;
using MediatR;

namespace CqrsMediatR.Handlers
{
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, Product?>
    {
        private readonly IDataStore _dataStore;

        public GetProductByIdHandler(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }
        public async Task<Product?> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            return await _dataStore.GetProductById(request.Id);
        }
    }
}
