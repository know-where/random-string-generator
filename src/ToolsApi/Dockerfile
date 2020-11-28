FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim AS build
WORKDIR /src
COPY ["ToolsApi/ToolsApi.csproj", "ToolsApi/"]
RUN dotnet restore "ToolsApi/ToolsApi.csproj"
COPY . .
WORKDIR "/src/ToolsApi/"
RUN dotnet build "ToolsApi.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ToolsApi.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ToolsApi.dll"]
