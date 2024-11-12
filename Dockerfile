# Stage 1: Build the .NET project
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

# Copy the solution file and the project files
COPY ToDoAPI.sln ./
COPY ToDoAPI/ ToDoAPI/

# Restore dependencies for the project
WORKDIR /app/ToDoAPI
RUN dotnet restore

# Build the project and publish the output
RUN dotnet publish -c Release -o /app/publish

# Stage 2: Run the .NET app in a lightweight runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime
WORKDIR /app
COPY --from=build /app/publish .
EXPOSE 80

# Set the entry point for the container
ENTRYPOINT ["dotnet", "ToDoAPI.dll"]
