# Integracion Factus

## Crear proyecto

dotnet new classlib -n Factus.Integration
dotnet sln add ./Integrations/Factus.Integration/Factus.Integration.csproj

## agregar referencia   
dotnet add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj reference ./Integrations/Factus.Integration/Factus.Integration.csproj   

## agregar FactusService

agregar Factus Service en Factus.Integration

## Crear archivo de configuracion

agregar FactusApiSettings en ApiRestClean.Infrastructure

## Crear FactusService Interface 

agregar IInvoiceService en ApiRestClean.Core

## agregar constructor de factusService

agregar el constructor con la inyeccion de dependencias

## agregar metodo de Authorization

agregar metodo de Authorization en FactusService

## agregar proyecto tests 

dotnet new classlib -n Factus.Tests --framework net8.0
dotnet sln add ./ApiRestClean.Tests/Factus.Tests/Factus.Tests.csproj
dotnet add ./ApiRestClean.Tests/Factus.Tests/Factus.Tests.csproj reference ./Integrations/Halltec.Factos/Halltec.Factos.csproj   

cd ./ApiRestClean.Tests/Factus.Tests/
dotnet add package Microsoft.NET.Test.Sdk 
dotnet add package xunit 
dotnet add package xunit.runner.visualstudio 
dotnet add package coverlet.collector 
dotnet add package Microsoft.Extensions.Configuration
dotnet add package Microsoft.Extensions.Configuration.Json
dotnet add package Microsoft.Extensions.Configuration.Binder

Agregar archivo de pruebas para el servicio de Factus

dotnet test