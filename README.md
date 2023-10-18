# BG-Library

## There are 5 components of the app:
- Authorization API
- Library API
- PostgreSQL database
- Angular Frontend
- Nginx Reverse proxy


# Usage
## Routes
To test API, use routes:
- http://localhost:8080/api/swagger/index.html
- http://localhost:8080/auth/swagger/index.html

## Functionality
### Identity:
- Register - registration using personal data
- Login - enter login and password to get JWT token
- Info - if JWT provided, you will see your personal information, which was provided during registration
### Library:
Books and authors CRUD. You can view avialable books and authors without authorization. If you want to add a new book or author, update its information or even delete it - authorize using JWT token, which you can get using Identity API.

# First launch
## Build and lauch .docker-compose.yaml to start using application
```
docker-compose build
docker-compose up
```

## If you hava troubles with lauching contenerized applications, run it manualy:
```
docker-compose up -d --force-recreate --no-deps --build postgresql
docker-compose up -d --force-recreate --no-deps --build library
docker-compose up -d --force-recreate --no-deps --build identity
docker-compose up -d --force-recreate --no-deps --build client
docker-compose up -d --force-recreate --no-deps --build nginx
```

## Database migrations
To initialize database, create migrations in directories of API (BGLibrary.Identity, BGLibrary.Library):
```
dotnet tool update --global dotnet-ef
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Required NodeJS packages
```
npm i @popperjs/core
npm i @auth0/angular-jwt
```
