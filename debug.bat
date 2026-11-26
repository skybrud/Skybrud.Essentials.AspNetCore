@echo off
dotnet build src/Skybrud.Essentials.AspNetCore --configuration Debug /t:rebuild /t:pack -p:PackageOutputPath=c:/nuget