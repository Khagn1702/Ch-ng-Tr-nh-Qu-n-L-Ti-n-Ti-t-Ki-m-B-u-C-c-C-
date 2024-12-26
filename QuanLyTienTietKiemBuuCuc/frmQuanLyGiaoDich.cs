using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;
using QuanLyTienTietKiemBuuCuc.Class;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmQuanLyGiaoDich : Form
    {
        public frmQuanLyGiaoDich()
        {
            InitializeComponent();
        }

        DataTable tblGiaoDich;

        private void frmQuanLyGiaoDich_Load(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblGiaoDichKhachHang";
            cboMaTaiKhoan.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;

            LoadDataGridView();
            Functions.FillCombo(sql, cboMaBuuCuc, "SoHieuBuuCucGiaoDich", "SoHieuBuuCucGiaoDich");
            cboMaBuuCuc.SelectedIndex = -1;
            LoadComboBoxData();
            ResetValues();
           
            mskNgayGiaoDich.Mask = "00/00/0000";
            mskNgayGiaoDich.PromptChar = '_';

            dgvQuanLyGiaoDich.CellFormatting += dgvQuanLyGiaoDich_CellFormatting_1;
            dgvQuanLyGiaoDich.CellEndEdit += dgvQuanLyGiaoDich_CellEndEdit;

        }

        private void LoadComboBoxData()
        {
            // Load dữ liệu cho ComboBox Mã Tài Khoản
            string sqlMaTaiKhoan = "SELECT MaTaiKhoan FROM tblTaiKhoan";
            Functions.FillCombo(sqlMaTaiKhoan, cboMaTaiKhoan, "MaTaiKhoan", "MaTaiKhoan");
            cboMaTaiKhoan.SelectedIndex = -1;

            // Load dữ liệu cho ComboBox Số Hiệu Bưu Cục
            string sqlBuuCuc = "SELECT SoHieuBuuCuc FROM tblBuuCuc";
            Functions.FillCombo(sqlBuuCuc, cboMaBuuCuc, "SoHieuBuuCuc", "SoHieuBuuCuc");
            cboMaBuuCuc.SelectedIndex = -1;

            // Loại giao dịch (Rút/Gửi)
            cboLoaiGiaoDich.Items.AddRange(new object[] { "Rut", "Gui" });
            cboLoaiGiaoDich.SelectedIndex = -1;
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT MaTaiKhoan, SoHieuBuuCucGiaoDich, FORMAT(NgayGiaoDich, 'dd/MM/yyyy') AS NgayGiaoDich, HinhThucGiaoDich, SoTienGiaoDich FROM tblGiaoDichKhachHang";
            tblGiaoDich = Functions.GetDataToTable(sql);
            dgvQuanLyGiaoDich.DataSource = tblGiaoDich;

            dgvQuanLyGiaoDich.Columns[0].HeaderText = "Mã Tài Khoản";
            dgvQuanLyGiaoDich.Columns[1].HeaderText = "Số Hiệu Bưu Cục";
            dgvQuanLyGiaoDich.Columns[2].HeaderText = "Ngày Giao Dịch";
            dgvQuanLyGiaoDich.Columns[3].HeaderText = "Hình Thức";
            dgvQuanLyGiaoDich.Columns[4].HeaderText = "Số Tiền";

            // Thêm định dạng ngày cho cột Ngày Giao Dịch
            dgvQuanLyGiaoDich.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";

            dgvQuanLyGiaoDich.AllowUserToAddRows = false;
            dgvQuanLyGiaoDich.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            cboMaTaiKhoan.SelectedIndex = -1;
            cboMaBuuCuc.SelectedIndex = -1;
            cboLoaiGiaoDich.SelectedIndex = -1;
            mskNgayGiaoDich.Clear();
            txtSoTien.Clear();
        }

        private void SetControlsState(bool editing)
        {
            btnThem.Enabled = !editing;
            btnSua.Enabled = !editing;
            btnXoa.Enabled = !editing;
            btnLuu.Enabled = editing;
            btnBoqua.Enabled = editing;

            cboMaTaiKhoan.Enabled = editing;
        }

        private void btnThem_Click(object sender, EventArgs e)
        {
            ResetValues();
            SetControlsState(true);
            cboMaTaiKhoan.Focus();
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            if (dgvQuanLyGiaoDich.CurrentRow == null || string.IsNullOrEmpty(cboMaTaiKhoan.Text))
            {
                MessageBox.Show("Bạn chưa chọn giao dịch nào!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (MessageBox.Show("Bạn có chắc chắn muốn cập nhật giao dịch này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Kiểm tra các giá trị nhập vào
                    if (cboMaBuuCuc.SelectedIndex == -1 || cboLoaiGiaoDich.SelectedIndex == -1 ||
                        !DateTime.TryParse(mskNgayGiaoDich.Text, out DateTime ngayGiaoDich) ||
                        !decimal.TryParse(txtSoTien.Text, out decimal soTien))
                    {
                        MessageBox.Show("Vui lòng nhập đầy đủ và chính xác các thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }

                    // Câu lệnh SQL để cập nhật giao dịch
                    string sql = "UPDATE tblGiaoDichKhachHang " +
                                 "SET SoHieuBuuCucGiaoDich = @SoHieuBuuCucGiaoDich, " +
                                 "NgayGiaoDich = @NgayGiaoDich, " +
                                 "HinhThucGiaoDich = @HinhThucGiaoDich, " +
                                 "SoTienGiaoDich = @SoTienGiaoDich " +
                                 "WHERE MaTaiKhoan = @MaTaiKhoan";

                    // Tạo danh sách tham số
                    SqlParameter[] parameters = new SqlParameter[]
                    {
                new SqlParameter("@MaTaiKhoan", cboMaTaiKhoan.Text),
                new SqlParameter("@SoHieuBuuCucGiaoDich", cboMaBuuCuc.SelectedValue),
                new SqlParameter("@NgayGiaoDich", ngayGiaoDich.ToString("yyyy-MM-dd")),
                new SqlParameter("@HinhThucGiaoDich", cboLoaiGiaoDich.Text),
                new SqlParameter("@SoTienGiaoDich", soTien)
                    };

                    // Thực thi câu lệnh SQL
                    int rowsAffected = Functions.RunSQLWithParams(sql, parameters);

                    if (rowsAffected > 0)
                    {
                        // Nếu cập nhật thành công
                        LoadDataGridView();
                        ResetValues();
                        MessageBox.Show("Cập nhật giao dịch thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    }
                    else
                    {
                        // Nếu không có dòng nào bị cập nhật
                        MessageBox.Show("Không có giao dịch nào được cập nhật. Vui lòng kiểm tra lại!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    }
                }
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Lỗi trong quá trình cập nhật: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnXoa_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có chắc chắn xóa không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                try
                {
                    // Câu lệnh SQL để xóa tài khoản
                    string sql = "DELETE FROM tblGiaoDichKhachHang WHERE MaTaiKhoan = @MaTaiKhoan";
                    SqlParameter param = new SqlParameter("@MaTaiKhoan", cboMaTaiKhoan.Text);

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
                catch (Exception ex)
                {
                    // Xử lý lỗi nếu có
                    MessageBox.Show("Lỗi trong quá trình xóa: " + ex.Message, "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }


        private void btnLuu_Click(object sender, EventArgs e)
        {
            if (cboMaTaiKhoan.SelectedIndex == -1 || cboMaBuuCuc.SelectedIndex == -1 || cboLoaiGiaoDich.SelectedIndex == -1 ||
       !DateTime.TryParse(mskNgayGiaoDich.Text, out DateTime ngayGiaoDich) || !decimal.TryParse(txtSoTien.Text, out decimal soTien))
            {
                MessageBox.Show("Bạn phải nhập đầy đủ và chính xác các thông tin!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            // Thêm dữ liệu vào cơ sở dữ liệu
            string sql = "INSERT INTO tblGiaoDichKhachHang (MaTaiKhoan, SoHieuBuuCucGiaoDich, NgayGiaoDich, HinhThucGiaoDich, SoTienGiaoDich) " +
                         "VALUES (N'" + cboMaTaiKhoan.SelectedValue + "', N'" + cboMaBuuCuc.SelectedValue + "', '" +
                         ngayGiaoDich.ToString("yyyy-MM-dd") + "', N'" + cboLoaiGiaoDich.Text + "', " + soTien + ")";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            SetControlsState(false);
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnBoqua_Click_1(object sender, EventArgs e)
        {
            ResetValues();
            SetControlsState(false);
        }

        private void dgvQuanLyGiaoDich_CellContentClick_1(object sender, DataGridViewCellEventArgs e)
        {
            if (tblGiaoDich.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            cboMaTaiKhoan.Text = dgvQuanLyGiaoDich.CurrentRow.Cells["MaTaiKhoan"].Value.ToString();
            cboMaBuuCuc.Text = dgvQuanLyGiaoDich.CurrentRow.Cells["SoHieuBuuCucGiaoDich"].Value.ToString();
            mskNgayGiaoDich.Text = dgvQuanLyGiaoDich.CurrentRow.Cells["NgayGiaoDich"].Value.ToString();
            cboLoaiGiaoDich.Text = dgvQuanLyGiaoDich.CurrentRow.Cells["HinhThucGiaoDich"].Value.ToString();
            txtSoTien.Text = dgvQuanLyGiaoDich.CurrentRow.Cells["SoTienGiaoDich"].Value.ToString();

            SetControlsState(false);
            // Định dạng lại ngày để hiển thị trong MaskedTextBox
            DateTime ngayMo;
            if (DateTime.TryParse(dgvQuanLyGiaoDich.CurrentRow.Cells["NgayGiaoDich"].Value.ToString(), out ngayMo))
            {
                mskNgayGiaoDich.Text = ngayMo.ToString("MM/dd/yyyy");
            }
            else
            {
                mskNgayGiaoDich.Clear();
            }
            dgvQuanLyGiaoDich.Columns[2].DefaultCellStyle.Format = "dd/MM/yyyy";
        }

        private void dgvQuanLyGiaoDich_CellFormatting_1(object sender, DataGridViewCellFormattingEventArgs e)
        {
            if (dgvQuanLyGiaoDich.Columns[e.ColumnIndex].Name == "NgayGiaoDich" && e.Value != null)
            {
                string dateString = e.Value.ToString();

                // Kiểm tra nếu giá trị ngày hợp lệ, thì định dạng lại
                if (DateTime.TryParse(dateString, out DateTime dateValue))
                {
                    e.Value = dateValue.ToString("dd/MM/yyyy");
                    e.FormattingApplied = true;
                }
                else
                {
                    // Nếu không hợp lệ, hiển thị giá trị lỗi
                    e.Value = "Lỗi ngày";
                }
            }
        }

        private void dgvQuanLyGiaoDich_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (dgvQuanLyGiaoDich.Columns[e.ColumnIndex].Name == "NgayGiaoDich")
            {
                string dateString = dgvQuanLyGiaoDich.Rows[e.RowIndex].Cells[e.ColumnIndex].Value?.ToString();
                if (DateTime.TryParse(dateString, out DateTime dateValue))
                {
                    dgvQuanLyGiaoDich.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = dateValue.ToString("dd/MM/yyyy");
                }
                else
                {
                    MessageBox.Show("Ngày không hợp lệ. Vui lòng nhập theo định dạng dd/MM/yyyy!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    dgvQuanLyGiaoDich.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = DBNull.Value;
                }
            }
        }
    }
}
