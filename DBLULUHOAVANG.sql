Create database dbLuLutHoaVang
drop database dbLuLutHoaVang
go
use dbLuLutHoaVang
go
create table tbTaiKhoan
(
	idTaiKhoan bigint IDENTITY(1,1) not null primary key,	
	tenDangNhap varchar(50) not null,
	matKhau varchar(50) not null,
	quyen nvarchar(50) not null,
	hoVaTen nvarchar(100) null,
	email varchar(50) unique check (email like '%@%') null,
	sdt varchar(11) null unique,
	diaChi nvarchar(50) null,
	tenPhuong nvarchar(100) null
)
go
create table tbDanhMuc
(
	idDanhMuc bigint IDENTITY(1,1) not null primary key,
	tenDanhMuc nvarchar(100) not null,
	moTa ntext null
)
go
create table tbDotLu
(
	idDotLu bigint IDENTITY(1,1) not null primary key,
	tenDotLu nvarchar(100) NOT NULL,
	ngayBatDau datetime NOT NULL,
	ngayKetThuc datetime NULL,
	check (ngayKetThuc >= ngayBatDau)
)
go
create table tbBaiDang
(
	idBaiDang bigint IDENTITY(1,1) not null primary key,
	tieuDe nvarchar(100) not null,
	noiDung nvarchar(max) not null,
	ngayDang datetime,
	idDotLu bigint foreign key references tbDotLu(idDotLu)
	on update
			cascade
	on delete
			cascade,

	idTaiKhoan bigint foreign key references tbTaiKhoan(idTaiKhoan)
	on update
			cascade
	on delete
			cascade

)
go
create table tbHinhAnh
(
	idHinhAnh bigint IDENTITY(1,1) not null primary key,
	urlHinhAnh varchar(100) not null,
	idBaiDang bigint foreign key references tbBaiDang(idBaiDang)
	on update
			cascade
	on delete
			cascade

)
go
create table tbDonDangKyUngHo
(
	idDonDK bigint IDENTITY(1,1) not null primary key,
	tenHangUngHo nvarchar(max),
	ngayDK datetime,
	trangThai nvarchar(100) check (trangThai in (N'Đã duyệt', N'Chưa duyệt',N'Đã trả')),
	idTaiKhoan bigint foreign key references tbTaiKhoan(idTaiKhoan)
	on update
			cascade
	on delete
			cascade,
	idDotLu bigint foreign key references tbDotLu(idDotLu)
	on update
			cascade
	on delete
			cascade
)
go
create table tbHangHoa
(
	idHangHoa bigint IDENTITY(1,1) not null primary key,
	tenHangHoa nvarchar(100) not null,
	soLuong int not null,
	moTa nvarchar(max) null,
	donViTinh nvarchar(50) not null,
	idDanhMuc bigint foreign key references tbDanhMuc(idDanhMuc)
	on update
			cascade
	on delete
			cascade
)
go


