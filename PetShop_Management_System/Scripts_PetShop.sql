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
    SELECT * FROM Appointment
    WHERE CONCAT( AppointmentID, CustomerID, AppointmentDate) 
          LIKE @Keyword
END
GO
