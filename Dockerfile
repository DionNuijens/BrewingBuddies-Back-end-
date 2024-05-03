FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app 

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["BrewingBuddies/BrewingBuddies.csproj", "BrewingBuddies/"]
COPY ["BrewingBuddies_DataService/BrewingBuddies_DataService.csproj", "BrewingBuddies_DataService/"]
COPY ["./BrewingBuddies/appsettings.json", "BrewingBuddies/"]
COPY ["BrewingBuddies-Entitys/BrewingBuddies-Entitys.csproj", "BrewingBuddies-Entitys/"]
COPY ["BrewingBuddies_RiotClient/BrewingBuddies_RiotClient.csproj", "BrewingBuddies_RiotClient/"]
RUN dotnet restore "BrewingBuddies/BrewingBuddies.csproj"
COPY . .
WORKDIR "/src/BrewingBuddies"
RUN dotnet build "BrewingBuddies.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "BrewingBuddies.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM base AS final 
WORKDIR /app
COPY --from=publish /app/publish .

EXPOSE 5000 

ENTRYPOINT [ "dotnet", "BrewingBuddies.dll" ]
