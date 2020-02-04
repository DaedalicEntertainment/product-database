# product-database
Company product database built as ASP.NET Core Web Application.

## Prerequisites

* [Visual Studio 2019 16.4](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019) or later with the ASP.NET and web development workload
* [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) or later
* [SQL Server 2019 Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

## Adding new data models

1. Add model class with references and validation.
1. Scaffold new and changed model pages
   1. Correct namespaces
   1. Add empty select items for Create and Edit, if necessary
   1. Set Index page title and h1
   1. Replace query parameters by path variables.
1. Add search, sorting and alerts to Index model and view.
1. Add help texts to Create and Edit views.
1. Use text areas for long strings.
1. Add page link to layout or admin page.
1. Add count to Home page if interesting.
1. Add missing data insights check, if applicable.
1. Add changing sort orders to admin page, if applicable.
1. Handle deletion (e.g. set references to null, cascade deletion, or prevent deletion).
1. Migrate database.
1. Seed database (if applicable).

See https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/?view=aspnetcore-3.1 for details.

## References

* Publishing for Linux: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1
* Installing ASP.NET Core for Linux: https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-ubuntu-1904#install-the-aspnet-core-runtime
* Installing MS SQL Server for Linux: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-linux-ver15
* Creating MS SQL logins: https://docs.microsoft.com/en-us/sql/t-sql/statements/create-login-transact-sql?view=sql-server-ver15
* Creating MS SQL Server users: https://docs.microsoft.com/en-us/sql/t-sql/lesson-2-configuring-permissions-on-database-objects?view=sql-server-linux-ver15
* Grant user roles: https://social.msdn.microsoft.com/Forums/sqlserver/en-US/1489337c-56c9-4bb8-9875-3a75be7596be/grant-select-insert-update-delete-on-all-table?forum=transactsql
* sqlcmd reference: https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility?view=sql-server-ver15
* SQL connection strings: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-string-syntax
* Generate migration SQL script: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs#generate-sql-scripts (add USE statement)
* Run SQL scripts: https://docs.microsoft.com/en-us/sql/ssms/scripting/sqlcmd-run-transact-sql-script-files?view=sql-server-ver15
* Configure Linux iptables: https://serverfault.com/questions/301903/cannot-access-port-80-from-remote-location-but-works-on-local, https://stackoverflow.com/a/39733403
* Backup database: https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/full-database-backups-sql-server?view=sql-server-ver15
* Restore database: https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/complete-database-restores-simple-recovery-model?view=sql-server-ver15
