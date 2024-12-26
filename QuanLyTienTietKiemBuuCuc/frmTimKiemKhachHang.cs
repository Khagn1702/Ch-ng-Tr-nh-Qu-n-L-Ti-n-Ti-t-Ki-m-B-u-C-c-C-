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
    public partial class frmTimKiemKhachHang : Form
    {
        public frmTimKiemKhachHang()
        {
            InitializeComponent();
        }

        DataTable tblTimKiemKhachHang;

        private void frmTimKiemKhachHang_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
        }

        private void LoadDataGridView()
        {
            string sql = "SELECT * FROM tblTaiKhoan";
            tblTimKiemKhachHang = Functions.GetDataToTable(sql);
            dgvTimKiemKhachHang.DataSource = tblTimKiemKhachHang;

            dgvTimKiemKhachHang.Columns[0].HeaderText = "Mã Tài Khoản";
            dgvTimKiemKhachHang.Columns[1].HeaderText = "Họ Tên Khách Hàng";
            dgvTimKiemKhachHang.Columns[2].HeaderText = "Địa Chỉ";
            dgvTimKiemKhachHang.Columns[3].HeaderText = "CMND";
            dgvTimKiemKhachHang.Columns[4].HeaderText = "Bưu Cục Mở Tài Khoản";
            dgvTimKiemKhachHang.Columns[5].HeaderText = "Ngày Mở Tài Khoản";

            dgvTimKiemKhachHang.AllowUserToAddRows = false;
            dgvTimKiemKhachHang.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void ResetValues()
        {
            txtMaTaiKhoan.Text = "";
            txtTenKhachHang.Text = "";
            txtCMND.Text = "";
        }

        private void btnTimKiem_Click(object sender, EventArgs e)
        {
            string sql = "SELECT * FROM tblTaiKhoan WHERE 1=1";

            if (txtMaTaiKhoan.Text.Trim() != "")
            {
                sql += " AND MaTaiKhoan LIKE N'%" + txtMaTaiKhoan.Text.Trim() + "%'";
            }

            if (txtTenKhachHang.Text.Trim() != "")
            {
                sql += " AND HoTenKhachHang LIKE N'%" + txtTenKhachHang.Text.Trim() + "%'";
            }

            if (txtCMND.Text.Trim() != "")
            {
                sql += " AND ChungMinhNhanDan LIKE '%" + txtCMND.Text.Trim() + "%'";
            }

            tblTimKiemKhachHang = Functions.GetDataToTable(sql);
            if (tblTimKiemKhachHang.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy bản ghi nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã tìm thấy " + tblTimKiemKhachHang.Rows.Count + " bản ghi.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            dgvTimKiemKhachHang.DataSource = tblTimKiemKhachHang;
        }

        private void btnBoqua_Click(object sender, EventArgs e)
        {
            ResetValues();
            LoadDataGridView();
        }

        private void frmTimKiemKhachHang_Load_1(object sender, EventArgs e)
        {

        }

        private void btnDong_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }
    }
}
