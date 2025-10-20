using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace BTL_64132201.Areas.Admin.Models
{
    public class ProductAdmin
    {
        [Key]
        public int Id { get; set; }
        [Display(Name = "Title")]
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string Title { get; set; }
        [Display(Name = "Content")]
        [Required(ErrorMessage = "*")]
        [MaxLength(500, ErrorMessage = "Tối đa 500 kí tự")]
        public string Content { get; set; }
        [Display(Name = "Image")]
        public string Img { get; set; }
        [Display(Name = "Price")]
        [Required(ErrorMessage = "*")]
        public int Price { get; set; }
        [Display(Name = "Quantity")]
        [Required(ErrorMessage = "*")]
        public int Quantity { get; set; }
        [Display(Name = "Rating")]
        [Required(ErrorMessage = "*")]
        public double Rate { get; set; }
        [Display(Name = "Create At")]
        public DateTime CreateAt { get; set; }
        [Display(Name = "Update At")]
        public DateTime UpdateAt { get; set; }
        [Display(Name = "Category Id")]
        [Required(ErrorMessage = "*")]
        public int CategoryId { get; set; }
        [Display(Name = "Category Title")]
        [Required(ErrorMessage = "*")]
        public string CategoryTitle { get; set; }
        [Display(Name = "Author Id")]
        [Required(ErrorMessage = "*")]
        public int AuthorId { get; set; }
        [Display(Name = "Author Name")]
        [Required(ErrorMessage = "*")]
        public string AuthorName { get; set; }
    }
    public class ProductFormAdmin : ProductAdmin
    {
        public string IdCategorySelected { get; set; }
        public string IdAuthorSelected { get; set; }
        public List<SelectListItem>? ListCategory { get; set; }
        public List<SelectListItem>? ListAuthor { get; set; }
    }
    public class ProductAdminModel
    {
        public List<ProductAdmin> ProductAdmins { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
