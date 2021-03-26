use master
go

if exists(select * from sys.databases where name='GuildFlooring')
drop database GuildFlooring
go

create database GuildFlooring
go

use GuildFlooring
go