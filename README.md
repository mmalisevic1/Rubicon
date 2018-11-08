# Rubicon
Coding challenge for Rubicon

* Web API built using ASP.NET and Entity Framework 6
* The database is local SQL Server Express, the path to the .mdf file is `Rubicon\App_Data\Rubicon.Data.RubiconContext.mdf`
> Database is seeded with some initial data. In case something happens with database file, there is `Seed` method that can be run
> in package manager console:
```sh
Update-Database
```
* Collection of all requests availible for this Web API is provided in this link: https://www.getpostman.com/collections/1663d3ccc7dc68129818
* This product is tested using both Unit tests and POSTMAN
