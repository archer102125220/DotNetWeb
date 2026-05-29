using System.ComponentModel.DataAnnotations;

namespace DotNetWeb.Models
{
    // 定義一個簡單的產品模型 (Model)，用於表示系統中的商品資料
    public class Product
    {
        // 產品的唯一識別碼
        public int Id { get; set; }

        // 產品名稱，設定為必填欄位，並加上顯示名稱
        [Required(ErrorMessage = "產品名稱為必填")]
        [Display(Name = "產品名稱")]
        public string Name { get; set; } = string.Empty;

        // 產品價格，設定範圍限制
        [Range(0.01, 10000, ErrorMessage = "價格必須大於 0")]
        [Display(Name = "產品價格")]
        public decimal Price { get; set; }
    }
}
