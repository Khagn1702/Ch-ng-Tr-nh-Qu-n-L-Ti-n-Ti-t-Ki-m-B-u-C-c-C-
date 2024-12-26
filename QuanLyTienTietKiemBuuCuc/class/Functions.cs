using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace QuanLyTienTietKiemBuuCuc.Class
{
    class Functions
    {
        public static SqlConnection Con;    //Khai báo đối tượng kết nối

        // Phương thức kết nối với tùy chọn hiển thị thông báo
        public static void Connect(bool showMessage = true)
        {
            if (Con == null)
            {
                Con = new SqlConnection();
            }

            if (Con.State == ConnectionState.Open)
            {
                Con.Close(); // Đóng kết nối cũ trước khi mở kết nối mới
            }

            Con.ConnectionString = "Data source=.\\SQLEXPRESS; Initial Catalog=QL_TienTietKiem; Integrated security=True";

            try
            {
                Con.Open();
                if (showMessage)
                {
                    MessageBox.Show("Kết nối thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Không thể kết nối với dữ liệu: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        public static void Disconnect()
        {
            if (Con != null && Con.State == ConnectionState.Open)
            {
                Con.Close();
                Con.Dispose();
                Con = null;
            }
        }

        // Lấy dữ liệu vào bảng
        public static DataTable GetDataToTable(string sql)
        {
            if (Con == null || Con.State == ConnectionState.Closed)
            {
                Connect(false); // Đảm bảo kết nối mở, không hiển thị thông báo
            }

            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();

            try
            {
                dap.Fill(table);
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy dữ liệu: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            return table;
        }

        public static bool CheckKey(string sql)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            return table.Rows.Count > 0;
        }

        public static int RunSQL(string sql)
        {
            SqlCommand cmd = new SqlCommand(sql, Con);
            int rowsAffected = 0;
            try
            {
                rowsAffected = cmd.ExecuteNonQuery(); // Thực hiện lệnh SQL và trả về số dòng bị ảnh hưởng
            }
            catch (SqlException ex)
            {
                throw ex; // Ném lại ngoại lệ để xử lý bên ngoài
            }
            return rowsAffected;
        }


        public static void RunSqlDel(string sql, SqlParameter[] parameters = null)
        {
            SqlCommand cmd = new SqlCommand(sql, Con);
            if (parameters != null)
            {
                cmd.Parameters.AddRange(parameters);
            }

            try
            {
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi thực thi câu lệnh SQL: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                cmd.Dispose();
            }
        }

        public static string GetFieldValues(string sql)
        {
            string value = "";
            SqlCommand cmd = new SqlCommand(sql, Con);
            SqlDataReader reader = null;

            try
            {
                reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    value = reader.GetValue(0).ToString();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi khi lấy giá trị: {ex.Message}", "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                reader?.Close();
                cmd.Dispose();
            }

            return value;
        }

        public static void FillCombo(string sql, ComboBox cbo, string ma, string ten)
        {
            SqlDataAdapter dap = new SqlDataAdapter(sql, Con);
            DataTable table = new DataTable();
            dap.Fill(table);
            cbo.DataSource = table;
            cbo.ValueMember = ma;
            cbo.DisplayMember = ten;
        }

        // Hàm thực thi SQL với tham số hóa
        public static int RunSQLWithParams(string sql, SqlParameter[] parameters)
        {
            // Đảm bảo kết nối đã mở
            if (Con == null || Con.State != ConnectionState.Open)
            {
                Connect(false); // Kết nối lại nếu cần
            }

            using (SqlCommand cmd = new SqlCommand(sql, Con))
            {
                if (parameters != null)
                {
                    cmd.Parameters.AddRange(parameters); // Thêm các tham số
                }

                try
                {
                    // Thực thi lệnh SQL và trả về số dòng bị ảnh hưởng
                    return cmd.ExecuteNonQuery();
                }
                catch
                {
                    throw; // Ném ngoại lệ ra để xử lý bên ngoài
                }
            }
        }


        public static bool IsDate(string date)
        {
            string[] elements = date.Split('/');
            return (Convert.ToInt32(elements[0]) >= 1 && Convert.ToInt32(elements[0]) <= 31) &&
                   (Convert.ToInt32(elements[1]) >= 1 && Convert.ToInt32(elements[1]) <= 12) &&
                   (Convert.ToInt32(elements[2]) >= 1900);
        }

        public static string ConvertDateTime(string date)
        {
            string[] elements = date.Split('/');
            return $"{elements[0]}/{elements[1]}/{elements[2]}";
        }
    }
}
