# Use the appropriate base image (e.g., sdk for development)
FROM mcr.microsoft.com/dotnet/sdk:7.0 AS build

WORKDIR /app

COPY . /app

# Enable debugging by setting the ASPNETCORE_ENVIRONMENT and ASPNETCORE_URLS environment variables
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:80

# Expose the ports
EXPOSE 80
EXPOSE 443

# Build the application in watch mode for automatic restart on changes
CMD ["dotnet", "watch", "run", "--no-launch-profile"]
