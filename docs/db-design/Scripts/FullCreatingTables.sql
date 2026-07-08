IF NOT EXISTS (SELECT * FROM sys.databases WHERE name = 'Hall_Booking')
BEGIN
    CREATE DATABASE Hall_Booking;
END
GO
Drop Table IF Exists INVENTORY;

Drop Table IF Exists MATERIALS;

Drop Table IF Exists BOOKING_SERVICES;

Drop Table IF Exists BOOKING;

Drop Table IF Exists SERVICES;

Drop Table IF Exists TYPES;

Drop Table IF Exists STATUS;

Drop Table IF Exists CUSTOMERS;



Use [Hall_Booking];

CREATE TABLE CUSTOMERS (
    CustomerID INT IDENTITY(1,1) PRIMARY KEY,
    FullName VARCHAR(100),
    Phone VARCHAR(20),
    Notes VARCHAR(MAX)
);

CREATE TABLE STATUS (
    StatusID INT IDENTITY(1,1) PRIMARY KEY,
    statusName VARCHAR(50)
);

CREATE TABLE TYPES (
    Type_ID INT IDENTITY(1,1) PRIMARY KEY,
    Type_Name VARCHAR(50)
);

CREATE TABLE SERVICES (
    Service_ID INT IDENTITY(1,1) PRIMARY KEY,
    Service_Name VARCHAR(50),
    Price DECIMAL(10,2)
);

CREATE TABLE BOOKING (
    Dooking_ID INT IDENTITY(1,1) PRIMARY KEY,
    CustomerID INT,
    TypeID INT,
    StatusID INT,
    EventDate DATE,
    StartTime TIME,
    DurationHours DECIMAL(10,2),
    Description TEXT,
    TotalPrice MONEY,
    PaidPrice MONEY,
    PaidAmpunt MONEY,
    CreatedAt DATETIME,

    FOREIGN KEY (CustomerID) REFERENCES CUSTOMERS(CustomerID),
    FOREIGN KEY (TypeID) REFERENCES TYPES(Type_ID),
    FOREIGN KEY (StatusID) REFERENCES STATUS(StatusID)
);


CREATE TABLE BOOKING_SERVICES (
    Detail_ID INT PRIMARY KEY,
    Booking_ID INT,
    Service_ID INT,
    Price DECIMAL(10,2),

    FOREIGN KEY (Service_ID) REFERENCES SERVICES(Service_ID),
    FOREIGN KEY (Service_ID) REFERENCES SERVICES(Service_ID)
);


CREATE TABLE MATERIALS(
MaterialID INT IDENTITY(1,1) PRIMARY KEY,
MaterialName NVARCHAR(255),
FullPiecesByKgOrQuantity INT,
LessPiecesByKgOrQuantity INT
);

CREATE TABLE INVENTORY(
InventoryID INT IDENTITY(1,1) PRIMARY KEY,
MaterialID INT,
CurrentQuantity INT,
FOREIGN KEY (MaterialID) REFERENCES MATERIALS(MaterialID)
);