create database TableFinder
go

use TableFinder
go

create table cadastro
(
	id_usuario		integer identity(1,1) primary key,
	nome_completo	varchar(50),
	cpf				varchar(50),
	email			varchar(50),
	login			varchar(12),
	senha			varchar(500),
	administrador	bit default 0
);

insert into cadastro values ('Administrador','065.961.317-42', 'admin@tablefinder.com.br', 'Admin', 'admin123', 1);
insert into cadastro values ('Usu�rio 1','030.504.189-40', 'usuario1@tablefinder.com.br', 'Usuario1', '123', 0);

create table estabelecimento
(
	id_estabelecimento  integer identity(1,1) primary key,
	nome				varchar(50),
	descricao			varchar(max),
	imagem				varchar(2000),
	cnpj				varchar(50),
	localizacao			varchar(1000),
	aprovado			int default 0,
	id_usuario			int foreign key references cadastro (id_usuario),
);

insert into estabelecimento
values
(
	'Churrascaria Espet�o',
	'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
	'hamburguer.jpg',
	'71.845.217/0001-40',
	'Rodovia BR-116, Km 10, 22730 - Loja-4 - Tatuquara, Curitiba - PR, 81690-500',
	'1',
	'2'
);

insert into estabelecimento
values
(
	'Marcelu�s lanches',
	'Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',
	'costela-de-chao-de-forno-do-jimmy-31937.jpg',
	'21.187.149/0001-00',
	'Av. Presidente Arthur da Silva Bernardes, 1069 - Port�o, Curitiba - PR, 80320-300',
	'1',
	'2'
);

create table feedback
(
	id_feedback			integer identity(1,1) primary key not null,
	id_usuario			int foreign key references cadastro (id_usuario),
	id_estabelecimento  int foreign key references estabelecimento (id_estabelecimento),
	data_hora			datetime not null default getdate(),
	opiniao				varchar(max),
	nota				int null
);

create table tipo_comida
(
	tipoId		int identity(1,1) primary key,
	tipoNome	varchar(20)
);

insert into tipo_comida
values
('Pizza'),
('Carne'),
('Massa'),
('Oriental'),
('Vegetariano'),
('Vegano');

create table cardapio
(
	id_cardapio			integer identity(1,1) primary key,
	id_estabelecimento  integer references estabelecimento (id_estabelecimento),
	id_tipo				integer references tipo_comida (tipoId),
	produto				varchar(200),
	descricao			varchar(max),
	preco				decimal(15,2)
)

insert into cardapio
values (1, 1, 'Carne de Boi','Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea commodo consequat. Duis aute irure dolor in reprehenderit in voluptate velit esse cillum dolore eu fugiat nulla pariatur. Excepteur sint occaecat cupidatat non proident, sunt in culpa qui officia deserunt mollit anim id est laborum.',50.00);
insert into cardapio 
values (2, 2, 'Hamburguer caseiro','Feito especialmente para pessoas com gostos refinados macio e delicioso, diferentciado.',30.00);