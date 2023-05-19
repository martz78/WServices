create database dbServicioEstudiantil
go

create table estudiante(
id int primary key identity (1,1),
nombre varchar(20),
apellido varchar(20),
telefono varchar(20)
);

select * from estudiante
