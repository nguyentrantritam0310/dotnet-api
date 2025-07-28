# --- BUILD STAGE ---
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# COPY solution and restore
COPY ["dotnet-api/dotnet-api.csproj", "dotnet-api/"]
RUN dotnet restore "dotnet-api/dotnet-api.csproj"

# COPY rest of source and publish
COPY . .
WORKDIR "/src/dotnet-api"
RUN dotnet publish "dotnet-api.csproj" -c Release -o /app/publish

# --- RUNTIME STAGE ---
FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS final
WORKDIR /app
COPY --from=build /app/publish .

# expose port 80 (Render expects this)
EXPOSE 80

ENTRYPOINT ["dotnet", "dotnet-api.dll"]
