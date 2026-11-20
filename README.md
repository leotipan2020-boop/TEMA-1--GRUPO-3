# Grupo 3 – Tema 1: Catálogo en línea de una tienda

**Asignatura:** Programación Integrada de Componentes Web – ESPE
**Docente:** Mgs. Orlen Araujo
**Repositorio:** https://github.com/leotipan2020-boop/TEMA-1--GRUPO-3
**Proyecto:** `TiendaOnline` (ASP.NET Core MVC, .NET 10, Entity Framework Core, SQL Server)

## Tema y problema asignado

Una tienda necesita su catálogo en línea. La información se llevaba en hojas de cálculo que cualquiera podía abrir y modificar: se borraban registros por error, nadie sabía quién cambió qué y los datos de una tabla no coincidían con los de la otra. Esta aplicación web valida los datos antes de guardarlos, relaciona cada producto con su categoría y permite que solo el rol de gestión cree, edite y elimine registros.

| Entidad principal | Entidad dependiente | Rol de gestión | Rol de consulta |
|---|---|---|---|
| **Categoria**: Id, Nombre, Descripcion? | **Producto**: Id, Nombre, Precio (decimal), Stock (int), CategoriaId | Administrador | Cliente |

Relación: una **Categoria** tiene muchos **Productos** (uno a muchos).

## Integrantes, orden de turnos y aportes

| Turno | Integrante | Fase | Aporte |
|---|---|---|---|
| 1 | Leonardo Darío Tipán Sanipatín | Fase 1 · Proyecto y entidad dependiente | Proyecto MVC .NET 10, repositorio y colaboradores, entidad Producto, CRUD con scaffolding, conexión a SQL Server, migración `InitialCreate`, README e informe PDF |
| 2 | Joseph Darío Paredes Tobar | Fase 2 y parte de la Fase 3 · Validaciones y Categoria | DataAnnotations en Producto con mensajes en español, migración `ValidacionesProducto`, entidad Categoria con validaciones y su CRUD, clave foránea `CategoriaId` |
| 3 | Melissa Verónica Quishpe Choloquinga | Fase 3 · Relación uno a muchos | Clave foránea `CategoriaId`, propiedades de navegación, migración de la relación, lista desplegable y nombre de la categoría en lista y detalle |
| 4 | Nicolás Aldair Gualoto Pillajo | Fase 4 · Identity | `ApplicationUser`, `IdentityDbContext<ApplicationUser>`, migración `AgregarIdentity`, registro, inicio y cierre de sesión, `_LoginPartial`, `[Authorize]` y `[AllowAnonymous]` |
| 5 | Fabricio Mateo Montachana Aldana | Fase 5 · Roles y componente autónomo | Roles Administrador y Cliente, usuarios de prueba, restricción por rol en ambos controladores y en las vistas, rol visible en el menú |

## Usuarios de prueba

| Rol | Correo | Contraseña |
|---|---|---|
| Administrador | admin@tienda.com | Tienda2026! |
| Cliente | cliente@tienda.com | Tienda2026! |

Los roles y los usuarios de prueba se crean automáticamente al iniciar la aplicación (`Program.cs`). Si un usuario ya tenía la sesión abierta antes de recibir su rol, debe cerrar sesión y volver a iniciarla.

## Permisos por rol

| Acción | Visitante sin sesión | Cliente | Administrador |
|---|---|---|---|
| Ver lista y detalle de Productos y Categorías | Sí | Sí | Sí |
| Crear, editar y eliminar Productos y Categorías | No | No | Sí |

Las acciones Create, Edit y Delete de `ProductosController` y `CategoriasController` tienen `[Authorize(Roles = "Administrador")]`; Index y Details tienen `[AllowAnonymous]`. Los enlaces Crear, Editar y Eliminar se ocultan en las vistas a quien no tiene el rol Administrador, y el menú muestra el correo y el rol del usuario.

## Migraciones

| Migración | Contenido |
|---|---|
| `InitialCreate` | Tabla Productos |
| `ValidacionesProducto` | Validaciones y tamaños de columnas de Producto |
| `Recuperacion` | Tabla Categorias y columna `CategoriaId` en Productos |
| `RelacionCategoriaProducto` | `CategoriaId` obligatoria y clave foránea de la relación uno a muchos |
| `AgregarIdentity` | Tablas de ASP.NET Core Identity |

## Video de demostración

_(enlace pendiente)_

## Pasos para ejecutar el proyecto

1. Clonar el repositorio: en Visual Studio, **Git > Clonar repositorio** y pegar `https://github.com/leotipan2020-boop/TEMA-1--GRUPO-3`.
2. Abrir `appsettings.json` y ajustar la cadena de conexión `DefaultConnection` al servidor local:
   ```json
   "DefaultConnection": "Server=localhost\\SQLEXPRESS;Database=TiendaOnlineDb;Trusted_Connection=True;TrustServerCertificate=True;MultipleActiveResultSets=true"
   ```
3. En **Herramientas > Administrador de paquetes NuGet > Consola del Administrador de paquetes**, ejecutar:
   ```
   Update-Database
   ```
4. Ejecutar el proyecto con **F5**.

## Reglas de trabajo del grupo

- Antes de modificar cualquier archivo: **Git > Extraer**. Si llegaron migraciones nuevas, ejecutar `Update-Database`.
- Las migraciones se crean por turnos: solo un integrante a la vez.
- Cada commit compila y su mensaje empieza con un verbo (por ejemplo: *Agregar validaciones a Producto*).
- Los paquetes de Entity Framework Core se mantienen en la versión **10.0.x**; no actualizar a 11.0.
- No se suben las carpetas `bin` ni `obj`.