go
create table tbDotCuuTro
(
	idDotCuuTro bigint IDENTITY(1,1) not null primary key,
	tenDotCuuTro nvarchar(50)  not null,
	ngayBatDau datetime,
	ngayKetThuc datetime,
	idDotLu bigint foreign key references tbDotLu(idDotLu)
	on update
			cascade
	on delete
			cascade,
	check (ngayKetThuc >= ngayBatDau)
)
go
create table tbChiTietHangUngHo
(
	idChiTietHangUngHo bigint IDENTITY(1,1) not null primary key,
	idHangHoa bigint not null foreign key references tbHangHoa(idHangHoa)
	on update
			cascade
	on delete
			cascade,
	idDonDK bigint not null foreign key references tbDonDangKyUngHo(idDonDK)
	on update
			cascade
	on delete
			cascade,
	soLuong int
)
go
create table tbChiTietHangCuuTro
(
	idHangHoa bigint not null foreign key references tbHangHoa(idHangHoa)
	on update
			cascade
	on delete
			cascade,
	idTaiKhoan bigint not null foreign key references tbTaiKhoan(idTaiKhoan)
	on update
			cascade
	on delete
			cascade,
	idDotCuuTro bigint not null foreign key references tbDotCuuTro(idDotCuuTro)
	on update
			cascade
	on delete
			cascade,
	primary key(idHangHoa,idTaiKhoan,idDotCuuTro),
	soLuong int not null
)
go
create table tbMucDoThietHai
(
	idMucDo bigint IDENTITY(1,1) not null primary key,
	mucThietHai nvarchar(50) NOT NULL,
	moTa nvarchar(max)
)
go
create table tbChitietMucDoThietHai
(
	idMucDo bigint not null foreign key references tbMucDoThietHai(idMucDo)
	on update
			cascade
	on delete
			cascade,
	idTaiKhoan bigint not null foreign key references tbTaiKhoan(idTaiKhoan)
	on update
			cascade
	on delete
			cascade,
	idDotLu bigint not null foreign key references tbDotLu(idDotLu)
	on update
			cascade
	on delete
			cascade,
	primary key(idMucDo,idTaiKhoan,idDotLu),
	mota nvarchar(max) null
)
go
create table tbThongBao
(
	idThongBao bigint IDENTITY(1,1) NOT NULL primary key,
	noiDung nvarchar(max) not null,
	ngayDang datetime,
	idTaiKhoan bigint foreign key references tbTaiKhoan(idTaiKhoan)
	on update
			cascade
	on delete
			cascade
)
go
create table tbFile
(
	idFile bigint IDENTITY(1,1) not null primary key,
	urlFile varchar(max),
	idThongBao bigint foreign key references tbThongBao(idThongBao) 
	on update
			cascade
	on delete
			cascade
)
--tbTaiKhoan
insert into tbTaiKhoan(tenDangNhap,matKhau,quyen,hoVaTen,email,sdt,diaChi,tenPhuong)
values
		('user1','12345','admin',N'Nguyễn Văn A', 'VanA@gmail.com','0361434567',N'Quảng Nam',null),
		('user2','12345','subadmin',N'Nguyễn Văn B','VanB@gmail.com','0361534567',N'Hà Tĩnh',null),
		('user3','12345','subadmin',N'Nguyễn Văn C', 'VanC@gmail.com','0361276567',N'Huế',null),
		('user4','12345','volunteer',N'Nguyễn Văn D','VanD@gmail.com','0361634567',N'Đà Nẵng',null),
		('user5','12345','citizen',N'Nguyễn Văn E', 'VanE@gmail.com','0361239867',N'Quảng Trị',null)

--tbDanhMuc
insert into tbDanhMuc(tenDanhMuc,moTa)
values
		(N'Thực phẩm',N'Thực phẩm dễ bảo quản và sử dụng như hạt, bột, gạo, mì ống, thịt đóng hộp, thực phẩm đóng gói có thời hạn sử dụng dài.'),
		(N'Nước uống',N'Nước uống đóng chai, nước lọc, nước giải khát, ...'),
		(N'Nhu yếu phẩm',N'Các vật dụng và sản phẩm cần thiết để đảm bảo sức khỏe và an toàn trong tình huống khẩn cấp, bao gồm bỉm sữa, thuốc, áo quần , đèn pin, đồ dùng cá nhân, đồ gia dụng và dụng cụ cần thiết khác.'),
		(N'Tiền',N'Loại hàng hóa có giá trị lưu thông không có giá trị lưu giữ')

--tbDotLu
SET DATEFORMAT dmy
INSERT INTO tbDotLu (tenDotLu, ngayBatDau, ngayKetThuc)
VALUES
    (N'Đợt lũ 1', '15/5/2023', '30/5/2023'),
    (N'Đợt lũ 2', '10/6/2023', '25/6/2023'),
    (N'Đợt lũ 3', '5/7/2023', '20/7/2023'),
    (N'Đợt lũ 4', '1/8/2023', '15/8/2023'),
    (N'Đợt lũ 5', '20/8/2023', '5/9/2023')


