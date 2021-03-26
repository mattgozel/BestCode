use GuildFlooring
go

if exists(select * from sys.tables where name='Orders')
drop table Orders
go

if exists(select * from sys.tables where name='TaxInfo')
drop table TaxInfo
go

if exists(select * from sys.tables where name='Products')
drop table Products
go

create table TaxInfo (
	StateAbbreviation char(2) not null primary Key,
	StateName varchar(15) not null,
	TaxRate decimal(5,2) not null
)
go

create table Products (
	ProductId int primary key identity(1,1) not null,
	ProductName nvarchar(30) not null,
	CostPerSquareFoot decimal(5,2) not null,
	LaborCostPerSquareFoot decimal(5,2) not null,
)
go

create table Orders (
	OrderNumber int primary key identity(1, 1),
	CustomerName nvarchar(50) not null,
	StateAbbreviation char(2) not null foreign key references TaxInfo(StateAbbreviation),
	ProductId int not null foreign key references Products(ProductId),
	Area decimal(15,2) not null,
	MaterialCost decimal(15,2) not null,
	LaborCost decimal(15,2) not null,
	Tax decimal(15,2) not null,
	Total decimal(15,2) not null,
	DateAdded date not null default(getdate())
)