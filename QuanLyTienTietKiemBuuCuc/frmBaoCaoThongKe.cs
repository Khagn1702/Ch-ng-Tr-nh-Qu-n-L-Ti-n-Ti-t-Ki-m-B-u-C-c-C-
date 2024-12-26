using System;
using System.Data;
using System.Windows.Forms;
using QuanLyTienTietKiemBuuCuc.Class;

namespace QuanLyTienTietKiemBuuCuc
{
    public partial class frmBaoCaoThongKe : Form
    {
        DataTable tblBaoCao;

        public frmBaoCaoThongKe()
        {
            InitializeComponent();
        }

        private void frmBaoCaoThongKe_Load(object sender, EventArgs e)
        {
            ResetValues();
            LoadComboBoxHinhThucGiaoDich();
        }

        private void ResetValues()
        {
            cboHinhThucGiaoDich.SelectedIndex = -1;
            mskNgayBatDau.Text = "";
            mskNgayKetThuc.Text = "";
            lblTongSoGiaoDich.Text = "0";
            lblTongSoTien.Text = "0";
        }

        private void LoadComboBoxHinhThucGiaoDich()
        {
            cboHinhThucGiaoDich.Items.Clear();
            cboHinhThucGiaoDich.Items.Add("Gui");
            cboHinhThucGiaoDich.Items.Add("Rut");
            cboHinhThucGiaoDich.SelectedIndex = -1;
        }

        private decimal TinhTongSoTien(DataTable dataTable)
        {
            decimal tongSoTien = 0;
            foreach (DataRow row in dataTable.Rows)
            {
                tongSoTien += Convert.ToDecimal(row["SoTienGiaoDich"]);
            }
            return tongSoTien;
        }

        private void btnXemBaoCao_Click(object sender, EventArgs e)
        {
            string sql = "SELECT SoThuTuGiaoDich, MaTaiKhoan, SoHieuBuuCucGiaoDich, " +
                         "CONVERT(VARCHAR, NgayGiaoDich, 103) AS NgayGiaoDich, SoTienGiaoDich, HinhThucGiaoDich " +
                         "FROM tblGiaoDichKhachHang WHERE 1=1";

            // Lọc theo hình thức giao dịch
            if (cboHinhThucGiaoDich.SelectedIndex != -1)
            {
                sql += " AND HinhThucGiaoDich = N'" + cboHinhThucGiaoDich.SelectedItem.ToString() + "'";
            }

            // Lọc theo thời gian bắt đầu
            if (!string.IsNullOrEmpty(mskNgayBatDau.Text.Trim()))
            {
                try
                {
                    DateTime ngayBatDau = DateTime.ParseExact(mskNgayBatDau.Text, "dd/MM/yyyy", null);
                    sql += " AND NgayGiaoDich >= '" + ngayBatDau.ToString("yyyy-MM-dd") + "'";
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ngày bắt đầu không hợp lệ. Vui lòng nhập đúng định dạng ngày (dd/MM/yyyy).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Lọc theo thời gian kết thúc
            if (!string.IsNullOrEmpty(mskNgayKetThuc.Text.Trim()))
            {
                try
                {
                    DateTime ngayKetThuc = DateTime.ParseExact(mskNgayKetThuc.Text, "dd/MM/yyyy", null);
                    sql += " AND NgayGiaoDich <= '" + ngayKetThuc.ToString("yyyy-MM-dd") + "'";
                }
                catch (FormatException)
                {
                    MessageBox.Show("Ngày kết thúc không hợp lệ. Vui lòng nhập đúng định dạng ngày (dd/MM/yyyy).", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
            }

            // Thực hiện truy vấn và nạp dữ liệu vào DataGridView
            tblBaoCao = Functions.GetDataToTable(sql);
            dgvBaoCao.DataSource = tblBaoCao;

            if (tblBaoCao.Rows.Count == 0)
            {
                MessageBox.Show("Không tìm thấy giao dịch nào phù hợp.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Đã tìm thấy " + tblBaoCao.Rows.Count + " giao dịch.", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            // Tính tổng số giao dịch và tổng số tiền giao dịch
            lblTongSoGiaoDich.Text = tblBaoCao.Rows.Count.ToString();
            lblTongSoTien.Text = TinhTongSoTien(tblBaoCao).ToString("N0");
        }


        private void btnLamMoi_Click(object sender, EventArgs e)
        {
            ResetValues();
            dgvBaoCao.DataSource = null;
        }

        private void btnDong_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void mskNgayKetThuc_MaskInputRejected(object sender, MaskInputRejectedEventArgs e)
        {

        }
    }
}
