FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BGLibrary.Identity/BGLibrary.Identity.csproj", "BGLibrary.Identity/"]
RUN dotnet restore "BGLibrary.Identity/BGLibrary.Identity.csproj"
COPY . .
WORKDIR "/src/BGLibrary.Identity"
RUN dotnet build "BGLibrary.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BGLibrary.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BGLibrary.Identity.dll"]
