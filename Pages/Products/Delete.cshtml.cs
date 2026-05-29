using DotNetWeb.Models;
using DotNetWeb.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace DotNetWeb.Pages.Products
{
    public class DeleteModel : PageModel
    {
        private readonly ProductService _service;

        public DeleteModel(ProductService service)
        {
            _service = service;
        }

        [BindProperty]
        public Product Product { get; set; } = default!;

        // 載入要刪除的資料供確認
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

            Product = product;
            return Page();
        }

        // 確認刪除 (Delete)
        public IActionResult OnPost(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _service.Delete(id.Value);
            return RedirectToPage("./Index");
        }
    }
}
