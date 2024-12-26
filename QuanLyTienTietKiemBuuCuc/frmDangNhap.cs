using System;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmDangNhap : Form
    {
        public bool IsLoggedIn { get; private set; } = false; // Kiểm tra nếu đã đăng nhập thành công
        public string UserRole { get; private set; } = string.Empty; // Lưu quyền của tài khoản

        public frmDangNhap()
        {
            InitializeComponent();
        }

        private void frmDangNhap_Load_1(object sender, EventArgs e)
        {
            // Thiết lập mật khẩu hiển thị dưới dạng ***
            txtMatKhau.PasswordChar = '*';
            chkShowPassword.Checked = false; // Mặc định không hiển thị mật khẩu
        }

        private void btnDangNhap_Click(object sender, EventArgs e)
        {
            string username = txtTenDangNhap.Text.Trim();
            string password = txtMatKhau.Text.Trim();

            // Kiểm tra tên đăng nhập và mật khẩu không trống
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
            {
                MessageBox.Show("Vui lòng nhập tên đăng nhập và mật khẩu!", "Thông báo");
                return;
            }

            try
            {
                // Câu lệnh SQL kiểm tra đăng nhập
                string sql = "SELECT VaiTro FROM NGUOIDUNG WHERE TenDangNhap = @UserName AND MatKhau = @Password";
                using (SqlCommand cmd = new SqlCommand(sql, Class.Functions.Con))
                {
                    cmd.Parameters.AddWithValue("@UserName", username);
                    cmd.Parameters.AddWithValue("@Password", password);

                    var result = cmd.ExecuteScalar();

                    if (result != null)
                    {
                        // Đăng nhập thành công
                        UserRole = result.ToString(); // Lưu vai trò
                        IsLoggedIn = true;

                        MessageBox.Show($"Đăng nhập thành công với quyền: {UserRole}!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        this.Close();
                    }
                    else
                    {
                        lblLoi.Text = "Tên đăng nhập hoặc mật khẩu không đúng!";
                        lblLoi.Visible = true;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Lỗi đăng nhập: {ex.Message}", "Thông báo");
            }
        }

        private void chkShowPassword_CheckedChanged(object sender, EventArgs e)
        {
            // Hiển thị hoặc ẩn mật khẩu
            txtMatKhau.PasswordChar = chkShowPassword.Checked ? '\0' : '*';
        }

        private void btnHuy_Click_1(object sender, EventArgs e)
        {
            // Đóng form đăng nhập khi bấm nút Hủy
            this.Close();
        }

    }
}
