FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 8080

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src
COPY ["Restaurant/Restaurant.csproj", "Restaurant/"]
COPY ["Restaurant.Infrastructure/Restaurant.Infrastructure.csproj", "Restaurant.Infrastructure/"]
COPY ["Restaurant.Application/Restaurant.Application.csproj", "Restaurant.Application/"]
COPY ["Restaurant.Domain/Restaurant.Domain.csproj", "Restaurant.Domain/"]
RUN dotnet restore "Restaurant/Restaurant.csproj"
COPY . .
WORKDIR "/src/Restaurant"
RUN dotnet build "Restaurant.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Restaurant.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Restaurant.dll"]
