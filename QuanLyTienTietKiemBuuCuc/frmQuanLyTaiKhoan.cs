using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;
using QuanLyTienTietKiemBuuCuc.Class;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        DataTable tblTaiKhoan;

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblBuuCuc";
            txtMaTaiKhoan.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;

            LoadDataGridView();
            Functions.FillCombo(sql, cboSoHieuBuuCucMoTaiKhoan, "SoHieuBuuCuc", "SoHieuBuuCuc");
            cboSoHieuBuuCucMoTaiKhoan.SelectedIndex = -1;
            ResetValues();

            dgvTaiKhoan.CellFormatting += dgvTaiKhoan_CellFormatting;
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT MaTaiKhoan, HoTenKhachHang, DiaChiKhachHang, ChungMinhNhanDan, SoHieuBuuCucMoTaiKhoan, NgayMoTaiKhoan FROM tblTaiKhoan";
            tblTaiKhoan = Functions.GetDataToTable(sql);
            dgvTaiKhoan.DataSource = tblTaiKhoan;

            dgvTaiKhoan.Columns[0].HeaderText = "Mã Tài Khoản";
            dgvTaiKhoan.Columns[1].HeaderText = "Họ tên khách hàng";
            dgvTaiKhoan.Columns[2].HeaderText = "Địa chỉ khách hàng";
            dgvTaiKhoan.Columns[3].HeaderText = "CMND";
            dgvTaiKhoan.Columns[4].HeaderText = "Bưu cục";
            dgvTaiKhoan.Columns[5].HeaderText = "Ngày mở";

            dgvTaiKhoan.AllowUserToAddRows = false;
            dgvTaiKhoan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaTaiKhoan.Clear();
            txtHoTenKhachHang.Clear();
            txtDiaChiKhachHang.Clear();
            txtChungMinhNhanDan.Clear();
            cboSoHieuBuuCucMoTaiKhoan.SelectedIndex = -1;
            mskNgayMoTaiKhoan.Clear();
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtMaTaiKhoan.Text))
            {
                MessageBox.Show("Bạn phải nhập mã tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTaiKhoan.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtHoTenKhachHang.Text))
            {
                MessageBox.Show("Bạn phải nhập họ tên khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTenKhachHang.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtDiaChiKhachHang.Text))
            {
                MessageBox.Show("Bạn phải nhập địa chỉ khách hàng!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiKhachHang.Focus();
                return false;
            }
            if (string.IsNullOrWhiteSpace(txtChungMinhNhanDan.Text))
            {
                MessageBox.Show("Bạn phải nhập CMND!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChungMinhNhanDan.Focus();
                return false;
            }
            if (cboSoHieuBuuCucMoTaiKhoan.SelectedIndex == -1)
            {
                MessageBox.Show("Bạn phải chọn bưu cục mở tài khoản!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSoHieuBuuCucMoTaiKhoan.Focus();
                return false;
            }

            if (!DateTime.TryParseExact(mskNgayMoTaiKhoan.Text, "dd/MM/yyyy", null, System.Globalization.DateTimeStyles.None, out _))
            {
                MessageBox.Show("Ngày mở tài khoản phải đúng định dạng dd/MM/yyyy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayMoTaiKhoan.Focus();
                return false;
            }

            return true;
        }

        private void dgvTaiKhoan_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvTaiKhoan.Columns[e.ColumnIndex].Name == "NgayMoTaiKhoan" && e.Value != null)
            {
                if (DateTime.TryParse(e.Value.ToString(), out DateTime dateValue))
                {
                    e.Value = dateValue.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
            }
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            DateTime ngayMo = DateTime.ParseExact(mskNgayMoTaiKhoan.Text, "dd/MM/yyyy", null);
            string ngayMoSql = ngayMo.ToString("yyyy-MM-dd");

            string sql = "UPDATE tblTaiKhoan SET HoTenKhachHang=@HoTen, DiaChiKhachHang=@DiaChi, ChungMinhNhanDan=@CMND, SoHieuBuuCucMoTaiKhoan=@BuuCuc, NgayMoTaiKhoan=@NgayMo WHERE MaTaiKhoan=@MaTaiKhoan";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTaiKhoan", txtMaTaiKhoan.Text),
                new SqlParameter("@HoTen", txtHoTenKhachHang.Text),
                new SqlParameter("@DiaChi", txtDiaChiKhachHang.Text),
                new SqlParameter("@CMND", txtChungMinhNhanDan.Text),
                new SqlParameter("@BuuCuc", cboSoHieuBuuCucMoTaiKhoan.Text),
                new SqlParameter("@NgayMo", ngayMoSql)
            };

            Functions.RunSQLWithParams(sql, parameters);
            LoadDataGridView();
            ResetValues();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Câu lệnh SQL để xóa tài khoản
                    string sql = "DELETE FROM tblTaiKhoan WHERE MaTaiKhoan=@MaTaiKhoan";
                    SqlParameter param = new SqlParameter("@MaTaiKhoan", txtMaTaiKhoan.Text);

                    // Thực thi câu lệnh SQL
                    int rowsAffected = Functions.RunSQLWithParams(sql, new SqlParameter[] { param });

                    if (rowsAffected > 0)
                    {
                        // Nếu có dòng bị xóa
                        LoadDataGridView();
                        ResetValues();
                        MessageBox.Show("Xóa tài khoản thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Không có dòng nào bị xóa
                        MessageBox.Show("Không có tài khoản nào bị xóa. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (SqlException ex)
                {
                    // Kiểm tra lỗi ràng buộc khóa ngoại
                    if (ex.Number == 547)
                    {
                        // Hiển thị thông báo tùy chỉnh với "Lỗi ràng buộc" ở trên cùng
                        MessageBox.Show("Lỗi ràng buộc:\n\nBạn không thể xóa tài khoản này vì dữ liệu đang được tham chiếu với các bảng khác.",
                            "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    else
                    {
                        // Hiển thị lỗi khác
                        MessageBox.Show($"Đã xảy ra lỗi: {ex.Message}", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
            }
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnThem.Enabled = true;
            btnSua.Enabled = btnXoa.Enabled = true;
            btnBoqua.Enabled = btnLuu.Enabled = false;
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (!ValidateInput()) return;

            DateTime ngayMo = DateTime.ParseExact(mskNgayMoTaiKhoan.Text, "dd/MM/yyyy", null);
            string ngayMoSql = ngayMo.ToString("yyyy-MM-dd");

            string sql = "INSERT INTO tblTaiKhoan VALUES (@MaTaiKhoan, @HoTen, @DiaChi, @CMND, @BuuCuc, @NgayMo)";
            SqlParameter[] parameters =
            {
                new SqlParameter("@MaTaiKhoan", txtMaTaiKhoan.Text),
                new SqlParameter("@HoTen", txtHoTenKhachHang.Text),
                new SqlParameter("@DiaChi", txtDiaChiKhachHang.Text),
                new SqlParameter("@CMND", txtChungMinhNhanDan.Text),
                new SqlParameter("@BuuCuc", cboSoHieuBuuCucMoTaiKhoan.Text),
                new SqlParameter("@NgayMo", ngayMoSql)
            };

            Functions.RunSQLWithParams(sql, parameters);
            LoadDataGridView();
            ResetValues();

            btnThem.Enabled = btnSua.Enabled = btnXoa.Enabled = true;
            btnBoqua.Enabled = btnLuu.Enabled = false;
            txtMaTaiKhoan.Enabled = false;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            btnSua.Enabled = btnXoa.Enabled = false;
            btnBoqua.Enabled = btnLuu.Enabled = true;
            btnThem.Enabled = false;

            ResetValues();
            txtMaTaiKhoan.Enabled = true;
            txtMaTaiKhoan.Focus();
        }

        private void dgvTaiKhoan_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTaiKhoan.Focus();
                return;
            }
            if (tblTaiKhoan.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            // Hiển thị dữ liệu từ dòng được chọn lên các control
            txtMaTaiKhoan.Text = dgvTaiKhoan.CurrentRow.Cells["MaTaiKhoan"].Value.ToString();
            txtHoTenKhachHang.Text = dgvTaiKhoan.CurrentRow.Cells["HoTenKhachHang"].Value.ToString();
            txtDiaChiKhachHang.Text = dgvTaiKhoan.CurrentRow.Cells["DiaChiKhachHang"].Value.ToString();
            txtChungMinhNhanDan.Text = dgvTaiKhoan.CurrentRow.Cells["ChungMinhNhanDan"].Value.ToString();
            cboSoHieuBuuCucMoTaiKhoan.Text = dgvTaiKhoan.CurrentRow.Cells["SoHieuBuuCucMoTaiKhoan"].Value.ToString();

            // Định dạng lại ngày để hiển thị trong MaskedTextBox
            DateTime ngayMo;
            if (DateTime.TryParse(dgvTaiKhoan.CurrentRow.Cells["NgayMoTaiKhoan"].Value.ToString(), out ngayMo))
            {
                mskNgayMoTaiKhoan.Text = ngayMo.ToString("dd/MM/yyyy");
            }
            else
            {
                mskNgayMoTaiKhoan.Clear();
            }

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
        }
    }
}
