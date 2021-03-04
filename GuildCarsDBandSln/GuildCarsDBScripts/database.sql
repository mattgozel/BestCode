use master
go

if exists(select * from sys.databases where name='GuildCars')
drop database GuildCars
go

create database GuildCars
go

use GuildCars
go