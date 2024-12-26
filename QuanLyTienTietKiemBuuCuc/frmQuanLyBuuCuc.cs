using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyTienTietKiemBuuCuc.Class;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmQuanLyBuuCuc : Form
    {
        private DataTable tblBuuCuc;

        public frmQuanLyBuuCuc()
        {
            InitializeComponent();
        }

        private void frmQuanLyBuuCuc_Load(object sender, EventArgs e)
        {
            Functions.Connect(false); // Kết nối CSDL
            txtSoHieuBuuCuc.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT * FROM tblBuuCuc";
            tblBuuCuc = Functions.GetDataToTable(sql);
            dgvBuuCuc.DataSource = tblBuuCuc;

            dgvBuuCuc.Columns[0].HeaderText = "Số hiệu bưu cục";
            dgvBuuCuc.Columns[1].HeaderText = "Tên bưu cục";
            dgvBuuCuc.Columns[2].HeaderText = "Địa chỉ";
            dgvBuuCuc.Columns[3].HeaderText = "Điện thoại";
            dgvBuuCuc.Columns[4].HeaderText = "Tỉnh thành";

            dgvBuuCuc.AllowUserToAddRows = false;
            dgvBuuCuc.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtSoHieuBuuCuc.Text = "";
            txtTenBuuCuc.Text = "";
            txtDiaChiBuuCuc.Text = "";
            mtbDienThoaiBuuCuc.Text = "";
            txtTinhThanhPho.Text = "";
        }

        private bool IsPhoneNumberValid(string phoneNumber)
        {
            // Kiểm tra số điện thoại phải bắt đầu bằng số 0 và có độ dài đúng 10 ký tự
            return phoneNumber.Length == 10 && phoneNumber.StartsWith("0") && long.TryParse(phoneNumber, out _);
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSoHieuBuuCuc.Text))
            {
                MessageBox.Show("Bạn phải nhập số hiệu bưu cục!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHieuBuuCuc.Focus();
                return;
            }

            if (!IsPhoneNumberValid(mtbDienThoaiBuuCuc.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Số điện thoại phải có 10 số và bắt đầu bằng số 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtbDienThoaiBuuCuc.Focus();
                return;
            }

            string sql = $"SELECT * FROM tblBuuCuc WHERE SoHieuBuuCuc = '{txtSoHieuBuuCuc.Text.Trim()}'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Số hiệu bưu cục đã tồn tại!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            sql = $"INSERT INTO tblBuuCuc VALUES ('{txtSoHieuBuuCuc.Text}', N'{txtTenBuuCuc.Text}', N'{txtDiaChiBuuCuc.Text}', '{mtbDienThoaiBuuCuc.Text}', N'{txtTinhThanhPho.Text}')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();

            // Thoát chế độ thêm và quay về trạng thái ban đầu
            btnSua.Enabled = btnXoa.Enabled = btnThem.Enabled = true;
            btnLuu.Enabled = btnBoqua.Enabled = false;
            txtSoHieuBuuCuc.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!IsPhoneNumberValid(mtbDienThoaiBuuCuc.Text))
            {
                MessageBox.Show("Số điện thoại không hợp lệ! Số điện thoại phải có 10 số và bắt đầu bằng số 0.", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                mtbDienThoaiBuuCuc.Focus();
                return;
            }

            string sql = $"UPDATE tblBuuCuc SET TenBuuCuc=N'{txtTenBuuCuc.Text}', DiaChiBuuCuc=N'{txtDiaChiBuuCuc.Text}', DienThoaiBuuCuc='{mtbDienThoaiBuuCuc.Text}', TinhThanhPho=N'{txtTinhThanhPho.Text}' WHERE SoHieuBuuCuc='{txtSoHieuBuuCuc.Text}'";
            Functions.RunSQL(sql);
            LoadDataGridView();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn chắc chắn muốn xóa bưu cục này?", "Xác nhận", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                try
                {
                    // Lệnh SQL để xóa bưu cục
                    string sql = $"DELETE FROM tblBuuCuc WHERE SoHieuBuuCuc='{txtSoHieuBuuCuc.Text}'";

                    // Thực hiện lệnh xóa
                    int result = Functions.RunSQL(sql); // Giả định hàm RunSQL trả về số dòng bị ảnh hưởng

                    if (result > 0) // Nếu có dòng bị xóa, thông báo thành công
                    {
                        LoadDataGridView();
                        MessageBox.Show("Xóa bưu cục thành công.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Không có dòng nào bị xóa, có thể là do bưu cục không tồn tại
                        MessageBox.Show("Không có bưu cục nào bị xóa. Vui lòng kiểm tra lại.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    // Kiểm tra mã lỗi ràng buộc khóa ngoại
                    if (ex.Number == 547) // 547 là mã lỗi khóa ngoại trong SQL Server
                    {
                        MessageBox.Show("Lỗi ràng buộc\nBạn không thể xóa bưu cục này vì dữ liệu đang được tham chiếu với các bảng khác.",
                                        "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Hiển thị lỗi khác (nếu có)
                        MessageBox.Show("Đã xảy ra lỗi: " + ex.Message, "Thông báo lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }


        private void btnDong_Click(object sender, EventArgs e)
        {
            Functions.Disconnect();
            this.Close();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnXoa.Enabled = btnSua.Enabled = true;
            btnBoqua.Enabled = btnLuu.Enabled = false;
            txtSoHieuBuuCuc.Enabled = false;
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnLuu.Enabled = true;
            btnBoqua.Enabled = true;
            ResetValues();
            txtSoHieuBuuCuc.Enabled = true;
            txtSoHieuBuuCuc.Focus();
        }

        private void dgvBuuCuc_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtSoHieuBuuCuc.Focus();
                return;
            }
            if (tblBuuCuc.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            // Hiển thị dữ liệu từ dòng được chọn lên các control
            txtSoHieuBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["SoHieuBuuCuc"].Value.ToString();
            txtTenBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["TenBuuCuc"].Value.ToString();
            txtDiaChiBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["DiaChiBuuCuc"].Value.ToString();
            mtbDienThoaiBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["DienThoaiBuuCuc"].Value.ToString();
            txtTinhThanhPho.Text = dgvBuuCuc.CurrentRow.Cells["TinhThanhPho"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
    }
}
