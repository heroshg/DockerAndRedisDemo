# Etapa de build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia o .csproj e restaura depend�ncias
COPY *.csproj ./
RUN dotnet restore

# Copia todo o c�digo-fonte
COPY . ./

# Publica a aplica��o para a pasta /app/publish
RUN dotnet publish -c Release -o /app/publish

# Etapa de runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# Vari�vel opcional para ajustar ambiente em tempo de execu��o
ENV DOTNET_ENVIRONMENT=Production

# Exp�e a porta padr�o usada pelo Kestrel
EXPOSE 80

ENTRYPOINT ["dotnet", "DockerAndRedisDemo.dll"]