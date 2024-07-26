FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

WORKDIR /src
COPY server.csproj .
RUN dotnet restore
COPY . .
RUN dotnet publish -c release -o /app

FROM mcr.microsoft.com/dotnet/sdk:8.0
WORKDIR /app
EXPOSE 8080
ENV ASPNETCORE_URLS=http://*:8080
COPY --from=build /app .
ENTRYPOINT ["dotnet", "server.dll"]