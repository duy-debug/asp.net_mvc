CREATE DATABASE Bookstore
USE Bookstore
CREATE TABLE LoaiSach
(	MaLoai INT IDENTITY(1, 1) PRIMARY KEY,
	TenLoai NVARCHAR(100),
	ThoiGianTao DATETIME,
	ThoiGianCapNhat DATETIME
)
CREATE TABLE TacGia
(	MaTacGia INT IDENTITY(1, 1) PRIMARY KEY,
	TenTacGia NVARCHAR(100),
	ThoiGianTao DATETIME,
	ThoiGianCapNhat DATETIME
)
CREATE TABLE SanPham
(	MaSach INT IDENTITY(1, 1) PRIMARY KEY,
	TenSach NVARCHAR(100),
	MaTacGia INT,
	MaLoaiSach INT,
	MoTa NVARCHAR(500),
	Anh NVARCHAR(200),
	DonGia INT,
	SoLuong INT,
	DanhGia FLOAT,
	ThoiGianTao DATETIME,
	ThoiGianCapNhat DATETIME,
	CONSTRAINT FK_SP_TG FOREIGN KEY (MaTacGia) REFERENCES TacGia (MaTacGia),
	CONSTRAINT FK_SP_LS FOREIGN KEY (MaLoaiSach) REFERENCES LoaiSach (MaLoai)
)
CREATE TABLE TaiKhoan
(	ID INT IDENTITY(1, 1) PRIMARY KEY,
	Ho NVARCHAR(100),
	Ten NVARCHAR(100),
	DiaChi NVARCHAR(500),
	SoDienThoai NVARCHAR(15),
	Email NVARCHAR(100),
	Anh NVARCHAR(200),
	ThoiGianDangKy DATETIME,
	ThoiGianCapNhat DATETIME,
	NgaySinh DATE,
	MatKhau NVARCHAR(200),
	TrangThai BIT,
	VaiTro INT
)
CREATE TABLE GioHang
(	MaGioHang INT IDENTITY(1, 1) PRIMARY KEY,
	MaKhachHang INT,
	MaSanPham INT,
	ThoiGianTao DATETIME,
	SoLuong INT,
	CONSTRAINT FK_GH_TK FOREIGN KEY (MaKhachHang) REFERENCES TaiKhoan (ID),
	CONSTRAINT FK_GH_SP FOREIGN KEY (MaSanPham) REFERENCES SanPham (MaSach)
)
CREATE TABLE HoaDon
(	MaHoaDon INT IDENTITY(1, 1) PRIMARY KEY,
	MaKhachHang INT,
	HoKH NVARCHAR(100),
	TenKH NVARCHAR(100),
	SDT NVARCHAR(15),
	Email NVARCHAR(200),
	ThoiGianTao DATETIME,
	TongTien FLOAT,
	CONSTRAINT FK_HD_TK FOREIGN KEY (MaKhachHang) REFERENCES TaiKhoan (ID)
)
CREATE TABLE ChiTietHoaDon
(	MaSP INT,
	MaHD INT,
	DonGia INT,
	SoLuong INT,
	ThanhTien FLOAT,
	ThoiGianTao DATETIME,
	CONSTRAINT PK_CTHD PRIMARY KEY (MaSP, MaHD),
	CONSTRAINT FK_CTHD_HD FOREIGN KEY (MaHD) REFERENCES HoaDon (MaHoaDon),
	CONSTRAINT FK_CTHD_SP FOREIGN KEY (MaSP) REFERENCES SanPham (MaSach)
)
CREATE TABLE Menu
(	ID INT IDENTITY(1, 1) PRIMARY KEY,
	ParentID INT,
	TieuDe NVARCHAR(100),
	MenuUrl NVARCHAR(100),
	MenuIndex INT,
	IsVisible BIT DEFAULT 1
)
INSERT INTO [dbo].[LoaiSach] VALUES (N'Văn học', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO [dbo].[LoaiSach] VALUES (N'Truyện trinh thám', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO [dbo].[LoaiSach] VALUES (N'Lịch sử', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO [dbo].[LoaiSach] VALUES (N'Sinh học', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO [dbo].[LoaiSach] VALUES (N'Khoa học viễn tưởng', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO [dbo].[LoaiSach] VALUES (N'Tiểu thuyết lãng mạn', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))

INSERT INTO dbo.TacGia VALUES (N'Nguyễn Du', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Tô Hoài', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Nguyễn Nhật Ánh', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Victor Hugo', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Trần Trọng Kim', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Dũng Phan', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Trang Quan Sen', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'William Gibson', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Kazuo Ishiguro', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Murakami Hazuki', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Emily Bronte', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Arthur Conan Doyle', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.TacGia VALUES (N'Agatha Christine', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))

INSERT INTO dbo.SanPham VALUES (N'Truyện Kiều', 1, 1, N'Truyện Kiều gồm 3254 câu thơ lục bát dựa vào cốt truyện Kim Vân Kiều truyện của Thanh Tâm Tài Nhân ở thời nhà Thanh, Trung Quốc. Nội dung của Truyện Kiều có giá trị hết sức sâu sắc, đó là giá trị tố cáo hiện thực, lên án xã hội phong kiến mục nát, lên án mặt trái của đồng tiền hôi tanh, quyền sống và hạnh con người bị những thế lực hắc ám, tàn bạo chà đạp dã man như bọn quan lại tham ô thối nát, bọn ma cô lưu manh tàn ác, bọn buôn thịt bán người.', N'truyen-kieu.jpg', 35000, 51, 5.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Dế Mèn phiêu lưu ký', 2, 1, N'Tác phẩm miêu tả cuộc phiêu lưu của một chú Dế Mèn qua thế giới loài vật và loài người. Mèn đã trải qua những cuộc phiêu lưu vào thế giới các loài vật, vượt qua cơ man nào là rủi ro và biến cố, nhưng từng bước Mèn vươn lên tự điều chỉnh, tự nhận thức để trở thành con người giàu lí tưởng và ham hiểu biết với bản lĩnh kiên cường của một trai tráng đầu đội trời chân đập đất.', N'de-men-phieu-luu-ky.jpg', 50000, 26, 5.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Kính vạn hoa (Tập 1)', 3, 1, N'"Kính Vạn Hoa" là một trong những bộ truyện thiếu nhi nổi tiếng và được yêu thích nhất tại Việt Nam, do nhà văn Nguyễn Nhật Ánh sáng tác. Bộ truyện là một bức tranh sinh động về cuộc sống của những cô cậu học trò với những niềm vui, nỗi buồn, những ước mơ và những bài học ý nghĩa. "Kính Vạn Hoa" không đơn thuần chỉ là những câu chuyện kể về cuộc sống học đường. Bộ truyện còn là một cuốn nhật ký ghi lại những suy nghĩ, cảm xúc, những khám phá và trưởng thành của những nhân vật tuổi teen.', N'kinh-van-hoa.jpg', 135000, 33, 4.8, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Tôi thấy hoa vàng trên cỏ xanh', 3, 1, N'"Tôi thấy hoa vàng trên cỏ xanh" kể về tuổi thơ nghèo khó của hai anh em Thiều và Tường cùng cô bạn thân hàng xóm. Mạch truyện tự nhiên, dẫn dắt người đọc chứng kiến những rung động đầu đời của tụi nhỏ, xen vào đó là những nét đẹp của tình anh em và vài nốt trầm của sự đau đớn khi trưởng thành. Truyện Nguyễn Nhật Ánh thường không nói quá nhiều về trắng đen, thiện ác nhưng trong tác phẩm này, tác giả đã đưa vấn đề đạo đức vào sách và khiến người đọc suy ngẫm.', N'toi-thay-hoa-vang-tren-co-xanh.jpg', 150000, 36, 4.8, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Mắt biếc', 3, 1, N'"Mắt biếc" kể về câu chuyện tình yêu đơn phương đầy xúc động của Ngạn, một chàng trai sống ở làng Đo Đo, dành tình cảm cho cô bạn gái Hà Lan xinh đẹp với đôi mắt xanh biếc. Tình yêu của Ngạn là sự trân trọng và chung thủy nhưng đầy đau khổ, khi Hà Lan không đáp lại. Tuy cuối cùng có sự gắn kết bởi con gái của Hà Lan, nhưng mối tình này vẫn dang dở, khi Ngạn phải rời xa quê hương và tình đầu. Cuốn sách đã chân thực và sâu sắc khắc họa những cung bậc cảm xúc của tuổi trẻ và tình yêu.', N'mat-biec.jpg', 110000, 22, 4.9, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Những người khốn khổ (Trọn bộ)', 4, 1, N'Tác phẩm là một câu chuyện đặc sắc xoay quanh những nhân vật sống trong những điều kiện khốn khổ và bất công ở Pháp thế kỷ 19. Không chỉ phản ánh về cảnh đời éo le, nghèo đói và bất công của những người dân tầng lớp thấp, tiểu thuyết "Những người khốn khổ" còn thể hiện sâu sắc về tình nhân ái, lòng tốt của người đối với người, khát khao sống mãnh liệt luôn tồn tại trong tâm hồn, cho dù sự bất hạnh đang đè nặng lên đôi vai họ.', N'nhung-nguoi-khon-kho.jpg', 650000, 15, 5.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Nhà thờ Đức bà Paris', 4, 6, N'Bằng cốt truyện khá bi thảm, nặng nề, kết thúc là những vụ chết không kém rùng rợn; bằng ngòi bút miêu tả thật rực rỡ, kỳ thú cũng như các tình tiết được xếp đặt một cách khéo léo, mang kịch tính và hình ảnh tô đậm, phóng đại, lẫn lộn thực hư, "Nhà thờ Đức Bà Paris" đã phục hồi lại không khí xa xưa của một thời trung cổ đen tối. Đây được xem là kiệt tác của loại tiểu thuyết lịch sử thời kì lãng mạn chủ nghĩa.', N'nha-tho-duc-ba-paris.jpg', 650000, 17, 4.5, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Việt Nam Sử Lược', 5, 3, N'"Việt Nam sử lược" – tác phẩm của nhà sử học Trần Trọng Kim, lần đầu ra mắt bạn đọc vào năm 1920. Cuốn sách này đã được tái bản nhiều lần cho đến hiện tại và thậm chí cuốn sách này còn được sử dụng làm sách giáo khoa trong một thời gian dài. Đây là cuốn sách lịch sử Việt Nam đầu tiên được viết bằng chữ quốc ngữ, cung cấp cho bạn đọc một cái nhìn tổng quan về lịch sử đất nước ta cho đến thời Pháp thuộc.', N'viet-nam-su-luoc.jpg', 180000, 89, 5.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Sử Việt - 12 khúc tráng ca', 6, 3, N'Nội dung sách kể về 12 câu chuyện dựng nước và giữ nước thời phong kiến, được chọn lọc theo tính chất quan trọng và hùng tráng trong dòng chảy lịch sử Việt Nam. Cuốn sách chính là sự kết hợp của những tư liệu lịch sử đã được kiểm chứng, xen kẽ với các nhận định và đánh giá riêng của tác giả Dũng Phan. Những câu chuyện được kể lại đầy hấp dẫn bằng một cách tiếp cận hoàn toàn mới, không phải như tiểu thuyết dã sử, nhưng cũng không phải là một tài liệu chuyên khảo khô khan.', N'su-viet-12-khuc-trang-ca.jpg', 119000, 85, 5.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Mendel và cây đậu vườn', 7, 4, N'Cuốn sách không chỉ giới thiệu về cuộc đời và sự nghiệp khoa học của Mendel mà còn nói về các phương pháp nghiên cứu khoa học của ông cũng như việc kết quả nghiên cứu của ông đã mang đến cho ngành sinh học một bước tiến cực kỳ quan trọng.', N'mendel-va-cay-dau-vuon.jpg', 50000, 20, 3.8, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Necromancer', 8, 5, N'"Neuromancer" được coi là một tác phẩm tiên phong trong thể loại cyberpunk, khám phá mối quan hệ giữa con người và công nghệ. Tác phẩm đã ảnh hưởng mạnh mẽ đến nhiều lĩnh vực văn hóa đại chúng, từ văn học đến điện ảnh và video game. Tác phẩm của William Gibson không chỉ vẽ nên một tương lai đen tối nhưng đầy phong phú, mà còn đặt ra những câu hỏi sâu sắc về bản chất của con người trong thế giới số hóa và trí tuệ nhân tạo.', N'necromancer.jpg', 128000, 18, 4.2, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Never Let Me Go', 9, 5, N'Cuốn tiểu thuyết kể về cuộc sống của ba người bạn, Kathy, Ruth và Tommy, lớn lên trong một trường nội trú biệt lập ở Anh tên là Hailsham. Khi đối mặt với những thách thức của tuổi thiếu niên và chuẩn bị cho vai trò cuối cùng là người hiến tạng, họ bắt đầu khám phá ra sự thật về sự tồn tại của mình và xã hội đã tạo ra họ cũng như những người nhân bản khác.', N'never-let-me-go.jpg', 337000, 15, 4.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Rừng Na Uy', 10, 6, N'"Rừng Na Uy" không chỉ đơn thuần là câu chuyện tình yêu mà còn là một tác phẩm mang đậm tính triết lý về những vấn đề phức tạp trong cuộc sống như cô đơn, cái chết, và sự trưởng thành. Haruki Murakami đã viết ra một cuốn tiểu thuyết với cảm xúc mãnh liệt, nhưng cũng không kém phần tinh tế, khiến cho người đọc cảm nhận sâu sắc về sự mong manh của con người trong một thế giới phức tạp.', N'rung-na-uy.jpg', 180000, 47, 4.3, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Đồi gió hú', 11, 6, N'"Đồi gió hú" được lấy bối cảnh ở vùng đồng cỏ hoang vắng xứ Yorkshire. Câu chuyện xoay quanh cuộc đời của Catherine Earnshaw và Heathcliff, hai con người có xuất thân khác nhau. Catherine là một cô gái xinh đẹp, giàu có, còn Heathcliff là một cậu bé mồ côi, nghèo khổ. Hai người đã yêu nhau từ khi còn nhỏ, nhưng tình yêu của họ đã phải trải qua nhiều sóng gió. Tác phẩm đã mang đến cho người đọc nhiều cảm xúc sâu sắc và những bài học ý nghĩa về cuộc sống.', N'doi-gio-hu.jpg', 138000, 38, 4.0, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Sherlock Holmes tập 5: Những hình nhân nhảy múa', 12, 2, N'Tại trang viên Riding Thorpe, hạt Norfolk, nước Anh - nơi có tổ ấm hạnh phúc của vợ chồng Hilton Cubitt và Elsie Cubitt. Thế rồi, cuộc sống của họ bắt đầu bị xáo trộn kể từ khi có sự xuất hiện của những hình nhân lạ. Ban đầu là những bức vẽ hình người nhảy múa trên cửa sổ rồi chúng lại tiếp tục xuất hiện trên các bức tượng và cả bên hông nhà vào những ngày sau đó...', N'sherlock-holmes-nhung-hinh-nhan-nhay-mua.jpg', 32000, 10, 4.2, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Án mạng trên sông Nile', 13, 2, N'"Án mạng trên sông Nile" là một cuốn tiểu thuyết hình sự của nhà văn Agatha Christie được hãng Collins Crime Club xuất bản lần đầu tại Anh ngày 1 tháng 11 năm 1937. Tác phẩm nói về vụ án mạng xảy ra tại một con tàu du lịch trên sông Nile trong thời gian thám tử Hercule Poirot đi nghỉ ở Ai Cập. Đây là một trong những tiểu thuyết được ưa thích nhất của Agatha Christie, nhiều lần được chuyển thể thành kịch và phim điện ảnh.', N'an-mang-tren-song-nile.jpg', 135000, 78, 4.1, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'Án mạng trên chuyến tàu tốc hành phương Đông', 13, 2, N'Một tên giết người trên chuyến tàu tốc hành Phương Đông được phát hiện đã bị giết chết bởi 12 nhát dao khác nhau. Phải chăng hung thủ là người ngoài hay chính 12 hành khách - vốn có mối thù với hắn - thông đồng với nhau để trả thù? Thám tử Poirot đã ra tay và vụ án dần dần được làm sáng tỏ. Kết quả thu được không chỉ đơn thuần là câu trả lời cho vấn đề hung thủ là ai, mà chính là lương tâm của con người trước ánh sáng của công lí.', N'an-mang-tren-chuyen-tau-toc-hanh-phuong-dong.jpg', 120000, 58, 4.2, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))
INSERT INTO dbo.SanPham VALUES (N'The Valley of Fear', 12, 2, N'"Thung lũng khủng khiếp" (The Valley of Fear) là một tiểu thuyết trinh thám của Arthur Conan Doyle, với sự tham gia của thám tử lừng danh Sherlock Holmes. Câu chuyện bắt đầu khi Holmes nhận được một bức điện báo mật mã cảnh báo về một vụ án sắp xảy ra. Họ điều tra vụ án mạng của John Douglas, một người đàn ông sống ở Birlstone Manor, bị bắn chết bởi một kẻ lạ mặt...', N'the-valley-of-fear.jpg', 50000, 14, 3.8, CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime))

INSERT INTO dbo.TaiKhoan ([Ho], [Ten], [DiaChi], [SoDienThoai], [Email], [Anh], [ThoiGianDangKy], [ThoiGianCapNhat], [NgaySinh], [MatKhau], [TrangThai], [VaiTro]) VALUES 
(N'Admin', N'Võ', N'Nha Trang', N'0819202180', N'admin@gmail.com', N'avt.img', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2004-03-25' AS Date), N'12345', 1, 1)
INSERT INTO dbo.TaiKhoan ([Ho], [Ten], [DiaChi], [SoDienThoai], [Email], [Anh], [ThoiGianDangKy], [ThoiGianCapNhat], [NgaySinh], [MatKhau], [TrangThai], [VaiTro]) VALUES 
(N'Thành', N'Võ', N'Nha Trang', N'0707733029', N'thanh@gmail.com', N'avt.img', CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2024-12-15T14:33:29.983' AS DateTime), CAST(N'2004-03-25' AS Date), N'12345', 1, 0)

INSERT INTO dbo.Menu VALUES (NULL, N'Trang chủ', N'/', 1, 1)
INSERT INTO dbo.Menu VALUES (NULL, N'Cửa hàng', N'/Product', 2, 1)
INSERT INTO dbo.Menu VALUES (NULL, N'Danh mục', NULL, 3, 1)
INSERT INTO dbo.Menu VALUES (3, N'Thể loại', NULL, 4, 1)
INSERT INTO dbo.Menu VALUES (4, N'Văn học', N'/Product?idcategory=1', 5, 1)
INSERT INTO dbo.Menu VALUES (4, N'Truyện trinh thám', N'/Product?idcategory=2', 6, 1)
INSERT INTO dbo.Menu VALUES (4, N'Lịch sử', N'/Product?idcategory=3', 7, 1)
INSERT INTO dbo.Menu VALUES (4, N'Sinh học', N'/Product?idcategory=4', 8, 1)
INSERT INTO dbo.Menu VALUES (4, N'Khoa học viễn tưởng', N'/Product?idcategory=5', 9, 1)
INSERT INTO dbo.Menu VALUES (4, N'Tiểu thuyết lãng mạn', N'/Product?idcategory=6', 10, 1)
INSERT INTO dbo.Menu VALUES (3, N'Tác giả', NULL, 11, 1)
INSERT INTO dbo.Menu VALUES (11, N'Nguyễn Du', N'/Product?idAuthor=1', 12, 1)
INSERT INTO dbo.Menu VALUES (11, N'Tô Hoài', N'/Product?idAuthor=2', 13, 1)
INSERT INTO dbo.Menu VALUES (11, N'Nguyễn Nhật Ánh', N'/Product?idAuthor=3', 14, 1)
INSERT INTO dbo.Menu VALUES (11, N'Victor Hugo', N'/Product?idAuthor=4', 15, 1)
INSERT INTO dbo.Menu VALUES (11, N'Trần Trọng Kim', N'/Product?idAuthor=5', 16, 1)
INSERT INTO dbo.Menu VALUES (11, N'Dũng Phan', N'/Product?idAuthor=6', 17, 1)
INSERT INTO dbo.Menu VALUES (11, N'Trang Quan Sen', N'/Product?idAuthor=7', 18, 1)
INSERT INTO dbo.Menu VALUES (11, N'William Gibson', N'/Product?idAuthor=8', 19, 1)
INSERT INTO dbo.Menu VALUES (11, N'Kazuo Ishiguro', N'/Product?idAuthor=9', 20, 1)
INSERT INTO dbo.Menu VALUES (11, N'Murakami Hazuki', N'/Product?idAuthor=10', 21, 1)
INSERT INTO dbo.Menu VALUES (11, N'Emily Bronte', N'/Product?idAuthor=11', 22, 1)
INSERT INTO dbo.Menu VALUES (11, N'Agatha Conan Doyle', N'/Product?idAuthor=12', 23, 1)
INSERT INTO dbo.Menu VALUES (11, N'Agatha Christine', N'/Product?idAuthor=13', 24, 1)
INSERT INTO dbo.Menu VALUES (NULL, N'Liên hệ', N'/Contact', 25, 1)
INSERT INTO dbo.Menu VALUES (NULL, N'Trang quản trị', N'/Admin/HomeAdmin', 26, 1)
