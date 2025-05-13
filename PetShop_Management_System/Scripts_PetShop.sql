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
    @ProductID varchar(10),
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
    @ProductID varchar(10)
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
	@ProductID varchar(10),
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


--LỊCH HẸN
--LẤY DANH SÁCH LỊCH HẸN
CREATE PROCEDURE [dbo].[uspGetAppointments]
AS
BEGIN
SELECT
a.AppointmentID,
a.CustomerID,
c.FirstName,
a.AppointmentDate
FROM
Appointment a
INNER JOIN
Customer c ON a.CustomerID = c.CustomerID;
END
GO

-- THÊM LỊCH HẸN
CREATE PROCEDURE [dbo].[uspAddAppointment]
    @CustomerID NVARCHAR(50),
    @AppointmentDate DATETIME
AS
BEGIN
SET NOCOUNT ON;
-- Kiểm tra mã khách hàng có tồn tại trong bảng Customer không (phân biệt chữ thường và chữ in hoa)
IF NOT EXISTS (
    SELECT 1 FROM Customer WHERE CustomerID = @CustomerID COLLATE Latin1_General_CS_AS
)
BEGIN

    RETURN -1; -- Trả về -1 nếu không tìm thấy khách hàng
END
-- Tự tạo AppointmentID mới (tăng dần)
DECLARE @NextID INT;
SELECT @NextID = ISNULL(MAX(AppointmentID), 0) + 1 FROM Appointment;

-- Thêm lịch hẹn mới vào bảng Appointment
INSERT INTO Appointment (AppointmentID, CustomerID, AppointmentDate)
VALUES (@NextID, @CustomerID, @AppointmentDate);

RETURN 0; -- Trả về 0 nếu thêm thành công
END
GO

--XÓA LỊCH HẸN
CREATE PROC [dbo].[uspDeleteAppointment]
    @AppointmentID int
AS
BEGIN
    SELECT SUM(TotalAmount) AS TotalRevenue
    FROM Invoice
    WHERE MONTH(InvoiceDate) = @Month AND YEAR(InvoiceDate) = @Year
    SET NOCOUNT ON;
    DELETE FROM Appointment WHERE AppointmentID = @AppointmentID;
END
GO

--SỬA LỊCH HẸN
CREATE PROCEDURE [dbo].[uspUpdateAppointment]
	@AppointmentID int,
	@CustomerID varchar(10),
	@AppointmentDate datetime
AS
BEGIN
    SELECT SUM(TotalAmount) AS TotalRevenue
    FROM Invoice
    WHERE YEAR(InvoiceDate) = @Year
SET NOCOUNT ON;

    UPDATE Appointment
    SET CustomerID = @CustomerID,
        AppointmentDate = @AppointmentDate

    WHERE AppointmentID = @AppointmentID;
END
GO

--TÌM KIẾM LỊCH HẸN
CREATE PROCEDURE [dbo].[uspSearchAppointment]
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT a.AppointmentID, a.CustomerID, a.AppointmentDate
    FROM Appointment a
    WHERE DATEDIFF(HOUR, GETDATE(), a.AppointmentDate) BETWEEN 0 AND 24
    ORDER BY a.AppointmentDate
    SELECT * FROM Appointment
    WHERE CONCAT( AppointmentID, CustomerID, AppointmentDate) 
          LIKE @Keyword
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
GO
---//----
--THÚ CƯNG
--LẤY DANH SÁCH THÚ CƯNG
CREATE PROCEDURE [dbo].[uspGetPet]
AS
BEGIN
    SELECT * FROM Pet;
END
GO
--THÊM THÚ CƯNG
CREATE PROCEDURE [dbo].[uspAddPet]
	@PetName nvarchar(50),
    @Type nvarchar(50),
	@Price decimal(10,2),
	@HealthStatus nvarchar(100),
	@CustomerID varchar(10)
AS 
BEGIN
    INSERT INTO Pet(PetName, Type, Price, HealthStatus, CustomerID)
    VALUES (@PetName, @Type, @Price, @HealthStatus, @CustomerID)
END
GO


--XÓA THÚ CƯNG
CREATE PROCEDURE uspDeletePet
    @PetID int
AS
BEGIN
    SET NOCOUNT ON; 
    DELETE FROM Pet
    WHERE PetID = @PetID;
END
GO


--TÌM KIẾM THÚ CƯNG
CREATE PROCEDURE [dbo].[uspSearchPet]
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT * FROM Pet
    WHERE CONCAT( petName,type, price, healthStatus) 
          LIKE @Keyword
END
GO

--CHỈNH SỬA THÚ CƯNG
CREATE PROCEDURE [dbo].[uspUpdatePet]
	
    @PetID INT,
    @PetName NVARCHAR(50),
    @Type NVARCHAR(50),
    @Price DECIMAL(10,2),
    @HealthStatus NVARCHAR(100),
    @CustomerID VARCHAR(10) = NULL -- cho phép NULL nếu chưa bán
AS
BEGIN
    UPDATE  Pet
    SET 
        PetName = @PetName,
        Type = @Type,
        Price = @Price,
        HealthStatus = @HealthStatus,
        CustomerID = @CustomerID
    WHERE PetID = @PetID;
