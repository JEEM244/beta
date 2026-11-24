# Instalación y ejecución

## Requisitos

- Visual Studio 2026 con la carga de trabajo ASP.NET y desarrollo web.
- SQL Server (instancia local o SQL Server Express).
- SQL Server Management Studio 22.
- .NET 10 SDK.

## 1. Crear la base de datos

Abre SSMS, conéctate a tu instancia de SQL Server y ejecuta `database/Cartelera5L1.sql`.

El script crea la base de datos `Cartelera5L1`, las tablas `Salas5L1` y `Movies5L1`, y algunos registros iniciales de salas. La relación es de una sala a muchas películas: cada película tiene una sala asignada.

## 2. Abrir el proyecto

Abre `MVCCartelera/MVCCartelera.csproj` desde Visual Studio y restaura los paquetes NuGet.

## 3. Ajustar la conexión

En `appsettings.json`, cambia `Server=localhost` por el nombre real de tu instancia si es necesario. Para SQL Server Express suele ser `Server=localhost\\SQLEXPRESS`.

## 4. Scaffolding desde la consola de NuGet

La estructura de modelos incluida en el repositorio corresponde a las tablas del script SQL. Si necesitas regenerarla desde tu base de datos, abre Tools > NuGet Package Manager > Package Manager Console y ejecuta:

```powershell
Scaffold-DbContext "Server=localhost;Database=Cartelera5L1;Integrated Security=True;TrustServerCertificate=True;" Microsoft.EntityFrameworkCore.SqlServer -OutputDir Models -Context Cartelera5L1Context -Tables Movies5L1,Salas5L1 -DataAnnotations -NoOnConfiguring -Force
```

Si tu instancia es Express, cambia el servidor por `localhost\\SQLEXPRESS`. El parámetro `-Force` reemplaza los archivos generados existentes de Models; úsalo solamente si deseas regenerarlos.

## 5. Ejecutar

Establece `MVCCartelera` como proyecto de inicio y presiona F5. En el menú superior podrás entrar a Películas y Salas.

## Relación entre las tablas

- `Salas5L1.IdSala`: clave primaria.
- `Movies5L1.IdSala`: clave foránea que apunta a una sala.
- Una sala puede estar relacionada con varias películas.
- Una película debe estar asignada a una sala.

## Nota

La cadena de conexión incluida es un ejemplo para desarrollo local. No publiques contraseñas reales ni cadenas con credenciales privadas.