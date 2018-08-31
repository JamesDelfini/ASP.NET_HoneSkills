# ASP.NET_HoneSkills

## Entity Framework 
1. First Install Entity Framework at NuGet Package Manager or install via NuGet Package Manager command line.
> _install-package EntityFramework -version x.x.x (Optional)_
2. Only run the this command once lifecycle of the app.
> _enable-migrations_
3. Adding Migration File
> _add-migration <title> -Force(Optional)_
4. Updating Database
> _update-database_
5. Downgrading Database to its Specified Version
> _update-database -TargetMigration:<name>_ 
