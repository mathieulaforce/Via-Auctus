FROM mcr.microsoft.com/dotnet/runtime:8.0 AS base
USER $APP_UID
WORKDIR /app

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["LaMa.Via.Auctus.Console/LaMa.Via.Auctus.Console.csproj", "LaMa.Via.Auctus.Console/"]
RUN dotnet restore "LaMa.Via.Auctus.Console/LaMa.Via.Auctus.Console.csproj"
COPY . .
WORKDIR "/src/LaMa.Via.Auctus.Console"
RUN dotnet build "LaMa.Via.Auctus.Console.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "LaMa.Via.Auctus.Console.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LaMa.Via.Auctus.Console.dll"]
