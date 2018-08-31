# C# Database Access
Working with Entity Framework and LINQ to access a database with its setup and queries.

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

[LINQ-Cheat-Sheet](http://www.mediafire.com/file/tknjgkg9n4qdgra/LINQ-Cheat-Sheet.pdf/file)
[LINQ-Cheat-Sheet-Extension-Methods](http://www.mediafire.com/file/989gac8bbyrmw80/LINQ-Cheat-Sheet-Extension-Methods.pdf/file)
[Data-Annotations-Cheat-Sheet](http://www.mediafire.com/file/989gac8bbyrmw80/LINQ-Cheat-Sheet-Extension-Methods.pdf/file)
[Fluent-API-Cheat-Sheet](http://www.mediafire.com/file/a8x71e5b9l68pv5/Fluent-API-Cheat-Sheet.pdf/file)
