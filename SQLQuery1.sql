Use NgCookingX
go

Create Table Categories(

id varchar(100) not null Primary key,
name varchar(250) not null
)
go

Create Table Ingredient(
id varchar(200) not null Primary key,
name varchar(250) not null,
isAvailable bit not null default (0),
picture varchar(250),
calories int,
category varchar(100) not null references Categories(id)
)
go

Create Table Communaute(
firstname varchar(250),
surname varchar(250),
id int primary key,
email varchar(250),
password varchar(250),
level int,
picture varchar(250),
city varchar(250),
birth varchar(4),
bio varchar(250)
)
go 

create Table Recettes(
id varchar(250) not null primary key,
name varchar(250),
creatorId int references Communaute(id),
isAvailable bit not null default (0),
picture varchar(250),
calories int,
preparation varchar(2000)
)

go 

create table Comment(
userID int references Communaute(id),
title varchar(150),
comment varchar(250),
mark int,
recipeId varchar(250) references Recettes(id),
Primary key (userID, recipeID)
)
go

create table Liste(
ingredientId varchar(200) not null references Ingredient(id),
recipeId varchar(250) not null references Recettes(id),
primary key(ingredientId, recipeId)
)

