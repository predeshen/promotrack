# Stage 1: Build the app using the full .NET 9 SDK
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build-env
WORKDIR /src
COPY . .
RUN dotnet restore
RUN dotnet publish src/API/PromoTrack.Api/PromoTrack.Api.csproj -c Release -o /app/publish

# --- THIS IS THE CRITICAL FIX ---
# The final image is based on the SDK.
# We will copy the ENTIRE source code into it, ensuring that any
# build-time tools like 'dotnet ef' have everything they need.
FROM mcr.microsoft.com/dotnet/sdk:9.0
WORKDIR /app

# First, copy the entire source code from the build context.
# This ensures all .csproj and .cs files are present.
COPY . .

# Install the dotnet-ef tool
RUN dotnet tool install --global dotnet-ef
ENV PATH="${PATH}:/root/.dotnet/tools"

# Now, copy the already-published application into a separate folder
COPY --from=build-env /app/publish ./publish

# The entrypoint will run the app from the 'publish' folder
ENTRYPOINT ["dotnet", "publish/PromoTrack.Api.dll"]