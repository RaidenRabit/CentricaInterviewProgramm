USE master
GO
IF DB_ID('CentricaProgram') IS NOT NULL
ALTER DATABASE CentricaProgram SET SINGLE_USER WITH ROLLBACK IMMEDIATE
DROP DATABASE IF EXISTS CentricaProgram;
GO

CREATE DATABASE CentricaProgram;
GO

USE CentricaProgram;

CREATE TABLE SalesPersons (
SalesPersonId int IDENTITY(1,1) PRIMARY KEY,
SalesPersonName varchar(20)
);

CREATE TABLE District(
DistrictId int IDENTITY(1,1) PRIMARY KEY,
DistrictName varchar(20)
);

CREATE TABLE RelationType(
RelationTypeId int IDENTITY(1,1) PRIMARY KEY,
RelationTypeName varchar(20)
);

CREATE TABLE SalesPersonsToDistrict(
SalesPersonToDistrictId int IDENTITY(1,1) PRIMARY KEY,
SalesPersonId int REFERENCES SalesPersons (SalesPersonId),
DistrictId int FOREIGN KEY REFERENCES District (DistrictId),
RelationTypeId int FOREIGN KEY REFERENCES RelationType (RelationTypeId)
);

CREATE TABLE Store(
StoreId int IDENTITY(1,1) PRIMARY KEY,
DistrictId int FOREIGN KEY REFERENCES District (DistrictId),
StoreName varchar(20)
);

CREATE TABLE SalesPersonToStore(
SalesPersonToStoreId int IDENTITY(1,1) PRIMARY KEY,
SalesPersonId int FOREIGN KEY REFERENCES SalesPersons (SalesPersonId),
StoreId int FOREIGN KEY REFERENCES Store (StoreId)
);