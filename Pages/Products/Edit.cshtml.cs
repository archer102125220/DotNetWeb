using DotNetWeb.Models;
using DotNetWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetWeb.Pages.Products
{
    public class EditModel : PageModel
    {
        private readonly ProductService _service;

        public EditModel(ProductService service)
        {
            _service = service;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // 接收 URL 傳來的 id 參數
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var product = _service.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            // 將資料放到綁定的屬性中，顯示在畫面上
            Product = product;
            return Page();
        }

        // 提交修改表單 (Update)
        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _service.Update(Product);
            return RedirectToPage("./Index");
        }
    }
}
