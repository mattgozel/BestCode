use GuildCars
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'StatesSelectAll')
		drop procedure StatesSelectAll
go

create procedure StatesSelectAll as
begin
	select * from States
end
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'BodyStylesSelectAll')
		drop procedure BodyStylesSelectAll
go

create procedure BodyStylesSelectAll as
begin
	select * from BodyStyle
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'MakesSelectAll')
		drop procedure MakesSelectAll
go

create procedure MakesSelectAll as
begin
	select * from Makes
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'CarModelsSelectAll')
		drop procedure CarModelsSelectAll
go

create procedure CarModelsSelectAll as
begin
	select * from CarModels
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'TransmissionSelectAll')
		drop procedure TransmissionSelectAll
go

create procedure TransmissionSelectAll as
begin
	select * from Transmission
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'ColorSelectAll')
		drop procedure ColorSelectAll
go

create procedure ColorSelectAll as
begin
	select * from Color
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InteriorSelectAll')
		drop procedure InteriorSelectAll
go

create procedure InteriorSelectAll as
begin
	select * from Interior
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'CarTypeSelectAll')
		drop procedure CarTypeSelectAll
go

create procedure CarTypeSelectAll as
begin
	select * from CarType
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsSelectAll')
		drop procedure InventoryDetailsSelectAll
go

create procedure InventoryDetailsSelectAll as
begin
	select * from InventoryDetails
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsInsert')
		drop procedure InventoryDetailsInsert
go

create procedure InventoryDetailsInsert (
	@InventoryId int output,
	@Year int,
	@MakeId int,
	@ModelId int,
	@BodyStyleId int,
	@TransmissionId int,
	@ColorId int,
	@InteriorId int,
	@Mileage int,
	@Vin varchar(30),
	@SalePrice int,
	@MSRP int,
	@TypeId int,
	@Description nvarchar(256),
	@ImageFileName nvarchar(50)
)
as
begin
	insert into InventoryDetails ([Year], MakeId, ModelId, BodyStyleId, TransmissionId, ColorId, InteriorId, Mileage, Vin, SalePrice, MSRP, TypeId, [Description], ImageFileName)
	values (@Year, @MakeId, @ModelId, @BodyStyleId, @TransmissionId, @ColorId, @InteriorId, @Mileage, @Vin, @SalePrice, @MSRP, @TypeId, @Description, @ImageFileName);

	set @InventoryId = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsUpdate')
		drop procedure InventoryDetailsUpdate
go

create procedure InventoryDetailsUpdate (
	@InventoryId int,
	@Year int,
	@MakeId int,
	@ModelId int,
	@BodyStyleId int,
	@TransmissionId int,
	@ColorId int,
	@InteriorId int,
	@Mileage int,
	@Vin varchar(30),
	@SalePrice int,
	@MSRP int,
	@TypeId int,
	@Description nvarchar(256),
	@ImageFileName nvarchar(50),
	@IsFeatured bit
)
as
begin
	update InventoryDetails set
	[Year] = @Year,
	MakeId = @MakeId,
	ModelId = @ModelId,
	BodyStyleId = @BodyStyleId,
	TransmissionId = @TransmissionId,
	ColorId = @ColorId,
	InteriorId = @InteriorId,
	Mileage = @Mileage,
	Vin = @Vin,
	SalePrice = @SalePrice,
	MSRP = @MSRP,
	TypeId = @TypeId,
	[Description] = @Description,
	ImageFileName = @ImageFileName,
	IsFeatured = @IsFeatured

	where InventoryId = @InventoryId
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsDelete')
		drop procedure InventoryDetailsDelete
go

create procedure InventoryDetailsDelete (
	@InventoryId int
) as
begin
	begin transaction

	delete from InventoryDetails where InventoryId = @InventoryId;

	commit transaction
end
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsSelectById')
		drop procedure InventoryDetailsSelectById
go

create procedure InventoryDetailsSelectById (
	@InventoryId int
)
as
begin
	select * from InventoryDetails
	where InventoryId = @InventoryId
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsFeatured')
		drop procedure InventoryDetailsFeatured
go

create procedure InventoryDetailsFeatured
as
begin
	select top 8 InventoryId, [Year], Make, Model, SalePrice, ImageFileName
from InventoryDetails
	inner join Makes
		on Makes.MakeId = InventoryDetails.MakeId
	inner join CarModels
		on CarModels.ModelId = InventoryDetails.ModelId
		where IsFeatured = 1
		order by InventoryDetails.InventoryId desc
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsView')
		drop procedure InventoryDetailsView
go

