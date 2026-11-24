USE master;
GO

IF DB_ID(N'Cartelera5L1') IS NULL
BEGIN
    CREATE DATABASE Cartelera5L1;
END
GO

USE Cartelera5L1;
GO

IF OBJECT_ID(N'dbo.Salas5L1', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Salas5L1
    (
        IdSala INT IDENTITY(1,1) NOT NULL,
        NombreSala NVARCHAR(40) NOT NULL,
        Capacidad INT NOT NULL,
        TipoSala NVARCHAR(30) NULL,
        Activa BIT NOT NULL CONSTRAINT DF_Salas5L1_Activa DEFAULT (1),
        CONSTRAINT PK_Salas5L1 PRIMARY KEY (IdSala),
        CONSTRAINT CK_Salas5L1_Capacidad CHECK (Capacidad > 0)
    );
END
GO

IF OBJECT_ID(N'dbo.Movies5L1', N'U') IS NULL
BEGIN
    CREATE TABLE dbo.Movies5L1
    (
        IdMovie INT IDENTITY(1,1) NOT NULL,
        Titulo NVARCHAR(100) NOT NULL,
        Genero NVARCHAR(40) NOT NULL,
        DuracionMinutos INT NOT NULL,
        Clasificacion NVARCHAR(10) NOT NULL,
        FechaEstreno DATE NULL,
        Sinopsis NVARCHAR(500) NULL,
        IdSala INT NOT NULL,
        CONSTRAINT PK_Movies5L1 PRIMARY KEY (IdMovie),
        CONSTRAINT CK_Movies5L1_Duracion CHECK (DuracionMinutos > 0),
        CONSTRAINT FK_Movies5L1_Salas5L1 FOREIGN KEY (IdSala)
            REFERENCES dbo.Salas5L1(IdSala)
    );
END
GO

IF NOT EXISTS (SELECT 1 FROM dbo.Salas5L1)
BEGIN
    INSERT INTO dbo.Salas5L1 (NombreSala, Capacidad, TipoSala, Activa)
    VALUES
        (N'Sala 1', 120, N'Tradicional', 1),
        (N'Sala 2', 80, N'3D', 1),
        (N'Sala VIP', 45, N'VIP', 1);
END
GO

SELECT * FROM dbo.Salas5L1;
SELECT * FROM dbo.Movies5L1;
GO