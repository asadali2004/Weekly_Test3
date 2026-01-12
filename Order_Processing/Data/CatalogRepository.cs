using Order_Processing.Models;

namespace Order_Processing.Data
{
    public static class CatalogRepository
    {
        public static readonly Dictionary<int, Product> Products = new Dictionary<int, Product>();

        public static void Clear() => Products.Clear();

        public static void Add(Product product)
        {
            Products[product.Id] = product;
        }

        public static IReadOnlyDictionary<int, Product> GetAll() => Products;
    }
}