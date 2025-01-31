# JSBankApi

Este proyecto fue creado en base a los requerimiento recibidos por parte de Ing. David Pellon para validar conocimientos en programación .NET
Se espera el mismo pueda cumplir con los puntos claves y directivas solicitadas.

##Requisitos Previos
-Tener isnataldo sdk de .Net 8 SDK
-Aplicacion Postman o similar para ejecutar las pruebas hacia la API.

## Configuración del Proyecto
1. Clonar el repositorio: https://github.com/jabrahamsc/JSBankApi.git
2. Navegar a la carpeta del proyecto JSBankApi
3. Restaurar las dependencias: dotnet restore
4. Ejecutar migraciones para crear la base de datos: dotnet ef database update
5. Iniciar la aplicación

Prueba de requisitos
1. Puede crear perfiles de clientes con el endpoint de tipo POST: http://localhost:5174/api/clientes e introducciendo el siguiente json en el body como raw
{
  "nombre": "Nombre Cliente",
  "fechaNacimiento": "2000-01-01",
  "sexo": "M",
  "ingresos": 100.00
}
2. Puede consultar todos los clientes creados con el endpoint de tipo GET: http://localhost:5174/api/clientes
3. Para crear una cuenta bancaria utilice el endpoitn de tipo POST: http://localhost:5174/api/cuentasbancarias y en el body raw el cuerpo del json:
{
  "numeroCuenta": "123456789",
  "saldo": 2000.00,
  "clienteId": 1
}
Indicando el id del cliente al que pertenece, no puede ingresar un numero de cuenta dos veces.
4. Para consultar saldo de una cuenta utilice el endpoint GET: http://localhost:5174/api/cuentasbancarias/saldo/numerocuenta, reemplazando el parametro "numerocuenta" por el que desea consultar
5. Realizar depositos POST: http://localhost:5174/api/transacciones/deposito?numeroCuenta=123456789&monto=100 especificando la cuenta a la que desea depositar y el monto
6. Realizar retiros POST: http://localhost:5174/api/transacciones/retiro?numeroCuenta=123456789&monto=66 especificando la cuenta a la que desea retirar y el monto
7. Puede aplicarle interes a una cuenta mediante el siguiente endpoint POST:  http://localhost:5174/api/cuentasbancarias/aplicar-intereses?numeroCuenta=123456789&tasaInteres=0.05, esto hara que conforme el interes enviado, se agregue el mismo de manera porcentual al saldo que tenga la cuenta.
8. Para visualizar el resumen de las transacciones de una cuenta metodo GET: http://localhost:5174/api/transacciones/historial/123456789 indicando el numero de cuenta a buscar.

Para cualquier duda o consulta la puede hacer al correo jorgeabrahamsanchez07@outlook.com
