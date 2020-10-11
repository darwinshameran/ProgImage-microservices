FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5000
EXPOSE 5001

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/transformation/ProgImage.Transformation/ProgImage.Transformation.csproj", "src/transformation/ProgImage.Transformation/"]
RUN dotnet restore "src/transformation/ProgImage.Transformation/ProgImage.Transformation.csproj"
COPY . .    
WORKDIR "/src"
RUN dotnet build "src/transformation/ProgImage.Transformation/ProgImage.Transformation.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/transformation/ProgImage.Transformation/ProgImage.Transformation.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Transformation.dll"]