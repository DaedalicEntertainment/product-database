# Debian 10 Setup

## Install ASP.NET Core

1. Log in to your Debian instance.
2. Register the Microsoft key and product repository.

```
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | gpg --dearmor > microsoft.asc.gpg
sudo mv microsoft.asc.gpg /etc/apt/trusted.gpg.d/

wget -q https://packages.microsoft.com/config/debian/10/prod.list
sudo mv prod.list /etc/apt/sources.list.d/microsoft-prod.list

sudo chown root:root /etc/apt/trusted.gpg.d/microsoft.asc.gpg
sudo chown root:root /etc/apt/sources.list.d/microsoft-prod.list

sudo apt-get update --allow-insecure-repositories
```

3. Install ASP.NET Core.

```
sudo apt-get install apt-transport-https
sudo apt-get install aspnetcore-runtime-3.1
```

## Install MS SQL Server

1. Register the Microsoft key and product repository.

```
wget -qO- https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -

sudo apt-get install software-properties-common
sudo add-apt-repository "$(wget -qO- https://packages.microsoft.com/config/ubuntu/16.04/mssql-server-2019.list)"
```

2. Install MS SQL Server.

```
sudo apt-get update --allow-insecure-repositories
sudo apt-get install mssql-server
```

3. Setup MS SQL Server.

```
sudo /opt/mssql/bin/mssql-conf setup
```

4. Verify the service.

```
systemctl status mssql-server --no-pager
```

If the service isn't running, you can investigate the log files using 

```
journalctl --no-page -u mssql-server.service
```

Probably, your machine doesn't have 2 GB of RAM available.

5. Install command-line tools.

```
curl https://packages.microsoft.com/keys/microsoft.asc | sudo apt-key add -
curl https://packages.microsoft.com/config/ubuntu/16.04/prod.list | sudo tee /etc/apt/sources.list.d/msprod.lis

sudo apt-get update --allow-insecure-repositories
sudo apt-get install mssql-tools
sudo apt-get install unixodbc-dev
```

## Setup MS SQL Server

1. Connect to your local SQL server instance.

```
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA
```

2. Create a login.

```
CREATE LOGIN <loginname> WITH PASSWORD = '<password>';
GO
```

3. Create the database.

```
CREATE DATABASE DaedalicProductDatabase
GO
```

4. Create a database user with access rights.

```
USE DaedalicProductDatabase
GO

CREATE USER <username> FOR LOGIN <loginname>;  
GO

EXEC sp_addrolemember N'db_datawriter', N'<username>'
GO

EXEC sp_addrolemember N'db_datareader', N'<username>'
GO
```

## Publish the application

1. In Windows, open Source\ProductDatabase\ProductDatabase.sln in Visual Studio.
2. Right-click Daedalic.ProductDatabase in the Solution Explorer and publish the app (e.g. to bin\Release\netcoreapp3.1\publish).
3. In your Package Manager Console in Visual Studio, generate the SQL script for your initial migration for creating all tables:

```
Script-Migration
```

4. Add ```USE DaedalicProductDatabase``` to the resulting file and save it as ```migrate.sql``` on disk.
5. In your published application, edit your application settings (e.g. ```bin\Release\netcoreapp3.1\publish\appsettings.json```), modifying the connection string as follows:   

```
"DaedalicProductDatabaseContext": "Server=localhost;Database=DaedalicProductDatabase;Persist Security Info=False;User ID=<loginname>;Password=<password>" 
```

6. Archive your published application and copy it to your Debian instance (e.g. using ```scp``` in Windows Subsystem for Linux).
7. Copy your ```migrate.sql``` script to your Debian instance.

## Setup the database

1. On your Debian instance, locate your ```migrate.sql``` script.
2. Apply your initial database migration.

```
/opt/mssql-tools/bin/sqlcmd -S localhost -U SA -i migrate.sql
```

## Setup your application

1. Install nginx.

