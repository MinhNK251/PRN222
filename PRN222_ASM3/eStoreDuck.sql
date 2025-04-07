USE [master]
GO

DROP DATABASE [eStoreDuck]
GO

CREATE DATABASE [eStoreDuck]
GO

USE [eStoreDuck]
GO

-- Tạo bảng Member
CREATE TABLE [dbo].[Member]
(
    MemberID INT IDENTITY(1,1) NOT NULL,
    Email VARCHAR(100) NOT NULL,
    CompanyName VARCHAR(40) NOT NULL,
    City VARCHAR(15) NOT NULL,
    Country VARCHAR(15) NOT NULL,
    [Password] VARCHAR(30) NOT NULL,
    CONSTRAINT PK_Member PRIMARY KEY (MemberID)
);
GO

-- Tạo bảng Category
CREATE TABLE [dbo].[Category]
(
    CategoryID INT IDENTITY(1,1) NOT NULL,
    CategoryName VARCHAR(50) NOT NULL,
    [Description] VARCHAR(255) NULL,
    CONSTRAINT PK_Category PRIMARY KEY (CategoryID)
);
GO

-- Tạo bảng Product
CREATE TABLE [dbo].[Product]
(
    ProductID INT IDENTITY(1,1) NOT NULL,
    CategoryID INT NOT NULL,
    ProductName VARCHAR(20) NOT NULL,
    Weight VARCHAR(20) NOT NULL,
    UnitPrice MONEY NOT NULL,
    UnitsInStock INT NOT NULL,
    CONSTRAINT PK_Product PRIMARY KEY (ProductID),
    CONSTRAINT FK_Product_Category FOREIGN KEY (CategoryID)
        REFERENCES [dbo].[Category](CategoryID)
);
GO

-- Tạo bảng [Order]
CREATE TABLE [dbo].[Order]
(
    OrderID INT IDENTITY(1,1) NOT NULL,
    MemberID INT NOT NULL,
    OrderDate DATETIME NOT NULL,
    RequiredDate DATETIME NULL,
    ShippedDate DATETIME NULL,
    Freight MONEY NULL,
    CONSTRAINT PK_Order PRIMARY KEY (OrderID),
    CONSTRAINT FK_Order_Member FOREIGN KEY (MemberID)
        REFERENCES [dbo].[Member](MemberID)
);
GO

-- Tạo bảng OrderDetail
CREATE TABLE [dbo].[OrderDetail]
(
    OrderID INT NOT NULL,
    ProductID INT NOT NULL,
    UnitPrice MONEY NOT NULL,
    Quantity INT NOT NULL,
    Discount FLOAT NOT NULL,
    CONSTRAINT PK_OrderDetail PRIMARY KEY (OrderID, ProductID),
    CONSTRAINT FK_OrderDetail_Order FOREIGN KEY (OrderID)
        REFERENCES [dbo].[Order](OrderID),
    CONSTRAINT FK_OrderDetail_Product FOREIGN KEY (ProductID)
        REFERENCES [dbo].[Product](ProductID)
);
GO
