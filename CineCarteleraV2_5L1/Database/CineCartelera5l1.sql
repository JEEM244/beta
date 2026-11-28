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

-- DATOS DE EJEMPLO: 4 PELICULAS
INSERT INTO Peliculas
    (Titulo, Genero, DuracionMinutos, Clasificacion, FechaEstreno, Sinopsis, Disponible)
VALUES
    (N'Horizonte de ceniza', N'Ciencia ficción', 118, N'B15', '2026-11-20',
     N'Una tripulación intenta encontrar un nuevo hogar después de recibir una señal desconocida.', 1),
    (N'El mapa de los sueños', N'Aventura', 105, N'A', '2026-10-15',
     N'Dos hermanos siguen un mapa antiguo que los lleva a lugares que creían imaginarios.', 1),
    (N'Código lunar', N'Thriller', 124, N'B', '2026-09-03',
     N'Una programadora descubre un mensaje oculto en una transmisión procedente de la Luna.', 1),
    (N'La casa del faro', N'Suspenso', 98, N'B15', '2026-08-22',
     N'Un visitante llega a un faro aislado y descubre que sus habitantes guardan un secreto.', 1);
GO

-- DATOS DE EJEMPLO: 3 SALAS
INSERT INTO Salas (NombreSala, Capacidad, FormatoSala, Activa)
VALUES
    (N'Sala 1', 90, N'Tradicional', 1),
    (N'Sala 2', 60, N'3D', 1),
    (N'Sala VIP', 30, N'VIP', 1);
GO

-- DATOS DE EJEMPLO: 3 FUNCIONES
INSERT INTO Funciones
    (IdPelicula, IdSala, FechaFuncion, HoraInicio, Precio, Idioma)
VALUES
    (1, 1, '2026-12-01', '16:00', 75.00, N'Español'),
    (2, 2, '2026-12-01', '18:30', 90.00, N'Subtitulada'),
    (3, 3, '2026-12-02', '20:00', 120.00, N'Español');
GO

-- CONSULTAR LAS TABLAS
SELECT * FROM Peliculas;
SELECT * FROM Salas;
SELECT * FROM Funciones;
GO