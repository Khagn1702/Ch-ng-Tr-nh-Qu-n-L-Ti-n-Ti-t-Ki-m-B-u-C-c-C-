using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using QuanLyTienTietKiemBuuCuc.Class;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmQuanLyTaiKhoan : Form
    {
        public frmQuanLyTaiKhoan()
        {
            InitializeComponent();
        }

        DataTable tblTaiKhoanKhachHang;

        private void frmQuanLyTaiKhoan_Load(object sender, EventArgs e)
        {
            string sql;
            sql = "SELECT * FROM tblBuuCuc";
            txtMaTaiKhoan.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            LoadDataGridView();
            Functions.FillCombo(sql, cboSoHieuBuuCucMoTaiKhoan, "SoHieuBuuCuc", "SoHieuBuuCucMoTaiKhoan");
            cboSoHieuBuuCucMoTaiKhoan.SelectedIndex = -1;
            ResetValues();
        }

        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tblTaiKhoanKhachHang";
            tblTaiKhoanKhachHang = Functions.GetDataToTable(sql); // Lấy dữ liệu từ cơ sở dữ liệu
            dgvTaiKhoan.DataSource = tblTaiKhoanKhachHang;

            dgvTaiKhoan.Columns[0].HeaderText = "Mã Tài Khoản";
            dgvTaiKhoan.Columns[1].HeaderText = "Họ tên khách hàng";
            dgvTaiKhoan.Columns[2].HeaderText = "Địa chỉ khách hàng";
            dgvTaiKhoan.Columns[3].HeaderText = "Chứng Minh Nhân Dân";
            dgvTaiKhoan.Columns[4].HeaderText = "Bưu cục mở tài khoản";
            dgvTaiKhoan.Columns[5].HeaderText = "Ngày mở tài khoản";

            dgvTaiKhoan.AllowUserToAddRows = false;
            dgvTaiKhoan.EditMode = DataGridViewEditMode.EditProgrammatically;
        }
        private void dgvTaiKhoan_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (btnThem.Enabled == false)
            {
                MessageBox.Show("Đang ở chế độ thêm mới!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtMaTaiKhoan.Focus();
                return;
            }
            if (tblTaiKhoanKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không có dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            txtMaTaiKhoan.Text = dgvTaiKhoan.CurrentRow.Cells["MaTaiKhoan"].Value.ToString();
            txtHoTenKhachHang.Text = dgvTaiKhoan.CurrentRow.Cells["HoTenKhachHang"].Value.ToString();
            txtDiaChiKhachHang.Text = dgvTaiKhoan.CurrentRow.Cells["DiaChiKhachHang"].Value.ToString();
            txtChungMinhNhanDan.Text = dgvTaiKhoan.CurrentRow.Cells["ChungMinhNhanDan"].Value.ToString();
            cboSoHieuBuuCucMoTaiKhoan.Text = dgvTaiKhoan.CurrentRow.Cells["SoHieuBuuCuc"].Value.ToString();
            mskNgayMoTaiKhoan.Text = dgvTaiKhoan.CurrentRow.Cells["NgayMoTaiKhoan"].Value.ToString();

            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnBoqua.Enabled = true;
        }

        private void ResetValues()
        {
            txtMaTaiKhoan.Text = "";
            txtHoTenKhachHang.Text = "";
            txtDiaChiKhachHang.Text = "";
            txtChungMinhNhanDan.Text = "";
            cboSoHieuBuuCucMoTaiKhoan.Text = "";
            mskNgayMoTaiKhoan.Text = "";
        }



        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtMaTaiKhoan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập mã tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtMaTaiKhoan.Focus();
                return;
            }
            if (txtHoTenKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập họ tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTenKhachHang.Focus();
                return;
            }
            if (txtDiaChiKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiKhachHang.Focus();
                return;
            }
            if (txtChungMinhNhanDan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập CMND", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtChungMinhNhanDan.Focus();
                return;
            }
            if (cboSoHieuBuuCucMoTaiKhoan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải chọn bưu cục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                cboSoHieuBuuCucMoTaiKhoan.Focus();
                return;
            }
            if (mskNgayMoTaiKhoan.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập ngày mở tài khoản", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mskNgayMoTaiKhoan.Focus();
                return;
            }

            sql = "INSERT INTO tblTaiKhoan VALUES (N'" + txtMaTaiKhoan.Text.Trim() + "', N'" + txtHoTenKhachHang.Text.Trim() + "', N'" + txtDiaChiKhachHang.Text.Trim() + "', '" + txtChungMinhNhanDan.Text.Trim() + "', N'" + cboSoHieuBuuCucMoTaiKhoan.Text.Trim() + "', '" + mskNgayMoTaiKhoan.Text.Trim() + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtMaTaiKhoan.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblTaiKhoanKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaTaiKhoan.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtHoTenKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập họ tên khách hàng", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtHoTenKhachHang.Focus();
                return;
            }
            if (txtDiaChiKhachHang.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiKhachHang.Focus();
                return;
            }
            sql = "UPDATE tblTaiKhoan SET HoTenKhachHang=N'" + txtHoTenKhachHang.Text.Trim().ToString() + "', DiaChiKhachHang=N'" + txtDiaChiKhachHang.Text.Trim().ToString() + "', ChungMinhNhanDan='" + txtChungMinhNhanDan.Text.Trim().ToString() + "', SoHieuBuuCuc=N'" + cboSoHieuBuuCucMoTaiKhoan.Text.Trim().ToString() + "', NgayMoTaiKhoan='" + mskNgayMoTaiKhoan.Text.Trim().ToString() + "' WHERE MaTaiKhoan=N'" + txtMaTaiKhoan.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblTaiKhoanKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtMaTaiKhoan.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblTaiKhoan WHERE MaTaiKhoan=N'" + txtMaTaiKhoan.Text + "'";
                Functions.RunSqlDel(sql);
                LoadDataGridView();
                ResetValues();
            }
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            btnBoqua.Enabled = false;
            btnThem.Enabled = true;
            btnXoa.Enabled = true;
            btnSua.Enabled = true;
            btnLuu.Enabled = false;
            txtMaTaiKhoan.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtMaTaiKhoan.Enabled = true;
            txtMaTaiKhoan.Focus();
        }

        private void btnXoa_Click_1(object sender, EventArgs e)
        {

        }
    }
}
