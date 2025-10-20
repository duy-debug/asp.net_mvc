using BTL_64132201.Areas.Admin.Models;
using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.DAL
{
    public class ProductDAL
    {
        DBConnect connect = new DBConnect();
        public List<Product> GetListProducts(int? idCategory, int? idAuthor)
        {
            connect.openConnection();
            List<Product> list = new List<Product>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select a.MaSach as ProductId,
                                a.TenSach as ProductTitle,
                                a.MoTa as ProductContent,
                                a.Anh as ProductImg,
                                a.DonGia as ProductPrice,
                                a.SoLuong as ProductQuantity,
                                a.DanhGia as ProductRate,
                                a.ThoiGianTao as ProductCreateAt,
                                a.ThoiGianCapNhat as ProductUpdateAt,
                                b.MaLoai as CategoryId,
                                b.TenLoai as CategoryTitle,
                                c.MaTacGia as AuthorId,
                                c.TenTacGia as AuthorName
                                from SanPham a
                                join LoaiSach b on a.MaLoaiSach = b.MaLoai
                                join TacGia c on a.MaTacGia = c.MaTacGia";
                if (idCategory.HasValue)
                {
                    query = query + " where b.MaLoai = " + idCategory + "";
                }
                if (idAuthor.HasValue)
                {
                    query = query + " where c.MaTacGia = " + idAuthor + "";
                }
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Quantity = Convert.ToInt32(reader["ProductQuantity"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                        AuthorId = Convert.ToInt32(reader["AuthorId"]),
                        AuthorName = reader["AuthorName"].ToString(),
                    };
                    list.Add(product);
                }
            }
            connect.closeConnection();
            return list;
        }
        public Product GetProductById(int id)
        {
            connect.openConnection();
            Product product = new Product();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select a.MaSach as ProductId,
                                a.TenSach as ProductTitle,
                                a.MoTa as ProductContent,
                                a.Anh as ProductImg,
                                a.DonGia as ProductPrice,
                                a.SoLuong as ProductQuantity,
                                a.DanhGia as ProductRate,
                                a.ThoiGianTao as ProductCreateAt,
                                a.ThoiGianCapNhat as ProductUpdateAt,
                                b.MaLoai as CategoryId,
                                b.TenLoai as CategoryTitle,
                                c.MaTacGia as AuthorId,
                                c.TenTacGia as AuthorName
                                from SanPham a
                                join LoaiSach b on a.MaLoaiSach = b.MaLoai
                                join TacGia c on a.MaTacGia = c.MaTacGia
                                where a.MaSach = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Quantity = Convert.ToInt32(reader["ProductQuantity"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                        AuthorId = Convert.ToInt32(reader["AuthorId"]),
                        AuthorName = reader["AuthorName"].ToString(),
                    };
                }
            }
            connect.closeConnection();
            return product;
        }
        public List<Product> FeaturedProducts(int? limitProduct)
        {
            connect.openConnection();
            List<Product> list = new List<Product>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select top " + limitProduct + @"a.MaSach as ProductId,
                                a.TenSach as ProductTitle,
                                a.MoTa as ProductContent,
                                a.Anh as ProductImg,
                                a.DonGia as ProductPrice,
                                a.SoLuong as ProductQuantity,
                                a.DanhGia as ProductRate,
                                a.ThoiGianTao as ProductCreateAt,
                                a.ThoiGianCapNhat as ProductUpdateAt,
                                b.MaLoai as CategoryId,
                                b.TenLoai as CategoryTitle,
                                c.MaTacGia as AuthorId,
                                c.TenTacGia as AuthorName
                                from SanPham a
                                join LoaiSach b on a.MaLoaiSach = b.MaLoai
                                join TacGia c on a.MaTacGia = c.MaTacGia
                                order by a.DanhGia desc";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Quantity = Convert.ToInt32(reader["ProductQuantity"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                        AuthorId = Convert.ToInt32(reader["AuthorId"]),
                        AuthorName = reader["AuthorName"].ToString(),
                    };
                    list.Add(product);
                }
            }
            connect.closeConnection();
            return list;
        }
        public List<Product> GetRelatedProducts(int Id)
        {
            connect.openConnection();
            List<Product> list = new List<Product>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"								select a.MaSach as ProductId,
                                a.TenSach as ProductTitle,
                                a.MoTa as ProductContent,
                                a.Anh as ProductImg,
                                a.DonGia as ProductPrice,
                                a.SoLuong as ProductQuantity,
                                a.DanhGia as ProductRate,
                                a.ThoiGianTao as ProductCreateAt,
                                a.ThoiGianCapNhat as ProductUpdateAt,
                                b.MaLoai as CategoryId,
                                b.TenLoai as CategoryTitle,
                                c.MaTacGia as AuthorId,
                                c.TenTacGia as AuthorName
                                from SanPham a
                                join LoaiSach b on a.MaLoaiSach = b.MaLoai
                                join TacGia c on a.MaTacGia = c.MaTacGia
                                where a.MaSach != @Id and (a.MaTacGia = (select MaTacGia from SanPham where MaSach = @Id) or a.MaLoaiSach = (select MaLoaiSach from SanPham where MaSach = @Id))
								order by a.DanhGia desc";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", Id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Quantity = Convert.ToInt32(reader["ProductQuantity"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                        AuthorId = Convert.ToInt32(reader["AuthorId"]),
                        AuthorName = reader["AuthorName"].ToString(),
                    };
                    list.Add(product);
                }
            }
            connect.closeConnection();
            return list;
        }
        public List<Product> GetProducts_Pagination(int? idCategory, int? idAuthor, int pageIndex, int pageSize, string sortOrder)
        {
            connect.openConnection();
            List<Product> list = new List<Product>();
            string categoryCondition = "";
            string authorCondition = "";
            if (idCategory.HasValue)
            {
                categoryCondition = @" WHERE a.MaLoaiSach = " + idCategory;
            }
            if (idAuthor.HasValue)
            {
                authorCondition = @"WHERE a.MaTacGia = " + idAuthor;
            }
            string sortQuery = " ORDER BY a.MaSach ";
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "price":
                        sortQuery = " ORDER BY a.DonGia ";
                        break;
                    case "price_desc":
                        sortQuery = " ORDER BY a.DonGia DESC ";
                        break;
                    case "rate_desc":
                        sortQuery = " ORDER BY a.DanhGia DESC ";
                        break;

                    default:
                        sortQuery = " ORDER BY a.MaSach ";
                        break;
                }
            }
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;

                string query = @"SELECT * FROM (
                         SELECT ROW_NUMBER() OVER ( " + sortQuery + @" ) AS RowNumber,
                                a.MaSach as ProductId,
                                a.TenSach as ProductTitle,
                                a.MoTa as ProductContent,
                                a.Anh as ProductImg,
                                a.DonGia as ProductPrice,
                                a.SoLuong as ProductQuantity,
                                a.DanhGia as ProductRate,
                                a.ThoiGianTao as ProductCreateAt,
                                a.ThoiGianCapNhat as ProductUpdateAt,
                                b.MaLoai as CategoryId,
                                b.TenLoai as CategoryTitle,
                                c.MaTacGia as AuthorId,
                                c.TenTacGia as AuthorName
                                from SanPham a
                                join LoaiSach b on a.MaLoaiSach = b.MaLoai
                                join TacGia c on a.MaTacGia = c.MaTacGia "
                             + categoryCondition + authorCondition + @"
                        ) TableResult
                        WHERE TableResult.RowNumber BETWEEN( @PageIndex -1) * @PageSize + 1 
                         AND @PageIndex * @PageSize 
                    ";

                command.CommandText = query;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    Product product = new Product()
                    {
                        Id = Convert.ToInt32(reader["ProductId"]),
                        Title = reader["ProductTitle"].ToString() ?? "",
                        Content = reader["ProductContent"].ToString() ?? "",
                        Img = reader["ProductImg"].ToString() ?? "",
                        Price = Convert.ToInt32(reader["ProductPrice"]),
                        Rate = Convert.ToDouble(reader["ProductRate"].ToString()),
                        CreateAt = DateTime.Parse(reader["ProductCreateAt"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ProductUpdateAt"]?.ToString()),
                        CategoryId = Convert.ToInt32(reader["CategoryId"]),
                        CategoryTitle = reader["CategoryTitle"].ToString(),
                    };

                    list.Add(product);
                }
            }
            connect.closeConnection();
            return list;
        }
    }
}
