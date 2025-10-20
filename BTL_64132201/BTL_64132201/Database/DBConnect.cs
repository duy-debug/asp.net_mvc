using Microsoft.Data.SqlClient;

namespace BTL_64132201.Database
{
    public class DBConnect
    {
        SqlConnection connect = new SqlConnection("Data Source=LAPTOP-BTLNC1MT;Initial Catalog=Bookstore;Integrated Security=True;TrustServerCertificate=False;Encrypt=false");
        public SqlConnection getConnecttion()
        {
            return connect;
        }
        //Function để mở kết nối với database
        public void openConnection()
        {
            if (connect.State == System.Data.ConnectionState.Closed)
            {
                connect.Open();
            }
        }
        //Function để đóng kết nối với database
        public void closeConnection()
        {
            if (connect.State == System.Data.ConnectionState.Open)
            {
                connect.Close();
            }
        }
    }
}
