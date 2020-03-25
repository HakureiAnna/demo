FROM mcr.microsoft.com/dotnet/core/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 5000
ENV ASPNETCORE_URLS=http://*:5000

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["frontend/frontend.csproj", "frontend/"]
COPY ["infrastructure/infrastructure.csproj", "infrastructure/"]
RUN dotnet restore "./frontend/frontend.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "./frontend/frontend.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "./frontend/frontend.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "frontend.dll"]
