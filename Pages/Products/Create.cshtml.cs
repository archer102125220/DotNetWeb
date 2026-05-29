using DotNetWeb.Models;
using DotNetWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetWeb.Pages.Products
{
    public class CreateModel : PageModel
    {
        private readonly ProductService _service;

        public CreateModel(ProductService service)
        {
            _service = service;
        }

        // 綁定屬性，讓表單提交時自動對應到 Product 物件
        [BindProperty]
        public Product Product { get; set; } = new();

        public void OnGet()
        {
            // 進入新增頁面時不需做特別處理
        }

        // 當表單提交 (POST 請求) 時觸發 (Create)
        public IActionResult OnPost()
        {
            // 驗證模型狀態 (例如：是否漏填必填欄位)
            if (!ModelState.IsValid)
            {
                return Page(); // 驗證失敗，返回當前頁面並顯示錯誤訊息
            }

            // 新增資料
            _service.Add(Product);
            
            // 成功後導向 Index 頁面
            return RedirectToPage("./Index");
        }
    }
}
