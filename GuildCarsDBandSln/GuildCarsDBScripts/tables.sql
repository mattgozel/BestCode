use GuildCars
go

if exists(select * from sys.tables where name='SalesPurchase')
drop table SalesPurchase
go

if exists(select * from sys.tables where name='InventoryDetails')
drop table InventoryDetails
go

if exists(select * from sys.tables where name='CarModels')
drop table CarModels
go

if exists(select * from sys.tables where name='Makes')
drop table Makes
go

if exists(select * from sys.tables where name='BodyStyle')
drop table BodyStyle
go

if exists(select * from sys.tables where name='Transmission')
drop table Transmission
go

if exists(select * from sys.tables where name='Color')
drop table Color
go

if exists(select * from sys.tables where name='Interior')
drop table Interior
go

if exists(select * from sys.tables where name='CarType')
drop table CarType
go

if exists(select * from sys.tables where name='PurchaseType')
drop table PurchaseType
go

if exists(select * from sys.tables where name='States')
drop table States
go

if exists(select * from sys.tables where name='Specials')
drop table Specials
go

if exists(select * from sys.tables where name='ContactUs')
drop table ContactUs
go

if exists(select * from sys.tables where name='Role')
drop table [Role]
go

create table Makes (
	MakeId int primary key identity(1, 1),
	Make nvarchar(30) not null,
	DateAdded date not null default(getdate()),
	Email nvarchar(256) not null
)
go

create table CarModels (
	ModelId int primary key identity(1, 1),
	Model nvarchar(50) not null,
	MakeId int foreign key references Makes(MakeId) not null,
	DateAdded date not null default(getdate()),
	Email nvarchar(256) not null
)
go

create table BodyStyle (
	BodyStyleId int primary key identity(1, 1),
	BodyStyleName nvarchar(50) not null
)
go

create table Transmission (
	TransmissionId int primary key identity(1, 1),
	TransmissionName varchar(50) not null
)
go

create table Color (
	ColorId int primary key identity(1, 1),
	ColorName nvarchar(50) not null,
)
go

create table Interior (
	InteriorId int primary key identity(1, 1),
	InteriorName nvarchar(50) not null,
)
go

create table CarType (
	TypeId int primary key identity(1, 1),
	TypeName varchar(30) not null,
)
go

create table PurchaseType (
	PurchaseTypeId int primary key identity(1,1) not null,
	PurchaseTypeName varchar(50) not null
)
go

create table States (
	StateId char(2) not null primary Key,
	StateName varchar(15) not null
)
go

create table InventoryDetails (
	InventoryId int primary key identity(1, 1),
	[Year] int null,
	MakeId int not null foreign key references Makes(MakeId),
	ModelId int not null foreign key references CarModels(ModelId),
	BodyStyleId int not null foreign key references BodyStyle(BodyStyleId),
	TransmissionId int not null foreign key references Transmission(TransmissionId),
	ColorId int not null foreign key references Color(ColorId),
	InteriorId int not null foreign key references Interior(InteriorId),
	Mileage int null,
	Vin varchar(30) null,
	SalePrice int not null,
	MSRP int not null,
	TypeId int not null foreign key references CarType(TypeId),
	[Description] nvarchar(256) null,
	ImageFileName nvarchar(50) null,
	IsFeatured bit default 0 not null,
	IsSold bit default 0 not null
)
go

create table Specials (
	SpecialId int primary key identity(1,1) not null,
	Title nvarchar(50) not null,
	[Description] nvarchar(256) not null,
	ImageFileName nvarchar(50) null
)
go

create table ContactUs (
	ContactUsId int primary key identity(1, 1),
	[Name] nvarchar(50) not null,
	Email nvarchar(256) null,
	Phone nvarchar(25) null,
	[Message] nvarchar(256) not null
)
go

create table SalesPurchase (
	SalesPurchaseId int primary key identity(1, 1),
	InventoryId int not null foreign key references InventoryDetails(InventoryId),
	UserId nvarchar(128) not null,
	[Name] nvarchar(128) not null,
	Email nvarchar(256) null,
	Phone nvarchar(25) null,
	Street1 nvarchar(256) not null,
	Street2 nvarchar(256) null,
	City nvarchar(50) not null,
	[State] char(2) not null foreign key references States(StateId),
	ZipCode nvarchar(5) not null,
	PurchasePrice int not null,
	PurchaseType int not null foreign key references PurchaseType(PurchaseTypeId),
	DateAdded date not null default(getdate())
)
go

create table [Role] (
	RoleId int primary key identity(1, 1),
	RoleName nvarchar(50) not null,
)
go