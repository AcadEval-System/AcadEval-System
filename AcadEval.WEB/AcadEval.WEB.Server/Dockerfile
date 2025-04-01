# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["AcadEval.WEB/AcadEval.WEB.Client/nuget.config", "AcadEval.WEB/AcadEval.WEB.Client/"]
COPY ["AcadEval.WEB/AcadEval.WEB.Server/AcadEval.WEB.csproj", "AcadEval.WEB/AcadEval.WEB.Server/"]
COPY ["AcadEval.BLL/AcadEval.BLL.csproj", "AcadEval.BLL/"]
COPY ["AcadEval.DAL/AcadEval.DAL.csproj", "AcadEval.DAL/"]
COPY ["AcadEval.ENTITY/AcadEval.ENTITY.csproj", "AcadEval.ENTITY/"]
COPY ["AcadEval.WEB/AcadEval.WEB.Client/AcadEval.WEB.Client.esproj", "AcadEval.WEB/AcadEval.WEB.Client/"]
RUN dotnet restore "./AcadEval.WEB/AcadEval.WEB.Server/AcadEval.WEB.csproj"
COPY . .

# Compilar el frontend React
WORKDIR "/src/AcadEval.WEB/AcadEval.WEB.Client"
RUN npm install && npm run build

# Compilar la API
WORKDIR "/src/AcadEval.WEB/AcadEval.WEB.Server"
RUN dotnet build "./AcadEval.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AcadEval.WEB.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Copiar los archivos generados del frontend al directorio wwwroot de la API
RUN cp -r /src/AcadEval.WEB/AcadEval.WEB.Client/build /app/publish/wwwroot

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcadEval.WEB.dll"]
