create database TableFinder
go

use TableFinder
go

create table cadastro
(
	nome_completo varchar(50),
	cpf varchar(50),
	email varchar(50),
	login varchar(12),
	senha varchar(500)
)

select * from cadastro