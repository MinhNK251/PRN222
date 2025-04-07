USE [master]
GO

USE [eStoreDuck]
GO

-- Insert sample data into Member table
INSERT INTO [dbo].[Member] (Email, CompanyName, City, Country, [Password])
VALUES 
('user1@example.com', 'User1 Company', 'Hanoi', 'Vietnam', '123456'),
('user2@example.com', 'User2 Company', 'HCMC', 'Vietnam', '123456'),
('user3@example.com', 'User3 Company', 'Danang', 'Vietnam', '123456');

-- Insert sample data into Category table
INSERT INTO [dbo].[Category] (CategoryName, [Description])
VALUES 
('Electronics', 'Devices and gadgets'),
('Clothing', 'Apparel and accessories'),
('Furniture', 'Home and office furniture'),
('Food', 'Groceries and consumables');

-- Insert sample data into Product table
INSERT INTO [dbo].[Product] (CategoryID, ProductName, Weight, UnitPrice, UnitsInStock)
VALUES 
(1, 'Smartphone', '0.3 kg', 300.00, 50),
(1, 'Laptop', '2.0 kg', 800.00, 30),
(2, 'T-shirt', '0.2 kg', 15.00, 100),
(2, 'Jeans', '0.5 kg', 40.00, 75),
(3, 'Office Chair', '7.5 kg', 150.00, 20),
(3, 'Desk', '15 kg', 200.00, 15),
(4, 'Rice', '5 kg', 10.00, 200),
(4, 'Bread', '0.5 kg', 2.00, 500);

-- Insert sample data into Order table
INSERT INTO [dbo].[Order] (MemberID, OrderDate, RequiredDate, ShippedDate, Freight)
VALUES 
(1, '2025-03-01', '2025-03-05', '2025-03-02', 20.00),
(2, '2025-03-02', '2025-03-06', '2025-03-03', 15.00),
(3, '2025-03-03', '2025-03-07', '2025-03-04', 25.00);

-- Insert sample data into OrderDetail table
INSERT INTO [dbo].[OrderDetail] (OrderID, ProductID, UnitPrice, Quantity, Discount)
VALUES 
(1, 1, 300.00, 1, 0.05),
(1, 2, 800.00, 1, 0.10),
(2, 3, 15.00, 3, 0.00),
(2, 4, 40.00, 2, 0.05),
(3, 5, 150.00, 1, 0.10),
(3, 6, 200.00, 1, 0.00);
