#!/usr/bin/env bash
green="\033[1;32m"
reset="\033[m"
projectName="AirTravel"
echo "About to create the directory"
cd src
dotnet new webapi -n $projectName.Aggregator
cd ..
echo -e "${green}Adding projects to the solution${reset}"
dotnet sln add ./src/$projectName.Aggregator/$projectName.Aggregator.csproj
