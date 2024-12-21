CREATE DATABASE ConstructoresAvance
GO

USE ConstructoresAvance
GO

CREATE TABLE Empleados
(
    EmpleadoID INT PRIMARY KEY IDENTITY,
    NumeroCarnet VARCHAR(50) UNIQUE,
    Nombre VARCHAR(100),
    FechaNacimiento DATE,
    Categoria VARCHAR(50) CHECK (Categoria IN ('Administrador', 'Operario', 'Peón')),
    Salario NUMERIC(10, 2) CHECK (Salario >= 250000 AND Salario <= 500000) DEFAULT 250000,
    Direccion VARCHAR(50) DEFAULT 'San José',
    Telefono VARCHAR(50),
    Correo VARCHAR(50) UNIQUE,
    Estado VARCHAR(50) CHECK (Estado IN ('Activo', 'Inactivo')),
    CONSTRAINT chk_MayorDeEdad CHECK (DATEDIFF(YEAR, FechaNacimiento, GETDATE()) >= 18)
)

CREATE TABLE Proyectos
(
    ProyectoID INT PRIMARY KEY IDENTITY, 
    CodigoProyecto INT UNIQUE,
    Nombre VARCHAR(50) UNIQUE,
    FechaInicio DATE,
    FechaFin DATE,
    Estado VARCHAR(50) CHECK (Estado IN ('Activo', 'Inactivo'))
)

CREATE TABLE Asignaciones
(
    AsignacionID INT PRIMARY KEY IDENTITY, 
    EmpleadoID INT FOREIGN KEY REFERENCES Empleados(EmpleadoID),
    ProyectoID INT FOREIGN KEY REFERENCES Proyectos(ProyectoID),
    FechaAsignacion DATE,
    Estado VARCHAR(50) CHECK (Estado IN ('Activo', 'Inactivo'))
)

CREATE TABLE EmpleadosCategorias
(
    CategoriaID INT PRIMARY KEY,
    NombreCategoria VARCHAR(50)
)

INSERT INTO EmpleadosCategorias VALUES 
(1, 'Administrador'),
(2, 'Operador'),
(3, 'Peón')

CREATE TABLE Estado
(
    ID INT PRIMARY KEY,
    Descripcion VARCHAR(20)
)

INSERT INTO Estado VALUES 
(1, 'Activo'),
(2, 'Inactivo')


CREATE PROCEDURE consultarEmpleadoFiltro
    @EmpleadoID INT
AS 
BEGIN 
    SELECT * FROM Empleados WHERE EmpleadoID = @EmpleadoID;
END
GO

CREATE PROCEDURE consultarEmpleado
AS 
BEGIN 
    SELECT * FROM Empleados WHERE Estado = 'Activo'
END
GO

CREATE PROCEDURE ingresarEmpleado
    @NumeroCarnet VARCHAR(50),
    @Nombre VARCHAR(100),
    @FechaNacimiento DATE,
    @Categoria VARCHAR(50),
    @Salario NUMERIC(10, 2),
    @Direccion VARCHAR(50),
    @Telefono VARCHAR(50),
    @Correo VARCHAR(50),
    @Estado VARCHAR(50)
AS 
BEGIN 
    INSERT INTO Empleados (NumeroCarnet, Nombre, FechaNacimiento, Categoria, Salario, Direccion, Telefono, Correo, Estado)
    VALUES (@NumeroCarnet, @Nombre, @FechaNacimiento, @Categoria, @Salario, @Direccion, @Telefono, @Correo, @Estado);
END
GO


EXEC ingresarEmpleado '2828828', 'kea','11/05/04','Administrador',300000,'San Jose','00992211','keagmail.com','Activo'


exec consultarEmpleado

CREATE PROCEDURE borrarEmpleado
    @EmpleadoID INT 
AS 
BEGIN 
    UPDATE Empleados SET Estado = 'Inactivo' WHERE EmpleadoID = @EmpleadoID;
END
GO

CREATE PROCEDURE consultarProyecto
AS 
BEGIN 
    SELECT * FROM Proyectos WHERE Estado = 'Activo';
END
GO

CREATE PROCEDURE ingresarProyecto
    @CodigoProyecto INT,
    @Nombre VARCHAR(50),
    @FechaInicio DATE,
    @FechaFin DATE,
    @Estado VARCHAR(50)
AS 
BEGIN 
    INSERT INTO Proyectos (CodigoProyecto, Nombre, FechaInicio, FechaFin, Estado)
    VALUES (@CodigoProyecto, @Nombre, @FechaInicio, @FechaFin, @Estado);
END
GO

exec ingresarProyecto 13,'A','2024-12-15','2025-12-15','Activo'

exec consultarEmpleado

CREATE PROCEDURE borrarProyecto
    @ProyectoID INT 
AS 
BEGIN 
    UPDATE Proyectos SET Estado = 'Inactivo' WHERE ProyectoID = @ProyectoID;
END
GO

CREATE PROCEDURE consultarAsignacion
AS 
BEGIN 
    SELECT * FROM Asignaciones WHERE Estado = 'Activo'
END
GO

CREATE PROCEDURE ingresarAsignacion
    @EmpleadoID INT,
    @ProyectoID INT,
    @FechaAsignacion DATE,
    @Estado VARCHAR(50)
AS 
BEGIN 
    INSERT INTO Asignaciones (EmpleadoID, ProyectoID, FechaAsignacion, Estado)
    VALUES (@EmpleadoID, @ProyectoID, @FechaAsignacion, @Estado);
END
GO

exec ingresarAsignacion 3,1,'2024-12-15','Activo'

exec consultarEmpleado

exec consultarProyecto

CREATE PROCEDURE borrarAsignacion
    @AsignacionID INT 
AS 
BEGIN 
    UPDATE Asignaciones SET Estado = 'Inactivo' WHERE AsignacionID = @AsignacionID;
END
GO






