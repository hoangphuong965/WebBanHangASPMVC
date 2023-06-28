using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Models.EF
{
    [Table("tb_Product")]
    public class Product : CommonAbstract
    {
        public Product()
        {
            this.ProductImage = new HashSet<ProductImage>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên sản phẩm không được bỏ trống")]
        [StringLength(250, ErrorMessage = "Không được vượt quá 250 ký tự")]
        public string Title { get; set; }
        [StringLength(250)]
        public string Alias { get; set; }

        [StringLength(50)]
        public string ProductCode { get; set; }

        [Required(ErrorMessage = "Danh mục không được bỏ trống")]
        public int ProductCategoryId { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }

        [AllowHtml]
        public string Detail { get; set; }
        public string Image { get; set; }

        [Required(ErrorMessage = "OriginalPrice không được bỏ trống")]
        public decimal OriginalPrice { get; set; }
        
        [Required(ErrorMessage = "Price không được bỏ trống")]
        public decimal Price { get; set; }

        [Required(ErrorMessage = "PriceSale không được bỏ trống")]
        public decimal PriceSale { get; set; }
        public int Quantity { get; set; }
        public int ViewCount { get; set; }
        public bool IsHome {get; set;}
        public bool IsActive {get; set; }
        public bool IsSale {get; set;}
        public bool IsFeature {get; set;}
        public bool IsHot { get; set;}
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyWords { get; set; }

        public virtual ProductCategory ProductCategory { get; set; }
        public virtual ICollection<ProductImage> ProductImage { get; set; }
    }
}