--tbBaiDang
SET DATEFORMAT dmy
insert into tbBaiDang(tieuDe,noiDung,ngayDang,idDotLu,idTaiKhoan)
values 
    (N'Tiêu đề bài đăng 1', N'Nội dung bài đăng 1', '2/5/2023', 1, 1),
    (N'Tiêu đề bài đăng 2', N'Nội dung bài đăng 2', '3/5/2023', 1, 2),
    (N'Tiêu đề bài đăng 3', N'Nội dung bài đăng 3', '4/5/2023', 2, 3),
    (N'Tiêu đề bài đăng 4', N'Nội dung bài đăng 4', '5/5/2023', 2, 4),
    (N'Tiêu đề bài đăng 5', N'Nội dung bài đăng 5', '6/5/2023', 3, 5)

--tbHinhAnh
insert into tbHinhAnh (urlHinhAnh, idBaiDang)
values 
    ('url_hinh_anh_1.jpg', 1),
    ('url_hinh_anh_2.jpg', 2),
    ('url_hinh_anh_3.jpg', 5),
    ('url_hinh_anh_4.jpg', 3),
    ('url_hinh_anh_5.jpg', 4);

--tbDonDangKyUngHo
SET DATEFORMAT dmy
insert into tbDonDangKyUngHo (tenHangUngHo, ngayDK, trangThai, idTaiKhoan, idDotLu)
values 
    (N'Gạo, lúa mỳ, mỳ tôm','02/05/2023', N'Đã duyệt', 1, 1),
    (N'Tiền, gạo, dầu ăn','03/05/2023', N'Đã duyệt', 2, 1),
    (N'Áo quần cũ, rách','04/05/2023', N'Đã trả', 3, 2),
    (N'Tiền','05/05/2023', N'Chưa duyệt', 4, 2),
    (N'Thuốc men, rau củ','06/05/2023', N'Chưa duyệt', 5, 3);

	--tbHangHoa
insert into tbHangHoa (tenHangHoa, soLuong,mota ,donViTinh, idDanhMuc)
values 
	(N'Tiền',100000000,null,'VND',4),
    (N'Gạo', 100,null,'KG',1),
    (N'Nước đóng chai', 200, null,'Lít',2),
    (N'Quần áo ấm', 150, null,'KG',3),
    (N'Thuốc chống sốt', 300, null,'Hộp',3),
    (N'Mỳ tôm hảo hảo', 500, null,'Thùng',1)
    

--tbDotCuuTro
insert into tbDotCuuTro (tenDotCuuTro, ngayBatDau, ngayKetThuc, idDotLu)
values 
    (N'Đợt cứu trợ 1', '15/05/2023', '30/05/2023', 1),
    (N'Đợt cứu trợ 2', '10/06/2023', '25/06/2023', 2),
    (N'Đợt cứu trợ 3', '05/07/2023', '20/07/2023', 3),
    (N'Đợt cứu trợ 4', '01/08/2023', '15/08/2023', 4),
    (N'Đợt cứu trợ 5', '20/08/2023', '05/09/2023', 5)

--tbChiTietHangUngHo
insert into tbChiTietHangUngHo (idHangHoa, idDonDK, soLuong)
values 
    (1, 1, 1000000),
    (2, 5, 10),
    (3, 2, 10),
    (6, 3, 10),
    (5, 4, 10)

--tbChiTietHangCuuTro
insert into tbChiTietHangCuuTro (idHangHoa, idTaiKhoan, idDotCuuTro, soLuong)
values 
    (1, 2, 1, 500000),
    (2, 1, 1, 10),
    (3, 3, 2, 15),
    (4, 4, 3, 20),
    (5, 5, 4, 10);

