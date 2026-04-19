FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Point to the specific subfolder for the project file
COPY Deliveroo.Gateway/*.csproj ./
RUN dotnet restore

# Copy everything else from the subfolder
COPY Deliveroo.Gateway/ ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out ./

# Ensure the DLL name matches your project name exactly
ENTRYPOINT ["dotnet", "Deliveroo.Gateway.dll"]