END
GO

--//--
---THANH TOAN---
CREATE PROCEDURE uspGetCash
AS
BEGIN
    SELECT CashID, Transno, Pcode, Pname, Qty, Price, Total, Cid, Cashier
    FROM Cash
END
GO

-- Thêm giao dịch
CREATE PROCEDURE [dbo].[uspAddCash]
  @Transno VARCHAR(20),
    @Pcode VARCHAR(10),
    @Pname NVARCHAR(50),
    @Qty INT,
    @Price DECIMAL(18, 2),
    @Total DECIMAL(18, 2),
    @Cid VARCHAR(10),
    @Cashier VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Khai báo biến bảng để lưu giá trị sequence
    DECLARE @SequenceValueTable TABLE (Value INT);

    -- Khai báo biến để lưu CashID và SequenceValue
    DECLARE @NewCashID VARCHAR(10);
    DECLARE @SequenceValue INT;

    BEGIN TRANSACTION;

    -- Tăng giá trị sequence và lưu vào biến bảng
    UPDATE SequenceGenerator
    SET CurrentValue = CurrentValue + 1
    OUTPUT INSERTED.CurrentValue INTO @SequenceValueTable
    WHERE SequenceName = 'CashID';

    -- Lấy giá trị từ biến bảng
    SELECT @SequenceValue = Value
    FROM @SequenceValueTable;

    -- Định dạng CashID (CASH001, CASH002, ...)
    SET @NewCashID = 'CASH' + RIGHT('000' + CAST(@SequenceValue AS VARCHAR(3)), 3);

    -- Thêm giao dịch vào bảng Cash
    INSERT INTO Cash (CashID, Transno, Pcode, Pname, Qty, Price, Total, Cid, Cashier)
    VALUES (@NewCashID, @Transno, @Pcode, @Pname, @Qty, @Price, @Total, @Cid, @Cashier);

    COMMIT TRANSACTION;
END
GO 

-- Cập nhật giao dịch
CREATE PROCEDURE [dbo].[uspUpdateCash]
    @CashID VARCHAR(10),
    @Transno VARCHAR(20),
    @Pcode VARCHAR(15),
    @Pname VARCHAR(50),
    @Qty INT,
    @Price DECIMAL(18,2),
    @Total DECIMAL(18,2),
    @Cid VARCHAR(10),
    @Cashier VARCHAR(10)
AS
BEGIN
    UPDATE Cash
    SET Transno = @Transno,
        Pcode = @Pcode,
        Pname = @Pname,
        Qty = @Qty,
        Price = @Price,
        Total = @Total,
        Cid = @Cid,
        Cashier = @Cashier
    WHERE CashID = @CashID;
END
GO

-- Xóa giao dịch
CREATE PROCEDURE [dbo].[uspDeleteCash]
    @CashID VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;
    DELETE FROM Cash WHERE CashID = @CashID;
END
GO

-- Tìm kiếm giao dịch
CREATE PROCEDURE [dbo].[uspSearchCash]
    @Keyword NVARCHAR(100)
AS
BEGIN
    SELECT CashID, Transno, Pcode, Pname, Qty, Price, Total, Cid, Cashier
    FROM Cash
    WHERE CONCAT(CashID, Transno, Pcode, Pname, Qty, Price, Total, Cid, Cashier) LIKE '%' + @Keyword + '%';
END
GO

---///----
CREATE TABLE SequenceGenerator
(
    SequenceName VARCHAR(50) PRIMARY KEY,
    CurrentValue INT NOT NULL DEFAULT 0
);
GO

-- Thêm giá trị ban đầu cho CashID
INSERT INTO SequenceGenerator (SequenceName, CurrentValue)
VALUES ('CashID', 0);
GO

ALTER PROCEDURE uspAddCash
    @Transno VARCHAR(20),
    @Pcode VARCHAR(10),
    @Pname NVARCHAR(50),
    @Qty INT,
    @Price DECIMAL(18, 2),
    @Total DECIMAL(18, 2),
    @Cid VARCHAR(10),
    @Cashier VARCHAR(10)
AS
BEGIN
    SET NOCOUNT ON;

    -- Tăng giá trị sequence và lấy giá trị mới
    DECLARE @NewCashID VARCHAR(10);
    DECLARE @SequenceValue INT;

    BEGIN TRANSACTION;

    UPDATE SequenceGenerator
    SET CurrentValue = CurrentValue + 1
    OUTPUT INSERTED.CurrentValue INTO @SequenceValue
    WHERE SequenceName = 'CashID';

    -- Định dạng CashID (CASH001, CASH002, ...)
    SET @NewCashID = 'CASH' + RIGHT('000' + CAST(@SequenceValue AS VARCHAR(3)), 3);

    -- Thêm giao dịch vào bảng Cash
    INSERT INTO Cash (CashID, Transno, Pcode, Pname, Qty, Price, Total, Cid, Cashier)
    VALUES (@NewCashID, @Transno, @Pcode, @Pname, @Qty, @Price, @Total, @Cid, @Cashier);

    COMMIT TRANSACTION;
END
GO