--tbMucDoThietHai
insert into tbMucDoThietHai (mucThietHai, moTa)
values 
    (N'mức 1', N'Thất thoát nhẹ nhàng, gây mất mát nhỏ, không ảnh hưởng nhiều đến cuộc sống hàng ngày.'),
    (N'mức 2', N'Thất thoát có mức độ trung bình, gây mất mát ổn định, có thể ảnh hưởng đến một số khía cạnh của cuộc sống hàng ngày.'),
    (N'mức 3', N'Thất thoát nặng, gây mất mát lớn và nghiêm trọng, ảnh hưởng đến nhiều khía cạnh của cuộc sống hàng ngày.'),
    (N'mức 4', N'Thất thoát rất nặng, gây mất mát rất lớn và nghiêm trọng, ảnh hưởng nghiêm trọng đến cuộc sống hàng ngày, có thể gây ra những thay đổi sâu sắc và kéo dài.')

--tbChiTietMucDoThietHai
insert into tbChiTietMucDoThietHai (idMucDo, idTaiKhoan, idDotLu,mota)
values 
    (1, 1, 1,N'Ngập lụt 1m'),
    (2, 2, 1,N'Nhà tốc mái'),
    (2, 3, 2,N'Mất 1 phần hoa màu'),
    (3, 4, 3,N'Ngập lụt, mất 2 hecta lúa'),
    (4, 5, 4,N'Nhà tốc mái hêt, sập 1 bên tường, chết 2 con heo do lũ cuốn')

--tbThongBao
insert into tbThongBao (noiDung, ngayDang, idTaiKhoan)
values 
    (N'Thông báo 1', '02/05/2023', 1),
    (N'Thông báo 2', '03/05/2023', 2),
    (N'Thông báo 3', '04/05/2023', 3),
    (N'Thông báo 4', '05/05/2023', 4),
    (N'Thông báo 5', '06/05/2023', 5)

--tbFile
insert into tbFile (urlFile, idThongBao)
values 
    ('url_file_1', 1),
    ('url_file_2', 2),
    ('url_file_3', 3),
    ('url_file_4', 4),
    ('url_file_5	', 5);


--Trigger cập nhật số lượng hiện còn (tbHangHoa)khi thêm hàng cứu trợ (tbChiTietHangCuuTro)
CREATE TRIGGER update_product_quantity
ON  tbChiTietHangCuuTro
AFTER INSERT
AS
BEGIN
    SET NOCOUNT ON;
    
    DECLARE @idHangHoa INT;
    DECLARE @soLuongCuuTro INT;
	DECLARE @soLuongHienCon INT;

    -- Lấy thông tin hàng hóa và số lượng hàng cứu trợ từ bảng tbChiTietCuuTro
    SELECT @idHangHoa = idHangHoa, @soLuongCuuTro = soLuong
    FROM inserted;

	-- Lấy số lượng hàng hóa hiện tại
	SELECT @soLuongHienCon = soLuong
    FROM tbHangHoa
    WHERE idHangHoa = @idHangHoa;

    -- Cập nhật số lượng hàng hóa trong bảng hàng hóa
	 IF @soLuongHienCon >= @soLuongCuuTro
    BEGIN
		UPDATE tbHangHoa
		SET soLuong = soLuong - @soLuongCuuTro
		WHERE idHangHoa = @idHangHoa;
	END;
	ELSE
    BEGIN
        -- Ném ngoại lệ khi số lượng cứu trợ vượt quá số lượng hiện còn
        DECLARE @errorMessage NVARCHAR(100);
        SET @errorMessage = 'Số lượng hàng hóa hiện còn ít hơn số lượng hàng hóa cứu trợ.';
        THROW 51000, @errorMessage, 1;
    END;
END;

