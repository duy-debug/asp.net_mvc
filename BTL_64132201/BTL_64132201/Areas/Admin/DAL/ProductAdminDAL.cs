using BTL_64132201.Areas.Admin.Models;
using BTL_64132201.Database;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.Areas.Admin.DAL
{
    public class ProductAdminDAL
    {
        DBConnect connect = new DBConnect();
        public List<ProductAdmin> getAll()
        {
            connect.openConnection();
            List<ProductAdmin> list = new List<ProductAdmin>();
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
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    ProductAdmin product = new ProductAdmin()
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
        public bool AddNew(ProductFormAdmin productNew)
        {
            connect.openConnection();
            int id = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"insert into SanPham (TenSach, MoTa, Anh, Gia, SoLuong, DanhGia, ThoiGianTao, ThoiGianCapNhat, MaLoaiSach, MaTacGia) values (@title, @content, @img, @price, @quantity, @rate, @createAt, @updateAt, @categoryId, @authorId); ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@title", productNew.Title);
                command.Parameters.AddWithValue("@content", productNew.Content);
                command.Parameters.AddWithValue("@img", productNew.Img);
                command.Parameters.AddWithValue("@price", productNew.Price);
                command.Parameters.AddWithValue("@quantity", productNew.Quantity);
                command.Parameters.AddWithValue("@rate", productNew.Rate);
                command.Parameters.AddWithValue("@createAt", productNew.CreateAt);
                command.Parameters.AddWithValue("@updateAt", productNew.UpdateAt);
                command.Parameters.AddWithValue("@categoryId", productNew.CategoryId);
                command.Parameters.AddWithValue("@authorId", productNew.AuthorId);
                Console.WriteLine("command Insert Product: " + command.CommandText);
                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }
        public ProductAdmin GetProductById(int id)
        {
            connect.openConnection();
            ProductAdmin product = new ProductAdmin();
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
                    product = new ProductAdmin()
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
        public bool UpdateProduct(ProductFormAdmin productNew, int Id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"update SanPham set 
                                TenLoai = @title,
                                MoTa = @content,
                                Anh = @img,
                                DonGia = @price,
                                SoLuong = @quantity,
                                DanhGia = @rate,
                                ThoiGianCapNhat = @updateAt,
                                MaLoaiSach = @categoryId,
                                MaTacGia = @authorId
                                where MaSach = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@title", productNew.Title);
                command.Parameters.AddWithValue("@content", productNew.Content);
                command.Parameters.AddWithValue("@img", productNew.Img);
                command.Parameters.AddWithValue("@price", productNew.Price);
                command.Parameters.AddWithValue("@quantity", productNew.Quantity);
                command.Parameters.AddWithValue("@rate", productNew.Rate);
                command.Parameters.AddWithValue("@updateAt", productNew.UpdateAt);
                command.Parameters.AddWithValue("@categoryId", productNew.CategoryId);
                command.Parameters.AddWithValue("@authorId", productNew.AuthorId);
                Console.WriteLine("Command update Category: " + command.CommandText);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public bool DeleteProduct(int id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"DELETE FROM SanPham WHERE MaSach = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);
                Console.WriteLine("Command delete Category: " + command.CommandText);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public List<ProductAdmin> getProduct_Pagination(int pageIndex, int pageSize, string? searchString, string sortOrder)
        {
            connect.openConnection();
            List<ProductAdmin> list = new List<ProductAdmin>();
            string condition = "";
            if (searchString != "" && searchString != null)
            {
                condition = @" Where a.TenSach Like '%" + searchString + "%' or a.MoTa like '%" + searchString + "%' ";
            }
            string sortQuery = " ORDER BY a.MaSach ";
            if (!string.IsNullOrEmpty(sortOrder))
            {
                switch (sortOrder)
                {
                    case "id_desc":
                        sortQuery = " ORDER BY a.MaSach DESC ";
                        break;
                    case "title":
                        sortQuery = " ORDER BY a.TenSach ";
                        break;
                    case "title_desc":
                        sortQuery = " ORDER BY a.TenSach DESC ";
                        break;
                    case "price":
                        sortQuery = " ORDER BY a.DonGia ";
                        break;
                    case "price_desc":
                        sortQuery = " ORDER BY a.DonGia DESC ";
                        break;
                    case "rate":
                        sortQuery = " ORDER BY a.SoLuong ";
                        break;
                    case "rate_desc":
                        sortQuery = " ORDER BY a.SoLuong DESC ";
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
                string query = @" 
                        SELECT * FROM (
                         SELECT ROW_NUMBER() OVER ( "+ sortQuery + @") AS RowNumber,
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
                                join TacGia c on a.MaTacGia = c.MaTacGia " + condition + @"
     	                    ) TableResult
                        WHERE TableResult.RowNumber BETWEEN( @PageIndex -1) * @PageSize + 1 
                         AND @PageIndex * @PageSize ";


                command.CommandText = query;
                command.Parameters.AddWithValue("@PageIndex", pageIndex);
                command.Parameters.AddWithValue("@PageSize", pageSize);

                SqlDataReader reader = command.ExecuteReader();

                while (reader.Read())
                {
                    ProductAdmin product = new ProductAdmin()
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
        public int getCountRow_Pagination(int pageIndex, int pageSize, string? searchString)
        {
            connect.openConnection();
            int count = 0;
            string condition = "";
            if (searchString != "" && searchString != null)
            {
                condition = @" Where a.TenSach Like '%" + searchString + "%' or a.MoTa like '%" + searchString + "%' ";
            }
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"SELECT Count(*) as CountRow from SanPham a
                                join LoaiSach b on a.MaLoaiSach = b.MaLoai
                                join TacGia c on a.MaTacGia = c.MaTacGia ";
                query = query + condition;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    count = Convert.ToInt32(reader["CountRow"]);
                }
            }
            connect.closeConnection();
            return count;
        }
    }
}
