USE CineCartelera5l1;
GO

-- 4 PELICULAS DE EJEMPLO
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

-- 3 SALAS DE EJEMPLO
INSERT INTO Salas (NombreSala, Capacidad, FormatoSala, Activa)
VALUES
    (N'Sala 1', 90, N'Tradicional', 1),
    (N'Sala 2', 60, N'3D', 1),
    (N'Sala VIP', 30, N'VIP', 1);
GO

-- 3 FUNCIONES DE EJEMPLO
-- Se relacionan por nombre para no depender de los valores identity.
INSERT INTO Funciones
    (IdPelicula, IdSala, FechaFuncion, HoraInicio, Precio, Idioma)
SELECT p.IdPelicula, s.IdSala, '2026-12-01', '16:00', 75.00, N'Español'
FROM Peliculas AS p
CROSS JOIN Salas AS s
WHERE p.Titulo = N'Horizonte de ceniza' AND s.NombreSala = N'Sala 1';

INSERT INTO Funciones
    (IdPelicula, IdSala, FechaFuncion, HoraInicio, Precio, Idioma)
SELECT p.IdPelicula, s.IdSala, '2026-12-01', '18:30', 90.00, N'Subtitulada'
FROM Peliculas AS p
CROSS JOIN Salas AS s
WHERE p.Titulo = N'El mapa de los sueños' AND s.NombreSala = N'Sala 2';

INSERT INTO Funciones
    (IdPelicula, IdSala, FechaFuncion, HoraInicio, Precio, Idioma)
SELECT p.IdPelicula, s.IdSala, '2026-12-02', '20:00', 120.00, N'Español'
FROM Peliculas AS p
CROSS JOIN Salas AS s
WHERE p.Titulo = N'Código lunar' AND s.NombreSala = N'Sala VIP';
GO

-- CONSULTAR LOS 10 REGISTROS
SELECT * FROM Peliculas;
SELECT * FROM Salas;
SELECT * FROM Funciones;
GO
