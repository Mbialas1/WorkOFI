# U¿yj obrazu ASP.NET Core jako bazy
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

# U¿yj obrazu SDK do budowania aplikacji
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src

# Skopiuj plik csproj i przywróæ wszystkie zale¿noœci
COPY OFI.UserService.Api.csproj .
RUN dotnet restore

# Skopiuj wszystkie pliki Ÿród³owe
COPY . .

# Buduj aplikacjê
RUN dotnet build "OFI.UserService.Api.csproj" -c Release -o /app/build 

FROM build AS publish
RUN dotnet publish "OFI.UserService.Api.csproj" -c Release -o /app/publish

# Finalny obraz
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "OFI.UserService.Api.dll"]
