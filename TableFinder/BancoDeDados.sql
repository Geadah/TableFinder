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
'Hamburgão do Gilberto','dsadjsadjsajdhsadhsadjsahdljsahdhsalkdjksahdlksahsahd
hsajdhashdljsahdlkhsalkdhsalkdhsalkhdlksakhdsalkdhsalkhdksahdlksahdksa
sahldhsalkdhsalkhdksahdlksahlkdhsalkdhsalkhdlksadlkhsadhsalkhdsadlkhsakdhlksahd','sadsadd','354354242435454','Casa da tua mãe'
)

select * from estabelecimento

drop table estabelecimento

create table cardapio
(
id_cardapio integer identity primary key,
id_estabelecimento integer references estabelecimento (id_estabelecimento),
produto varchar(50),
tipo varchar(50),
descricao text,
preco varchar(50),
imagem varchar(1000),
)



insert into cardapio
values
('1','Carne de Cavalo','Carne','Carne do Cavalo do seu pai','R$200,00','dsadd')

select * from cardapio
drop table cardapio


update estabelecimento set imagem = 'hamburguer.jpg' where id_estabelecimento = 1;