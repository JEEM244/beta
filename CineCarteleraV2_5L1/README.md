# CineCarteleraV2_5L1

Segunda versión del sistema de administración de cartelera de cine.

## Tecnologías

- ASP.NET Core MVC
- C#
- Entity Framework Core 10
- SQL Server
- Bootstrap 5

## Preparación de la base de datos

1. Abre SQL Server Management Studio.
2. Ejecuta `Database/CineCartelera5l1.sql` para crear la base de datos y sus tablas.
3. Ejecuta `Database/DatosEjemplo.sql` para insertar 10 registros: 4 películas, 3 salas y 3 funciones.
4. Revisa la cadena de conexión de `appsettings.json` y coloca el nombre correcto de tu servidor SQL Server si es diferente de `localhost`.
5. Abre `CineCarteleraV2_5L1.csproj` con Visual Studio, restaura los paquetes NuGet y ejecuta el proyecto.

Si la base de datos ya está creada, no vuelvas a ejecutar el primer script; ejecuta únicamente `DatosEjemplo.sql`.

## Secciones

- **Películas:** registro, consulta, edición y eliminación.
- **Salas:** registro, consulta, edición y eliminación.
- **Funciones:** programación de películas en salas con fecha, hora, precio e idioma.

La tabla Funciones relaciona Peliculas y Salas. El sistema evita registrar dos funciones en la misma sala a la misma fecha y hora.

## Diseño

Interfaz oscura con acentos naranja, panel de resumen, catálogo de películas y listados adaptados al mismo estilo.