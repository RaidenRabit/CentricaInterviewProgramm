USE CentricaProgram;

INSERT INTO District VALUES('North Denmark'), ('South Denmark'), ('Mid Denmark');
INSERT INTO Store VALUES(1, 'ND1'), (1,'ND2'), (2,'SD1');
INSERT INTO SalesPersons VALUES('SP1'), ('SP2'), ('SP3');
INSERT INTO SalesPersonToStore(salesPersonId, storeId) VALUES(1, 1), (1, 2), (2, 1);
INSERT INTO RelationType VALUES('Primary'), ('Secondary');
INSERT INTO SalesPersonsToDistrict(salesPersonId, districtId, relationTypeId) VALUES(1, 1, 1), (2,1,2), (2,2,1);