#!/usr/bin/env bash
green="\033[1;32m"
reset="\033[m"
projectName="AirTravel"
echo "About to create the directory"
mkdir src
cd src

echo -e "${green}Creating solution and projects${reset}"
dotnet new sln -n $projectName
dotnet new webapi -n $projectName.API
dotnet new classlib -n $projectName.Application
dotnet new classlib -n $projectName.Domain
dotnet new classlib -n $projectName.Persistence

echo -e "${green}Adding projects to the solution${reset}"
dotnet sln add $projectName.API/$projectName.API.csproj
dotnet sln add $projectName.Application/$projectName.Application.csproj
dotnet sln add $projectName.Domain/$projectName.Domain.csproj
dotnet sln add $projectName.Persistence/$projectName.Persistence.csproj

echo -e "${green}Setting up project dependancies${reset}"
cd $projectName.API
dotnet add reference ../$projectName.Application/$projectName.Application.csproj
cd ../$projectName.Application
dotnet add reference ../$projectName.Domain/$projectName.Domain.csproj
dotnet add reference ../$projectName.Persistence/$projectName.Persistence.csproj
cd ../$projectName.Persistence
dotnet add reference ../$projectName.Domain/$projectName.Domain.csproj
cd ..

echo -e "${green}Executing dotnet restore${reset}"
dotnet restore

echo -e "${green}Finished!${reset}"
