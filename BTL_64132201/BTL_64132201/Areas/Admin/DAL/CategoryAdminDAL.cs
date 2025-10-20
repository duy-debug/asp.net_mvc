using BTL_64132201.Areas.Admin.Models;
using BTL_64132201.Database;
using Microsoft.Data.SqlClient;
using System.Diagnostics.CodeAnalysis;

namespace BTL_64132201.Areas.Admin.DAL
{
    public class CategoryAdminDAL
    {
        DBConnect connect = new DBConnect();
        public List<CategoryAdmin> getAll()
        {
            connect.openConnection();
            List<CategoryAdmin> list = new List<CategoryAdmin>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select * from LoaiSach ";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CategoryAdmin category = new CategoryAdmin()
                    {
                        Id = Convert.ToInt32(reader["MaLoai"]),
                        Title = reader["TenLoai"].ToString() ?? "",
                        CreateAt = DateTime.Parse(reader["ThoiGianTao"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ThoiGianCapNhat"]?.ToString()),
                    };
                    list.Add(category);
                }
            }
            connect.closeConnection();
            return list;
        }
        public bool AddNew(CategoryAdmin categoryAdd)
        {
            connect.openConnection();
            int id = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"insert into LoaiSach (TenLoai, ThoiGianTao, ThoiGianCapNhat) values (@title, @createAt, @updateAt); ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@title", categoryAdd.Title);
                command.Parameters.AddWithValue("@createAt", categoryAdd.CreateAt.ToString());
                command.Parameters.AddWithValue("@updateAt", categoryAdd.UpdateAt.ToString());
                Console.WriteLine("Command Insert Category: " + command.CommandText);
                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }
        public CategoryAdmin getCategoryById(int id)
        {
            connect.openConnection();
            CategoryAdmin category = new CategoryAdmin();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select * from LoaiSach where MaLoai = " + id;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    category.Id = id;
                    category.Title = reader["TenLoai"].ToString() ?? "";
                    category.CreateAt = DateTime.Parse(reader["ThoiGianTao"]?.ToString());
                    category.UpdateAt = DateTime.Parse(reader["ThoiGianCapNhat"]?.ToString());
                }
            }
            connect.closeConnection();
            return category;
        }
        public bool updateCategoryById(int id, CategoryAdmin categoryUpdate)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"update LoaiSach set TenLoai = @title, ThoiGianCapNhat = @updateAt where MaLoai = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);
                command.Parameters.AddWithValue("@title", categoryUpdate.Title);
                command.Parameters.AddWithValue("@updateAt", categoryUpdate.UpdateAt);
                Console.WriteLine("Command update Category: " + command.CommandText);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public bool deleteCategoryById(int id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"DELETE FROM LoaiSach WHERE MaLoai = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", id);
                Console.WriteLine("Command delete Category: " + command.CommandText);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
    }
}
