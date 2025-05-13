using Microsoft.AspNetCore.Mvc;
using ApiRestClean.Core.Entities;
using ApiRestClean.Core.Interfaces;

namespace ApiRestClean.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProductController : ControllerBase
{
    private readonly IProductRepository _repository;

    public ProductController(IProductRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var products = _repository.GetAll();
        return Ok(products);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(Guid id)
    {
        var product = _repository.GetById(id);
        return product is null ? NotFound() : Ok(product);
    }

    [HttpPost]
    public IActionResult Create(Product product)
    {
        _repository.Add(product);
        return CreatedAtAction(nameof(GetById), new { id = product.Id }, product);
    }

    [HttpPut("{id}")]
    public IActionResult Update(Guid id, Product updated)
    {
        var existing = _repository.GetById(id);
        if (existing is null) return NotFound();

        existing.Name = updated.Name;
        existing.Price = updated.Price;
        _repository.Update(existing);

        return Ok(existing);
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(Guid id)
    {
        var product = _repository.GetById(id);
        if (product is null) return NotFound();

        _repository.Delete(id);
        return NoContent();
    }
}
