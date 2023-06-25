using Catalog.API.Entities;
using Catalog.API.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Catalog.API.Controllers;

[Route("api/v1/[controller]")]
[ApiController]
public class CatalogController : ControllerBase
{
    private readonly IProductRepository _productRepository;

    public CatalogController(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
    public async Task<IActionResult> GetProducts()
        => Ok(await _productRepository.GetProducts());

    [HttpGet("{id:length(24)}",Name ="GetProduct")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetProductById(string id)
    {
        var product = await _productRepository.GetProduct(id);

        return product is null ? NotFound() : Ok(product);  
    }

    [HttpGet]
    [Route("[action]/{category}",Name ="GetProductByCategory")]
    [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(IEnumerable<Product>))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> GetProductsByCategory(string category)
    {
        if(category == null) return BadRequest("Invalid Category!");

        var products = await _productRepository.GetProductsByCategory(category);

        return Ok(products);
    }

    [HttpPost]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateProduct(Product product)
    {
        if (product is null) return BadRequest("Invalid Product!");

        await _productRepository.CreateProduct(product);

        return CreatedAtRoute("GetProduct", new { id = product.Id }, product);
    }

    [HttpPut]
    [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(Product))]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    //[ApiConventionMethod(typeof(DefaultApiConventions), nameof(DefaultApiConventions.Put)]
    public async Task<IActionResult> UpdateProduct(Product product)
    {
        if (product is null) return BadRequest("Invalid Product");

        return Ok(await _productRepository.UpdateProduct(product));
    }


    [HttpDelete("{id:length(24)}", Name = "DeleteProduct")]
    [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
    public async Task<IActionResult> DeleteProductById(string id)
        => Ok(await _productRepository.DeleteProduct(id));
}
