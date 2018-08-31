# ASP.NET_HoneSkills

##Entity Framework 
1. First Install Entity Framework at NuGet Package Manager or install via NuGet Package Manager command line.
* install-package EntityFramework -version x.x.x (Optional)
2. Only run the this command once lifecycle of the app.
* enable-migrations
3. Adding Migration File
* add-migration <title> -Force(Optional)
4. Updating Database
* update-database
5. Downgrading Database to its Specified Version
* update-database -TargetMigration:<name> 
