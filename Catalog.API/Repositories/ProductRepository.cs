using Catalog.API.Data;
using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Repositories;

// O Padrão Repositorio - ele separa a logica de acesso a dados
// e mapeia essa logica para as entidades na logica de negocio
// No Padrao Repositorio a logica de dominio e a logica de acesso a dados
// se comunicam usando interfaces e isso esconde os detalhes do acesso a dados
// da camada de negocio.
// O Repositorio é um servico do dominio que abstrai a camada de persistencia
// da sua aplicacao e atua como api para servicos da aplicacao, nesse caso
// para os controladores.
public class ProductRepository : IProductRepository
{
    private readonly ICatalogContext _catalogContext;

    public ProductRepository(ICatalogContext catalogContext)
    {
        _catalogContext = catalogContext;
    }

    public async Task CreateProduct(Product product)
    {
       await  _catalogContext.Products.InsertOneAsync(product);
    }

    public async Task<bool> DeleteProduct(string id)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter.Eq(d=> d.Id,id);

        DeleteResult deleteResult = await _catalogContext.Products.DeleteOneAsync(filter);

        return deleteResult.IsAcknowledged
            && deleteResult.DeletedCount > 0;
    }

    public async Task<Product> GetProduct(string id)
    {
        return await _catalogContext.Products.Find(f => f.Id == id).FirstOrDefaultAsync();
    }

    public async Task<IEnumerable<Product>> GetProducts()
    {
        return await _catalogContext.Products.Find(p=>true).ToListAsync();
    }

    public async Task<IEnumerable<Product>> GetProductsByCategory(string categoryName)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter
            .Eq(f => f.Category, categoryName);

        return await _catalogContext.Products.Find(filter).ToListAsync();

    }

    public async Task<IEnumerable<Product>> GetProductsByName(string name)
    {
        FilterDefinition<Product> filter = Builders<Product>.Filter
            .Eq(f => f.Name, name);

        return await _catalogContext.Products.Find(filter).ToListAsync();
    }

    public async Task<bool> UpdateProduct(Product product)
    {
        var updateResult = await _catalogContext.Products.ReplaceOneAsync(
            filter: f=> f.Id == product.Id,replacement:product);

        return updateResult.IsAcknowledged
            && updateResult.ModifiedCount > 0;
    }
}
