use GuildCars
go

if exists (select * from INFORMATION_SCHEMA.ROUTINES
	where ROUTINE_NAME = 'DbReset')
		drop procedure DbReset
go

create procedure DbReset as
begin
	delete from SalesPurchase;
	delete from InventoryDetails;
	delete from States;
	delete from PurchaseType;
	delete from BodyStyle;
	delete from AspNetUsers;
	delete from CarModels;
	delete from Makes;
	delete from Transmission;
	delete from Color;
	delete from Interior;
	delete from CarType;
	delete from Specials;
	delete from ContactUs;
	delete from [Role];
	delete from AspNetRoles;
	delete from AspNetUserRoles;

	DBCC CHECKIDENT ('InventoryDetails', RESEED, 1)
	DBCC CHECKIDENT ('ContactUs', RESEED, 1)
	DBCC CHECKIDENT ('SalesPurchase', RESEED, 1)
	DBCC CHECKIDENT ('Makes', RESEED, 1)
	DBCC CHECKIDENT ('CarModels', RESEED, 1)
	DBCC CHECKIDENT ('Specials', RESEED, 1)

	insert into States (StateId, StateName)
	values ('OH', 'Ohio'),
	('KY', 'Kentucky'),
	('MN', 'Minnesota');

	set identity_insert BodyStyle on;

	insert into BodyStyle (BodyStyleId, BodyStyleName)
	values (1, 'Car'),
	(2, 'Truck'),
	(3, 'SUV'),
	(4, 'Van');

	set identity_insert BodyStyle off;

	set identity_insert [Role] on;

	insert into [Role] (RoleId, RoleName)
	values (1, 'Admin'),
	(2, 'Sales'),
	(3, 'Disabled')
	set identity_insert [Role] off;

	insert into AspNetUsers(Id, FirstName, LastName, Email, EmailConfirmed, PasswordHash, SecurityStamp, PhoneNumberConfirmed, TwoFactorEnabled, LockoutEnabled, AccessFailedCount, UserName)
	values ('09155241-9436-43de-b523-f56c83f1c61f', 'Matty', 'Humble', 'admin@test.com', 0, 'AIVVAFet3UYa5lzW3w4VMV1s55gd6Wa12AhXRf4zy+abh8MpGdTKir6AIYITME9dFw==', 'a367dbe5-a81e-4c07-83bf-c5dbcefebbdf', 0, 0, 0, 0, 'admin@test.com'),
	('33013668-e794-4ec4-9093-6a662aaf416d', 'Sales', 'Guy1', 'salesguy1@test.com', 0, 'APgvP8hgfPDI/Iw4ZNkOdXaIO1zeskR56iwVZ+8i1dAhRkE7ytmBQrQnaiqiYLZM7g==', 'd6b2b7d8-59e0-4eb2-bd09-19c89301cd6a', 0, 0, 0, 0, 'salesguy1@test.com'),
	('350da951-ac05-4aa6-a044-210cc2e97166', 'Sales', 'Guy2', 'salesguy2@test.com', 0, 'AC5D7nNyj4bgBchBg9eIqhpz8PtvzWeyT2NreVZCIWpFpOcgiatnVnAT6pMiNQJnBw==', '4abf42ad-f5a5-4f35-be84-69d5b13d0c25', 0, 0, 0, 0, 'salesguy2@test.com'),
	('2ac90a73-8cd0-46be-b2bc-004708060e2c', 'Sales', 'Guy3', 'salesguy3@test.com', 0, 'AGgML/geUNp6PGuQzU8I8EzUUgtMFjNoOR55hWeIsrdYDNk6CO6zWhIyIeurr9Z51A==', '91c7746a-40f4-43d7-b4cc-5c6a1585c80d', 0, 0, 0, 0, 'salesguy3@test.com');

	set identity_insert Makes on;

	insert into Makes(MakeId, Make, Email)
	values (1, 'Audi', 'sam@guildcars.com'),
	(2, 'Buick', 'sam@guildcars.com');

	set identity_insert Makes off;

	set identity_insert CarModels on;

	insert into CarModels(ModelId, Model, MakeId, Email)
	values (1, 'A4', 1, 'sam@guildcars.com'),
		(2, 'A6', 1, 'sam@guildcars.com'),
		(3, 'Century', 2, 'sam@guildcars.com'),
		(4, 'Park Avenue', 2, 'sam@guildcars.com')

	set identity_insert CarModels off;

	set identity_insert Transmission on;

	insert into Transmission(TransmissionId, TransmissionName)
	values (1, 'Automatic'),
	(2, 'Manual');

	set identity_insert Transmission off;

	set identity_insert Color on;

	insert into Color(ColorId, ColorName)
	values (1, 'Black'),
	(2, 'Red'),
	(3, 'Green'),
	(4, 'White'),
	(5, 'Blue');

	set identity_insert Color off;

	set identity_insert Interior on;

	insert into Interior(InteriorId, InteriorName)
	values (1, 'Black'),
	(2, 'Gray'),
	(3, 'Brown'),
	(4, 'White'),
	(5, 'Blue'); 

	set identity_insert Interior off;

	set identity_insert CarType on;

	insert into CarType(TypeId, TypeName)
	values (1, 'New'),
	(2, 'Used');

	set identity_insert CarType off;

	set identity_insert InventoryDetails on;

	insert into InventoryDetails(InventoryId, [Year], MakeId, ModelId, BodyStyleId, TransmissionId, ColorId, InteriorId, Mileage, Vin, SalePrice, MSRP, TypeId, [Description], ImageFileName)
	values (1, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20000, 22000, 1, 'A new car', 'test.jpg'),
	(2, 2021, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20001, 22000, 2, 'A used car', 'test.jpg'),
	(3, 2021, 1, 2, 1, 1, 1, 1, 0, 'T35TV1N', 20002, 22000, 1, 'A new car', 'test.jpg'),
	(4, 2003, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20003, 22000, 2, 'A used car', 'test.jpg'),
	(5, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20004, 22000, 1, 'A new car', 'test.jpg'),
	(6, 2021, 1, 1, 1, 1, 1, 1, 10000, 'T35TV1N', 20004, 22000, 2, 'A used car', 'test.jpg'),
	(7, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20006, 22000, 1, 'A new car', 'test.jpg'),
	(8, 2003, 1, 2, 1, 1, 1, 1, 1000, 'T35TV1N', 20007, 22000, 2, 'A used car', 'test.jpg'),
	(9, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20008, 22000, 1, 'A new car', 'test.jpg'),
	(10, 2021, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20009, 22000, 2, 'A used car', 'test.jpg')

	set identity_insert InventoryDetails off;

	set identity_insert InventoryDetails on;

	insert into InventoryDetails(InventoryId, [Year], MakeId, ModelId, BodyStyleId, TransmissionId, ColorId, InteriorId, Mileage, Vin, SalePrice, MSRP, TypeId, [Description], ImageFileName, IsSold)
	values (11, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20000, 22000, 1, 'A new car', 'test.jpg', 1),
	(12, 2021, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20001, 22000, 2, 'A used car', 'test.jpg', 1),
	(13, 2021, 1, 2, 1, 1, 1, 1, 0, 'T35TV1N', 20002, 22000, 1, 'A new car', 'test.jpg', 1),
	(14, 2003, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20003, 22000, 2, 'A used car', 'test.jpg', 1),
	(15, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20004, 22000, 1, 'A new car', 'test.jpg', 1),
	(16, 2021, 1, 1, 1, 1, 1, 1, 10000, 'T35TV1N', 20004, 22000, 2, 'A used car', 'test.jpg', 1)

	set identity_insert InventoryDetails off;

	set identity_insert InventoryDetails on;

	insert into InventoryDetails(InventoryId, [Year], MakeId, ModelId, BodyStyleId, TransmissionId, ColorId, InteriorId, Mileage, Vin, SalePrice, MSRP, TypeId, [Description], ImageFileName, IsFeatured)
	values (17, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20000, 22000, 1, 'A new car', 'test.jpg', 1),
	(18, 2021, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20001, 22000, 2, 'A used car', 'test.jpg', 1),
	(19, 2021, 1, 2, 1, 1, 1, 1, 0, 'T35TV1N', 20002, 22000, 1, 'A new car', 'test.jpg', 1),
	(20, 2003, 1, 1, 1, 1, 1, 1, 1000, 'T35TV1N', 20003, 22000, 2, 'A used car', 'test.jpg', 1),
	(21, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20004, 22000, 1, 'A new car', 'test.jpg', 1),
	(22, 2021, 1, 1, 1, 1, 1, 1, 10000, 'T35TV1N', 20004, 22000, 2, 'A used car', 'test.jpg', 1),
	(23, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20006, 22000, 1, 'A new car', 'test.jpg', 1),
	(24, 2003, 1, 2, 1, 1, 1, 1, 1000, 'T35TV1N', 20007, 22000, 2, 'A used car', 'test.jpg', 1),
	(25, 2021, 1, 1, 1, 1, 1, 1, 0, 'T35TV1N', 20008, 22000, 1, 'A new car', 'test.jpg', 1)

	set identity_insert InventoryDetails off;

	set identity_insert Specials on;

	insert into Specials (SpecialId, Title, [Description], ImageFileName)
	values (1, 'Car Special', 'You get a car!', 'test1.jpg'),
	(2, 'Truck Special', 'You get a truck!', 'test1.jpg'),
	(3, 'SUV Special', 'You get an SUV!', 'test1.jpg')
	set identity_insert Specials off;

	set identity_insert ContactUs on;

	insert into ContactUs (ContactUsId, [Name], Email, Phone, [Message])
	values (1, 'Todd', 'todd@test.com', '1111111111', 'I WANNA BUY CAR!'),
	(2, 'Rod', 'rod@test.com', '2222222222', 'I WANNA BUY TRUCK!'),
	(3, 'Clod', 'clod@test.com', '3333333333', 'I WANNA BUY SUV!')
	set identity_insert ContactUs off;

	set identity_insert PurchaseType on;

	insert into PurchaseType(PurchaseTypeId, PurchaseTypeName)
	values (1, 'Dealer Finance'),
	(2, 'Cash'),
	(3, 'Bank Finance')

	set identity_insert PurchaseType off;

	set identity_insert SalesPurchase on;

	insert into SalesPurchase (SalesPurchaseId, InventoryId, UserId, [Name], Email, Phone, Street1, Street2, City, [State], ZipCode, PurchasePrice, PurchaseType)
	values (1, 11, '33013668-e794-4ec4-9093-6a662aaf416d', 'Test Purchase1', 'tp1@test.com', '651-484-4810', '1030 Lake Beach Dr', null, 'Shoreview', 'MN', '55126', 20000, 1),
	(2, 12, '350da951-ac05-4aa6-a044-210cc2e97166', 'Test Purchase2', 'tp1@test.com', '651-484-4810', '1030 Lake Beach Dr', null, 'Shoreview', 'MN', '55126', 20000, 1),
	(3, 13, '2ac90a73-8cd0-46be-b2bc-004708060e2c', 'Test Purchase3', 'tp1@test.com', '651-484-4810', '1030 Lake Beach Dr', null, 'Shoreview', 'MN', '55126', 20000, 1),
	(4, 14, '350da951-ac05-4aa6-a044-210cc2e97166', 'Test Purchase4', 'tp1@test.com', '651-484-4810', '1030 Lake Beach Dr', null, 'Shoreview', 'MN', '55126', 20000, 2),
	(5, 15, '33013668-e794-4ec4-9093-6a662aaf416d', 'Test Purchase5', 'tp1@test.com', '651-484-4810', '1030 Lake Beach Dr', null, 'Shoreview', 'MN', '55126', 19000, 2),
	(6, 16, '2ac90a73-8cd0-46be-b2bc-004708060e2c', 'Test Purchase6', 'tp1@test.com', '651-484-4810', '1030 Lake Beach Dr', null, 'Shoreview', 'MN', '55126', 18000, 2)

	set identity_insert SalesPurchase off;

	insert into AspNetRoles(Id, [Name])
	values ('1', 'Admin'),
	('2', 'Sales'),
	('3', 'Disabled')

	insert into AspNetUserRoles(UserId, RoleId)
	values ('09155241-9436-43de-b523-f56c83f1c61f', '1'),
	('33013668-e794-4ec4-9093-6a662aaf416d', '2'),
	('350da951-ac05-4aa6-a044-210cc2e97166', '2'),
	('2ac90a73-8cd0-46be-b2bc-004708060e2c', '2');
end

