using CqrsMediatR.Contracts;
using CqrsMediatR.Models;

namespace CqrsMediatR.Datastore
{
    public class FakeDataStore : IDataStore
    {
        private static List<Product> _products;

        public FakeDataStore()
        {
            _products = new List<Product>
            {
                new() {Id = 1, Name = "Test Product 1"},
                new() {Id = 2, Name = "Test Product 2"},
                new() {Id = 3, Name = "Test Product 3"}
            };
        }


        public Task CreateProduct(Product product)
        {
            _products.Add(product);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<Product>> GetAllProducts()
        {
            return Task.FromResult(_products.AsEnumerable());
        }

        public Task<Product?> GetProductById(int requestId)
        {
            return Task.FromResult(_products.SingleOrDefault(p => p.Id == requestId));
        }

        public async Task UpdateEventOccured(Product product, string updateEvent)
        {
            var storeProduct = _products.SingleOrDefault(p => p.Id == product.Id);

            if (storeProduct == null) return;

            storeProduct.Name = $"{product.Name} updateEvent: {updateEvent}";

            await Task.CompletedTask;
        }
    }
}