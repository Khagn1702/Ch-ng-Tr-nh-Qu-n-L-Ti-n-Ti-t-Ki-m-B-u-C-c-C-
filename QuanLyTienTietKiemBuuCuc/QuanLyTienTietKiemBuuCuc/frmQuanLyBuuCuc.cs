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
    public partial class frmQuanLyBuuCuc : Form
    {
        public frmQuanLyBuuCuc()
        {
            InitializeComponent();
        }

        DataTable tblBuuCuc;

        private void frmQuanLyBuuCuc_Load(object sender, EventArgs e)
        {
            txtSoHieuBuuCuc.Enabled = false;
            btnLuu.Enabled = false;
            btnBoqua.Enabled = false;
            LoadDataGridView();
        }

        public void LoadDataGridView()
        {
            string sql;
            sql = "SELECT * FROM tblBuuCuc";
            tblBuuCuc = Functions.GetDataToTable(sql); //lấy dữ liệu
            dgvBuuCuc.DataSource = tblBuuCuc;
            dgvBuuCuc.Columns[0].HeaderText = "Số hiệu bưu cục";
            dgvBuuCuc.Columns[1].HeaderText = "Tên bưu cục";
            dgvBuuCuc.Columns[2].HeaderText = "Địa chỉ bưu cục";
            dgvBuuCuc.Columns[3].HeaderText = "Điện thoại bưu cục";
            dgvBuuCuc.Columns[4].HeaderText = "Tỉnh thành Phố";
            dgvBuuCuc.Columns[0].Width = 100;
            dgvBuuCuc.Columns[1].Width = 150;
            dgvBuuCuc.Columns[2].Width = 150;
            dgvBuuCuc.Columns[3].Width = 100;
            dgvBuuCuc.Columns[4].Width = 100;
            dgvBuuCuc.AllowUserToAddRows = false;
            dgvBuuCuc.EditMode = DataGridViewEditMode.EditProgrammatically;
        }

        private void dgvBuuCuc_CellClick(object sender, DataGridViewCellEventArgs e)
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
            txtSoHieuBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["SoHieuBuuCuc"].Value.ToString();
            txtTenBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["TenBuuCuc"].Value.ToString();
            txtDiaChiBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["DiaChiBuuCuc"].Value.ToString();
            mtbDienThoaiBuuCuc.Text = dgvBuuCuc.CurrentRow.Cells["DienThoaiBuuCuc"].Value.ToString();
            txtTinhThanhPho.Text = dgvBuuCuc.CurrentRow.Cells["TinhThanhPho"].Value.ToString();
            btnSua.Enabled = true;
            btnXoa.Enabled = true;
            btnXoa.Enabled = true;
        }
        private void btnThem_Click_1(object sender, EventArgs e)
        {
            btnSua.Enabled = false;
            btnXoa.Enabled = false;
            btnBoqua.Enabled = true;
            btnLuu.Enabled = true;
            btnThem.Enabled = false;
            ResetValues();
            txtSoHieuBuuCuc.Enabled = true;
            txtSoHieuBuuCuc.Focus();
        }
        private void ResetValues()
        {
            txtSoHieuBuuCuc.Text = "";
            txtTenBuuCuc.Text = "";
            txtDiaChiBuuCuc.Text = "";
            mtbDienThoaiBuuCuc.Text = "";
            txtTinhThanhPho.Text = "";
            
        }

        private void btnLuu_Click(object sender, EventArgs e)
        {
            string sql;
            if (txtSoHieuBuuCuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập số hiệu bưu cục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHieuBuuCuc.Focus();
                return;
            }
            if (txtTenBuuCuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên bưu cục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenBuuCuc.Focus();
                return;
            }
            if (txtDiaChiBuuCuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiBuuCuc.Focus();
                return;
            }
            if (mtbDienThoaiBuuCuc.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoaiBuuCuc.Focus();
                return;
            }
            if (txtTinhThanhPho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTinhThanhPho.Focus();
                return;
            }
       
            sql = "SELECT SoHieuBuuCuc FROM tblBuuCuc WHERE SoHieuBuuCuc=N'" + txtSoHieuBuuCuc.Text.Trim() + "'";
            if (Functions.CheckKey(sql))
            {
                MessageBox.Show("Mã nhân viên này đã có, bạn phải nhập số khác", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSoHieuBuuCuc.Focus();
                txtSoHieuBuuCuc.Text = "";
                return;
            }
            sql = "INSERT INTO tblBuuCuc VALUES (N'" + txtSoHieuBuuCuc.Text.Trim() + "',N'" + txtTenBuuCuc.Text.Trim() + "',N'" + txtDiaChiBuuCuc.Text.Trim() + "','" + mtbDienThoaiBuuCuc.Text + "',N'" + txtTinhThanhPho.Text.Trim() + "')";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnXoa.Enabled = true;
            btnThem.Enabled = true;
            btnSua.Enabled = true;
            btnBoqua.Enabled = false;
            btnLuu.Enabled = false;
            txtSoHieuBuuCuc.Enabled = false;
        }

        private void btnSua_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblBuuCuc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSoHieuBuuCuc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtTenBuuCuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên bưu cục", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTenBuuCuc.Focus();
                return;
            }
            if (txtDiaChiBuuCuc.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập địa chỉ", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtDiaChiBuuCuc.Focus();
                return;
            }
            if (mtbDienThoaiBuuCuc.Text == "(   )     -")
            {
                MessageBox.Show("Bạn phải nhập điện thoại", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                mtbDienThoaiBuuCuc.Focus();
                return;
            }
            if (txtTinhThanhPho.Text.Trim().Length == 0)
            {
                MessageBox.Show("Bạn phải nhập tên tỉnh thành", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtTinhThanhPho.Focus();
                return;
            }
            sql = "UPDATE tblBuuCuc SET  TenBuuCuc=N'" + txtTenBuuCuc.Text.Trim().ToString() +
                    "',DiaChiBuuCuc=N'" + txtDiaChiBuuCuc.Text.Trim().ToString() +
                    "',DienThoaiBuuCuc='" + mtbDienThoaiBuuCuc.Text.ToString() +
                    "',TinhThanhPho=N'" + txtTinhThanhPho.Text.Trim().ToString() +
                    "' WHERE SoHieuBuuCuc=N'" + txtSoHieuBuuCuc.Text + "'";
            Functions.RunSQL(sql);
            LoadDataGridView();
            ResetValues();
            btnBoqua.Enabled = false;
        }

        private void btnXoa_Click(object sender, EventArgs e)
        {
            string sql;
            if (tblBuuCuc.Rows.Count == 0)
            {
                MessageBox.Show("Không còn dữ liệu!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (txtSoHieuBuuCuc.Text == "")
            {
                MessageBox.Show("Bạn chưa chọn bản ghi nào", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa không?", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question) == DialogResult.OK)
            {
                sql = "DELETE tblBuuCuc WHERE SoHieuBuuCuc=N'" + txtSoHieuBuuCuc.Text + "'";
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
            txtSoHieuBuuCuc.Enabled = false;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
