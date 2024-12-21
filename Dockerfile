FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
ARG APP_UID=1000
ARG APP_GID=1000
RUN mkdir -p /app && chown $APP_UID:$APP_GID /app
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["ClickerWeb.csproj", "./"]
RUN dotnet restore "ClickerWeb.csproj"
COPY . .
WORKDIR "/src/"
RUN dotnet build "ClickerWeb.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "ClickerWeb.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
COPY Resources /app/Resources
ENTRYPOINT ["dotnet", "ClickerWeb.dll"]
