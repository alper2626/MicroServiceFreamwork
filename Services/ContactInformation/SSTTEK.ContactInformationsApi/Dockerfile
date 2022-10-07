#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/ContactInformations/SSTTEK.ContactInformationsApi/SSTTEK.ContactInformationsApi.csproj", "Services/ContactInformations/SSTTEK.ContactInformationsApi/"]
RUN dotnet restore "Services/ContactInformations/SSTTEK.ContactInformationsApi/SSTTEK.ContactInformationsApi.csproj"
COPY . .
WORKDIR "/src/Services/ContactInformations/SSTTEK.ContactInformationsApi"
RUN dotnet build "SSTTEK.ContactInformationsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSTTEK.ContactInformationsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSTTEK.ContactInformationsApi.dll"]