#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Maplr.SuggarShack.Api/Maplr.SuggarShack.Api.csproj", "Maplr.SuggarShack.Api/"]
RUN dotnet restore "Maplr.SuggarShack.Api/Maplr.SuggarShack.Api.csproj"
COPY . .
WORKDIR "/src/Maplr.SuggarShack.Api"
RUN dotnet build "Maplr.SuggarShack.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Maplr.SuggarShack.Api.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Maplr.SuggarShack.Api.dll"]