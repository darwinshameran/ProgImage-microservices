FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5002
EXPOSE 5003

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/thumbnail/ProgImage.Thumbnail/ProgImage.Thumbnail.csproj", "src/thumbnail/ProgImage.Thumbnail/"]
RUN dotnet restore "src/thumbnail/ProgImage.Thumbnail/ProgImage.Thumbnail.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "src/thumbnail/ProgImage.Thumbnail/ProgImage.Thumbnail.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/thumbnail/ProgImage.Thumbnail/ProgImage.Thumbnail.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Thumbnail.dll"]