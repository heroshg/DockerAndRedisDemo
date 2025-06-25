# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o .csproj e restaura dependências
COPY *.csproj ./
RUN dotnet restore

# Copia todo o código-fonte
COPY . ./

# Publica a aplicação para a pasta /app/publish
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Variável opcional para ajustar ambiente em tempo de execução
ENV DOTNET_ENVIRONMENT=Production

# Expõe a porta padrão usada pelo Kestrel
EXPOSE 80

ENTRYPOINT ["dotnet", "DockerAndRedisDemo.dll"]