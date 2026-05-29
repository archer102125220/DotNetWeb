using DotNetWeb.Models;
using System.Collections.Generic;
using System.Linq;

namespace DotNetWeb.Services
{
    // 模擬資料庫操作的服務，處理 CRUD 邏輯
    public class ProductService
    {
        // 使用靜態清單來儲存資料，避免每次請求都重置
        private static readonly List<Product> _products = new()
        {
            new Product { Id = 1, Name = "蘋果 (Apple)", Price = 10.5m },
            new Product { Id = 2, Name = "香蕉 (Banana)", Price = 5.0m }
        };

        // 取得所有產品列表 (Read All)
        public List<Product> GetAll() => _products;

        // 取得單一產品詳細資訊 (Read One)
        public Product? GetById(int id) => _products.FirstOrDefault(p => p.Id == id);

        // 新增一筆產品 (Create)
        public void Add(Product product)
        {
            // 找出目前最大的 Id，然後加 1 當作新的 Id
            product.Id = _products.Any() ? _products.Max(p => p.Id) + 1 : 1;
            _products.Add(product);
        }

        // 更新產品資料 (Update)
        public void Update(Product updatedProduct)
        {
            var existing = GetById(updatedProduct.Id);
            if (existing != null)
            {
                existing.Name = updatedProduct.Name;
                existing.Price = updatedProduct.Price;
            }
        }

        // 刪除產品資料 (Delete)
        public void Delete(int id)
        {
            var product = GetById(id);
            if (product != null)
            {
                _products.Remove(product);
            }
        }
    }
}
