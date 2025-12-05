using CqrsMediatR.Models;

namespace CqrsMediatR.Contracts;

public interface IDataStore
{
    Task CreateProduct(Product product);
    Task<IEnumerable<Product>> GetAllProducts();
    Task<Product?> GetProductById(int requestId);
    Task UpdateEventOccured(Product product, string updateEvent);
}