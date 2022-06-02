FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["LetsKube/LetsKube.csproj", "LetsKube/"]
RUN dotnet restore "LetsKube/LetsKube.csproj"
COPY . .
WORKDIR "/src/LetsKube"
RUN dotnet build "LetsKube.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "LetsKube.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "LetsKube.dll"]
