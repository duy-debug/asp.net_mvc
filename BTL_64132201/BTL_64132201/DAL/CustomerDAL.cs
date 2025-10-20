using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.DAL
{
    public class CustomerDAL
    {
        DBConnect connect = new DBConnect();
        public Customer? GetCustomerById(int Id)
        {
            connect.openConnection();
            Customer customer = new Customer();
            bool hasData = false;
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
                    hasData = true;
                    customer = new Customer()
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
                    };
                }
            }
            connect.closeConnection();
            if (!hasData) return null;
            return customer;
        }
        public bool UpdateDetailCustomer(Customer customerUpdate, int Id)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"UPDATE TaiKhoan SET
                                Ten = @LastName,
                                Ho = @FirstName,
                                Email = @Email,
                                SoDienThoai = @Phone,
                                Anh = @Img,
                                DiaChi = @Address,
                                NgaySinh = @DateOfBirth,
                                ThoiGianCapNhat = @updateAt
                                WHERE ID = @Id";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Id", Id);
                command.Parameters.AddWithValue("@LastName", customerUpdate.LastName);
                command.Parameters.AddWithValue("@FirstName", customerUpdate.FirstName);
                command.Parameters.AddWithValue("@Email", customerUpdate.Email);
                command.Parameters.AddWithValue("@Phone", customerUpdate.Phone);
                command.Parameters.AddWithValue("@Img", customerUpdate.Img);
                command.Parameters.AddWithValue("@Address", customerUpdate.Address);
                command.Parameters.AddWithValue("@DateOfBirth", customerUpdate.DateOfBirth);
                command.Parameters.AddWithValue("@updateAt", customerUpdate.UpdateAt);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public bool SignUp(Customer customer)
        {
            connect.openConnection();
            int isSuccess = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection= connect.getConnecttion();
                command.CommandType= System.Data.CommandType.Text;
                string query = @"INSERT INTO TaiKhoan ([Ho], [Ten], [DiaChi], [SoDienThoai], [Email], [Anh], [ThoiGianDangKy], [ThoiGianCapNhat], [NgaySinh], [MatKhau]) 
                                VALUES (@Ho, @Ten, @DiaChi, @SoDienThoai, @Email, @Anh, @ThoiGianDangKy, @ThoiGianCapNhat, @NgaySinh, @MatKhau)";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Ho", customer.FirstName);
                command.Parameters.AddWithValue("@Ten", customer.LastName);
                command.Parameters.AddWithValue("@DiaChi", customer.Address);
                command.Parameters.AddWithValue("@SoDienThoai", customer.Phone);
                command.Parameters.AddWithValue("@Email", customer.Email);
                command.Parameters.AddWithValue("Anh", customer.Img);
                command.Parameters.AddWithValue("@NgaySinh", customer.DateOfBirth == null ? DBNull.Value : customer.DateOfBirth);
                command.Parameters.AddWithValue("@ThoiGianDangKy", customer.RegisterAt);
                command.Parameters.AddWithValue("@ThoiGianCapNhat", customer.UpdateAt);
                command.Parameters.AddWithValue("@MatKhau", customer.Password);
                isSuccess = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return isSuccess > 0;
        }
        public Customer? GetCustomerByEmail(string email)
        {
            connect.openConnection();
            Customer customer = new Customer();
            bool hasData = false;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"SELECT * FROM TaiKhoan WHERE Email = @Email";
                command.CommandText = query;
                command.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    hasData = true;
                    customer = new Customer()
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
                    };
                }
            }
            connect.closeConnection();

            if (!hasData) return null;
            return customer;
        }
    }
}
