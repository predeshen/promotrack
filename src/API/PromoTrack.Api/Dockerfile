# Use the official .NET SDK image to build the app
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build-env
WORKDIR /app

# Copy csproj and restore as distinct layers to leverage Docker cache
COPY src/Core/PromoTrack.Domain/*.csproj ./src/Core/PromoTrack.Domain/
COPY src/Core/PromoTrack.Application/*.csproj ./src/Core/PromoTrack.Application/
COPY src/Infrastructure/PromoTrack.Infrastructure/*.csproj ./src/Infrastructure/PromoTrack.Infrastructure/
COPY src/API/PromoTrack.Api/*.csproj ./src/API/PromoTrack.Api/

# Copy the solution file and restore all dependencies
COPY PromoTrack.sln .
RUN dotnet restore

# Copy the rest of the source code
COPY . .

# Publish the API
RUN dotnet publish src/API/PromoTrack.Api/PromoTrack.Api.csproj -c Release -o /app/publish

# Use the smaller ASP.NET runtime image for the final image
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build-env /app/publish .
ENTRYPOINT ["dotnet", "PromoTrack.Api.dll"]