-- trigger tự động cập nhật số lượng hàng hóa khi thêm chi tiết hàng ủng hộ
CREATE TRIGGER CapNhatSoLuongHangUngHo
ON  tbChiTietHangUngHo
AFTER INSERT, UPDATE, DELETE
AS
BEGIN
    -- Cập nhật số lượng hàng hóa trong bảng hàng hóa khi insert từ tbCTHUH
	UPDATE tbHangHoa
	SET soLuong = tbHangHoa.soLuong + ist.soLuong
	FROM inserted as ist
	WHERE tbHangHoa.idHangHoa = ist.idHangHoa;
	-- Cập nhật số lượng hàng hóa trong bảng hàng hóa khi delete từ tbCTHUH
	UPDATE tbHangHoa
	SET soLuong = tbHangHoa.soLuong - d.soLuong
	FROM deleted as d
	WHERE tbHangHoa.idHangHoa = d.idHangHoa;
END 

--Cập nhật trạng thái "Đã nhận hàng" cho đơn đăng ký sau khi insert dữ liệu vào bảng chi tiết ủng hộ
CREATE TRIGGER trg_UpdateTrangThaiDonDangKy
ON tbChiTietHangUngHo
AFTER INSERT
AS
BEGIN
    -- Cập nhật trạng thái của đơn đăng ký ủng hộ
    UPDATE tbDonDangKyUngHo
    SET trangThai = N'Đã duyệt'
    FROM tbDonDangKyUngHo d
    INNER JOIN inserted i ON d.idDonDK = i.idDonDK;
END 

------FUNCTION

--Tính tổng tiền từng đợt cứu trợ
CREATE FUNCTION dbo.TinhTongTienDotCuuTro(
    @IDdotCuuTro NVARCHAR(50)
)
RETURNS INT
AS
BEGIN
    DECLARE @TongTien INT = 0
	
    SELECT @TongTien = SUM(cthct.SoLuong)
    FROM tbChiTietHangCuuTro cthct 
    INNER JOIN tbDotCuuTro dct ON cthct.idDotCuuTro = dct.idDotCuuTro
    INNER JOIN tbHangHoa hh ON cthct.idHangHoa = hh.idHangHoa
    INNER JOIN tbDanhMuc dm ON hh.idDanhMuc = dm.idDanhMuc
    WHERE dct.idDotCuuTro = @IDdotCuuTro
    AND dm.TenDanhMuc = N'Tiền'

    RETURN @TongTien
END 

-- function tính tổng sl của 1 hàng hóa ủng hộ trong 1 đợt lũ
CREATE FUNCTION dbo.TinhTongSoLuongHangUngHoTheoMaHH_DotLu
(@IdDotLu int, @IdHangHoa int)
RETURNS INT
AS
BEGIN
    DECLARE @TongSL INT = 0
	
    SELECT @TongSL = SUM(cthuh.SoLuong)
    FROM tbChiTietHangUngHo cthuh 
    INNER JOIN tbDonDangKyUngHo ddk ON cthuh.idDonDK = ddk.idDonDK
    WHERE cthuh.idHangHoa = @IdHangHoa AND ddk.idDotLu = @IdDotLu

    RETURN @TongSL
END 

--Xây dựng function Tính tổng số lần đã ủng hộ của 1 mạnh thường quân
CREATE FUNCTION fn_SoLanUngHoThanhCong(@ManhThuongQuanID INT)
RETURNS INT
AS
BEGIN
    DECLARE @SoLanUngHo INT;

    SELECT @SoLanUngHo = COUNT(*)
    FROM tbDonDangKyUngHo
    WHERE idTaiKhoan = @ManhThuongQuanID
    AND trangThai = N'Đã duyệt';

    RETURN @SoLanUngHo
END 

--Hiển thị hàng hóa theo danh mục
CREATE PROC prHIENTHIHANGHOATHEODANHMUC 
    @idDanhMuc bigint 
AS
BEGIN TRANSACTION

    SELECT hanghoa.*
    FROM tbHangHoa AS hanghoa 
    LEFT JOIN tbDanhMuc AS dm ON dm.idDanhMuc = hanghoa.idDanhMuc
    WHERE hanghoa.SoLuong >= 1 
    AND dm.idDanhMuc = @idDanhMuc

IF @@ERROR <> 0
    ROLLBACK TRANSACTION
