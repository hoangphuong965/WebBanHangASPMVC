using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace WebBanHang.Models.EF
{
    [Table("tb_ProductCategory")]
    public class ProductCategory:CommonAbstract
    {
        public ProductCategory()
        {
            this.Products = new HashSet<Product>();
        }
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tên danh mục không được bỏ trống")]
        [StringLength(150, ErrorMessage = "Không được vượt quá 150 ký tự")]
        //[Index(IsUnique = true)]
        public string Title { get; set; }

        [Required]
        [StringLength(150)]
        //[Index("Danh muc ton tai", IsUnique = true)]
        public string Alias { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyWords { get; set; }

        public virtual ICollection<Product> Products { get; set; }

        //IEnumerable<ValidationResult> IValidatableObject.Validate(ValidationContext validationContext)
        //{
        //    ApplicationDbContext db = new ApplicationDbContext();
        //    var validateName = db.Products.FirstOrDefault
        //    (x => x.Alias == Alias && x.Id != Id);
        //    if (validateName != null)
        //    {
        //        ValidationResult errorMessage = new ValidationResult
        //        ("Alias name already exists.", new[] { "Alias" });
        //        yield return errorMessage;
        //    }
        //    else
        //    {
        //        yield return ValidationResult.Success;
        //    }
        //}
    }
}