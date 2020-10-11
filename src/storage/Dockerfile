FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 8000
EXPOSE 8081

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/storage/ProgImage.Storage/ProgImage.Storage.csproj", "src/storage/ProgImage.Storage/"]
COPY ["src/storage/ProgImage.Tests/ProgImage.Tests.csproj", "ProgImage.Tests/"]
RUN dotnet restore "src/storage/ProgImage.Storage/ProgImage.Storage.csproj"
COPY . .
RUN dotnet build "src/storage/ProgImage.Storage/ProgImage.Storage.csproj" -c Release -o /app

FROM build AS testrunner
COPY src/storage/ProgImage.Tests/. .
ENTRYPOINT ["dotnet", "test"]
CMD ["--logger:trx", "/p:CollectCoverage=true", "/p:CoverletOutputFormat=cobertura", "/p:CoverletOutput=./TestResults/"]

FROM build AS publish
RUN dotnet publish "src/storage/ProgImage.Storage/ProgImage.Storage.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Storage.dll"]