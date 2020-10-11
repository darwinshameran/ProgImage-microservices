FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5012
EXPOSE 5013

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/blur/ProgImage.Blur/ProgImage.Blur.csproj", "src/blur/ProgImage.Blur/"]
RUN dotnet restore "src/blur/ProgImage.Blur/ProgImage.Blur.csproj"
COPY . .    
WORKDIR "/src"
RUN dotnet build "src/blur/ProgImage.Blur/ProgImage.Blur.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/blur/ProgImage.Blur/ProgImage.Blur.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Blur.dll"]