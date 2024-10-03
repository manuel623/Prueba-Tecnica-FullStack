CREATE DATABASE TaskManagement;
GO


USE TaskManagement;
GO


CREATE TABLE States (
    Id INT IDENTITY(1,1) PRIMARY KEY,  
    Name VARCHAR(100) NOT NULL         
);
GO


CREATE TABLE Tasks (
    Id INT IDENTITY(1,1) PRIMARY KEY,   
    Name VARCHAR(100) NOT NULL,         
    StateId INT NOT NULL,               
    FOREIGN KEY (StateId) REFERENCES States(Id)  
);
GO
