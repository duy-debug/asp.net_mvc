using BTL_64132201.Areas.Admin.Models;
using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.Areas.Admin.DAL
{
    public class AccountAdminDAL
    {
        DBConnect connect = new DBConnect();
        public List<AccountAdmin> GetListAccount()
        {
            connect.openConnection();
            List<AccountAdmin> list = new List<AccountAdmin>();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select * from TaiKhoan";
                command.CommandText = query;
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    AccountAdmin account = new AccountAdmin()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        FirstName = reader["Ho"].ToString() ?? "",
                        LastName = reader["Ten"].ToString() ?? "",
                        Address = reader["DiaChi"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Phone = reader["SoDienThoai"].ToString() ?? "",
                        DateOfBirth = reader["NgaySinh"] == DBNull.Value ? null : DateOnly.FromDateTime(DateTime.Parse(reader["NgaySinh"].ToString() ?? "")),
                        Img = reader["Anh"].ToString() ?? "",
                        Password = reader["MatKhau"].ToString() ?? "",
                        IsActive = Convert.ToInt32(reader["TrangThai"]),
                        Role = Convert.ToInt32(reader["VaiTro"]),
                        RegisterAt = DateTime.Parse(reader["ThoiGianDangKy"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ThoiGianCapNhat"]?.ToString()),
                    };
                    list.Add(account);
                }
            }
            connect.closeConnection();
            return list;
        }
        public AccountAdmin GetAccountById(int Id)
        {
            connect.openConnection();
            AccountAdmin account = new AccountAdmin();
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"select * from TaiKhoan where ID = @Id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", Id);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    account = new AccountAdmin()
                    {
                        Id = Convert.ToInt32(reader["ID"]),
                        FirstName = reader["Ho"].ToString() ?? "",
                        LastName = reader["Ten"].ToString() ?? "",
                        Address = reader["DiaChi"].ToString() ?? "",
                        Email = reader["Email"].ToString() ?? "",
                        Phone = reader["SoDienThoai"].ToString() ?? "",
                        DateOfBirth = reader["NgaySinh"] == DBNull.Value ? null : DateOnly.FromDateTime(DateTime.Parse(reader["NgaySinh"].ToString() ?? "")),
                        Img = reader["Anh"].ToString() ?? "",
                        Password = reader["MatKhau"].ToString() ?? "",
                        IsActive = Convert.ToInt32(reader["TrangThai"]),
                        Role = Convert.ToInt32(reader["VaiTro"]),
                        RegisterAt = DateTime.Parse(reader["ThoiGianDangKy"]?.ToString()),
                        UpdateAt = DateTime.Parse(reader["ThoiGianCapNhat"]?.ToString()),
                    };
                }
            }
            connect.closeConnection();
            return account;
        }

        public bool UpdateAccount(AccountAdmin account, int Id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"update TaiKhoan set TrangThai = @IsActive, VaiTro = @Role, ThoiGianCapNhat = @updateAt where ID = @id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@id", Id);
                command.Parameters.AddWithValue("@IsActive", account.IsActive);
                command.Parameters.AddWithValue("@Role", account.Role);
                command.Parameters.AddWithValue("@updateAt", account.UpdateAt);
                Console.WriteLine("Command update Category: " + command.CommandText);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public bool DeleteAccount(int id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"DELETE FROM TaiKhoan WHERE ID = @id";
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
