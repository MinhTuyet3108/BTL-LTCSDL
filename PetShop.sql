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
	[Password][NVARCHAR](50) NOT NULL,
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
[PrName][NVARCHAR](100) NOT NULL,
[Price][DECIMAL](18, 2) NOT NULL,
[Stock][INT] NOT NULL,
[Category][NVARCHAR](100)NOT NULL,
CONSTRAINT PK_Product PRIMARY KEY (ProductID)
);
GO
-- TẠO BẢNG [dbo].[Service]
CREATE TABLE [dbo].[Service] (
[ServiceID][INT] NOT NULL,
[ServiceName][NVARCHAR](255) NOT NULL,
[Price][DECIMAL](10, 2) NOT NULL,
CONSTRAINT PK_Service PRIMARY KEY (ServiceID)
);
GO

-- TẠO BẢNG [dbo].[Cash]
CREATE TABLE [dbo].[Cash] (
[CashID][VARCHAR](10) NOT NULL,
[Transno][VARCHAR](20) NOT NULL,
[Pcode][VARCHAR](15) NULL,
[Pname][VARCHAR](50)  NULL,
[Qty][int] NULL,
[Price][DECIMAL](18, 2) NOT NULL,
[Total][DECIMAL](18, 2) NULL,
[Cid][VARCHAR](10)  NULL,
[Cashier][NVARCHAR](20) NULL,
CONSTRAINT PK_Cash PRIMARY KEY (CashID)
);
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
    ('CUST005', N'Hoàng', N'Thị Hoa', 'Female', '0945678901', N'12 Trần Phú, Nha Trang', 'hoa.hoang@email.com');

--[Employee]
INSERT INTO [dbo].[Employee] (EmployeeID, LastName, FirstName, Position, Salary, Phone, Username, Password)
VALUES 
    ('EMP001', N'Nguyễn', N'Thanh Bình', N'Quản lý', 15000000.00, '0987654321', 'binhnguyen', 'pass123'),
    ('EMP002', N'Trần', N'Văn Cường', N'Nhân viên bán hàng', 8000000.00, '0976543210', 'cuongtran', 'pass456'),
    ('EMP003', N'Lê', N'Thị Dung', N'Nhân viên chăm sóc', 7000000.00, '0965432109', 'dungle', 'pass789'),
    ('EMP004', N'Phạm', N'Hồng Đức', N'Kế toán', 10000000.00, '0954321098', 'ducpham', 'pass101'),
    ('EMP005', N'Hoàng', N'Minh Hùng', N'Nhân viên bán hàng', 7500000.00, '0943210987', 'hunghoang', 'pass202');


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
    ( N'Ki', N'Chó Corgi',  8000000.00, N'Khỏe mạnh', NULL),
    ( N'Bông', N'Mèo Anh', 6000000.00, N'Tốt', 'CUST004'),
    ( N'Vàng', N'Chó Shiba', 9000000.00, N'Khỏe mạnh', NULL);


--[Product]
INSERT INTO [dbo].[Product] (ProductID, PrName, Price, Stock, Category)
VALUES 
    (1, N'Thức ăn cho chó', 500000.00, 20, 'Chó'),
    (2, N'Sữa tắm cho mèo', 150000.00, 15, 'Mèo'),
    (3, N'Đồ chơi bóng cao su', 80000.00, 30, 'Chó'),
    (4, N'Chuồng sắt cho thú cưng', 1200000.00, 10, 'Chó'),
    (5, N'Thức ăn cho mèo', 300000.00, 25, 'Mèo');


--[Service]
INSERT INTO [dbo].[Service] (ServiceID, ServiceName, Price)
VALUES 
    (1, N'Cắt tỉa lông', 200000.00),
    (2, N'Tắm thú cưng', 150000.00),
    (3, N'Khám sức khỏe', 300000.00),
    (4, N'Trông giữ thú cưng (1 ngày)', 250000.00),
    (5, N'Huấn luyện cơ bản', 1000000.00);
