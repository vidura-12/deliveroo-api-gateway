FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# The Dockerfile is already inside Deliveroo.Gateway, 
# so we just copy from the current location.
COPY *.csproj ./
RUN dotnet restore

# Copy all files from the current folder (where Program.cs and ocelot.json are)
COPY . ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app

COPY --from=build /app/out ./

ENTRYPOINT ["dotnet", "Deliveroo.Gateway.dll"]