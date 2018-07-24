# IdentityNpgsqlNodatime

## Running the sample

After cloning or downloading the sample you will need to update the database in order to run it.

Ensure you add a .env in the root of the `Link.Web` project. Than add the necessary environment variables:

```
DATABASE_URL=User ID=postgres;Password=;Host=localhost;Port=5432;Database=link_development;
```

Open a terminal in the Link.Web folder and execute the following commands:

```
dotnet restore
dotnet ef database update -c applicationdbcontext -p ../IdentityNpgsqlNodatime.Infrastructure -s IdentityNpgsqlNodatime.Web.csproj
```

These commands will create a database with two seperate dbcontext, one for the AspNet Identity contents and the other for the IdentityServer configuration.

## Adding migrations

If you need to create migrations you can use this commands

```
-- create migration (from Link.Web folder CLI)
dotnet ef migrations add InitialIdentityModel --context applicationdbcontext -p ../IdentityNpgsqlNodatime.Infrastructure/IdentityNpgsqlNodatime.Infrastructure.csproj -s IdentityNpgsqlNodatime.Web.csproj -o Data/Application/Migrations
```

## Running the sample using Docker

You can run the sample by running the commands from the root folder (where the .sln file is located)
TODO:
