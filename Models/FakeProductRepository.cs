using SportsStore.Models;

namespace SportsStore.Models
{
    public class FakeProductRepository : IProductRepository
    {
        public IEnumerable<Product> Products => new List<Product> {

        new Product {ProductID = 1, Name = "Football", Description = "A standard football", Price = 20 },
    new Product {ProductID = 2, Name = "Basketball", Description = "A professional basketball", Price = 25 },
    new Product {ProductID = 3, Name = "Tennis Racket", Description = "A high-quality tennis racket", Price = 50 }
    };
    }
}