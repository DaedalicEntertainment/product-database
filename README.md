# product-database
Company product database built as ASP.NET Core Web Application.

## Setup

For instructions on how to host the database on Debian 10, see SETUP.md.

## Development
### Prerequisites

* [Visual Studio 2019 16.4](https://visualstudio.microsoft.com/downloads/?utm_medium=microsoft&utm_source=docs.microsoft.com&utm_campaign=inline+link&utm_content=download+vs2019) or later with the ASP.NET and web development workload
* [.NET Core 3.1 SDK](https://dotnet.microsoft.com/download/dotnet-core/3.1) or later
* [SQL Server 2019 Express](https://www.microsoft.com/en-us/sql-server/sql-server-downloads)
* [SQL Server Management Studio](https://docs.microsoft.com/en-us/sql/ssms/download-sql-server-management-studio-ssms?view=sql-server-ver15)

### Adding new data models

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
