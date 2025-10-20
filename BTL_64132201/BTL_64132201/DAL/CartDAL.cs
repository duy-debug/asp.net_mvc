using BTL_64132201.Database;
using BTL_64132201.Models;
using Microsoft.Data.SqlClient;

namespace BTL_64132201.DAL
{
    public class CartDAL
    {
        DBConnect connect = new DBConnect();
        public bool CheckOut(Customer customer, List<CartItem> cart)
        {
            connect.openConnection();
            bool insertPaymentSuccess = false;
            bool CheckOutSuccess = true;
            int? SCOPE_IDENTITY = null;
            int TOTAL_CART = cart.Sum(p => p.Total);
            // Insert vào bảng Payment và lấy Id được tạo tự động sau khi insert
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"insert into HoaDon (MaKhachHang, HoKH, TenKH, SDT, Email, ThoiGianTao, TongTien)
                                values (@customerId, @firstName, @lastName, @phone, @email, GETDATE(), @total);
                                SELECT SCOPE_IDENTITY() AS SCOPE_IDENTITY";
                command.CommandText = query;
                command.Parameters.AddWithValue("@customerId", customer.Id);
                command.Parameters.AddWithValue("@firstName", customer.FirstName);
                command.Parameters.AddWithValue("@lastName", customer.LastName);
                command.Parameters.AddWithValue("@phone", customer.Phone);
                command.Parameters.AddWithValue("@email", customer.Email);
                command.Parameters.AddWithValue("@total", TOTAL_CART);
                SqlDataReader reader = command.ExecuteReader();
                while (reader.Read())
                {
                    insertPaymentSuccess = true;
                    SCOPE_IDENTITY = reader["SCOPE_IDENTITY"] == DBNull.Value ? null
                    : Convert.ToInt32(reader["SCOPE_IDENTITY"]);
                }
            }
            connect.closeConnection();
            if (!insertPaymentSuccess || SCOPE_IDENTITY == null)
            {
                return false;
            }
            foreach (var itemCart in cart)
            {
                bool success = InsertToPaymentDetail(SCOPE_IDENTITY ?? 0, itemCart);
                if (!success)
                {
                    CheckOutSuccess = false;
                }
                bool isSucces = UpdateProduct(itemCart);
                if (!isSucces)
                {
                    CheckOutSuccess = false;
                }
            }

            return CheckOutSuccess;
        }
        public bool InsertToPaymentDetail(int SCOPE_IDENTITY, CartItem itemCart)
        {
            connect.openConnection();
            int numberOfRows = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"insert into ChiTietHoaDon (MaHD, MaSP, DonGia, SoLuong, ThanhTien, ThoiGianTao)
                                values (@paymentId, @productId, @price, @quantity, @total, GETDATE()) ";
                command.CommandText = query;
                command.Parameters.AddWithValue("@paymentId", SCOPE_IDENTITY);
                command.Parameters.AddWithValue("@productId", itemCart.IdProduct);
                command.Parameters.AddWithValue("@price", itemCart.Price);
                command.Parameters.AddWithValue("@quantity", itemCart.Quantity);
                command.Parameters.AddWithValue("@total", itemCart.Total);
                numberOfRows = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return numberOfRows > 0;
        }
        public bool UpdateProduct(CartItem itemCart)
        {
            connect.openConnection();
            int numberOfRows = 0;
            using (SqlCommand command = new SqlCommand())
            {
                command.Connection = connect.getConnecttion();
                command.CommandType = System.Data.CommandType.Text;
                string query = @"UPDATE SanPham
                                SET SoLuong = SoLuong - @quantity
                                WHERE MaSach = @productId";
                command.CommandText = query;
                command.Parameters.AddWithValue("@productId", itemCart.IdProduct);
                command.Parameters.AddWithValue("@quantity", itemCart.Quantity);
                numberOfRows = command.ExecuteNonQuery();
            }
            connect.closeConnection();
            return numberOfRows > 0;
        }
    }
}
