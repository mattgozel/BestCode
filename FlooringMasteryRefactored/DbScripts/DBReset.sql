use GuildFlooring
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DbReset')
		drop procedure DbReset
go

create procedure DbReset as
begin
	delete from Orders;
	delete from Products;
	delete from TaxInfo;

	DBCC CHECKIDENT ('Orders', RESEED, 1)
	DBCC CHECKIDENT ('Products', RESEED, 1)

	insert into TaxInfo (StateAbbreviation, StateName, TaxRate)
	values ('OH', 'Ohio', 6.25),
	('PA', 'Pennsylvania', 6.75),
	('MI', 'Michigan', 5.75),
	('IN', 'Indiana', 6.00);

	set identity_insert Products on;

	insert into Products (ProductId, ProductName, CostPerSquareFoot, LaborCostPerSquareFoot)
	values (1, 'Carpet', 2.25, 2.10),
	(2, 'Laminate', 1.75, 2.10),
	(3, 'Tile', 3.50, 4.15),
	(4, 'Wood', 5.15, 4.75);

	set identity_insert Products off;

	set identity_insert Orders on;

	insert into Orders (OrderNumber, CustomerName, StateAbbreviation, ProductId, Area, MaterialCost, LaborCost, Tax, Total)
	values (1, 'Wise', 'OH', 4, 100.00, 515.00, 475.00, 61.88, 1051.88);

	set identity_insert Orders off;
end