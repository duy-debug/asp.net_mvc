CREATE DATABASE KT0720_65130650
GO
USE KT0720_65130650
GO
CREATE TABLE Lop
(
	MaLop nvarchar(15) PRIMARY KEY,
	TenLop nvarchar(50) NOT NULL
)
GO
CREATE TABLE SinhVien
(
	MaSV nvarchar(8) PRIMARY KEY,
	HoSV nvarchar(50) NOT NULL,
	TenNV nvarchar(20) NOT NULL,
	NgaySinh date,
	GioiTinh bit DEFAULT(1),
	AnhSV nvarchar(100),
	DiaChi nvarchar(100) NOT NULL,
	MaLop nvarchar(15) NOT NULL FOREIGN KEY REFERENCES Lop(MaLop)
	ON UPDATE CASCADE
	ON DELETE CASCADE
)
GO
INSERT INTO Lop VALUES('65.CNTT-1',N'Lớp 65.CNTT-1'),('65.CNTT-2',N'Lớp 65.CNTT-2'),('65.CNTT-3',N'Lớp 65.CNTT-3')
GO
INSERT INTO SinhVien VALUES (N'65130650', N'Trần Mai Ngọc', N'Duy','10/30/2005',1, N'65130650.png', N'Nha Trang, Khánh Hòa', N'65.CNTT-1'),
						(N'65130306', N'Võ Huỳnh Kim', N'Chi','10/16/2005',0, N'65130603.png', N'Sông Cầu, Phú Yên', N'65.CNTT-1')
GO
CREATE PROCEDURE SinhVien_TimKiem
    @MaSV varchar(8)=NULL,
	@HoTen nvarchar(70)=NULL
AS
BEGIN
DECLARE @SqlStr NVARCHAR(4000),
		@ParamList nvarchar(2000)
SELECT @SqlStr = '
       SELECT * 
       FROM SinhVien
       WHERE  (1=1)
       '
IF @MaSV <> ''
       SELECT @SqlStr = @SqlStr + '
              AND (MaSV = '''+@MaSV+''')
              '
IF @HoTen IS NOT NULL
       SELECT @SqlStr = @SqlStr + '
              AND (HoSV+'' ''+TenNV LIKE ''%'+@HoTen+'%'')
              '
	EXEC SP_EXECUTESQL @SqlStr
END