ELSE
COMMIT TRANSACTION 

-- Procedure lấy ra hàng hóa mà hộ gia đình x nhận được vào đợt cứu trợ x 
CREATE PROC pr_HIENTHIHANGHOAHOGIADINHNHANDUOCCHOTUNGDOTLU
    @idDotCuuTro bigint,
    @idHoGiaDinh bigint
AS
BEGIN  
    SELECT  
        tbTaiKhoan.hoVaTen as N'Hộ gia đình', 
        tbDotCuuTro.tenDotCuuTro as N'Đợt cứu trợ',
        tbDotLu.tenDotLu as N'Đợt lũ',
        tbHangHoa.tenHangHoa as N'Tên hàng hóa',
        tbChiTietHangCuuTro.soLuong as N'Số lượng'
    FROM 
        tbHangHoa
    INNER JOIN 
        tbChiTietHangCuuTro ON tbHangHoa.idHangHoa = tbChiTietHangCuuTro.idHangHoa
    INNER JOIN 
        tbDotCuuTro ON tbChiTietHangCuuTro.idDotCuuTro = tbDotCuuTro.idDotCuuTro
    INNER JOIN 
        tbTaiKhoan ON tbChiTietHangCuuTro.idTaiKhoan = tbTaiKhoan.idTaiKhoan
    INNER JOIN 
        tbDotLu ON tbDotCuuTro.idDotLu = tbDotLu.idDotLu
    WHERE 
        tbDotCuuTro.idDotCuuTro = @idDotCuuTro 
        AND tbTaiKhoan.idTaiKhoan = @idHoGiaDinh
END 

-- Procedure lấy ra đợt thiệt hại của hộ gia đình x trong đợt lũ x 
CREATE PROC pr_HIENTHIDOTTHIETHAI
    @idDotLu bigint,
    @idHoGiaDinh bigint
AS
BEGIN  
    SELECT  
        tbTaiKhoan.hoVaTen as N'Hộ gia đình', 
        tbDotLu.tenDotLu as N'Đợt lũ',
        tbMucDoThietHai.mucThietHai as N'Mức độ thiệt hại'
    FROM 
        tbMucDoThietHai
    INNER JOIN 
        tbChitietMucDoThietHai ON tbMucDoThietHai.idMucDo = tbChitietMucDoThietHai.idMucDo
    INNER JOIN 
        tbTaiKhoan ON tbTaiKhoan.idTaiKhoan= tbChitietMucDoThietHai.idTaiKhoan
    INNER JOIN 
        tbDotLu ON tbDotLu.idDotLu = tbChitietMucDoThietHai.idDotLu
    
    WHERE 
        tbDotLu.idDotLu = @idDotLu
        AND tbTaiKhoan.idTaiKhoan = @idHoGiaDinh
END 

--procedure hiển thị chi tiết hàng ủng hộ của 1 MTQ trong 1 đợt lũ 
CREATE PROC pr_HienThiChiTietHH_MTQ_UngHoTheoDotLu
    @idDotLu bigint,
    @idMTQ bigint
AS
BEGIN  
    SELECT  
        tbTaiKhoan.hoVaTen as N'Mạnh thường quân', 
        tbDotLu.tenDotLu as N'Đợt lũ',
		hh.tenHangHoa as N'Hàng ủng hộ',
        sum(ctuh.soLuong) as N'Số lượng'
    FROM 
        tbChiTietHangUngHo as ctuh
	INNER JOIN 
        tbHangHoa as hh ON hh.idHangHoa = ctuh.idHangHoa
    INNER JOIN 
        tbDonDangKyUngHo as ddk ON ddk.idDonDK = ctuh.idDonDK
    INNER JOIN 
        tbTaiKhoan ON tbTaiKhoan.idTaiKhoan= ddk.idTaiKhoan
    INNER JOIN 
        tbDotLu ON tbDotLu.idDotLu = ddk.idDotLu
    WHERE 
        tbDotLu.idDotLu = @idDotLu
        AND tbTaiKhoan.idTaiKhoan = @idMTQ
	GROUP BY tbTaiKhoan.hoVaTen, tbDotLu.tenDotLu, hh.tenHangHoa
