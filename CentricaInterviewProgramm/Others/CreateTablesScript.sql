USE CentricaProgram;

DROP TABLE SalesPersonToStore;
DROP TABLE Store;
DROP TABLE SalesPersonsToDistrict;
DROP TABLE RelationType;
DROP TABLE District;
DROP TABLE SalesPersons;


CREATE TABLE SalesPersons (
Id int IDENTITY(1,1) PRIMARY KEY,
Name varchar(20)
);

CREATE TABLE District(
Id int IDENTITY(1,1) PRIMARY KEY,
Name varchar(20)
);

CREATE TABLE RelationType(
Id int IDENTITY(1,1) PRIMARY KEY,
Name varchar(20)
);

CREATE TABLE SalesPersonsToDistrict(
Id int IDENTITY(1,1) PRIMARY KEY,
salesPersonId int REFERENCES SalesPersons (Id),
districtId int FOREIGN KEY REFERENCES District (Id),
relationTypeId int FOREIGN KEY REFERENCES RelationType (Id)
);

CREATE TABLE Store(
Id int IDENTITY(1,1) PRIMARY KEY,
DistrictId int FOREIGN KEY REFERENCES District (Id),
Name varchar(20)
);

CREATE TABLE SalesPersonToStore(
Id int IDENTITY(1,1) PRIMARY KEY,
SalesPersonId int FOREIGN KEY REFERENCES SalesPersons (Id),
StoreId int FOREIGN KEY REFERENCES Store (Id)
);