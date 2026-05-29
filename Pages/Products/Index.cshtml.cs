using DotNetWeb.Models;
using DotNetWeb.Services;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;

namespace DotNetWeb.Pages.Products
{
    // IndexModel 負責處理 /Products 頁面的後端邏輯
    public class IndexModel : PageModel
    {
        private readonly ProductService _service;

        // 透過依賴注入 (Dependency Injection) 取得 ProductService
        public IndexModel(ProductService service)
        {
            _service = service;
        }

        // 用來將產品清單傳遞給前端 View 的屬性
        public List<Product> Products { get; set; } = new();

        // 當使用者發送 GET 請求到這頁時觸發 (Read All)
        public void OnGet()
        {
            // 從 Service 抓取所有產品資料
            Products = _service.GetAll();
        }
    }
}
