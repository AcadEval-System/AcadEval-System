# Consulte https://aka.ms/customizecontainer para aprender a personalizar su contenedor de depuración y cómo Visual Studio usa este Dockerfile para compilar sus imágenes para una depuración más rápida.

# Esta fase se usa cuando se ejecuta desde VS en modo rápido (valor predeterminado para la configuración de depuración)
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1000
USER ${APP_UID}
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

# Esta fase se usa para compilar el proyecto de servicio
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
# ARG DEBIAN_FRONTEND=noninteractive # Keep it here or also set per command
WORKDIR /src

# Install prerequisites for adding NodeSource and Node.js itself
RUN apt-get update && \
    echo 'debconf debconf/frontend select Noninteractive' | debconf-set-selections && \
    apt-get install -y -o Dpkg::Options::="--force-confdef" -o Dpkg::Options::="--force-confold" --no-install-recommends \
        apt-utils \
        ca-certificates \
        curl \
        gnupg && \
    # Add NodeSource repository for Node.js LTS
    mkdir -p /etc/apt/keyrings && \
    curl -fsSL https://deb.nodesource.com/gpgkey/nodesource-repo.gpg.key | gpg --dearmor -o /etc/apt/keyrings/nodesource.gpg && \
    echo "deb [signed-by=/etc/apt/keyrings/nodesource.gpg] https://deb.nodesource.com/node_20.x nodistro main" | tee /etc/apt/sources.list.d/nodesource.list && \
    # Install Node.js
    apt-get update && \
    env DEBIAN_FRONTEND=noninteractive apt-get install -y --no-install-recommends nodejs && \
    # Clean up apt lists to reduce image size
    rm -rf /var/lib/apt/lists/*

# Verify npm installation (optional, for debugging)
# RUN npm --version

# Copy solution file and all project files first for better layer caching
# Assuming the Docker build context is the solution directory (e.g., /home/tomib4n/Develop/AcadEval-System/)
COPY ["AcadEvalSys.sln", "./"]
COPY ["src/AcadEvalSys.WEB/AcadEvalSys.WEB.Server/AcadEvalSys.API.csproj", "src/AcadEvalSys.WEB/AcadEvalSys.WEB.Server/"]
COPY ["src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/AcadEvalSys.WEB.Client.esproj", "src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/"]
COPY ["src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/nuget.config", "src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/"]
COPY ["src/AcadEvalSys.Application/AcadEvalSys.Application.csproj", "src/AcadEvalSys.Application/"]
COPY ["src/AcadEvalSys.Domain/AcadEvalSys.Domain.csproj", "src/AcadEvalSys.Domain/"]
COPY ["src/AcadEvalSys.Infrastructure/AcadEvalSys.Infrastructure.csproj", "src/AcadEvalSys.Infrastructure/"]

# Restore dependencies for the API project and its dependencies
RUN dotnet restore "src/AcadEvalSys.WEB/AcadEvalSys.WEB.Server/AcadEvalSys.API.csproj"

# Copy client app's package files and install dependencies
COPY ["src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/package.json", "src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/"]
COPY ["src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/package-lock.json", "src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/"]
# If you use other files for npm like .npmrc, copy them here
WORKDIR /src/src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client
RUN npm install && \
    npm cache clean --force

# Copy the rest of the source code
# Note: WORKDIR is now /src/src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client
# We need to go back to /src to copy everything relative to the solution root.
WORKDIR /src
COPY . .

# Build the React frontend
WORKDIR "/src/src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client"
ENV DOCKER_BUILD=true
RUN npm run build

# Build and publish the API
WORKDIR "/src/src/AcadEvalSys.WEB/AcadEvalSys.WEB.Server"
RUN dotnet build "./AcadEvalSys.API.csproj" -c $BUILD_CONFIGURATION -o /app/build

# Esta fase se usa para publicar el proyecto de servicio que se copiará en la fase final.
FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "./AcadEvalSys.API.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

# Copiar los archivos generados del frontend al directorio wwwroot de la API
# Ensure the client build artifacts are in /src/src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/dist or /build based on your vite config
# The default for vite build is 'dist'. If you changed it to 'build', update the path below.
RUN cp -r /src/src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client/dist /app/publish/wwwroot

# Esta fase se usa en producción o cuando se ejecuta desde VS en modo normal (valor predeterminado cuando no se usa la configuración de depuración)
FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "AcadEvalSys.API.dll"]
