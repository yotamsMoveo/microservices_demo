#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/core/sdk:5.0 
WORKDIR /webapi_yotam_sln
COPY . .
ENTRYPOINT ["dotnet","run"]


FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["webapi_yotam/webapi_yotam.csproj", "webapi_yotam/"]
RUN dotnet restore "webapi_yotam/webapi_yotam.csproj"
COPY . .
WORKDIR "/src/webapi_yotam"
RUN dotnet build "webapi_yotam.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "webapi_yotam.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "webapi_yotam.dll"]
