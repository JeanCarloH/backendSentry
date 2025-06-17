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
   git clone https://github.com/tuusuario/TaskManagementAPI.git
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

## 🧩 Autor y Licencia

Desarrollado por **Jean Carlo Herrera Delgado**  
Licencia: MIT
