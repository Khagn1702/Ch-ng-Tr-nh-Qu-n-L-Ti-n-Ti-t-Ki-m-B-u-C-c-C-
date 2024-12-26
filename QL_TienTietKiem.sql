-- Tạo cơ sở dữ liệu
CREATE DATABASE QL_TienTietKiem;
USE QL_TienTietKiem;

-- Bảng BUUCUC (Bưu Cục)
CREATE TABLE tblBuuCuc (
    SoHieuBuuCuc CHAR(10) PRIMARY KEY,            -- Số hiệu bưu cục (khóa chính)
    TenBuuCuc NVARCHAR(100) NOT NULL,            -- Tên bưu cục
    DiaChiBuuCuc NVARCHAR(200) NOT NULL,         -- Địa chỉ
    DienThoaiBuuCuc CHAR(15),                    -- Số điện thoại
    TinhThanhPho NVARCHAR(100) NOT NULL          -- Tỉnh/Thành phố
);

-- Bảng TAIKHOAN (Tài Khoản Khách Hàng)
CREATE TABLE tblTaiKhoan (
    MaTaiKhoan CHAR(10) PRIMARY KEY,             -- Mã tài khoản (khóa chính)
    HoTenKhachHang NVARCHAR(100) NOT NULL,       -- Họ tên khách hàng
    DiaChiKhachHang NVARCHAR(200) NOT NULL,      -- Địa chỉ khách hàng
    ChungMinhNhanDan CHAR(12) UNIQUE NOT NULL,   -- Số chứng minh nhân dân (không trùng lặp)
    SoHieuBuuCucMoTaiKhoan CHAR(10) NOT NULL,    -- Số hiệu bưu cục mở tài khoản (khóa ngoại)
    NgayMoTaiKhoan DATE NOT NULL,                -- Ngày mở tài khoản
    FOREIGN KEY (SoHieuBuuCucMoTaiKhoan) REFERENCES tblBuuCuc(SoHieuBuuCuc)
);

-- Bảng GIAODICH (Giao Dịch Khách Hàng)
CREATE TABLE tblGiaoDichKhachHang (
    SoThuTuGiaoDich INT IDENTITY(1,1) PRIMARY KEY, -- Số thứ tự giao dịch tự tăng
    MaTaiKhoan CHAR(10) NOT NULL,                 -- Mã tài khoản (khóa ngoại)
    SoHieuBuuCucGiaoDich CHAR(10) NOT NULL,       -- Số hiệu bưu cục giao dịch (khóa ngoại)
    NgayGiaoDich DATE NOT NULL,                   -- Ngày giao dịch
    SoTienGiaoDich DECIMAL(18, 2) NOT NULL,       -- Số tiền giao dịch
    HinhThucGiaoDich NVARCHAR(10) NOT NULL,       -- Hình thức giao dịch ('Gui' hoặc 'Rut')
    FOREIGN KEY (MaTaiKhoan) REFERENCES tblTaiKhoan(MaTaiKhoan),
    FOREIGN KEY (SoHieuBuuCucGiaoDich) REFERENCES tblBuuCuc(SoHieuBuuCuc)
);

-- Thêm dữ liệu vào bảng BuuCuc
INSERT INTO tblBuuCuc (SoHieuBuuCuc, TenBuuCuc, DiaChiBuuCuc, DienThoaiBuuCuc, TinhThanhPho)
VALUES 
('BC001', N'Bưu cục Hà Nội', N'123 Đường ABC, Hà Nội', '0123456789', N'Hà Nội'),
('BC002', N'Bưu cục Hồ Chí Minh', N'456 Đường XYZ, TP.HCM', '0987654321', N'TP.HCM');

-- Thêm dữ liệu vào bảng TaiKhoan
INSERT INTO tblTaiKhoan (MaTaiKhoan, HoTenKhachHang, DiaChiKhachHang, ChungMinhNhanDan, SoHieuBuuCucMoTaiKhoan, NgayMoTaiKhoan)
VALUES 
('TK001', N'Nguyễn Văn A', N'12 Đường Mẫu, Hà Nội', '123456789012', 'BC001', '2024-01-01'),
('TK002', N'Trần Thị B', N'34 Đường Mẫu, TP.HCM', '987654321098', 'BC002', '2024-02-01');

-- Thêm dữ liệu vào bảng GiaoDichKhachHang
INSERT INTO tblGiaoDichKhachHang (MaTaiKhoan, SoHieuBuuCucGiaoDich, NgayGiaoDich, SoTienGiaoDich, HinhThucGiaoDich)
VALUES 
('TK001', 'BC001', '2024-03-01', 1000000, 'Gui'),
('TK001', 'BC002', '2024-03-02', 500000, 'Rut'),
('TK002', 'BC002', '2024-03-05', 2000000, 'Gui');
	


-- Kiểm tra dữ liệu trong các bảng
SELECT * FROM tblBuuCuc;
SELECT * FROM tblTaiKhoan;
SELECT * FROM tblGiaoDichKhachHang;







