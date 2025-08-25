### Instrucciones para correr el proyecto de manera correcta:

1. Descargar el codigo fuente
2. Abrir la terminal en la carpeta de la solucion. Por ejemplo: `D:\Users\MiUsuario\Documents\TaskManagementSystem>`
3. Correr las migraciones con los comandos
```
dotnet ef migrations add InitialCreate --project TaskManagementSystem.Infrastructure --startup-project TaskManagementSystem.Api

dotnet ef database update --project TaskManagementSystem.Infrastructure --startup-project TaskManagementSystem.Api      
```
4. Se creará el archivo ```TaskManagementSystem.db``` que corresponde a la bases de datos
5. Listo! Inicia el proyecto.

⚠️ Antes de iniciar con las pruebas de los endpoints, es necesario que te crees un usuario mediante el endpoint de `CreateUser` ya que es necesario un usuario para "emular" la autentificacion. Esto se realizó de esta manera por falta de tiempo. 

### Arquitecturas:
- Clean Architecture
- Domain Driven Design

### Patrones:
- Mediator
- Repository
- Dto

### Tecnologias:
- .Net 8
- EntityFrameworkCore
- Migrations
- SQLite
- Automapper
- Swagger
- Jwt


### Consideraciones
- **CQRS**: Si bien, ya implementamos el patron CQRS, podríamos separarlo aun más si separamos las operaciones de escritura con las de lectura. 
- **Autentificacion JWT**: Todos los endpoints están expuestos, no tienen autentificacion. `Si bien, creamos el modulo de autentificacion para obtener los token, faltó implementar la etiqueta en los endpoints `[Authorize]`
- **ErrorOr**: Era escencial aplicar el patron para no tener que lanzar excepciones, este patron nos trae la ventaja de tener errores más descriptivos en las respuestas de los endpoints.
- **Propiedad public long Version**: Se agrega esta propiedad para gestionar la concurrencia, usualmente en otro motor de bases de datos se utiliza `RowVersion`. Esto se realiza ya que los endpoints son asincronos. 
- **Deleted**: Los AggregateRoot tienen un campo Deleted, la cual gestionará una baja logica. 
- **IPasswordHasher:** Se crea este service con el fin de gestionar el hasheado de las contraseñas ingresadas por los usuarios. 
- **IJwtTokenGenerator:** Se crea este service con el fin de gestionar los Token JWT que se devuelven en el endpoint de Auth.

### Logica de negocio
- **Task:**
    - Puede estar en estado: Pendiente, En Proceso, Completada.
    - Está asociada unicamente a un User.
    - Posee un historial de cambios (TaskHistory)
    - No se pueden asignar Task a Users dados de baja (o innexistentes).

- **User**
    - No existe un usuario con el mismo email o username.
    - La contraseña se hashea en la base de datos, los endpoint no la devuelven jamas. 
    - Al eliminarse un User, las Task asociadas se eliminan tambien.
