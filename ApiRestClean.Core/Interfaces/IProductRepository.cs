using ApiRestClean.Core.Entities;

namespace ApiRestClean.Core.Interfaces;

public interface IProductRepository
{
    IEnumerable<Product> GetAll();
    Product? GetById(Guid id);
    void Add(Product product);
    void Update(Product product);
    void Delete(Guid id);
}
