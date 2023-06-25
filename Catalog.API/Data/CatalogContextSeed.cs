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
                    Id = "602d2149e773f2a39990b47f5",
                    Name="IPhone X",
                    Description="Lorem ipsum dolor sit amet, consectetur adipicing ...",
                    Imagem="product-1.png",
                    Price = 5000.00M,
                    Category="Smart Phone"
                },
                 new Product()
                {
                    Id = "602d2149e773f2a39990b47f6",
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
