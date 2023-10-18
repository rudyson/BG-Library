FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BGLibrary.Library/BGLibrary.Library.csproj", "BGLibrary.Library/"]
RUN dotnet restore "BGLibrary.Library/BGLibrary.Library.csproj"
COPY . .
WORKDIR "/src/BGLibrary.Library"
RUN dotnet build "BGLibrary.Library.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BGLibrary.Library.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BGLibrary.Library.dll"]
