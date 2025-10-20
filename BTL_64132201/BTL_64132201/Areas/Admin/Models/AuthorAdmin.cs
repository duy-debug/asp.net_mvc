using System.ComponentModel.DataAnnotations;

namespace BTL_64132201.Areas.Admin.Models
{
    public class AuthorAdmin
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public string Title { get; set; }
        [Required(ErrorMessage = "*")]
        [MaxLength(100, ErrorMessage = "Tối đa 100 kí tự")]
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
    }
}
