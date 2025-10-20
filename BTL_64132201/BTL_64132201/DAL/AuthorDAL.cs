using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.DAL
{
    public class AuthorDAL
    {
        DBConnect connect = new DBConnect();
        public List<AuthorMenu> getAuthorWithCount()
        {
            connect.openConnection();
            List<AuthorMenu> list = new List<AuthorMenu>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"SELECT A.MaTacGia AS Id,
                                A.TenTacGia AS Title,
                                COUNT(B.MaSach) AS Count
                                FROM TacGia A LEFT JOIN SanPham B ON A.MaTacGia = B.MaTacGia
                                GROUP BY A.MaTacGia, A.TenTacGia
                                ORDER BY Count DESC";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AuthorMenu author = new AuthorMenu()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString() ?? "",
                        Count = Convert.ToInt32(reader["Count"]),
                    };
                    list.Add(author);
                }
            }
            connect.closeConnection();
            return list;
        }
    }
}
