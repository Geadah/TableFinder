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

insert into estabelecimento
values
(
'Iguarias do Edemar','Só carninha da boa e feita na hora
tudo feito fresquinho e bem saboroso pelo grandioso e renomado
chefe, o Edemar Raimundo Eliziano da Silva, o melhor chefe de
cozinha do nordeste','sadsda','564564564564','Rua Chinelo Rasgado'
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
insert into cardapio
values
('2','Carne de Elefante','Carne','Carne da elefanta da sua tia aquela gorda','R$2548,00','dsaddsa')

select * from cardapio
drop table cardapio


update estabelecimento set imagem = 'hamburguer.jpg' where id_estabelecimento = 1;

update estabelecimento set imagem = 'smalahove_just_eaten.jpg' where id_estabelecimento = 2;



create table feedback
(
id_feedback integer identity primary key not null,
id_usuario int foreign key references cadastro (id_usuario),
id_estabelecimento int foreign key references estabelecimento (id_estabelecimento),
data_hora datetime not null default getdate(),
opiniao text,
nota int null
)

drop table feedback


SELECT 
                                    p.*, 
                                    u.nome_completo as usuario 
                                FROM feedback p 
                                INNER JOIN cadastro u ON (u.id_usuario = p.id_usuario);


								select * from feedback;


create table tipo_comida
(
tipoId int identity primary key,
tipoNome varchar(20),

)	
select * from tipo_comida

insert into tipo_comida
values
('Pizza'),
('Carne'),
('Massa'),
('Oriental'),
('Vegetariano'),
('Vegano')								