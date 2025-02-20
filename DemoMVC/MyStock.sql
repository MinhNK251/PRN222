-- Check if the database exists and drop it if it does
IF EXISTS (SELECT name FROM sys.databases WHERE name = 'MyStock')
BEGIN
    DROP DATABASE MyStock;
END
GO

-- Create the database
CREATE DATABASE MyStock;
GO

-- Switch to the newly created database
USE MyStock;
GO

-- Create the Products table
CREATE TABLE Products (
    ProductID INT PRIMARY KEY,
    ProductName NVARCHAR(40),
    UnitPrice MONEY,
    UnitsInStock INT
);
GO

-- Insert sample records into the Products table
INSERT INTO Products (ProductID, ProductName, UnitPrice, UnitsInStock)
VALUES
(1, 'Apple', 1.50, 100),
(2, 'Banana', 0.75, 200),
(3, 'Orange', 1.20, 150),
(4, 'Mango', 2.00, 80),
(5, 'Grapes', 2.50, 60),
(6, 'Pineapple', 3.00, 40),
(7, 'Blueberry', 4.00, 120),
(8, 'Watermelon', 5.00, 30),
(9, 'Peach', 1.80, 90),
(10, 'Strawberry', 2.20, 110);
GO
