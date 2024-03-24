#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:8.0-preview AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:8.0-preview AS build
WORKDIR /src
COPY ["src/AirTravel.API/AirTravel.API.csproj", "src/AirTravel.API/"]
COPY ["src/AirTravel.Application/AirTravel.Application.csproj", "src/AirTravel.Application/"]
COPY ["src/AirTravel.Domain/AirTravel.Domain.csproj", "src/AirTravel.Domain/"]
COPY ["src/AirTravel.Persistence/AirTravel.Persistence.csproj", "src/AirTravel.Persistence/"]
RUN dotnet restore "src/AirTravel.API/AirTravel.API.csproj"
COPY . .
WORKDIR "/src/src/AirTravel.API"
RUN dotnet build "AirTravel.API.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "AirTravel.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AirTravel.API.dll"]
