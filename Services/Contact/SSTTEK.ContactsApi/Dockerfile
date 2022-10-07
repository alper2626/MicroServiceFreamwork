#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Services/Contacts/SSTTEK.ContactsApi/SSTTEK.ContactsApi.csproj", "Services/Contacts/SSTTEK.ContactsApi/"]
RUN dotnet restore "Services/Contacts/SSTTEK.ContactsApi/SSTTEK.ContactsApi.csproj"
COPY . .
WORKDIR "/src/Services/Contacts/SSTTEK.ContactsApi"
RUN dotnet build "SSTTEK.ContactsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "SSTTEK.ContactsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "SSTTEK.ContactsApi.dll"]