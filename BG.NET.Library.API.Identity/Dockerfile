FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build
WORKDIR /src
COPY ["BG.NET.Library.API.Identity/BG.NET.Library.API.Identity.csproj", "BG.NET.Library.API.Identity/"]
RUN dotnet restore "BG.NET.Library.API.Identity/BG.NET.Library.API.Identity.csproj"
COPY . .
WORKDIR "/src/BG.NET.Library.API.Identity"
RUN dotnet build "BG.NET.Library.API.Identity.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "BG.NET.Library.API.Identity.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "BG.NET.Library.API.Identity.dll"]