create procedure InventoryDetailsView (
	@InventoryId int
)
as
begin
select InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, Mileage, Vin, SalePrice, MSRP, [Description], ImageFileName, IsSold
from InventoryDetails
	inner join Makes
		on Makes.MakeId = InventoryDetails.MakeId
	inner join CarModels
		on CarModels.ModelId = InventoryDetails.ModelId
	inner join BodyStyle
		on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId
	inner join Transmission
		on Transmission.TransmissionId = InventoryDetails.TransmissionId
	inner join Color
		on Color.ColorId = InventoryDetails.ColorId
	inner join Interior
		on Interior.InteriorId = InventoryDetails.InteriorId
		where InventoryId = @InventoryId
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsNew')
		drop procedure InventoryDetailsNew
go

create procedure InventoryDetailsNew
as
begin
select InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, Mileage, Vin, SalePrice, MSRP, ImageFileName, TypeId
from InventoryDetails
	inner join Makes
		on Makes.MakeId = InventoryDetails.MakeId
	inner join CarModels
		on CarModels.ModelId = InventoryDetails.ModelId
	inner join BodyStyle
		on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId
	inner join Transmission
		on Transmission.TransmissionId = InventoryDetails.TransmissionId
	inner join Color
		on Color.ColorId = InventoryDetails.ColorId
	inner join Interior
		on Interior.InteriorId = InventoryDetails.InteriorId
		where TypeId = 1
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryDetailsUsed')
		drop procedure InventoryDetailsUsed
go

create procedure InventoryDetailsUsed
as
begin
select InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, Mileage, Vin, SalePrice, MSRP, ImageFileName, TypeId
from InventoryDetails
	inner join Makes
		on Makes.MakeId = InventoryDetails.MakeId
	inner join CarModels
		on CarModels.ModelId = InventoryDetails.ModelId
	inner join BodyStyle
		on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId
	inner join Transmission
		on Transmission.TransmissionId = InventoryDetails.TransmissionId
	inner join Color
		on Color.ColorId = InventoryDetails.ColorId
	inner join Interior
		on Interior.InteriorId = InventoryDetails.InteriorId
		where TypeId = 2
end

go
 
if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SpecialsSelectAll')
		drop procedure SpecialsSelectAll
go

create procedure SpecialsSelectAll as
begin
	select * from Specials
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'ContactUsSelectAll')
		drop procedure ContactUsSelectAll
go

create procedure ContactUsSelectAll as
begin
	select * from ContactUs
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'ContactUsInsert')
		drop procedure ContactUsInsert
go

create procedure ContactUsInsert (
	@ContactUsId int output,
	@Name nvarchar(50),
	@Email nvarchar(256),
	@Phone nvarchar(25),
	@Message nvarchar(256)
)
as
begin
	insert into ContactUs([Name], Email, Phone, [Message])
	values (@Name, @Email, @Phone, @Message);

	set @ContactUsId = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SalesView')
		drop procedure SalesView
go

create procedure SalesView
as
begin
select InventoryId, [Year], Make, Model, BodyStyleName, TransmissionName, ColorName, InteriorName, Mileage, Vin, SalePrice, MSRP, ImageFileName
from InventoryDetails
	inner join Makes
		on Makes.MakeId = InventoryDetails.MakeId
	inner join CarModels
		on CarModels.ModelId = InventoryDetails.ModelId
	inner join BodyStyle
		on BodyStyle.BodyStyleId = InventoryDetails.BodyStyleId
	inner join Transmission
		on Transmission.TransmissionId = InventoryDetails.TransmissionId
	inner join Color
		on Color.ColorId = InventoryDetails.ColorId
	inner join Interior
		on Interior.InteriorId = InventoryDetails.InteriorId
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'RoleSelectAll')
		drop procedure RoleSelectAll
go

create procedure RoleSelectAll as
begin
	select * from [Role]
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SalesPurchaseInsert')
		drop procedure SalesPurchaseInsert
go

create procedure SalesPurchaseInsert (
	@SalesPurchaseId int output,
	@InventoryId int,
	@UserId nvarchar(128),
	@Name nvarchar(128),
	@Email nvarchar(256),
	@Phone nvarchar(25),
	@Street1 nvarchar(256),
	@Street2 nvarchar(256),
	@City nvarchar(50),
	@State char(2),
	@ZipCode int,
	@PurchasePrice int,
	@PurchaseTypeId int
)
as
begin
	insert into SalesPurchase(InventoryId, UserId, [Name], Email, Phone, Street1, Street2, City,	[State], ZipCode, PurchasePrice, PurchaseType)
	values (@InventoryId, @UserId, @Name, @Email, @Phone, @Street1, @Street2, @City, @State, @ZipCode, @PurchasePrice, @PurchaseTypeId);

	set @SalesPurchaseId = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'PurchaseTypeSelectAll')
		drop procedure PurchaseTypeSelectAll
