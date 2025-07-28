# ==============================
# Build Stage
# ==============================
FROM mcr.microsoft.com/dotnet/sdk:9.0-preview AS build
WORKDIR /src

# Copy csproj và restore trước để cache hiệu quả
COPY ["dotnet-api/dotnet-api.csproj", "dotnet-api/"]
RUN dotnet restore "dotnet-api/dotnet-api.csproj"

# Copy toàn bộ source và build
COPY . .
WORKDIR "/src/dotnet-api"
RUN dotnet publish -c Release -o /app/publish

# ==============================
# Runtime Stage
# ==============================
FROM mcr.microsoft.com/dotnet/aspnet:9.0-preview AS runtime
WORKDIR /app
COPY --from=build /app/publish .

# Expose port để Render biết lắng nghe
EXPOSE 8080

# Biến env mặc định cho Render
ENV ASPNETCORE_URLS=http://+:8080 \
    DOTNET_RUNNING_IN_CONTAINER=true

ENTRYPOINT ["dotnet", "dotnet-api.dll"]
