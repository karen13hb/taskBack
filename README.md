# Tasks API & Angular Frontend

Este proyecto es una aplicación de gestión de tareas que utiliza una arquitectura por capas en .NET para el backend (API RESTful) y Angular para el frontend. La solución incluye:

- **Backend (.NET 9.0):**
  - **Capa de Dominio:** Definición de entidades y contratos (interfaces).
  - **Capa de Infraestructura:** Implementación del acceso a datos mediante Entity Framework Core y SQL Server.
  - **Capa de Aplicación:** Lógica de negocio y servicios (por ejemplo, `TaskService`).
  - **Capa de Presentación (API):** Controladores de ASP.NET Core Web API y documentación interactiva con Swagger.
  - 
## Características

- **CRUD de Tareas:**  
  Permite obtener, crear, actualizar (con validación de existencia) y eliminar tareas.
- **Documentación de API:**  
  Swagger UI está disponible para consultar y probar los endpoints.
- **Arquitectura por Capas:**  
  Separación clara de responsabilidades entre Dominio, Infraestructura, Aplicación y Presentación.
- **Pruebas Unitarias:**  
  Uso de xUnit y Moq para validar la lógica de negocio.

  ## Requisitos

- .NET 9.0 SDK
- SQL Server (Azure SQL Database, SQL Server Express o similar)
- Visual Studio 2022

  ## Arquitectura

La solución se divide en los siguientes proyectos o carpetas:

Esta elección de arquitectura permite desarrollar aplicaciones robustas, fáciles de mantener y con un alto grado de adaptabilidad a futuros cambios.

Al desacoplar la lógica de negocio de la lógica de acceso a datos y de la presentación, se pueden crear pruebas unitarias y de integración de forma más sencilla. Esto mejora la calidad del código y facilita la detección temprana de errores.

- **Tasks.Domain:**
  - **Entities:** Contiene la entidad `TaskItem`.
  - **Interfaces:** Define los contratos de repositorios (por ejemplo, `ITaskRepository`).

- **Tasks.Infrastructure:**
  - **Context:** Implementa el `AppDbContext` con EF Core.
  - **Repositories:** Implementación de `TaskRepository`.

- **Tasks.Application:**
  - **Services:** Lógica de negocio (por ejemplo, `TaskService`) y validaciones.

- **Tasks.API:**
  - **Controllers:** Exponen los endpoints de la API.
  - **Swagger:** Configuración para generar la documentación interactiva.

- **Tasks.Test:**
  - Pruebas unitarias para validar la lógica de negocio usando xUnit y Moq.

  ## Instalación y Ejecución Local

### Backend

1. **Clonar el repositorio:**

   ```bash
   git clone https://tu-repositorio-url.git
   cd Tasks.API
   ```
2. **Configurar la cadena de conexión:** 

Edita appsettings.json y actualiza la sección de ConnectionStrings
```bash
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=TU_SERVIDOR;Database=NombreDeTuBase;User Id=tu_usuario;Password=tu_contraseña;"
  }
}
```
