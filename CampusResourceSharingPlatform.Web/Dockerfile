#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["CampusResourceSharingPlatform.Web/CampusResourceSharingPlatform.Web.csproj", "CampusResourceSharingPlatform.Web/"]
COPY ["CampusResourceSharingPlatform.Interface/CampusResourceSharingPlatform.Interface.csproj", "CampusResourceSharingPlatform.Interface/"]
COPY ["CampusResourceSharingPlatform.Model/CampusResourceSharingPlatform.Model.csproj", "CampusResourceSharingPlatform.Model/"]
COPY ["CampusResourceSharingPlatform.Data/CampusResourceSharingPlatform.Data.csproj", "CampusResourceSharingPlatform.Data/"]
COPY ["CampusResourceSharingPlatform.Service/CampusResourceSharingPlatform.Service.csproj", "CampusResourceSharingPlatform.Service/"]
RUN dotnet restore "CampusResourceSharingPlatform.Web/CampusResourceSharingPlatform.Web.csproj"
COPY . .
WORKDIR "/src/CampusResourceSharingPlatform.Web"
RUN dotnet build "CampusResourceSharingPlatform.Web.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CampusResourceSharingPlatform.Web.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CampusResourceSharingPlatform.Web.dll"]