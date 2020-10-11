FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5003
EXPOSE 5004

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/resize/ProgImage.Resize/ProgImage.Resize.csproj", "src/resize/ProgImage.Resize/"]
RUN dotnet restore "src/resize/ProgImage.Resize/ProgImage.Resize.csproj"
COPY . .
WORKDIR "/src"
RUN dotnet build "src/resize/ProgImage.Resize/ProgImage.Resize.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/resize/ProgImage.Resize/ProgImage.Resize.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Resize.dll"]