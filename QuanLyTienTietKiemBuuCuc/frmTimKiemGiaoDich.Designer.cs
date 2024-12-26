namespace QuanLyTienTietKiemBuuCuc
{
    partial class frmTimKiemGiaoDich
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.txtMaTaiKhoan = new System.Windows.Forms.TextBox();
            this.cboSoHieuBuuCuc = new System.Windows.Forms.ComboBox();
            this.mskNgayGiaoDich = new System.Windows.Forms.MaskedTextBox();
            this.cboHinhThucGiaoDich = new System.Windows.Forms.ComboBox();
            this.panel1 = new System.Windows.Forms.Panel();
            this.btnDong = new System.Windows.Forms.Button();
            this.btnTimKiem = new System.Windows.Forms.Button();
            this.dgvTimKiemGiaoDich = new System.Windows.Forms.DataGridView();
            this.txtSoTienGiaoDich = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimKiemGiaoDich)).BeginInit();
            this.SuspendLayout();
            // 
            // txtMaTaiKhoan
            // 
            this.txtMaTaiKhoan.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtMaTaiKhoan.Location = new System.Drawing.Point(262, 80);
            this.txtMaTaiKhoan.Name = "txtMaTaiKhoan";
            this.txtMaTaiKhoan.Size = new System.Drawing.Size(100, 26);
            this.txtMaTaiKhoan.TabIndex = 0;
            // 
            // cboSoHieuBuuCuc
            // 
            this.cboSoHieuBuuCuc.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboSoHieuBuuCuc.FormattingEnabled = true;
            this.cboSoHieuBuuCuc.Location = new System.Drawing.Point(339, 116);
            this.cboSoHieuBuuCuc.Name = "cboSoHieuBuuCuc";
            this.cboSoHieuBuuCuc.Size = new System.Drawing.Size(121, 27);
            this.cboSoHieuBuuCuc.TabIndex = 1;
            // 
            // mskNgayGiaoDich
            // 
            this.mskNgayGiaoDich.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.mskNgayGiaoDich.Location = new System.Drawing.Point(512, 80);
            this.mskNgayGiaoDich.Name = "mskNgayGiaoDich";
            this.mskNgayGiaoDich.Size = new System.Drawing.Size(100, 26);
            this.mskNgayGiaoDich.TabIndex = 2;
            // 
            // cboHinhThucGiaoDich
            // 
            this.cboHinhThucGiaoDich.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.cboHinhThucGiaoDich.FormattingEnabled = true;
            this.cboHinhThucGiaoDich.Location = new System.Drawing.Point(627, 115);
            this.cboHinhThucGiaoDich.Name = "cboHinhThucGiaoDich";
            this.cboHinhThucGiaoDich.Size = new System.Drawing.Size(121, 27);
            this.cboHinhThucGiaoDich.TabIndex = 3;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.btnDong);
            this.panel1.Controls.Add(this.btnTimKiem);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.panel1.Location = new System.Drawing.Point(0, 390);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(800, 60);
            this.panel1.TabIndex = 4;
            // 
            // btnDong
            // 
            this.btnDong.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnDong.Location = new System.Drawing.Point(478, 21);
            this.btnDong.Margin = new System.Windows.Forms.Padding(2);
            this.btnDong.Name = "btnDong";
            this.btnDong.Size = new System.Drawing.Size(56, 28);
            this.btnDong.TabIndex = 37;
            this.btnDong.Text = "Đóng";
            this.btnDong.UseVisualStyleBackColor = true;
            this.btnDong.Click += new System.EventHandler(this.btnDong_Click_1);
            // 
            // btnTimKiem
            // 
            this.btnTimKiem.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btnTimKiem.Location = new System.Drawing.Point(198, 21);
            this.btnTimKiem.Margin = new System.Windows.Forms.Padding(2);
            this.btnTimKiem.Name = "btnTimKiem";
            this.btnTimKiem.Size = new System.Drawing.Size(56, 28);
            this.btnTimKiem.TabIndex = 36;
            this.btnTimKiem.Text = "Tìm";
            this.btnTimKiem.UseVisualStyleBackColor = true;
            this.btnTimKiem.Click += new System.EventHandler(this.btnTimKiem_Click_1);
            // 
            // dgvTimKiemGiaoDich
            // 
            this.dgvTimKiemGiaoDich.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvTimKiemGiaoDich.Location = new System.Drawing.Point(6, 153);
            this.dgvTimKiemGiaoDich.Name = "dgvTimKiemGiaoDich";
            this.dgvTimKiemGiaoDich.Size = new System.Drawing.Size(794, 241);
            this.dgvTimKiemGiaoDich.TabIndex = 5;
            this.dgvTimKiemGiaoDich.CellContentClick += new System.Windows.Forms.DataGridViewCellEventHandler(this.dgvTimKiemGiaoDich_CellContentClick);
            // 
            // txtSoTienGiaoDich
            // 
            this.txtSoTienGiaoDich.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txtSoTienGiaoDich.Location = new System.Drawing.Point(99, 116);
            this.txtSoTienGiaoDich.Name = "txtSoTienGiaoDich";
            this.txtSoTienGiaoDich.Size = new System.Drawing.Size(100, 26);
            this.txtSoTienGiaoDich.TabIndex = 6;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.SkyBlue;
            this.label1.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(156, 83);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(98, 19);
            this.label1.TabIndex = 7;
            this.label1.Text = "Mã Tài Khoản";
            this.label1.Click += new System.EventHandler(this.label1_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.SkyBlue;
            this.label2.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(214, 118);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(117, 19);
            this.label2.TabIndex = 8;
            this.label2.Text = "Số Hiệu Bưu Cục";
            this.label2.Click += new System.EventHandler(this.label2_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.SkyBlue;
            this.label3.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(397, 83);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 19);
            this.label3.TabIndex = 9;
            this.label3.Text = "Ngày Giao Dịch";
            this.label3.Click += new System.EventHandler(this.label3_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.SkyBlue;
            this.label4.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(36, 118);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(55, 19);
            this.label4.TabIndex = 10;
            this.label4.Text = "Số Tiền";
            this.label4.Click += new System.EventHandler(this.label4_Click);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.BackColor = System.Drawing.Color.SkyBlue;
            this.label5.Font = new System.Drawing.Font("Times New Roman", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label5.Location = new System.Drawing.Point(478, 118);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(138, 19);
            this.label5.TabIndex = 11;
            this.label5.Text = "Hình Thức Giao Dịch";
            this.label5.Click += new System.EventHandler(this.label5_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("Times New Roman", 20.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label6.Location = new System.Drawing.Point(267, 19);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(258, 31);
            this.label6.TabIndex = 12;
            this.label6.Text = "Tìm Kiếm Giao Dịch";
            // 
            // frmTimKiemGiaoDich
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.SkyBlue;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtSoTienGiaoDich);
            this.Controls.Add(this.dgvTimKiemGiaoDich);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.cboHinhThucGiaoDich);
            this.Controls.Add(this.mskNgayGiaoDich);
            this.Controls.Add(this.cboSoHieuBuuCuc);
            this.Controls.Add(this.txtMaTaiKhoan);
            this.Name = "frmTimKiemGiaoDich";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "frmTimKiemGiaoDich";
            this.Load += new System.EventHandler(this.frmTimKiemGiaoDich_Load);
            this.panel1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvTimKiemGiaoDich)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtMaTaiKhoan;
        private System.Windows.Forms.ComboBox cboSoHieuBuuCuc;
        private System.Windows.Forms.MaskedTextBox mskNgayGiaoDich;
        private System.Windows.Forms.ComboBox cboHinhThucGiaoDich;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.Button btnDong;
        private System.Windows.Forms.Button btnTimKiem;
        private System.Windows.Forms.DataGridView dgvTimKiemGiaoDich;
        private System.Windows.Forms.TextBox txtSoTienGiaoDich;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label6;
    }
}