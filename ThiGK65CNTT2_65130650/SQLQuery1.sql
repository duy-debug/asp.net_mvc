CREATE DATABASE Thi_65130650
GO
USE Thi_65130650
GO
CREATE TABLE LoaiTinTuc
(
	MaLTT nvarchar(5) PRIMARY KEY,
	TenLTT nvarchar(50) NOT NULL
)
GO
CREATE TABLE TinTuc
(
	MaTT nvarchar(8) PRIMARY KEY,
	TieuDe nvarchar(50) NOT NULL,
	NgonNgu bit DEFAULT(1),
	NgayDang date,
	NguoiDang nvarchar(50) NOT NULL,
	AnhDaiDien nvarchar(100),
	MaLTT nvarchar(5) NOT NULL FOREIGN KEY REFERENCES LoaiTinTuc(MaLTT)
	ON UPDATE CASCADE
	ON DELETE CASCADE,
	GhiChu nvarchar(100) NOT NULL,
)
-- Thêm 3 dòng dữ liệu vào LoaiTinTuc
GO
INSERT INTO LoaiTinTuc (MaLTT, TenLTT) VALUES
('LTT01', N'Tin Công Nghệ'),
('LTT02', N'Tin Thể Thao'),
('LTT03', N'Tin Giải Trí')
GO
-- Thêm 3 dòng dữ liệu vào TinTuc
INSERT INTO TinTuc (MaTT, TieuDe, NgonNgu, NgayDang, NguoiDang, AnhDaiDien, MaLTT, GhiChu) VALUES
('TT001', N'Ra mắt iPhone 17', 1, '2025-10-30', N'Nguyễn Văn A', N'iphone17.jpg', 'LTT01', N'Tin mới nhất về công nghệ.'),
('TT002', N'Việt Nam thắng Thái Lan 3-0', 1, '2025-10-28', N'Lê Văn B', N'vn-tha.jpg', 'LTT02', N'Tin thể thao hấp dẫn.'),
('TT003', N'Phim mới của Marvel gây sốt', 0, '2025-10-25', N'Trần Thị C', N'marvel.jpg', 'LTT03', N'Tin giải trí nổi bật.')
GO
CREATE PROCEDURE TinTuc_TimKiem
	@TieuDe NVARCHAR(50) = NULL,
    @TenLTT NVARCHAR(50) = NULL
AS
BEGIN
    DECLARE @SqlStr NVARCHAR(4000),
            @ParamList NVARCHAR(2000)

    SELECT @SqlStr = '
           SELECT TT.MaTT, TT.TieuDe, TT.NgayDang, TT.NguoiDang, LTT.TenLTT, TT.GhiChu
           FROM TinTuc TT
           JOIN LoaiTinTuc LTT ON TT.MaLTT = LTT.MaLTT
           WHERE (1=1)
           '

    IF @TieuDe <> ''
           SELECT @SqlStr = @SqlStr + '
                  AND (TT.TieuDe LIKE N''%' + @TieuDe + '%'')
                  '

    IF @TenLTT IS NOT NULL
           SELECT @SqlStr = @SqlStr + '
                  AND (LTT.TenLTT LIKE N''%' + @TenLTT + '%'')
                  '

    EXEC sp_executesql @SqlStr
END