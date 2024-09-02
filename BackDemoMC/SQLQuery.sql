 
--SELECT *FROM AspNetRoles

SELECT COUNT(Name) AS AspNetRoles

--INSERT INTO AspNetRoles (Name) VALUES ('ProjectAdmin') 
--UPDATE dbo.ProductCategories SET TestCategory='Test2091' WHERE ParentProductCategoryId=1390;
--SELECT *FROM dbo.ProductCategories WHERE ParentProductCategoryId=1390

--SELECT dbo.Persons.Id, dbo.Persons.name "Имя сотрудника", dbo.Positions.name "Должность" FROM dbo.Persons 
--LEFT JOIN 
--dbo.Positions ON dbo.Persons.Id = dbo.Positions.Id

--SELECT dbo.Persons.Id, dbo.Persons.name "Имя сотрудника", dbo.Positions.name "Должность"  FROM dbo.Persons 
--INNER JOIN 
--dbo.Positions ON dbo.Persons.Id = dbo.Positions.Id

--SELECT dbo.Persons.Id, dbo.Persons.name "Имя сотрудника", dbo.Positions.name "Должность"  FROM dbo.Persons 
--RIGHT JOIN 
--dbo.Positions ON dbo.Persons.Id = dbo.Positions.Id


--SELECT *FROM dbo.UserMigrant
--SELECT *FROM dbo.[Order]
--DELETE dbo.Order WHERE Name ='user'
--SELECT *FROM MigrationCenterDb.dbo.Role
--SELECT *FROM MigrationCenterDb.dbo.Order
--SELECT *FROM dbo.UserMigrant
--SELECT *FROM dbo.OrderItem
--SELECT *FROM dbo.Migrant

/*DELETE dbo.Person WHERE Role ='user'*/
/*SELECT Role FROM dbo.Person WHERE login='admin';*/
/*UPDATE dbo.Person SET Role='admin' WHERE login='admin';*/
/*SELECT *FROM dbo.StatRecords*/
/*DELETE dbo.StatRecords WHERE EndTime ='0001-01-01 00:00:00.0000000' AND ClickCount=0*/
/*DELETE dbo.StatRecords WHERE GameGuid ='17e05d59-79cc-4e52-a93c-08d710d8a54e'*/
/*DELETE dbo.StatRecords WHERE Login ='user'*/
/*DROP TABLE dbo.Person*/