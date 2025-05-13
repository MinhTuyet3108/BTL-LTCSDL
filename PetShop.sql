IF DB_ID('PetShop') IS NULL 
	CREATE DATABASE PetShop
GO

USE PetShop
GO

CREATE TABLE [dbo].[Customer](
	 [CustomerID][VARCHAR](10) NOT NULL,
    [LastName][NVARCHAR](50) NOT NULL,
    [FirstName][NVARCHAR](20) NOT NULL,
    [Gender][VARCHAR](10) NOT NULL,
    [Phone][VARCHAR](10) NOT NULL,
    [Address][NVARCHAR](255) NOT NULL,
    [Email][NVARCHAR](50) NULL,
    CONSTRAINT PK_Customer PRIMARY KEY (CustomerID)
	)
GO

CREATE TABLE [dbo].[Employee] (
    [EmployeeID][VARCHAR](10) NOT NULL,
    [LastName][NVARCHAR](50) NOT NULL,
    [FirstName][NVARCHAR](20) NOT NULL,
    [Position][NVARCHAR](50) NOT NULL,
    [Salary][DECIMAL](18, 2) NOT NULL, 
    [Phone][VARCHAR](10) NOT NULL,
    [Username][NVARCHAR](50) NOT NULL,
	[Password][NVARCHAR](256) NOT NULL,
    CONSTRAINT PK_Employee PRIMARY KEY (EmployeeID)
);
GO


-- TẠO BẢNG [dbo].[Appointment]
CREATE TABLE [dbo].[Appointment] (
[AppointmentID][INT] NOT NULL,
[CustomerID][VARCHAR](10) NOT NULL,
[AppointmentDate][DATETIME] NOT NULL,
CONSTRAINT PK_Appointment PRIMARY KEY (AppointmentID)
);
GO

-- TẠO BẢNG [dbo].[Pet]
CREATE TABLE [dbo].[Pet] (
[PetID][INT]IDENTITY(1,1) NOT NULL,
[PetName][NVARCHAR](50) NOT NULL,
[Type][NVARCHAR](50) NOT NULL,
[Price][DECIMAL](10, 2) NOT NULL, 
[HealthStatus][NVARCHAR](100) NOT NULL,
[CustomerID][VARCHAR](10) NULL, -- Cho phép NULL vì có thể thú cưng chưa bán
CONSTRAINT PK_Pet PRIMARY KEY (PetID)
-- Khóa ngoại sẽ được thêm sau
);
GO
-- TẠO BẢNG [dbo].[Product]
CREATE TABLE [dbo].[Product] (
[ProductID][VARCHAR](10) NOT NULL,
[PrName][NVARCHAR](50) NOT NULL,
[Price][DECIMAL](18, 2) NOT NULL,
[Stock][INT] NOT NULL,
[Category][NVARCHAR](100)NOT NULL,
CONSTRAINT PK_Product PRIMARY KEY (ProductID)
);
GO

-- TẠO BẢNG [dbo].[Service]
CREATE TABLE [dbo].[Service] (
[ServiceID][INT] IDENTITY(1,1) NOT NULL,
[ServiceName][NVARCHAR](255) NOT NULL,
[Price][DECIMAL](10, 2) NOT NULL,
CONSTRAINT PK_Service PRIMARY KEY (ServiceID)
);
GO

-- TẠO BẢNG [dbo].[Cash]
CREATE TABLE [dbo].[Cash] (
[CashID][VARCHAR](10) NOT NULL,
[Transno][VARCHAR](20) NOT NULL,
[Pcode][VARCHAR](10) NOT NULL,
[Pname][VARCHAR](50)  NOT NULL,
[Qty][int] NOT NULL,
[Price][DECIMAL](18, 2) NOT NULL,
[Total][DECIMAL](18, 2) NOT NULL,
[Cid][VARCHAR](10)  NOT NULL,
[Cashier][NVARCHAR](50) NOT NULL,
[Date][DATETIME],
CONSTRAINT PK_Cash PRIMARY KEY (CashID)
);
GO

ALTER TABLE [dbo].[Cash] 
ADD CONSTRAINT FK_Cash_Customer FOREIGN KEY (Cid)
REFERENCES [dbo].[Customer] (CustomerID) 
GO

