@echo off
dotnet build src/Skybrud.Essentials.AspNetCore --configuration Release /t:rebuild /t:pack -p:PackageOutputPath=../../releases/nuget