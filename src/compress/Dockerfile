FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5010
EXPOSE 5011

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/compress/ProgImage.Compress/ProgImage.Compress.csproj", "src/compress/ProgImage.Compress/"]
RUN dotnet restore "src/compress/ProgImage.Compress/ProgImage.Compress.csproj"
COPY . .    
WORKDIR "/src"
RUN dotnet build "src/compress/ProgImage.Compress/ProgImage.Compress.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/compress/ProgImage.Compress/ProgImage.Compress.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Compress.dll"]