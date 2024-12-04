# Contributors:

Jonas Etmann Skjøth (316954) -> https://github.com/jonasskjoeth 

Frederik Boysen (316997) -> https://github.com/fredboysen 

Benjamin Kring Suner (316963) -> https://github.com/BenjaminKringSuner 

Thomas Schelde (316966) -> https://github.com/Zoichk

*All contributions are made collectively / everything is co-authored.*

- [X] **[Blog 1 - Project Formulation & Initial Requirements](https://github.com/fredboysen/DNDPROJECT/blob/main/Blog%231.md)**

- [X] **[Blog 2 - Web Service Design & Implementation](https://github.com/fredboysen/DNDPROJECT/blob/main/Blog%232.md)**

- [X] **[Blog 3 - Web Application](https://github.com/fredboysen/DNDPROJECT/blob/main/Blog%233.md)**

- [X] **[Blog 4 - User Management](https://github.com/fredboysen/DNDPROJECT/blob/main/Blog%234.md)**

- [X] **[Blog 5 - Data Access](https://github.com/fredboysen/DNDPROJECT/blob/main/Blog%235.md)**

- [X] **[Blog 6 - Project Conclusion & Demonstration](https://github.com/fredboysen/DNDPROJECT/blob/main/Blog%236.md)**


## Required install to run / process
1. Download SQLite
2. Ensure you have the latest .NET version (9.0 as of writing)
```
dotnet --version
```
3. Download entity framework packages
```
dotnet tool install --global dotnet-ef
```
4. Restore packages
```
dotnet restore
```
6. Create database in SQlite (You can use DB Browser (SQLite) to create db on local)
7. Update appsettings.json in BookTradingHub.WebAPI
8. Update database to add tables
```
dotnet ef database update
```
10. Run API on https
```
dotnet run --launch-profile https
```
10. Run WebApp
```
dotnet run
```

## User passwords for testing
**Admin** <br>
`username: testadmin` <br>
`password: test123`

**Regular user** <br>
`username: testuser` <br>
`password: test123`


