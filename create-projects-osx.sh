#!/usr/bin/env bash
green="\033[1;32m"
reset="\033[m"
projectName="AirTravel"
echo "About to create the directory"
mkdir src
echo -e "${green}Creating solution and projects${reset}"
dotnet new sln -n $projectName
cd src
dotnet new webapi -n $projectName.API
dotnet new classlib -n $projectName.Application
dotnet new classlib -n $projectName.Domain
dotnet new classlib -n $projectName.Persistence
cd ..
echo -e "${green}Adding projects to the solution${reset}"
dotnet sln add ./src/$projectName.API/$projectName.API.csproj
dotnet sln add ./src/$projectName.Application/$projectName.Application.csproj
dotnet sln add ./src/$projectName.Domain/$projectName.Domain.csproj
dotnet sln add ./src/$projectName.Persistence/$projectName.Persistence.csproj

cd ./src

echo -e "${green}Setting up project dependancies${reset}"
cd $projectName.API
dotnet add reference ../$projectName.Application/$projectName.Application.csproj
cd ../$projectName.Application
dotnet add reference ../$projectName.Domain/$projectName.Domain.csproj
dotnet add reference ../$projectName.Persistence/$projectName.Persistence.csproj
cd ../$projectName.Persistence
dotnet add reference ../$projectName.Domain/$projectName.Domain.csproj

cd ..
cd ..
echo "Create folder tests"
mkdir tests
echo -e "${green}Create tests project to the solution${reset}"
cd ./tests
dotnet new xunit -o Tests.$projectName.API
dotnet new xunit -o Tests.$projectName.Application
dotnet new xunit -o Tests.$projectName.Domain
dotnet new xunit -o Tests.$projectName.Persistence

cd ..
# cd ../src

echo -e "${green}Adding tests project to the solution${reset}"
dotnet sln add ./tests/Tests.$projectName.API/Tests.$projectName.API.csproj
dotnet sln add ./tests/Tests.$projectName.Application/Tests.$projectName.Application.csproj
dotnet sln add ./tests/Tests.$projectName.Domain/Tests.$projectName.Domain.csproj
dotnet sln add ./tests/Tests.$projectName.Persistence/Tests.$projectName.Persistence.csproj

cd ./tests
echo -e "${green}Setting up project dependancies${reset}"
cd Tests.$projectName.API
dotnet add reference ../../src/$projectName.API/$projectName.API.csproj
cd ../Tests.$projectName.Application
dotnet add reference ../../src/$projectName.Application/$projectName.Application.csproj
cd ../Tests.$projectName.Domain
dotnet add reference ../../src/$projectName.Domain/$projectName.Domain.csproj
cd ../Tests.$projectName.Persistence
dotnet add reference ../../src/$projectName.Persistence/$projectName.Persistence.csproj
cd ..

echo -e "${green}Create IntegratonTests project to the solution${reset}"
cd ./tests
dotnet new xunit -o IntegratonTests.$projectName.API
dotnet new xunit -o IntegratonTests.$projectName.Application
dotnet new xunit -o IntegratonTests.$projectName.Domain
dotnet new xunit -o IntegratonTests.$projectName.Persistence

# cd ../src
cd ..
echo -e "${green}Adding tests project to the solution${reset}"
dotnet sln add ./tests/IntegratonTests.$projectName.API/IntegratonTests.$projectName.API.csproj
dotnet sln add ./tests/IntegratonTests.$projectName.Application/IntegratonTests.$projectName.Application.csproj
dotnet sln add ./tests/IntegratonTests.$projectName.Domain/IntegratonTests.$projectName.Domain.csproj
dotnet sln add ./tests/IntegratonTests.$projectName.Persistence/IntegratonTests.$projectName.Persistence.csproj

cd ./tests
echo -e "${green}Setting up project dependancies${reset}"
cd IntegratonTests.$projectName.API
dotnet add reference ../../src/$projectName.API/$projectName.API.csproj
cd ../IntegratonTests.$projectName.Application
dotnet add reference ../../src/$projectName.Application/$projectName.Application.csproj
cd ../IntegratonTests.$projectName.Domain
dotnet add reference ../../src/$projectName.Domain/$projectName.Domain.csproj
cd ../IntegratonTests.$projectName.Persistence
dotnet add reference ../../src/$projectName.Persistence/$projectName.Persistence.csproj
cd ..
cd ..

echo -e "${green}Executing dotnet restore${reset}"
dotnet restore

echo -e "${green}Finished!${reset}"
