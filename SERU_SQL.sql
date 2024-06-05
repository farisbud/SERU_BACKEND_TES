create table users(
id int primary key identity,
[name] varchar(300) null,
[password] varchar(100) null,
is_admin bit,
created_at datetime,
updated_at datetime
)


create table vehicle_brand(
id int primary key identity,
[name] varchar(300) null,
created_at datetime,
updated_at datetime
)


create table vehicle_type(
id int primary key identity,
[name] varchar(300) null,
brand_id int foreign key references vehicle_brand(id),
created_at datetime,
updated_at datetime
)

create table vehicle_model(
id int primary key identity,
[name] varchar(300) null,
[type_id] int foreign key references vehicle_type(id),
created_at datetime,
updated_at datetime
)

create table vehicle_year(
id int primary key identity,
[year] varchar(20) null,
created_at datetime,
updated_at datetime
)

create table pricelist(
id int primary key identity,
code varchar(50) null,
price float,
year_id int foreign key references vehicle_year(id), 
model_id int foreign key references vehicle_model(id),
created_at datetime,
updated_at datetime
)

insert into vehicle_brand
select 'BMW',GETDATE(), null UNION
select 'VOLVO',GETDATE(), null UNION
select 'JEEP',GETDATE(), null UNION
select 'HONDA',GETDATE(), null UNION
select 'TOYOTA',GETDATE(), null UNION
select 'DAIHATSU',GETDATE(), null UNION
select 'MITSHUBISHI',GETDATE(), null UNION
select 'RANGE ROVER',GETDATE(), null UNION
select 'HYUNDAI',GETDATE(), null UNION
select 'FORD',GETDATE(), null UNION
select 'CHEVROLET',GETDATE(), null UNION
select 'AUDI',GETDATE(), null UNION
select 'FERARI',GETDATE(), null UNION
select 'CAT',GETDATE(), null UNION
select 'PORSCHE',GETDATE(), null UNION
select 'MERCEDES',GETDATE(), null UNION
select 'SUBARU',GETDATE(), null UNION
select 'SUZUKI',GETDATE(), null

insert into vehicle_type
select 'SEDAN',1,GETDATE(), null UNION
select 'SUV',1,GETDATE(), null UNION
select 'TRUCK',1,GETDATE(), null

insert into vehicle_type
select 'SEDAN',2,GETDATE(), null UNION
select 'SUV',2,GETDATE(), null UNION
select 'TRUCK',2,GETDATE(), null union
select 'MOTORCYCLE',2,GETDATE(), null


