CREATE DATABASE SalesDb;
GO

USE SalesDb;
GO

CREATE TABLE Buyers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL
);

CREATE TABLE Sellers (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL
);

CREATE TABLE Sales (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    BuyerId INT FOREIGN KEY REFERENCES Buyers(Id) ON DELETE CASCADE,
    SellerId INT FOREIGN KEY REFERENCES Sellers(Id) ON DELETE CASCADE,
    Amount DECIMAL(18, 2) NOT NULL,
    SaleDate DATETIME NOT NULL
);

INSERT INTO Buyers (FirstName, LastName) VALUES (N'Іван', N'Петров'), (N'Олена', N'Сидорова');
INSERT INTO Sellers (FirstName, LastName) VALUES (N'Олег', N'Коваль'), (N'Марія', N'Бондар');
INSERT INTO Sales (BuyerId, SellerId, Amount, SaleDate) VALUES (1, 1, 1500.00, GETDATE()), (2, 2, 2300.50, GETDATE());
