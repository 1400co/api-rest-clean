# 🏗️ ApiRestClean - Curso Clean Architecture con .NET 8

Este proyecto forma parte de una serie educativa donde construimos una **API RESTful profesional con .NET 8**, aplicando principios de **Clean Architecture**, **Clean Code** y **Desarrollo Profesional Backend**.

---


## 📂 Estructura del Proyecto

ApiRestClean/
├── ApiRestClean.API/           --> Capa de presentación (Endpoints, Controllers, Configs)
├── ApiRestClean.Core/          --> Dominio + Casos de uso (Entidades, Interfaces, DTOs, lógica pura)
└── ApiRestClean.Infrastructure/ --> Implementaciones concretas (Base de datos, servicios externos)

#Modulo 1 


## Paso 1 – Crear la solución y carpetas base
dotnet new sln -n ApiRestClean

## Paso 2 – Crear los 3 proyectos principales
### Capa de Dominio + Casos de Uso
dotnet new classlib -n ApiRestClean.Core

### Capa de Infraestructura
dotnet new classlib -n ApiRestClean.Infrastructure

### API (punto de entrada)
dotnet new webapi -n ApiRestClean.API

## Paso 3 – Agregar proyectos a la solución
dotnet sln add ./ApiRestClean.Core/ApiRestClean.Core.csproj
dotnet sln add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj
dotnet sln add ./ApiRestClean.API/ApiRestClean.API.csproj

## Paso 4 – Establecer referencias correctas

### Infrastructure necesita al Core
dotnet add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj reference ./ApiRestClean.Core/ApiRestClean.Core.csproj

### API necesita a Core e Infrastructure
dotnet add ./ApiRestClean.API/ApiRestClean.API.csproj reference ./ApiRestClean.Core/ApiRestClean.Core.csproj
dotnet add ./ApiRestClean.API/ApiRestClean.API.csproj reference ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj

## Paso 5
cd ApiRestClean.API
dotnet build 

## Paso 6

dotnet watch run

#Modulo 2

🎯 Objetivo del Módulo 2 
Crear un CRUD completo para la entidad Product bajo Clean Architecture con:

💾 Entity: Product

🧠 Core: Interfaces + lógica

🧱 Infrastructure: Repositorio InMemory (por ahora)

🌐 API: Endpoints usando Minimal APIs (y opción con Controller)

🧪 Test manual vía Swagger o Postman

## Paso 7
Agregar refrencia a DependencyInjection

dotnet add ApiRestClean.Infrastructure package Microsoft.Extensions.DependencyInjection.Abstractions

## Paso 8
Crear entidad producto

## Paso 9
Crear IProductRepository

## Paso 10
crear implementacion de IProductRepository llamado InMemoryProductRepository

## Paso 11
Crear la extension de inyeccion de dependencias DependencyInjection

## Paso 12 
registrar en el api el AddInfrastructureServices()

## Paso 13 
Crear Controller y Endpoints

## Paso 14 
dotnet build
dotnet watch run

#Modulo 2 Bonus - Implementar Vertical Slice

## Paso 15

Agregar referencia de MediatR
dotnet add ApiRestClean.Core package MediatR
dotnet add ApiRestClean.Core package FluentValidation
dotnet add ApiRestClean.Core package MediatR
dotnet add ApiRestClean.Core package FluentResults

##Paso 16

Crear la carpeta Features y agregar la clase CreateProduct

## Paso 17
prepara el procyeto infrastructure para usar MediatR

dotnet add ApiRestClean.Infrastructure package MediatR.Extensions.Microsoft.DependencyInjection
dotnet add ApiRestClean.Infrastructure package FluentValidation.DependencyInjectionExtensions

##Paso 18

agregar las dependencias de MediatR

    using Microsoft.Extensions;
    using Microsoft.Extensions.DependencyInjection;
    using ApiRestClean.Core.Interfaces;
    using ApiRestClean.Infrastructure.Repositories;
    using MediatR;
    using FluentValidation;
    using ApiRestClean.Core.Features.PipelineBehaviors;
    using System.Reflection;

    services.AddSingleton<IProductRepository, InMemoryProductRepository>();
        services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(Assembly.Load("ApiRestClean.Core"));
                cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(ValidationPipelineBehavior<,>), ServiceLifetime.Scoped);
            });

        // Registra todos los validadores de FluentValidation en el assembly de Core
        services.AddValidatorsFromAssembly(Assembly.Load("ApiRestClean.Core"));

        // Si usaras EF Core: services.AddDbContext<AppDbContext>(options => ...);

## Paso 19

Crear la carpeta Features y agregar la clase CreateProduct en el API
agregar el endpoint POST /products


# Bonus - Integracion Halltec

## crear proyecto 

dotnet new classlib -n Halltec.Factos
dotnet sln add ./Integrations/Halltec.Factos/Halltec.Factos.csproj
dotnet add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj reference ./Integrations/Halltec.Factos/Halltec.Factos.csproj