# 🧠 TaskManagementAPI

API RESTful construida en **.NET 8** para la gestión de tareas, estados y usuarios, con autenticación mediante **JWT**, persistencia con **Entity Framework Core (Code-First)** y documentación interactiva mediante **Swagger/OpenAPI**.

---

## 🚀 Tecnologías Utilizadas

- ASP.NET Core 8
- Entity Framework Core (Code-First)
- SQL Server
- JWT (Json Web Token)
- Swagger/OpenAPI
- CORS configurado para frontend en React (`http://localhost:3000`)

---

## 🏗️ Estructura del Proyecto

- `Controllers/`: Controladores HTTP (`TasksController`, `StatesController`, `AuthController`)
- `Services/`: Lógica de negocio por capa (`ITaskService`, `IAuthService`, etc.)
- `Models/`: Entidades que representan las tablas
- `data/`: `AppDbContext` y configuración EF
- `Middlewares/`: Manejo global de errores
- `Settings/`: Configuración para JWT

---

## 📦 Instalación y Configuración

1. **Clona el repositorio:**

   ```bash
   git clone https://github.com/JeanCarloH/backendSentry.git
   cd TaskManagementAPI
   ```

2. **Configura la cadena de conexión en `appsettings.json`:**

   ```json
   "ConnectionStrings": {
     "DefaultConnection": "Server=localhost;Database=TaskManagement;Trusted_Connection=True;TrustServerCertificate=True;"
   }
   ```

3. **Configura las claves JWT en `appsettings.json`:**

   ```json
   "Jwt": {
     "Key": "d62A!rS93*vQkwR#Xc5^0BmL9uPzTfYhW2&eJqNzUxVgLaKr",
     "Issuer": "TaskAPI",
     "Audience": "TaskAPIUsers",
     "ExpireMinutes": 60
   }
   ```

4. **Aplica las migraciones:**

   ```bash
   dotnet ef database update
   ```

   Esto creará las tablas y agregará un usuario inicial:
   - `Email`: `admin@gmail.com`
   - `Password`: `123456`

5. **Ejecuta la API:**

   ```bash
   dotnet run
   ```

   Por defecto corre en `http://localhost:5009`

---

## 🛠️ Endpoints Principales

### 🔐 Autenticación

- `POST /api/auth/login`
  ```json
  {
    "email": "admin@gmail.com",
    "password": "123456"
  }
  ```
  Respuesta:
  ```json
  {
    "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6..."
  }
  ```

### ✅ Estados

- `GET /api/states`
- `POST /api/states`
- `PUT /api/states/{id}`
- `DELETE /api/states/{id}`

### ✅ Tareas

- `GET /api/tasks`
- `POST /api/tasks`
- `PUT /api/tasks/{id}`
- `DELETE /api/tasks/{id}`

> Algunos endpoints pueden requerir token JWT en el encabezado:
```
Authorization: Bearer {token}
```

---

## 📚 Swagger

Una vez en ejecución, accede a:

```
http://localhost:5009/swagger
```

Ahí podrás explorar y probar todos los endpoints directamente desde el navegador.

---

## 🧪 Comandos útiles de Entity Framework

- Crear nueva migración:
  ```bash
  dotnet ef migrations add NombreMigracion
  ```
- Aplicar migraciones:
  ```bash
  dotnet ef database update
  ```

---
## ⚠️ Manejo de errores globalizado

La API implementa un middleware personalizado llamado `ErrorHandlingMiddleware`, el cual intercepta todas las excepciones no controladas y responde con un mensaje JSON estandarizado. Este middleware está registrado en el `Program.cs` de forma anticipada en la cadena de ejecución:

```csharp
app.UseMiddleware<ErrorHandlingMiddleware>();
```

Ubicado estratégicamente **antes de** `UseAuthentication()` y `UseAuthorization()`, permite capturar errores sin exponer detalles internos del servidor ni generar respuestas HTML innecesarias.

Este enfoque garantiza una experiencia de desarrollo más controlada y cumple con el requerimiento de la prueba técnica de manejar errores de forma globalizada.

---

## 🧠 Arquitectura definida y decisiones

La API fue desarrollada siguiendo una **arquitectura por capas (Layered Architecture)**, separando las responsabilidades en carpetas específicas como `Controllers`, `Services`, `Models`, `Data`, `Dtos`, `Middlewares` y `Settings`. Este enfoque promueve la claridad, el desacoplamiento y facilita las pruebas y la escalabilidad.

### 📁 Estructura de carpetas y roles

- **`Controllers/`**  
  Expone los endpoints HTTP, actuando como punto de entrada de las peticiones. No contiene lógica de negocio, solo delega a los servicios.

- **`Services/`**  
  Encapsulan la lógica de negocio de cada dominio (`TaskService`, `StateService`, `AuthService`, etc.). Esto permite probar fácilmente la lógica sin necesidad de invocar controladores o dependencias externas.

- **`Models/`**  
  Contienen las entidades del dominio que se traducen a tablas mediante Entity Framework Core (`TaskItem`, `State`, `User`).

- **`data/` (con `AppDbContext.cs`)**  
  Gestiona la conexión y configuración con la base de datos usando Entity Framework Core (Code-First).

- **`Migrations/`**  
  Carpeta generada por EF Core para rastrear cambios en el modelo y aplicar actualizaciones a la base de datos de forma controlada.

- **`dtos/`**  
  Los DTOs (Data Transfer Objects) definen contratos de datos seguros y específicos para entrada/salida, protegiendo el modelo interno y permitiendo validaciones personalizadas.

- **`middlewares/`**  
  Incluye un middleware de manejo de errores global para capturar excepciones de manera centralizada, mejorar trazabilidad y evitar duplicación de lógica de manejo de errores.

- **`settings/`**  
  Centraliza configuraciones como las claves de autenticación JWT, separando la lógica de negocio de la configuración sensible o variable por entorno.
---

## 🧩 Autor y Licencia

Desarrollado por **Jean Carlo Herrera Delgado**  
Licencia: MIT
