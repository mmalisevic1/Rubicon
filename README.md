# Rubicon
Coding challenge for Rubicon

* Web API built using ASP.NET and Entity Framework 6
* The database is **local** SQL Server Express, the path to the .mdf file is `Rubicon\App_Data\Rubicon.Data.RubiconContext.mdf`
> Database is seeded with some initial data. In case something happens with database file, there is `Seed` method that can be run
> in package manager console having Rubicon.Data set as the Deafult Project:
```sh
Update-Database
```
> This will create the database with all the tables seeded with initial data
* Collection of all requests available for this Web API is provided in this link: https://www.getpostman.com/collections/1663d3ccc7dc68129818
* This product is tested using both Unit tests and POSTMAN

# Important
It is possible, after building and running the project for the first time to get following error or similar:
`Could not find a part of the path...roslyn\csc.exe`
The solution is to:
* Rebuild the project
* Run it again

This did not happen on my local machine, but I did notice it happen on some other machines
