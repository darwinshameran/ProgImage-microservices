FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5006
EXPOSE 5007

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/rotate/ProgImage.Rotate/ProgImage.Rotate.csproj", "src/rotate/ProgImage.Rotate/"]
RUN dotnet restore "src/rotate/ProgImage.Rotate/ProgImage.Rotate.csproj"
COPY . .    
WORKDIR "/src"
RUN dotnet build "src/rotate/ProgImage.Rotate/ProgImage.Rotate.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/rotate/ProgImage.Rotate/ProgImage.Rotate.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Rotate.dll"]