CREATE TABLE Customers (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100),
    Phone VARCHAR(20),
    Notes VARCHAR(MAX)
);

CREATE TABLE Status (
    StatusID INT IDENTITY(1,1) PRIMARY KEY,
    statusName VARCHAR(50)
);