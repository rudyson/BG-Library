# BG-Library

## There are 5 components of the app:

- Authorization API
- Library API
- Postgres database
- Angular Frontend
- Nginx Reverse proxy

# Usage

## Routes

Data API:
- http://localhost:44303/swagger/index.html
- https://localhost:44304/swagger/index.html
Authorization API:
- http://localhost:44301/swagger/index.html
- https://localhost:44302/swagger/index.html

## Functionality

### Identity:

- Register - registration using personal data
- Login - enter login and password to get JWT token
- Info - if JWT provided, you will see your personal information, which was provided during registration

### Library:

Books and authors CRUD. You can view available books and authors without authorization. If you want to add a new book or author, update its information or even delete it - authorize using JWT token, which you can get using Identity API.

# First launch

## Build and launch .docker-compose.yaml to start using application

```
docker-compose build
docker-compose up
```

## If you hava troubles with launching containerized applications, run it manually:

```
docker-compose up -d --force-recreate --no-deps --build postgresql
docker-compose up -d --force-recreate --no-deps --build library
docker-compose up -d --force-recreate --no-deps --build identity
docker-compose up -d --force-recreate --no-deps --build client
docker-compose up -d --force-recreate --no-deps --build nginx
```

## Configurations

There are two required files with environment variables:

**postgres.env**
```dotenv
POSTGRES_PASSWORD=password
POSTGRES_USER=user
POSTGRES_HOST=postgresql
POSTGRES_DB=bglibrary
```

```dotenv
Jwt__Secret=Jwt-Secret-Key
Jwt__Issuer=Jwt-Issuer
Jwt__Audience=Jwt-Audience
ConnectionStrings__LibraryData="Server=postgresql;Database=database;Port=5432;User Id=username;Password=password;"
```

## Database migrations

To initialize database, create migrations in directories of API (BGLibrary.Identity, BGLibrary.Library):

```
dotnet tool update --global dotnet-ef
dotnet ef migrations add InitialCreate --context IdentityDbContext
dotnet ef migrations add InitialCreate --context LibraryDbContext
dotnet ef database update
```

## Required NodeJS packages

```
npm i @popperjs/core
npm i @auth0/angular-jwt
```
