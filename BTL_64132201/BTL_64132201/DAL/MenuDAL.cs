using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.DAL
{
    public class MenuDAL
    {
        DBConnect connect = new DBConnect();
        public List<MenuItem> GetAllMenu()
        {
            connect.openConnection();
            List<MenuItem> list = new List<MenuItem>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"SELECT * FROM Menu ORDER BY MenuIndex";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    MenuItem MenuItem = new MenuItem()
                    {
                        Id = Convert.ToInt32(reader["Id"]),
                        Title = reader["TieuDe"].ToString() ?? "",
                        ParentId = reader["ParentId"] != DBNull.Value ? Convert.ToInt32(reader["ParentId"]) : null,
                        MenuUrl = reader["MenuUrl"]?.ToString() ?? null,
                        MenuIndex = Convert.ToInt32(reader["MenuIndex"]),
                        isVisible = Convert.ToInt32(reader["IsVisible"]) == 1
                    };
                    list.Add(MenuItem);
                }
            }
            connect.closeConnection();
            return list;
        }

    }
}