go

create procedure PurchaseTypeSelectAll as
begin
	select * from PurchaseType
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SalesPurchaseSelectAll')
		drop procedure SalesPurchaseSelectAll
go

create procedure SalesPurchaseSelectAll as
begin
	select * from SalesPurchase
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'AdminUsersSelectAll')
		drop procedure AdminUsersSelectAll
go

create procedure AdminUsersSelectAll
as
begin
	select AspNetUsers.Id, LastName, FirstName, Email, AspNetRoles.Id as RoleId, AspNetRoles.[Name] as RoleName
from AspNetUsers
	inner join AspNetUserRoles
		on AspNetUserRoles.UserId = AspNetUsers.Id
	inner join AspNetRoles
		on AspNetRoles.Id = AspNetUserRoles.RoleId
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'AdminMakeInsert')
		drop procedure AdminMakeInsert
go

create procedure AdminMakeInsert (
	@MakeId int output,
	@Make nvarchar(30),
	@Email nvarchar(256)
)
as
begin
	insert into Makes(Make, Email)
	values (@Make, @Email);

	set @MakeId = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'AdminModelInsert')
		drop procedure AdminModelInsert
go

create procedure AdminModelInsert (
	@ModelId int output,
	@Model nvarchar(50),
	@MakeId int,
	@Email nvarchar(256)
)
as
begin
	insert into CarModels(Model, MakeId, Email)
	values (@Model, @MakeId, @Email);

	set @ModelId = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'AdminSpecialInsert')
		drop procedure AdminSpecialInsert
go

create procedure AdminSpecialInsert (
	@SpecialId int output,
	@Title nvarchar(50),
	@Description nvarchar(256),
	@ImageFileName nvarchar(50)
)
as
begin
	insert into Specials(Title, [Description], ImageFileName)
	values (@Title, @Description, @ImageFileName);

	set @SpecialId = SCOPE_IDENTITY();
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'AdminSpecialDelete')
		drop procedure AdminSpecialDelete
go

create procedure AdminSpecialDelete (
	@SpecialId int
) as
begin
	begin transaction

	delete from Specials where SpecialId = @SpecialId;

	commit transaction
end
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'SalesReport')
		drop procedure SalesReport
go

create procedure SalesReport
as
begin
select UserId, FirstName, LastName, sum(PurchasePrice) as 'TotalSales', count(*) as 'TotalVehicles'
from SalesPurchase
	inner join AspNetUsers
		on SalesPurchase.UserId = AspNetUsers.Id
	group by UserId, FirstName, LastName
end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryReportNew')
		drop procedure InventoryReportNew
go

create procedure InventoryReportNew
as
begin
select [Year], Make, Model, count(*) as 'Count', sum(MSRP) as 'StockValue'
from InventoryDetails
	inner join Makes
		on InventoryDetails.MakeId = Makes.MakeId
	inner join CarModels
		on InventoryDetails.ModelId = CarModels.ModelId
		where TypeId = 1 and IsSold = 0
	group by [Year], Make, Model

end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'InventoryReportUsed')
		drop procedure InventoryReportUsed
go

create procedure InventoryReportUsed
as
begin
select [Year], Make, Model, count(*) as 'Count', sum(MSRP) as 'StockValue'
from InventoryDetails
	inner join Makes
		on InventoryDetails.MakeId = Makes.MakeId
	inner join CarModels
		on InventoryDetails.ModelId = CarModels.ModelId
		where TypeId = 2 and IsSold = 0
	group by [Year], Make, Model

end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'GetPrices')
		drop procedure GetPrices
go

create procedure GetPrices
as
begin
select SalePrice from InventoryDetails
	group by SalePrice
	order by SalePrice asc

end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'GetYears')
		drop procedure GetYears
go

create procedure GetYears
as
begin
select [Year] from InventoryDetails
	group by [Year]
	order by [Year] asc

end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'GetRoles')
		drop procedure GetRoles
go

create procedure GetRoles
as
begin
select * from AspNetRoles
order by Id asc

end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'AdminGetModels')
		drop procedure AdminGetModels
go

create procedure AdminGetModels
as
begin
select Make, Model, CarModels.DateAdded, CarModels.Email 
from CarModels
	inner join Makes on CarModels.MakeId = Makes.MakeId
group by Make, Model, CarModels.DateAdded, CarModels.Email

end

go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'Sold')
		drop procedure Sold
go

create procedure Sold (
	@InventoryId int
)
as
begin
	update InventoryDetails set
	IsSold = 1

	where InventoryId = @InventoryId
end

go






