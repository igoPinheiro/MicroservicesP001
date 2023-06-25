using Catalog.API.Entities;
using MongoDB.Driver;

namespace Catalog.API.Data
{
    public class CatalogContextSeed
    {
        public static void SeedData(IMongoCollection<Product> productCollection)
        {
            // Verifica se existe dados na coleção
            // Caso não exista dados default serão add na coleção
            bool existProduct = productCollection.Find(p=>true).Any();

            if (!existProduct)
                productCollection.InsertManyAsync(GetMyProducts());
        }

        private static IEnumerable<Product> GetMyProducts()
        {
            return new List<Product>()
            {
                new Product()
                {
                    Id = "60d1e292439175d6e87abcf1",
                    Name="IPhone X",
                    Description="Lorem ipsum dolor sit amet, consectetur adipicing ...",
                    Imagem="product-1.png",
                    Price = 5000.00M,
                    Category="Smart Phone"
                },
                 new Product()
                {
                    Id = "60d1e292439175d6e87abcf2",
                    Name="Samsung 10",
                    Description="Lorem ipsum dolor sit amet, consectetur adipicing ...",
                    Imagem="product-2.png",
                    Price = 4000.99M,
                    Category="Smart Phone"
                }
            };
        }
    }
}
