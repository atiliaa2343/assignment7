# Build stage
FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
WORKDIR /src

# Copy solution and project files
COPY reference.sln .
COPY DBConnector/DBConnector.csproj DBConnector/
COPY ConsoleApp/ConsoleApp.csproj ConsoleApp/

# Restore dependencies
RUN dotnet restore

# Copy all source files
COPY DBConnector/ DBConnector/
COPY ConsoleApp/ ConsoleApp/

# Build the solution
WORKDIR /src
RUN dotnet build -c Release -o /app/build

# Publish the console application
RUN dotnet publish ConsoleApp/ConsoleApp.csproj -c Release -o /app/publish

# Runtime stage
FROM mcr.microsoft.com/dotnet/runtime:9.0
WORKDIR /app

# Copy published application
COPY --from=build /app/publish .

# Set entrypoint
ENTRYPOINT ["dotnet", "ConsoleApp.dll"]

