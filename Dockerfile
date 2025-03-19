# Step 1: Build Stage
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy the project files
COPY *.csproj ./
RUN dotnet restore

# Copy everything else and build the app
COPY . . 
RUN dotnet publish -c Release -o /out

# Step 2: Runtime Stage
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

# Copy built app from previous stage
COPY --from=build /out .

# Expose the required port
EXPOSE 5000
EXPOSE 5001

# Start the application
ENTRYPOINT ["dotnet", "E_commerce.dll"]
