# Sistema de Gestión de Ventas e Inventario - Prueba Técnica

Proyecto desarrollado como **Prueba Técnica**, estructurado en una arquitectura desacoplada basada en **.NET 10** (tecnología seleccionada para garantizar soporte a largo plazo y máximo rendimiento), compuesta por dos proyectos principales: una **Web API RESTful** backend y una **aplicación cliente en ASP.NET Core MVC** encargada de consumirla.

El sistema permite administrar el inventario de productos, la gestión del punto de venta con cálculo de montos e IVA (13%) en tiempo real, control de acceso basado en roles de usuario (*Administrador* y *Operador*), auditoría de transacciones y generación de reportes corporativos en Excel y PDF, interactuando con **Microsoft SQL Server** mediante **Procedimientos Almacenados (Stored Procedures)** y **Table-Valued Parameters (TVP)**.

## 🚀 Características Principales

### Requerimientos Funcionales

* **Autenticación y Seguridad:** Inicio de sesión con contraseñas encriptadas en la base de datos mediante **BCrypt** y almacenamiento de sesión HTTP para control de acceso.

* **Control de Permisos por Rol:**
  * **Administrador:** Acceso completo a los módulos de *Inicio*, *Productos*, *Ventas* y *Reportes*.
  * **Operador:** Acceso restringido exclusivamente al registro de *Ventas* e *Inicio*.

* **CRUD Completo de Productos:** Mantenimiento de inventario (*consultar por código, registrar, actualizar y eliminar*) con confirmación interactiva.

* **Módulo de Registro de Ventas:**
  * Búsqueda dinámica de productos por código consumiendo la API backend.
  * Cálculo automático de Subtotal, IVA (13%) y Total a pagar en tiempo real mediante JavaScript.
  * Confirmación de eliminación de ítems temporales antes de procesar la venta.
  * Inserción transaccional atómica de cabecera y detalle utilizando **Table-Valued Parameters (TVP)** en SQL Server y descuento automático de stock.

* **Reportes Corporativos:** Módulo para la consulta y exportación de ventas filtradas por rango de fechas en formatos **Excel** y **PDF**.

### Extras e Implementaciones Destacadas

* **Documentación Interactiva (Swagger / OpenAPI 3.0):** Swagger UI integrado en la Web API para exploración y prueba interactiva de endpoints.

* **Arquitectura Limpia y Decoupled (Services → Repositories):** Inversión de control (IoC) e Inyección de Dependencias (DI) mediante interfaces (`IProductoRepository`, `IProductoService`, `IVentaRepository`, `IVentaService`, `IUsuarioRepository`, `IAuthService`).

* **Uso Exclusivo de Procedimientos Almacenados (`Usp_`):** Toda la interacción con la base de datos se ejecuta mediante T-SQL compilado en SQL Server.

* **Manejo Global de Excepciones:** Middleware de errores personalizado en la API (`ExceptionMiddleware`) y vista de error formal en la app MVC.

* **DTOs y Proyecciones Desacopladas:** Separación estricta en carpetas `Requests`, `Responses` y `Reportes`.

* **Interfaz de Usuario Corporativa (UI):** Diseño moderno e interactivo con **Bootstrap Icons**, **SweetAlert2** para modales y **Toastr.js** para notificaciones flotantes encapsuladas en un módulo JS global (`site.js`).

## 🛠️ Tecnologías Utilizadas

| Tecnología | Versión | Descripción |
|------------|---------|-------------|
| **.NET** | 10.0 | Framework base del proyecto (seleccionado para LTS y máxima compatibilidad futura) |
| **ASP.NET Core Web API** | 10.0 | Backend RESTful Full REST documentado con Swagger |
| **ASP.NET Core MVC** | 10.0 | Frontend Web cliente que consume los servicios de la Web API |
| **Entity Framework Core** | 10.0 | ORM para ejecución de Stored Procedures y Fluent API |
| **SQL Server** | 2019 / 2022 | Base de datos relacional (`PruebaTecnicaStbcDb`) |
| **BCrypt.Net-Next** | 4.0.3 | Encriptación y verificación de contraseñas |
| **ClosedXML** | Open Source | Generación y exportación de reportes en Excel (`.xlsx`) |
| **QuestPDF** | Open Source | Generación y exportación de reportes en PDF (`.pdf`) |
| **Bootstrap / Bootstrap Icons** | 5.3 / 1.11 | Framework CSS e iconografía corporativa |
| **SweetAlert2 & Toastr.js** | CDN | Librerías para confirmaciones interactivas y notificaciones |

## 🗂️ Estructura del Repositorio

```
mr-prueba-tecnica-stbc/
├── docs/                                # Scripts de base de datos
│   └── PruebaTecnicaStbcDb.sql          # Script T-SQL (Esquema, Tablas, TVP, SPs y Datos Semilla)
│
└── src/                                 # Código fuente de la solución
    ├── PruebaTecnica.Api/               # Backend Web API RESTful (.NET 10)
    │   ├── Properties/
    │   ├── Controllers/                 # Endpoints REST (ProductosController, VentasController, AuthController)
    │   ├── Data/                        # DbContext (PruebaTecnicaDbContext)
    │   ├── Dtos/                        # DTOs estructurados por dominio
    │   │   ├── Auth/
    │   │   ├── Productos/
    │   │   └── Ventas/
    │   ├── Entities/                    # Entidades T-SQL (Usuario, Producto, Venta, DetalleVenta)
    │   ├── Exceptions/                  # Excepciones personalizadas (AppException, NotFoundException, etc.)
    │   ├── Middlewares/                 # ExceptionMiddleware (Gestión global de errores)
    │   ├── Repositories/                # Implementación de repositorios e Interfaces/
    │   ├── Services/                    # Lógica de negocio e Interfaces/
    │   ├── appsettings.json             # Configuración de cadenas de conexión
    │   └── Program.cs                   # Registros DI, Swagger UI y Pipeline HTTP
    │
    └── PruebaTecnica.Mvc/               # Frontend ASP.NET Core MVC (.NET 10)
        ├── Controllers/                 # AccountController, ProductosController, VentasController, HomeController
        ├── Models/                      # Dtos y ViewModels de soporte
        ├── Views/                       # Vistas Razor (_Layout, Account, Home, Productos, Ventas)
        ├── wwwroot/                     # Archivos estáticos (css, js/site.js, lib)
        ├── appsettings.json             # Configuración del cliente HTTP
        └── Program.cs                   # Configuración de HttpClient, Session, Rutas y Pipeline
```