```
sudo apt-get install nginx
sudo service nginx start
```

2. Verify nginx is running by visiting http://localhost/index.nginx-debian.html.
3. Configure nginx by editing /etc/nginx/sites-available/default.

```
server {
        listen 8080 default_server;
        listen [::]:8080 default_server;

        server_name _;

        location / {
                proxy_pass http://localhost:5000;
                proxy_http_version 1.1;
                proxy_set_header Upgrade $http_upgrade;
                proxy_set_header Connection keep-alive;
                proxy_set_header Host $host;
                proxy_cache_bypass $http_upgrade;
                proxy_set_header X-Forwarded-For $proxy_add_x_forwarded_for;
                proxy_set_header X-Forwarded-Proto $scheme;
        }
}
```

4. Update the nginx configuration.

```
sudo nginx -t
sudo nginx -s reload
``` 

5. Setup your firewall.

```
sudo /sbin/iptables -I INPUT -p tcp -m tcp --dport 8080 -j ACCEPT
```

## Start your application

1. Locate your published application and extract it (e.g. using ```unzip```).
2. Start the application.

```
dotnet Daedalic.ProductDatabase.dll
```

## Backups

You can backup and restore your database using Tools\backupdb.sql and Tools\restoredb.sql. The database will be backed up to (and restored from) three files. If you are replacing those files by other copies, make sure they're owned by the correct user before running restoredb.sql:

```
sudo chown mssql /var/opt/mssql/data/DaedalicProductDatabase.bak
sudo chown mssql /var/opt/mssql/data/DaedalicProductDatabase_log.ldf
sudo chown mssql /var/opt/mssql/data/DaedalicProductDatabase.mdf
```

Also, if you're restoring your database from another system, you might need to re-create your database users after restore.

## References

* Installing ASP.NET Core for Linux: https://docs.microsoft.com/en-us/dotnet/core/install/linux-package-manager-debian10
* Force update from unsigned repository: https://askubuntu.com/questions/732985/force-update-from-unsigned-repository
* Installing MS SQL Server for Linux: https://docs.microsoft.com/en-us/sql/linux/quickstart-install-connect-ubuntu?view=sql-server-linux-ver15
* Check MS SQL Server logs: https://serverfault.com/a/826685
* Creating MS SQL logins: https://docs.microsoft.com/en-us/sql/t-sql/statements/create-login-transact-sql?view=sql-server-ver15
* Creating MS SQL Server users: https://docs.microsoft.com/en-us/sql/t-sql/lesson-2-configuring-permissions-on-database-objects?view=sql-server-linux-ver15
* Grant user roles: https://social.msdn.microsoft.com/Forums/sqlserver/en-US/1489337c-56c9-4bb8-9875-3a75be7596be/grant-select-insert-update-delete-on-all-table?forum=transactsql
* Publishing for Linux: https://docs.microsoft.com/en-us/aspnet/core/host-and-deploy/linux-nginx?view=aspnetcore-3.1
* Generate migration SQL script: https://docs.microsoft.com/en-us/ef/core/managing-schemas/migrations/?tabs=vs#generate-sql-scripts
* SQL connection strings: https://docs.microsoft.com/en-us/dotnet/framework/data/adonet/connection-string-syntax
* Run SQL scripts: https://docs.microsoft.com/en-us/sql/ssms/scripting/sqlcmd-run-transact-sql-script-files?view=sql-server-ver15
* sqlcmd reference: https://docs.microsoft.com/en-us/sql/tools/sqlcmd-utility?view=sql-server-ver15
* Configure Linux iptables: https://serverfault.com/questions/301903/cannot-access-port-80-from-remote-location-but-works-on-local
* Backup database: https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/full-database-backups-sql-server?view=sql-server-ver15
* Restore database: https://docs.microsoft.com/en-us/sql/relational-databases/backup-restore/complete-database-restores-simple-recovery-model?view=sql-server-ver15
