FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 5008
EXPOSE 5009

FROM mcr.microsoft.com/dotnet/core/sdk:3.1 AS build
WORKDIR /src
COPY ["src/mask/ProgImage.Mask/ProgImage.Mask.csproj", "src/mask/ProgImage.Mask/"]
RUN dotnet restore "src/mask/ProgImage.Mask/ProgImage.Mask.csproj"
COPY . .    
WORKDIR "/src"
RUN dotnet build "src/mask/ProgImage.Mask/ProgImage.Mask.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "src/mask/ProgImage.Mask/ProgImage.Mask.csproj" -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "ProgImage.Mask.dll"]