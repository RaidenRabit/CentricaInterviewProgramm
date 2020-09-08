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
SalesPersonIdSPTDFK int REFERENCES SalesPersons (SalesPersonId),
DistrictIdSPTDFK int FOREIGN KEY REFERENCES District (DistrictId),
RelationTypeIdSPTDFK int FOREIGN KEY REFERENCES RelationType (RelationTypeId)
);

CREATE TABLE Store(
StoreId int IDENTITY(1,1) PRIMARY KEY,
DistrictIdSFK int FOREIGN KEY REFERENCES District (DistrictId),
StoreName varchar(20)
);

CREATE TABLE SalesPersonToStore(
SalesPersonToStoreId int IDENTITY(1,1) PRIMARY KEY,
SalesPersonIdSPTSFK int FOREIGN KEY REFERENCES SalesPersons (SalesPersonId),
StoreIdSPTSFK int FOREIGN KEY REFERENCES Store (StoreId)
);