FROM mcr.microsoft.com/dotnet/core/aspnet:3.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.0 AS build
WORKDIR /src
COPY ["CalculaJuros.csproj", "./"]
RUN dotnet restore "./CalculaJuros.csproj"
COPY . .
WORKDIR "/src/."
RUN dotnet build "CalculaJuros.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CalculaJuros.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CalculaJuros.dll"]
