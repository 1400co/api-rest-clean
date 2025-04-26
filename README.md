ApiRestClean/
├── ApiRestClean.API/           --> Capa de presentación (Endpoints, Controllers, Configs)
├── ApiRestClean.Core/          --> Dominio + Casos de uso (Entidades, Interfaces, DTOs, lógica pura)
└── ApiRestClean.Infrastructure/ --> Implementaciones concretas (Base de datos, servicios externos)


# Paso 1 – Crear la solución y carpetas base
dotnet new sln -n ApiRestClean

# Paso 2 – Crear los 3 proyectos principales
## Capa de Dominio + Casos de Uso
dotnet new classlib -n ApiRestClean.Core

## Capa de Infraestructura
dotnet new classlib -n ApiRestClean.Infrastructure

## API (punto de entrada)
dotnet new webapi -n ApiRestClean.API

# Paso 3 – Agregar proyectos a la solución
dotnet sln add ./ApiRestClean.Core/ApiRestClean.Core.csproj
dotnet sln add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj
dotnet sln add ./ApiRestClean.API/ApiRestClean.API.csproj

# Paso 4 – Establecer referencias correctas

## Infrastructure necesita al Core
dotnet add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj reference ./ApiRestClean.Core/ApiRestClean.Core.csproj

## API necesita a Core e Infrastructure
dotnet add ./ApiRestClean.API/ApiRestClean.API.csproj reference ./ApiRestClean.Core/ApiRestClean.Core.csproj
dotnet add ./ApiRestClean.API/ApiRestClean.API.csproj reference ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj

# Paso 5
cd ApiRestClean.API
dotnet watch run