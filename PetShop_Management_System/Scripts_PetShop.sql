USE [PetShop]
GO


--NHÂN VIÊN
--LẤY DANH SÁCH NHÂN VIÊN
CREATE PROCEDURE [dbo].[uspGetEmployees]
AS
BEGIN
    SELECT * FROM Employee;
END
GO

--THÊM NHÂN VIÊN
CREATE PROC [dbo].[uspAddEmployee]
	@employeeID varchar(10),
	@lastName nvarchar(50),
	@firstName nvarchar(20),
	@position nvarchar(50),
	@salary decimal(18,2),
	@phone varchar(10),
	@username nvarchar(50),
	@password nvarchar(50)

AS 
BEGIN

INSERT INTO Employee(EmployeeID,LastName, FirstName, Position, Salary, Phone, Username, Password)
	VALUES (@employeeID, @lastName, @firstName, @position, @salary, @phone, @username, @password);

END
GO

--XÓA NHÂN VIÊN
CREATE PROC [dbo].[uspDeleteEmployee]
    @employeeID varchar(10)
AS 
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Employee WHERE EmployeeID = @employeeID;
END
GO

--TÌM KIẾM NHÂN VIÊN
CREATE PROCEDURE [dbo].[uspSearchEmployee]
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Employee
    WHERE CONCAT( employeeID, lastName, firstName, position, salary, phone, username, password) 
          LIKE @Keyword
END
GO

--CHỈNH SỬA NHÂN VIÊN
CREATE PROCEDURE [dbo].[uspUpdateEmployee]
    @EmployeeID varchar(10),
    @LastName NVARCHAR(50),
    @FirstName NVARCHAR(20),
    @Position NVARCHAR(50),
    @Salary decimal(18,2),
    @Phone varchar(10),
    @Username NVARCHAR(50),
    @Password NVARCHAR(50)
AS
BEGIN
    UPDATE Employee
    SET LastName = @LastName,
        FirstName = @FirstName,
        Position = @Position,
        Salary = @Salary,
        Phone = @Phone,
        Username = @Username,
        Password = @Password
    WHERE EmployeeID = @EmployeeID;
END
GO

--SẢN PHẨM
--LẤY DANH SÁCH SẢN PHẨM
CREATE PROCEDURE [dbo].[uspGetProducts]
AS
BEGIN
    SELECT * FROM Product;
END
GO

--THÊM SẢN PHẨM
CREATE PROC [dbo].[uspAddProduct]
    @ProductID varchar(20),
	@PrName nvarchar(100),
	@Price decimal(18,2),
	@Stock int,
	@Category nvarchar(100)

AS 
BEGIN

INSERT INTO Product(ProductID,PrName, Price, Stock, Category)
	VALUES (@ProductID, @PrName, @Price, @Stock, @Category);

END
GO


--XÓA SẢN PHẨM
CREATE PROC [dbo].[uspDeleteProduct]
    @ProductID int
AS 
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Product WHERE ProductID = @ProductID;
END
GO


--TÌM KIẾM SẢN PHẨM
CREATE PROCEDURE [dbo].[uspSearchProduct]
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Product
    WHERE CONCAT( productID, prName, price, stock,category) 
          LIKE @Keyword
END
GO

--CHỈNH SỬA SẢN PHẨM
CREATE PROCEDURE [dbo].[uspUpdateProduct]
	@ProductID int,
	@PrName nvarchar(100),
	@Price decimal(18,2),
	@Stock int,
	@Category nvarchar(100)
AS
BEGIN
    UPDATE Product
    SET PrName = @PrName,
        Price = @Price,
        Stock = @Stock,
        Category = @Category
    WHERE ProductID = @ProductID;
END
GO

-- CUSTOMER VÀ DASHBOARD

-- Lấy tất cả khách hàng
CREATE PROCEDURE uspGetAllCustomers
AS
BEGIN
    SELECT * FROM Customer
END
GO

-- Thêm khách hàng
CREATE PROCEDURE uspAddCustomer
    @CustomerID NVARCHAR(10),
    @LastName NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @Gender NVARCHAR(10),
    @Phone NVARCHAR(15),
    @Address NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    INSERT INTO Customer (CustomerID, LastName, FirstName, Gender, Phone, Address, Email)
    VALUES (@CustomerID, @LastName, @FirstName, @Gender, @Phone, @Address, @Email)
END
GO

-- Cập nhật thông tin khách hàng
CREATE PROCEDURE uspUpdateCustomer
    @CustomerID NVARCHAR(10),
    @LastName NVARCHAR(50),
    @FirstName NVARCHAR(50),
    @Gender NVARCHAR(10),
    @Phone NVARCHAR(15),
    @Address NVARCHAR(100),
    @Email NVARCHAR(100)
