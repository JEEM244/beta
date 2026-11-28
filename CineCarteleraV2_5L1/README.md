# CineCarteleraV2_5L1

Segunda versión del sistema de administración de cartelera de cine.

## Tecnologías

- ASP.NET Core MVC
- C#
- Entity Framework Core 10
- SQL Server
- Bootstrap 5

## Preparación

1. Abre SQL Server Management Studio.
2. Ejecuta el script `Database/CineCartelera5l1.sql`.
3. Revisa la cadena de conexión de `appsettings.json` y coloca el nombre correcto de tu servidor SQL Server si es diferente de `localhost`.
4. Abre `CineCarteleraV2_5L1.csproj` con Visual Studio.
5. Restaura los paquetes NuGet y ejecuta el proyecto.

## Secciones

- **Películas:** registro, consulta, edición y eliminación.
- **Salas:** registro, consulta, edición y eliminación.
- **Funciones:** programación de películas en salas con fecha, hora, precio e idioma.

La tabla Funciones relaciona Peliculas y Salas. El sistema evita registrar dos funciones en la misma sala a la misma fecha y hora.