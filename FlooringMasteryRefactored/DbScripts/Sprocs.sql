use GuildFlooring
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'ProductsSelectAll')
		drop procedure ProductsSelectAll
go

create procedure ProductsSelectAll as
begin
	select * from Products
end
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'TaxInfoSelectAll')
		drop procedure TaxInfoSelectAll
go

create procedure TaxInfoSelectAll as
begin
	select * from TaxInfo
end
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'OrderInsert')
		drop procedure OrderInsert
go

create procedure OrderInsert (
	@OrderNumber int output,
	@CustomerName nvarchar(50),
	@StateAbbreviation char(2),
	@ProductId int,
	@Area decimal(15,2),
	@MaterialCost decimal(15,2),
	@LaborCost decimal(15,2),
	@Tax decimal(15,2),
	@Total decimal(15,2)
)
as
begin
	insert into Orders (CustomerName, StateAbbreviation, ProductId, Area, MaterialCost, LaborCost, Tax, Total)
	values (@CustomerName, @StateAbbreviation, @ProductId, @Area, @MaterialCost, @LaborCost, @Tax, @Total);

	set @OrderNumber = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'OrderUpdate')
		drop procedure OrderUpdate
go

create procedure OrderUpdate (
	@OrderNumber int,
	@CustomerName nvarchar(50),
	@StateAbbreviation char(2),
	@ProductId int,
	@Area decimal(15,2),
	@MaterialCost decimal(15,2),
	@LaborCost decimal(15,2),
	@Tax decimal(15,2),
	@Total decimal(15,2)
)
as
begin
	update Orders set
	CustomerName = @CustomerName,
	StateAbbreviation = @StateAbbreviation,
	ProductId = @ProductId,
	Area = @Area,
	MaterialCost = @MaterialCost,
	LaborCost = @LaborCost,
	Tax = @Tax,
	Total = @Total

	where OrderNumber = @OrderNumber
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'OrderDelete')
		drop procedure OrderDelete
go

create procedure OrderDelete (
	@OrderNumber int
) as
begin
	begin transaction

	delete from Orders where OrderNumber = @OrderNumber;

	commit transaction
end
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'OrderSelectById')
		drop procedure OrderSelectById
go

create procedure OrderSelectById (
	@OrderNumber int
)
as
begin
	select * from Orders
	where OrderNumber = @OrderNumber
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'OrderSelectAll')
		drop procedure OrderSelectAll
go

create procedure OrderSelectAll
as
begin
	select * from Orders
end

go