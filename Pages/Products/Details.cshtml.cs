using DotNetWeb.Models;
using DotNetWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetWeb.Pages.Products
{
    public class DetailsModel : PageModel
    {
        private readonly ProductService _service;

        public DetailsModel(ProductService service)
        {
            _service = service;
        }

        public Product Product { get; set; } = default!;

        // 讀取單一資料 (Read One)
        public IActionResult OnGet(int? id)
        {
            if (id == null)
            {
                return NotFound(); // 找不到頁面
            }

            var product = _service.GetById(id.Value);
            if (product == null)
            {
                return NotFound();
            }

            Product = product;
            return Page();
        }
    }
}