ALTER TABLE [dbo].[Cash] 
ADD CONSTRAINT FK_Cash_Product FOREIGN KEY (Pcode)
REFERENCES [dbo].[Product] (ProductID) 
GO

ALTER TABLE [dbo].[Appointment] 
ADD CONSTRAINT FK_Appointment_Customer FOREIGN KEY (CustomerID)
REFERENCES [dbo].[Customer] (CustomerID) 
GO

ALTER TABLE [dbo].[Pet] 
ADD CONSTRAINT FK_Pet_Customer FOREIGN KEY (CustomerID)
REFERENCES [dbo].[Customer] (CustomerID) 
GO


------------------------------Thêm dữ liệu cho từng bảng-----------------------------------
--[Customer]
INSERT INTO [dbo].[Customer] (CustomerID, LastName, FirstName, Gender, Phone, Address, Email)
VALUES 
    ('CUST001', N'Nguyễn', N'Văn An', 'Male', '0901234567', N'123 Đường Láng, Hà Nội', 'an.nguyen@email.com'),
    ('CUST002', N'Trần', N'Thị Bình', 'Female', '0912345678', N'45 Lê Lợi, TP.HCM', 'binh.tran@email.com'),
    ('CUST003', N'Lê', N'Hồng Cường', 'Male', '0923456789', N'78 Hùng Vương, Đà Nẵng', NULL),
    ('CUST004', N'Phạm', N'Minh Đức', 'Male', '0934567890', N'56 Nguyễn Huệ, Huế', 'duc.pham@email.com'),
    ('CUST005', N'Hoàng', N'Thị Hoa', 'Female', '0945678901', N'12 Trần Phú, Nha Trang', 'hoa.hoang@email.com'),
	('CUST006', N'Ngô', N'Thị F', 'Female', '0906789012', N'987 Bà Triệu, Hà Nội', 'thif@example.com'),
    ('CUST007', N'Đặng', N'Văn G', 'Male', '0907890123', N'147 Lý Thường Kiệt, TP.HCM', 'vang@example.com'),
    ('CUST008', N'Bùi', N'Thị H', 'Female', '0908901234', N'258 Tôn Đức Thắng, Đà Nẵng', 'thih@example.com'),
    ('CUST009', N'Vũ', N'Văn I', 'Male', '0909012345', N'369 Nguyễn Văn Cừ, Hà Nội', 'vani@example.com'),
    ('CUST010', N'Đỗ', N'Thị K', 'Female', '0900123456', N'741 Hai Bà Trưng, TP.HCM', 'thik@example.com');

--[Employee]
INSERT INTO [dbo].[Employee] (EmployeeID, LastName, FirstName, Position, Salary, Phone, Username, Password)
VALUES 
    ('EMP001', N'Nguyễn', N'Thanh Bình', N'Quản lý', 15000000.00, '0987654321', 'binhnguyen', 'pass123'),
    ('EMP002', N'Trần', N'Văn Cường', N'Nhân viên bán hàng', 8000000.00, '0976543210', 'cuongtran', 'pass456'),
    ('EMP003', N'Lê', N'Thị Thùy Dung', N'Nhân viên chăm sóc', 7000000.00, '0965432109', 'dungle', 'pass789'),
    ('EMP004', N'Phạm', N'Hồng Minh Đức', N'Kế toán', 10000000.00, '0954321098', 'ducpham', 'pass101'),
    ('EMP005', N'Hoàng', N'Minh Hùng', N'Nhân viên bán hàng', 7500000.00, '0943210987', 'hunghoang', 'pass202'),
	('EMP006', N'Ngô', N'Thị Mai', N'Nhân viên chăm sóc', 7200000.00, '0931234567', 'maingo', 'pass303'),
    ('EMP007', N'Đặng', N'Văn Nam', N'Nhân viên bán hàng', 7800000.00, '0912345678', 'namdang', 'pass404'),
    ('EMP008', N'Bùi', N'Thị Ngọc Lan', N'Quản lý', 9000000.00, '0923456789', 'lanbui', 'pass505'),
    ('EMP009', N'Vũ', N'Hồng Nhung', N'Kế toán', 9500000.00, '0945678901', 'nhungvu', 'pass606'),
    ('EMP010', N'Đỗ', N'Quang Vinh', N'Nhân viên chăm sóc', 8500000.00, '0956789012', 'vinhdo', 'pass707');