## ⚙️ Requisitos Previos

* **.NET 10.0 SDK** o superior.
* **Microsoft SQL Server** (2019 o superior / Express / LocalDB).
* **SQL Server Management Studio (SSMS)** o Azure Data Studio.
* Entorno de desarrollo recomendado: **Visual Studio 2022 / Visual Studio vNext** (con soporte para .NET 10) o **Visual Studio Code**.

## 🗄️ Configuración de la Base de Datos

1. Abre **SQL Server Management Studio (SSMS)** y conéctate a tu servidor local.
2. Abre y ejecuta el script T-SQL ubicado en **`docs/PruebaTecnicaStbcDb.sql`**.

El script creará automáticamente:
* La base de datos **`PruebaTecnicaStbcDb`**.
* Las tablas principales: `Usuarios`, `Productos`, `Ventas`, `DetalleVentas` con campos de auditoría.
* El tipo de tabla para TVP: `dbo.TipoDetalleVenta`.
* Los Procedimientos Almacenados: `dbo.UspObtenerProductos`, `dbo.UspInsertarProducto`, `dbo.UspActualizarProducto`, `dbo.UspEliminarProducto`, `dbo.UspRegistrarVenta`, `dbo.UspReporteVentas`.
* Los datos semilla (*Seed Data*) para usuarios y productos iniciales.

> **Credenciales Iniciales (Usuarios de Prueba):**
> * **Administrador:** `username: admin` | `password: Admin123*`
> * **Operador:** `username: operador1` | `password: Admin123*`

## ▶️ Ejecución del Proyecto

### 1. Ajustar Cadena de Conexión y Puerto Base

Verifica que la cadena de conexión en `src/PruebaTecnica.Api/appsettings.json` apunte a tu servidor local de SQL Server:

```json
{
  "ConnectionStrings": {
    "PruebaTecnica": "Server=localhost;Database=PruebaTecnicaStbcDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

Revisa en `src/PruebaTecnica.Mvc/Program.cs` que la URL base de `ApiClient` coincida con el puerto donde corre tu Web API (ejemplo: `https://localhost:7094/`).

### 2. Ejecución desde Visual Studio

1. Abre la solución `PruebaTecnica.slnx` (o `.sln`).
2. Haz clic derecho sobre la Solución → **Configurar proyectos de inicio...**
3. Selecciona **Múltiples proyectos de inicio**:
   * `PruebaTecnica.Api`: **Iniciar**
   * `PruebaTecnica.Mvc`: **Iniciar**
4. Presiona `F5` o el botón **Iniciar**.

### 3. Ejecución desde Consola / CLI (.NET)

1. **Inicia la Web API:**
   ```bash
   cd src/PruebaTecnica.Api
   dotnet run
   ```

2. **Inicia la Aplicación MVC (en otra terminal):**
   ```bash
   cd src/PruebaTecnica.Mvc
   dotnet run
   ```

3. Abre tu navegador e ingresa a `https://localhost:7210` (o el puerto asignado a la app MVC).

## 🔑 Endpoints Principales (Web API REST)

Documentación interactiva disponible con la API en ejecución en: 👉 **`https://localhost:7094/swagger`** (o en la raíz `https://localhost:7094/`).

### Autenticación (`/api/auth`)

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/auth/login` | Autentica un usuario validando credenciales y contraseña BCrypt |

### Productos (`/api/productos`)

| Método | Ruta | Descripción |
|--------|------|-------------|
| GET | `/api/productos` | Obtener lista de productos (soporta filtro opcional `?codigo=`) |
| POST | `/api/productos` | Registrar un nuevo producto en inventario |
| PUT | `/api/productos/{id}` | Actualizar un producto existente |
| DELETE | `/api/productos/{id}` | Eliminar un producto físicamente del sistema |

### Ventas (`/api/ventas`)

| Método | Ruta | Descripción |
|--------|------|-------------|
| POST | `/api/ventas` | Registrar una venta (Cabecera + TVP de Detalle) y descontar stock |
| GET | `/api/ventas/reporte` | Consultar reporte de ventas por rango de fechas (`?fechaInicio=` & `?fechaFin=`) |
| GET | `/api/ventas/exportar-excel` | Descargar reporte corporativo en formato Excel (`.xlsx`) |
| GET | `/api/ventas/exportar-pdf` | Descargar reporte corporativo en formato PDF (`.pdf`) |

---

## 👤 Autor

**Ing. Miguel Antonio Rodríguez Guevara**

* **GitHub:** [mrodriguez882013](https://github.com/mrodriguez882013)
* **Correo:** mrodriguez882013@gmail.com

> Proyecto desarrollado como prueba técnica (.NET 10 Web API + ASP.NET Core MVC).