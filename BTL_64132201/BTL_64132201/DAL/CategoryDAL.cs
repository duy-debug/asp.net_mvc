using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.DAL
{
    public class CategoryDAL
    {
        DBConnect connect = new DBConnect();
        public List<CategoryMenu> getCategoryWithCount()
        {
            connect.openConnection();
            List<CategoryMenu> list = new List<CategoryMenu>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"SELECT A.MaLoai AS Id,
                                A.TenLoai AS Title,
                                COUNT(B.MaSach) AS Count
                                FROM LoaiSach A LEFT JOIN SanPham B ON A.MaLoai = B.MaLoaiSach
                                GROUP BY A.MaLoai, A.TenLoai
                                ORDER BY Count DESC";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    CategoryMenu category = new CategoryMenu()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["Title"].ToString() ?? "",
                        Count = Convert.ToInt32(reader["Count"]),
                    };
                    list.Add(category);
                }
            }
            connect.closeConnection();
            return list;
        }
    }
}
