CREATE DATABASE LibraryDb;
GO

USE LibraryDb;
GO

CREATE TABLE Books (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    Title NVARCHAR(100) NOT NULL
);

CREATE TABLE Authors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL
);

CREATE TABLE BookAuthors (
    BookId INT FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE,
    AuthorId INT FOREIGN KEY REFERENCES Authors(Id) ON DELETE CASCADE,
    PRIMARY KEY (BookId, AuthorId)
);

CREATE TABLE Visitors (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    FirstName NVARCHAR(50) NOT NULL,
    LastName NVARCHAR(50) NOT NULL,
    IsDebtor BIT NOT NULL DEFAULT 0
);

CREATE TABLE VisitorBooks (
    Id INT IDENTITY(1,1) PRIMARY KEY,
    VisitorId INT FOREIGN KEY REFERENCES Visitors(Id) ON DELETE CASCADE,
    BookId INT FOREIGN KEY REFERENCES Books(Id) ON DELETE CASCADE,
    TakeDate DATETIME NOT NULL,
    ReturnDate DATETIME NULL
);

INSERT INTO Authors (FirstName, LastName) VALUES (N'Тарас', N'Шевченко'), (N'Леся', N'Українка');
INSERT INTO Books (Title) VALUES (N'Кобзар'), (N'Лісова пісня');
INSERT INTO BookAuthors (BookId, AuthorId) VALUES (1, 1), (2, 2);
INSERT INTO Visitors (FirstName, LastName, IsDebtor) VALUES (N'Олександр', N'Захаров', 1), (N'Анна', N'Коваленко', 0);
INSERT INTO VisitorBooks (VisitorId, BookId, TakeDate, ReturnDate) VALUES (1, 1, GETDATE(), NULL);
