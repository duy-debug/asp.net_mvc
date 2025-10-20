using BTL_64132201.Areas.Admin.Models;
using BTL_64132201.Database;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.Areas.Admin.DAL
{
    public class AuthorAdminDAL
    {
        DBConnect connect = new DBConnect();
        public List<AuthorAdmin> getAll()
        {
            connect.openConnection();
            List<AuthorAdmin> list = new List<AuthorAdmin>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select * from TacGia ";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AuthorAdmin author = new AuthorAdmin()
                    {
                        Id = Convert.ToInt32(reader["MaTacGia"]),
                        Title = reader["TenTacGia"].ToString() ?? "",
                        CreateAt = DateTime.Parse(reader["ThoiGianTao"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ThoiGianCapNhat"]?.ToString()),
                    };
                    list.Add(author);
                }
            }
            connect.closeConnection();
            return list;
        }
        public bool AddNew(AuthorAdmin authorAdd)
        {
            connect.openConnection();
            int id = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"insert into TacGia values(@title, @createAt, @updateAt); ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@title", authorAdd.Title);
                command.Parameters.AddWithValue("@createAt", authorAdd.CreateAt);
                command.Parameters.AddWithValue("@updateAt", authorAdd.UpdateAt);
                Console.WriteLine("Command Insert Category: " + command.CommandText);
                id = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return id > 0;
        }
        public AuthorAdmin getAuthorById(int id)
        {
            connect.openConnection();
            AuthorAdmin author = new AuthorAdmin();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select * from TacGia where MaTacGia = " + id;
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    author.Id = id;
                    author.Title = reader["TenTacGia"].ToString() ?? "";
                    author.CreateAt = DateTime.Parse(reader["ThoiGianTao"]?.ToString());
                    author.UpdateAt = DateTime.Parse(reader["ThoiGianCapNhat"]?.ToString());
                }
            }
            connect.closeConnection();
            return author;
        }
        public bool UpdateAuthorById(AuthorAdmin authorAdd, int Id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"update TacGia set
                                TenTacGia = @title,
                                ThoiGianTao = @createAt,
                                ThoiGianCapNhat = @updateAt
                                where MaTacGia = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@title", authorAdd.Title);
                command.Parameters.AddWithValue("@createAt", authorAdd.CreateAt);
                command.Parameters.AddWithValue("@updateAt", authorAdd.UpdateAt);
                Console.WriteLine("Command Insert Category: " + command.CommandText);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public bool DeleteAuthorById(int id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"DELETE FROM TacGia WHERE MaTacGia = @id";
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
