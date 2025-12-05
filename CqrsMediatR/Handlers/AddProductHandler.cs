using CqrsMediatR.Commands;
using CqrsMediatR.Contracts;
using CqrsMediatR.Models;
using MediatR;

namespace CqrsMediatR.Handlers
{
    public class AddProductHandler : IRequestHandler<AddProductCommand, Product>
    {
        private readonly IDataStore _dataStore;

        public AddProductHandler(IDataStore dataStore)
        {
            _dataStore = dataStore;
        }

        public async Task<Product> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            await _dataStore.CreateProduct(request.Product);
            
            return request.Product;
        }
    }
}