using ApiRestClean.Core.Entities;
using ApiRestClean.Core.Interfaces;

namespace ApiRestClean.Infrastructure.Repositories;

public class InMemoryProductRepository : IProductRepository
{
    private readonly List<Product> _products = new();

    public IEnumerable<Product> GetAll() => _products;

    public Product? GetById(Guid id) => _products.FirstOrDefault(p => p.Id == id);

    public void Add(Product product) => _products.Add(product);

    public void Update(Product product)
    {
        var index = _products.FindIndex(p => p.Id == product.Id);
        if (index == -1) return;
        _products[index] = product;
    }

    public void Delete(Guid id)
    {
        var product = GetById(id);
        if (product != null)
            _products.Remove(product);
    }
}
