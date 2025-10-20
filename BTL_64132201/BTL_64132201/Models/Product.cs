using System.ComponentModel.DataAnnotations;

namespace BTL_64132201.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Img { get; set; }
        public int Price { get; set; }
        public int Quantity { get; set; }
        public double Rate { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public int CategoryId { get; set; }
        public string CategoryTitle { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }

    }
    public class ProductPagination
    {
        public List<Product> Products { get; set; }
        public int CurrentPageIndex { get; set; }
        public int PageCount { get; set; }
    }
}
