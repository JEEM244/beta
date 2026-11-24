USE master;
GO

CREATE DATABASE Cartelera5L1;
GO

USE Cartelera5L1;
GO

CREATE TABLE Salas5L1 (
    IdSala INT IDENTITY(1,1) PRIMARY KEY,
    NombreSala VARCHAR(40),
    Capacidad INT,
    TipoSala VARCHAR(30),
    Activa BIT
);
GO

CREATE TABLE Movies5L1 (
    IdMovie INT IDENTITY(1,1) PRIMARY KEY,
    Titulo VARCHAR(100),
    Genero VARCHAR(40),
    DuracionMinutos INT,
    Clasificacion VARCHAR(10),
    FechaEstreno DATE,
    Sinopsis VARCHAR(500),
    IdSala INT,
    FOREIGN KEY (IdSala) REFERENCES Salas5L1(IdSala)
);
GO

INSERT INTO Salas5L1 (NombreSala, Capacidad, TipoSala, Activa)
VALUES
('Sala 1', 120, 'Tradicional', 1),
('Sala 2', 80, '3D', 1),
('Sala VIP', 45, 'VIP', 1);
GO

SELECT * FROM Salas5L1;
SELECT * FROM Movies5L1;
GO
