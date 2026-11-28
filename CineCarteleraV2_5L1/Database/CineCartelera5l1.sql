-- CREAR BASE DE DATOS
CREATE DATABASE CineCartelera5l1;
GO

USE CineCartelera5l1;
GO

-- TABLA DE PELICULAS
CREATE TABLE Peliculas
(
    IdPelicula INT IDENTITY(1,1) PRIMARY KEY,
    Titulo NVARCHAR(120) NOT NULL,
    Genero NVARCHAR(50) NOT NULL,
    DuracionMinutos SMALLINT NOT NULL,
    Clasificacion NVARCHAR(10) NOT NULL,
    FechaEstreno DATE NULL,
    Sinopsis NVARCHAR(600) NULL,
    Disponible BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Peliculas_Duracion
        CHECK (DuracionMinutos > 0)
);
GO

-- TABLA DE SALAS
CREATE TABLE Salas
(
    IdSala INT IDENTITY(1,1) PRIMARY KEY,
    NombreSala NVARCHAR(40) NOT NULL,
    Capacidad SMALLINT NOT NULL,
    FormatoSala NVARCHAR(30) NOT NULL,
    Activa BIT NOT NULL DEFAULT 1,

    CONSTRAINT CK_Salas_Capacidad
        CHECK (Capacidad > 0)
);
GO

-- TABLA DE FUNCIONES
CREATE TABLE Funciones
(
    IdFuncion INT IDENTITY(1,1) PRIMARY KEY,
    IdPelicula INT NOT NULL,
    IdSala INT NOT NULL,
    FechaFuncion DATE NOT NULL,
    HoraInicio TIME(0) NOT NULL,
    Precio DECIMAL(8,2) NOT NULL,
    Idioma NVARCHAR(30) NOT NULL,

    CONSTRAINT FK_Funciones_Peliculas
        FOREIGN KEY (IdPelicula)
        REFERENCES Peliculas(IdPelicula),

    CONSTRAINT FK_Funciones_Salas
        FOREIGN KEY (IdSala)
        REFERENCES Salas(IdSala),

    CONSTRAINT CK_Funciones_Precio
        CHECK (Precio >= 0),

    CONSTRAINT UQ_Funciones_Horario
        UNIQUE (IdSala, FechaFuncion, HoraInicio)
);
GO

-- CONSULTAR LAS TABLAS
SELECT * FROM Peliculas;
SELECT * FROM Salas;
SELECT * FROM Funciones;
GO
