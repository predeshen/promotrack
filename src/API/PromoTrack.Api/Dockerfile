# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy csproj files for all backend projects
COPY src/Core/PromoTrack.Domain/*.csproj ./src/Core/PromoTrack.Domain/
COPY src/Core/PromoTrack.Application/*.csproj ./src/Core/PromoTrack.Application/
COPY src/Infrastructure/PromoTrack.Infrastructure/*.csproj ./src/Infrastructure/PromoTrack.Infrastructure/
COPY src/API/PromoTrack.Api/*.csproj ./src/API/PromoTrack.Api/

# Restore dependencies for the API project specifically
RUN dotnet restore src/API/PromoTrack.Api/PromoTrack.Api.csproj

# Copy the rest of the source code
COPY . .

# Publish just the API project
RUN dotnet publish src/API/PromoTrack.Api/PromoTrack.Api.csproj -c Release -o /app/publish

# Use the smaller ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:9.0
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "PromoTrack.Api.dll"]