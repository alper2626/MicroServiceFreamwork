#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Report/SSTTEK.LocationApi/SSTTEK.LocationApi.csproj", "Services/Report/SSTTEK.LocationApi/"]
RUN dotnet restore "Services/Report/SSTTEK.LocationApi/SSTTEK.LocationApi.csproj"
COPY . .
WORKDIR "/src/Services/Report/SSTTEK.LocationApi"
RUN dotnet build "SSTTEK.LocationApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSTTEK.LocationApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSTTEK.LocationApi.dll"]