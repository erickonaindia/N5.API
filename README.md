# Backend

N5 Backend Challenge, using .net core 8, clean architecture, cqrs, repository pattern, fluent validation, Important: in the root you got a docker compose file, run this command: "docker compose up" on your terminal to get all the env, also, in that file have the secrets for db; after that you need to run the migrations with update-database command
Challenge:

N5 company requests a Web API for registering user permissions, to carry outthis task it is necessary to comply with the following steps: 
● Create a **Permissions** table with the following fields
 ![image](https://github.com/erickonaindia/N5.API/assets/196637/ae8c5144-2cf0-40ab-8b74-548c7eb08fed)

● Create a PermissionTypes table with the following fields:
 ![image](https://github.com/erickonaindia/N5.API/assets/196637/a694a8b7-ba61-42e6-9a10-b92e7293bb31)

 ● Create relationship between Permission and PermissionType.
 
● Create a Web API using ASP .NET Core and persist data on SQL 
Server.

● Make use of EntityFramework.
● The Web API must have 3 services “Request Permission”, “Modify 
Permission” and “Get Permissions”. Every service should persist a
permission registry in an elasticsearch index, the register inserted in
elasticsearch must contains the same structure of database table
“permission”. 
● Create apache kafka in local environment and create new topic where 
persist every operation a message with the next dto structure:
-Id: random Guid
-Name operation: “modify”, “request” or “get”. 
● Making use of repository pattern and Unit of Work and CQRS
pattern(Desired). Bear in mind that is required to stick to a proper
service architecture so that creating different layers and dependency
injection is a must-have. 
● Create Unit Testing and Integration Testing to call the three of the
services. 
 
