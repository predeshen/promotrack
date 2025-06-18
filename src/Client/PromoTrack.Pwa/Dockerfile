# Stage 1: Build the Blazor PWA
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /app

# Copy the PWA project file first and restore its dependencies
COPY src/Client/PromoTrack.Pwa/*.csproj ./src/Client/PromoTrack.Pwa/
RUN dotnet restore src/Client/PromoTrack.Pwa/PromoTrack.Pwa.csproj

# Copy the rest of the PWA project files
COPY src/Client/PromoTrack.Pwa/. ./src/Client/PromoTrack.Pwa/

# Publish the PWA
RUN dotnet publish src/Client/PromoTrack.Pwa/PromoTrack.Pwa.csproj -c Release -o /app/publish

# Stage 2: Create the final Nginx image
FROM nginx:alpine
WORKDIR /usr/share/nginx/html

# Copy the published PWA files from the build stage
COPY --from=build-env /app/publish/wwwroot .

# Copy our custom Nginx configuration
COPY src/Client/PromoTrack.Pwa/nginx.conf /etc/nginx/conf.d/default.conf