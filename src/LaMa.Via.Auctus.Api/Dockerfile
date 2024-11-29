FROM mcr.microsoft.com/dotnet/aspnet:9.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/sdk:9.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LaMa.Via.Auctus.Api/LaMa.Via.Auctus.Api.csproj", "LaMa.Via.Auctus.Api/"]
RUN dotnet restore "LaMa.Via.Auctus.Api/LaMa.Via.Auctus.Api.csproj"
COPY . .
WORKDIR "/src/LaMa.Via.Auctus.Api"
RUN dotnet build "LaMa.Via.Auctus.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LaMa.Via.Auctus.Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LaMa.Via.Auctus.Api.dll"]
