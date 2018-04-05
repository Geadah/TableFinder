create database TableFinder
go

use TableFinder
go

create table cadastro
(
	id_usuario integer identity primary key,
	nome_completo varchar(50),
	cpf varchar(50),
	email varchar(50),
	login varchar(12),
	senha varchar(500)
)

select * from cadastro


create table estabelecimento
(
id_estabelecimento integer identity primary key,
nome varchar(50),
descricao text,
imagem varchar(1000),
cnpj varchar(50),
localizacao varchar(100),

)

insert into estabelecimento
values
(
''
)

drop table estabelecimento

create table cardapio
(
id_estabelecimento integer identity primary key references estabelecimento (id_estabelecimento),
produto varchar(50),
descricao text,
preco varchar(50),
imagem varchar(1000),
)
select * from cardapio
drop table cardapio
