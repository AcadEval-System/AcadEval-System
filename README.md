# SISTEMA EVAC ITEC 

El sistema EVAC-TEC es una plataforma integral para la gestión y evaluación académica, orientada a instituciones educativas que buscan optimizar procesos de evaluación por competencias y recolección de feedback a través de encuestas. El sistema está diseñado para ser escalable, seguro y fácil de integrar con otras soluciones educativas.

##  Características Principales

- **Evaluaciones por Competencias:** Permite definir, asignar y evaluar competencias, facilitando el seguimiento del desarrollo académico y profesional.
- **Encuestas Académicas:** Recolecta retroalimentación de estudiantes, docentes y coordinadores mediante encuestas personalizables.
- **Generación de Reportes:** Crea informes detallados y visualizaciones para la toma de decisiones basadas en datos.
- **Gestión de Roles y Permisos:** Administración granular de usuarios (docentes, estudiantes, coordinadores, administradores) con autenticación segura.
- **Interfaz Moderna:** Experiencia de usuario intuitiva y responsiva, basada en las últimas tendencias de diseño.


##  Tecnologías Utilizadas

- **Backend:** ASP.NET Core 8, MediatR, CQRS, Fluent Validation, Entity Framework Core
- **Frontend:** React 19, Tailwind CSS 4, ShadCN UI, Zustand, Wouter, React Hook Form
- **Base de Datos:** PostgreSQL
- **Contenedores:** Docker
- **DevOps y Observabilidad:** Serilog, Swagger/OpenAPI



##  Arquitectura del Proyecto

AcademicEval System sigue los principios de Clean Architecture, garantizando separación de responsabilidades y escalabilidad:

```
├── Domain            # Entidades, interfaces y lógica de negocio central
├── Application       # Casos de uso, CQRS, validaciones y servicios de aplicación
├── Infrastructure    # Acceso a datos, integraciones externas y servicios de infraestructura
├── Web.Server        # Endpoints HTTP, autenticación y presentación
├── Web.Client        # Aplicación React 
```



##  Instalación y Puesta en Marcha

### Prerrequisitos

- Docker & Docker Compose
- .NET 8 SDK
- Node.js 20+
- PostgreSQL

### Pasos 

1. **Clona el repositorio:**
   ```bash
   git clone https://github.com/AcadEval-System/AcadEval-System.git
   cd AcadEval-System
   ```

2. **Configura las variables de entorno:**
   - Backend: `src/AcadEvalSys.WEB/AcadEvalSys.WEB.Server/appsettings.Development.json`
   - Frontend: `.env`
     
3. **Ejecuta la base de datos y servicios:**
   ```bash
   docker-compose up -d
   ```
4. **Instala dependencias del front**
   ```bash
    cd src/AcadEvalSys.WEB/AcadEvalSys.WEB.Client
    npm install
    npm run dev
   ```

5. **Inicia la aplicación:**
   ```bash
   cd src/AcadEvalSys.WEB/AcadEvalSys.WEB.Server
   dotnet run
   ```

6. **Accede a la plataforma:**
  - Frontend: [http://localhost:5173](http://localhost:5173)
  - API Swagger: [https://localhost:7004/swagger/index.html](https://localhost:7004/swagger/index.html)