END 

--Xây dựng 1 Procedure Hiển thị Tổng số lượng Hàng ủng hộ và Tổng số lượng cứu trợ (theo Đợt lũ, Tên danh mục, Tên hàng hóa) 
CREATE PROCEDURE pr_ThongKeSlgHangUngHo_CuuTro_TheoDot
AS
BEGIN
    -- Hiển thị tổng số lượng hàng hóa ủng hộ theo danh mục, tên hàng hóa và đợt lũ
    WITH HangUngHo AS (
        SELECT 
            dl.tenDotLu AS [Đợt lũ], 
            dm.tenDanhMuc AS [Tên danh mục], 
            hh.tenHangHoa AS [Tên hàng hóa], 
            SUM(cthhuh.soLuong) AS [Tổng số lượng ủng hộ]
        FROM tbDotLu dl
        LEFT JOIN tbDonDangKyUngHo ddkuh ON dl.idDotLu = ddkuh.idDotLu
        LEFT JOIN tbChiTietHangUngHo cthhuh ON ddkuh.idDonDK = cthhuh.idDonDK
        LEFT JOIN tbHangHoa hh ON cthhuh.idHangHoa = hh.idHangHoa
        LEFT JOIN tbDanhMuc dm ON hh.idDanhMuc = dm.idDanhMuc
        GROUP BY dl.tenDotLu, dm.tenDanhMuc, hh.tenHangHoa
    ),
    -- Hiển thị tổng số lượng hàng hóa cứu trợ theo danh mục, tên hàng hóa và đợt lũ
    HangCuuTro AS (
        SELECT 
            dl.tenDotLu AS [Đợt lũ], 
            dm.tenDanhMuc AS [Tên danh mục], 
            hh.tenHangHoa AS [Tên hàng hóa], 
            SUM(cthhct.soLuong) AS [Tổng số lượng cứu trợ]
        FROM tbDotLu dl
        LEFT JOIN tbDotCuuTro dct ON dl.idDotLu = dct.idDotLu
        LEFT JOIN tbChiTietHangCuuTro cthhct ON dct.idDotCuuTro = cthhct.idDotCuuTro
        LEFT JOIN tbHangHoa hh ON cthhct.idHangHoa = hh.idHangHoa
        LEFT JOIN tbDanhMuc dm ON hh.idDanhMuc = dm.idDanhMuc
        GROUP BY dl.tenDotLu, dm.tenDanhMuc, hh.tenHangHoa
    )
    -- Kết hợp 2 bảng dlieu
    SELECT 
        COALESCE(uho.[Đợt lũ], ctro.[Đợt lũ]) AS [Đợt lũ], 
        COALESCE(uho.[Tên danh mục], ctro.[Tên danh mục]) AS [Tên danh mục], 
        COALESCE(uho.[Tên hàng hóa], ctro.[Tên hàng hóa]) AS [Tên hàng hóa], 
        ISNULL(uho.[Tổng số lượng ủng hộ], 0) AS [Tổng số lượng ủng hộ], 
        ISNULL(ctro.[Tổng số lượng cứu trợ], 0) AS [Tổng số lượng cứu trợ]
    FROM HangUngHo uho
    FULL OUTER JOIN HangCuuTro ctro
    ON uho.[Đợt lũ] = ctro.[Đợt lũ] AND uho.[Tên danh mục] = ctro.[Tên danh mục] AND uho.[Tên hàng hóa] = ctro.[Tên hàng hóa]
    WHERE (uho.[Tổng số lượng ủng hộ] IS NOT NULL OR ctro.[Tổng số lượng cứu trợ] IS NOT NULL)
    ORDER BY [Đợt lũ], [Tên danh mục], [Tên hàng hóa];
END

