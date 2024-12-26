using System;
using System.Data;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyTienTietKiemBuuCuc.Class;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmTimKiemGiaoDich : Form
    {
        DataTable tblTimKiemGiaoDich;

        public frmTimKiemGiaoDich()
        {
            InitializeComponent();
        }

        private void frmTimKiemGiaoDich_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
            LoadComboBoxSoHieuBuuCuc(); // Nạp dữ liệu cho ComboBox SoHieuBuuCuc
            LoadComboBoxHinhThucGiaoDich(); // Nạp dữ liệu cho ComboBox HinhThucGiaoDich
        }

        private void ResetValues()
        {
            txtMaTaiKhoan.Text = "";
            cboSoHieuBuuCuc.SelectedIndex = -1;
            mskNgayGiaoDich.Text = "";
            cboHinhThucGiaoDich.SelectedIndex = -1; // Reset ComboBox HinhThucGiaoDich
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT SoThuTuGiaoDich, MaTaiKhoan, SoHieuBuuCucGiaoDich, NgayGiaoDich, SoTienGiaoDich, HinhThucGiaoDich FROM tblGiaoDichKhachHang";
            tblTimKiemGiaoDich = Functions.GetDataToTable(sql);
            dgvTimKiemGiaoDich.DataSource = tblTimKiemGiaoDich;

            // Đặt tiêu đề các cột
            dgvTimKiemGiaoDich.Columns[0].HeaderText = "Số Thứ Tự Giao Dịch";
            dgvTimKiemGiaoDich.Columns[1].HeaderText = "Mã Tài Khoản";
            dgvTimKiemGiaoDich.Columns[2].HeaderText = "Số Hiệu Bưu Cục";
            dgvTimKiemGiaoDich.Columns[3].HeaderText = "Ngày Giao Dịch";
            dgvTimKiemGiaoDich.Columns[4].HeaderText = "Số Tiền Giao Dịch";
            dgvTimKiemGiaoDich.Columns[5].HeaderText = "Hình Thức Giao Dịch";

            dgvTimKiemGiaoDich.AllowUserToAddRows = false;
            dgvTimKiemGiaoDich.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        // Nạp dữ liệu cho ComboBox SoHieuBuuCuc
        private void LoadComboBoxSoHieuBuuCuc()
        {
            string sql = "SELECT SoHieuBuuCuc FROM tblBuuCuc"; // Truy vấn lấy Số Hiệu Bưu Cục
            DataTable tblSoHieuBuuCuc = Functions.GetDataToTable(sql);

            cboSoHieuBuuCuc.Items.Clear(); // Xóa tất cả các mục cũ trong ComboBox
            foreach (DataRow row in tblSoHieuBuuCuc.Rows)
            {
                cboSoHieuBuuCuc.Items.Add(row["SoHieuBuuCuc"].ToString()); // Thêm mỗi Số Hiệu Bưu Cục vào ComboBox
            }
        }

        // Nạp dữ liệu cho ComboBox HinhThucGiaoDich
        private void LoadComboBoxHinhThucGiaoDich()
        {
            // Giả sử chỉ có hai giá trị "Gui" và "Rut" cho Hình Thức Giao Dịch
            cboHinhThucGiaoDich.Items.Clear(); // Xóa tất cả các mục cũ trong ComboBox
            cboHinhThucGiaoDich.Items.Add("Gui");
            cboHinhThucGiaoDich.Items.Add("Rut");
            cboHinhThucGiaoDich.SelectedIndex = -1; // Đặt chỉ số mặc định là -1 (chưa chọn gì)
        }

        // Xử lý sự kiện khi bấm nút Tìm Kiếm
        private void btnTimKiem_Click_1(object sender, EventArgs e)
        {
            string sql = "SELECT SoThuTuGiaoDich, MaTaiKhoan, SoHieuBuuCucGiaoDich, NgayGiaoDich, SoTienGiaoDich, HinhThucGiaoDich FROM tblGiaoDichKhachHang WHERE 1=1";

            // Điều kiện tìm kiếm theo Mã Tài Khoản
            if (!string.IsNullOrEmpty(txtMaTaiKhoan.Text))
            {
                sql += " AND MaTaiKhoan LIKE N'%" + txtMaTaiKhoan.Text.Trim() + "%'";
            }

            // Điều kiện tìm kiếm theo Số Hiệu Bưu Cục
            if (cboSoHieuBuuCuc.SelectedIndex != -1)
            {
                sql += " AND SoHieuBuuCucGiaoDich = N'" + cboSoHieuBuuCuc.SelectedItem.ToString() + "'";
            }

            // Điều kiện tìm kiếm theo Ngày Giao Dịch
            if (!string.IsNullOrEmpty(mskNgayGiaoDich.Text.Trim()))
            {
                sql += " AND CONVERT(VARCHAR, NgayGiaoDich, 103) = '" + mskNgayGiaoDich.Text.Trim() + "'";
            }

            // Điều kiện tìm kiếm theo Số Tiền Giao Dịch
            if (!string.IsNullOrEmpty(txtSoTienGiaoDich.Text.Trim()))
            {
                if (decimal.TryParse(txtSoTienGiaoDich.Text.Trim(), out decimal soTien))
                {
                    sql += " AND SoTienGiaoDich = " + soTien;
                }
                else
                {
                    MessageBox.Show("Số tiền giao dịch không hợp lệ.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }
            }

            // Điều kiện tìm kiếm theo Hình Thức Giao Dịch
            if (cboHinhThucGiaoDich.SelectedIndex != -1)
            {
                sql += " AND HinhThucGiaoDich = N'" + cboHinhThucGiaoDich.SelectedItem.ToString() + "'";
            }

            // Sắp xếp kết quả theo Ngày Giao Dịch và Số Tiền Giao Dịch
            sql += " ORDER BY NgayGiaoDich ASC, SoTienGiaoDich DESC";

            tblTimKiemGiaoDich = Functions.GetDataToTable(sql);
            if (tblTimKiemGiaoDich.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy bản ghi nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã tìm thấy " + tblTimKiemGiaoDich.Rows.Count + " bản ghi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvTimKiemGiaoDich.DataSource = tblTimKiemGiaoDich;
        }

        // Xử lý sự kiện khi bấm nút Làm Mới
        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
        }

        // Xử lý sự kiện khi bấm nút Đóng
        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void dgvTimKiemGiaoDich_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
