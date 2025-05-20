# Integracion Factus

## Crear proyecto

mkdir Integrations
cd Integrations
dotnet new classlib -n Halltec.Factus --framework net8.0
cd ..
dotnet sln add ./Integrations/Halltec.Factus/Halltec.Factus.csproj

## agregar referencia   
dotnet add ./ApiRestClean.Infrastructure/ApiRestClean.Infrastructure.csproj reference ./Integrations/Halltec.Factus/Halltec.Factus.csproj

## agregar FactusService

agregar Factus Service en Halltec.Factus

## Crear archivo de configuracion

agregar FactusApiSettings en Halltec.Factus

## Copiar Dtos


## agregar proyecto tests 

mkdir ApiRestClean.Tests
cd ApiRestClean.Tests
dotnet new classlib -n Factus.Tests --framework net8.0
copiar archivo appsettings.json
agregar al csproj 
 <ItemGroup>
    <None Update="appsettings.json">
      <CopyToOutputDirectory>PreserveNewest</CopyToOutputDirectory>
    </None>
  </ItemGroup>
  
cd ..
dotnet sln add ./ApiRestClean.Tests/Factus.Tests/Factus.Tests.csproj
dotnet add ./ApiRestClean.Tests/Factus.Tests/Factus.Tests.csproj reference ./Integrations/Halltec.Factus/Halltec.Factus.csproj   

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