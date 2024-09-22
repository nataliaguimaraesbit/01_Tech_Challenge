FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 8080

ENV ASPNETCORE_URLS=http://+:8080

FROM --platform=$BUILDPLATFORM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG configuration=Release
WORKDIR /src

# Copia todos os projetos
COPY ["src/LocalFriendzApi/LocalFriendzApi.csproj", "LocalFriendzApi/"]
COPY ["src/LocalFriendzApi.Application/LocalFriendzApi.Application.csproj", "LocalFriendzApi.Application/"]
COPY ["src/LocalFriendzApi.Core/LocalFriendzApi.Core.csproj", "LocalFriendzApi.Core/"]
COPY ["src/LocalFriendzApi.Infrastructure/LocalFriendzApi.Infrastructure.csproj", "LocalFriendzApi.Infrastructure/"]

# Restaura as dependências
RUN dotnet restore "LocalFriendzApi/LocalFriendzApi.csproj"

# Agora copia todos os arquivos de cada projeto
COPY src/LocalFriendzApi/. ./LocalFriendzApi/
COPY src/LocalFriendzApi.Application/. ./LocalFriendzApi.Application/
COPY src/LocalFriendzApi.Core/. ./LocalFriendzApi.Core/
COPY src/LocalFriendzApi.Infrastructure/. ./LocalFriendzApi.Infrastructure/

# Define o diretório de trabalho para o build
WORKDIR "/src/LocalFriendzApi"
RUN dotnet build "LocalFriendzApi.csproj" -c $configuration -o /app/build

FROM build AS publish
RUN dotnet publish "LocalFriendzApi.csproj" -c $configuration -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LocalFriendzApi.dll"]