--[Appointment]
INSERT INTO [dbo].[Appointment] (AppointmentID, CustomerID, AppointmentDate)
VALUES 
    (1, 'CUST001', '2025-04-08 10:00:00'),
    (2, 'CUST002', '2025-04-08 14:30:00'),
    (3, 'CUST003', '2025-04-09 09:15:00'),
    (4, 'CUST004', '2025-04-09 15:45:00'),
    (5, 'CUST005', '2025-04-10 11:00:00');


--[Pet]
INSERT INTO [dbo].[Pet] ( PetName, Type, Price, HealthStatus, CustomerID)
VALUES 
    ( N'Mun', N'Chó Poodle', 5000000.00, N'Khỏe mạnh', 'CUST001'),
    ( N'Miu', N'Mèo Ba Tư', 7000000.00, N'Khỏe mạnh', 'CUST002'),
	(N'Sky', N'Chim Chào Mào', 800000.00, N'Khỏe mạnh', NULL),
    (N'Goldie', N'Cá Vàng', 200000.00, N'Bình thường', NULL),
	(N'Zoe', N'Mèo Ragdoll', 8000000.00, N'Khỏe mạnh', 'CUST008'),
    (N'Tiger', N'Chó Shiba', 9500000.00, N'Khỏe mạnh', NULL),
    ( N'Ki', N'Chó Corgi',  8000000.00, N'Khỏe mạnh', NULL),
    ( N'Bông', N'Mèo Anh', 6000000.00, N'Tốt', 'CUST004'),
    ( N'Vàng', N'Chó Shiba', 9000000.00, N'Khỏe mạnh', NULL),
	(N'Rex', N'Chó Husky', 10000000.00, N'Khỏe mạnh', 'CUST007'),
    (N'Tweety', N'Chim Vẹt', 2000000.00, N'Tốt', 'CUST009'),
    (N'Nemo', N'Cá Hề', 500000.00, N'Bình thường', 'CUST010'),
    (N'Buddy', N'Chó Golden', 9000000.00, N'Khỏe mạnh', NULL),
    (N'Mimi', N'Mèo Munchkin', 6000000.00, N'Tốt', NULL),
    (N'Fluffy', N'Mèo Maine Coon', 8500000.00, N'Cần chăm sóc', 'CUST005'),
    (N'Parrot', N'Chim Vẹt Xám', 2500000.00, N'Khỏe mạnh', NULL),
    (N'Sharky', N'Cá Koi', 1500000.00, N'Tốt', NULL),
    (N'Max', N'Chó Pug', 7000000.00, N'Bình thường', NULL);


--[Product]
INSERT INTO [dbo].[Product] (ProductID, PrName, Price, Stock, Category)
VALUES 
    ('1', N'Thức ăn cho chó', 500000.00, 20, 'Chó'),
    ('2', N'Sữa tắm cho mèo', 150000.00, 15, 'Mèo'),
    ('3', N'Đồ chơi bóng cao su', 80000.00, 30, 'Chó'),
    ('4', N'Chuồng sắt cho thú cưng', 1200000.00, 10, 'Chó'),
    ('5', N'Thức ăn cho mèo', 300000.00, 25, 'Mèo'),
	('6', N'Thức ăn cho cá', 150000.00, 25, N'Cá'),
    ('7', N'Hạt giống cho chim', 100000.00, 30, N'Chim'),
    ('8', N'Cát vệ sinh cho mèo', 200000.00, 20, N'Mèo'),
    ('9', N'Lồng chim bằng tre', 300000.00, 15, N'Chim'),
    ('10', N'Bể cá mini', 800000.00, 10, N'Cá'),
    ('11', N'Dây dắt chó', 120000.00, 25, N'Chó'),
    ('12', N'Xịt khử mùi cho thú cưng', 180000.00, 18, N'Chó'),
    ('13', N'Đồ chơi gặm xương cho chó', 90000.00, 30, N'Chó');


--[Service]
INSERT INTO [dbo].[Service] ( ServiceName, Price)
VALUES 
    (N'Cắt tỉa lông', 200000.00),
    (N'Tắm thú cưng', 150000.00),
    (N'Khám sức khỏe', 300000.00),
    (N'Trông giữ thú cưng (1 ngày)', 250000.00),
    (N'Huấn luyện cơ bản', 1000000.00);