AS
BEGIN
    UPDATE Customer SET 
        LastName = @LastName,
        FirstName = @FirstName,
        Gender = @Gender,
        Phone = @Phone,
        Address = @Address,
        Email = @Email
    WHERE CustomerID = @CustomerID
END
GO

-- Xóa khách hàng
CREATE PROCEDURE uspDeleteCustomer
    @CustomerID NVARCHAR(10)
AS
BEGIN
    DELETE FROM Customer WHERE CustomerID = @CustomerID
END
GO

-- Tìm kiếm khách hàng
CREATE PROCEDURE uspSearchCustomers
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Customer
    WHERE CustomerID LIKE @Keyword
       OR LastName LIKE @Keyword
       OR FirstName LIKE @Keyword
       OR Gender LIKE @Keyword
       OR Phone LIKE @Keyword
       OR Address LIKE @Keyword
       OR Email LIKE @Keyword
END
GO



-- Lấy số lượng thú cưng theo loại.
--Create Proc uspGetPetTypeQT
--AS
--BEGIN
--    SELECT LOWER(LTRIM(RTRIM(Category))) AS Category, SUM(Qty) AS TotalQty 
--    FROM Pet 
--    WHERE CustomerID IS NULL AND Qty > 0
--    GROUP BY LOWER(LTRIM(RTRIM(Category)))
--END
--GO

Create Proc uspGetPetTypeQT
AS
BEGIN
    SELECT 
        LoaiThuCung,
        COUNT(*) AS SoLuong
    FROM (
        SELECT 
            LEFT(Type, CHARINDEX(' ', Type + ' ') - 1) AS LoaiThuCung
        FROM Pet
        WHERE CustomerID IS NULL  -- Lọc thú cưng chưa bán
    ) AS Thucung
    WHERE LoaiThuCung IN (N'Chó', N'Mèo', N'Cá', N'Chim')
    GROUP BY LoaiThuCung
END;
GO
-- Lấy danh sách sản phẩm tồn kho thấp
CREATE PROCEDURE uspGetLowStockProducts
AS
BEGIN
    SELECT PrName FROM Product WHERE Stock <= 5
END
GO

---- SP: Tính doanh thu hôm nay
--CREATE PROCEDURE uspGetTodayRevenue
--AS
--BEGIN
--    SELECT SUM(TotalAmount) AS Revenue 
--    FROM Invoice 
--    WHERE CONVERT(date, InvoiceDate) = CONVERT(date, GETDATE())
--END
--GO
----SP: TÍnh doanh thu tháng
--CREATE PROCEDURE uspGetRevenueByMonth
--    @Month INT,
--    @Year INT
--AS
--BEGIN
--    SELECT ISNULL(SUM(TotalAmount), 0) AS Revenue
--    FROM Invoice
--    WHERE MONTH(InvoiceDate) = @Month AND YEAR(InvoiceDate) = @Year
--END
--GO
----SP: Tính doanh thu năm
--CREATE PROCEDURE uspGetRevenueByYear
--    @Year INT
--AS
--BEGIN
--    SELECT ISNULL(SUM(TotalAmount), 0) AS Revenue
--    FROM Invoice
--    WHERE YEAR(InvoiceDate) = @Year
--END
--GO

--SP: Doanh thu theo ngày
CREATE PROC uspGetRevenueByDate
    @Date DATE
AS
BEGIN
    SELECT SUM(TotalAmount) AS TotalRevenue
    FROM Invoice
    WHERE CAST(InvoiceDate AS DATE) = @Date
END
GO
--SP: Doanh thu theo tháng
CREATE PROC uspGetRevenueByMonth
    @Month INT,
    @Year INT
AS
BEGIN
    SELECT SUM(TotalAmount) AS TotalRevenue
    FROM Invoice
    WHERE MONTH(InvoiceDate) = @Month AND YEAR(InvoiceDate) = @Year
END
GO

--SP: Doanh thu theo năm
CREATE PROC uspGetRevenueByYear
    @Year INT
AS
BEGIN
    SELECT SUM(TotalAmount) AS TotalRevenue
    FROM Invoice
    WHERE YEAR(InvoiceDate) = @Year
END
GO

--SP: Lịch hẹn sắp đến
CREATE PROCEDURE uspGetUpcomingAppointments
AS
BEGIN
    SELECT a.AppointmentID, a.CustomerID, a.AppointmentDate
    FROM Appointment a
    WHERE DATEDIFF(HOUR, GETDATE(), a.AppointmentDate) BETWEEN 0 AND 24
    ORDER BY a.AppointmentDate
END
