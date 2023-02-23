#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /src
COPY ["src/Api/Presentation/ApartmentsBilling.ApiUI/ApartmentsBilling.ApiUI.csproj", "src/Api/Presentation/ApartmentsBilling.ApiUI/"]
COPY ["src/Projections/ApartmentsBilling.Cache/ApartmentsBilling.Cache.csproj", "src/Projections/ApartmentsBilling.Cache/"]
COPY ["src/Api/Bussines/ApartmentsBilling.BussinessLayer/ApartmentsBilling.BussinessLayer.csproj", "src/Api/Bussines/ApartmentsBilling.BussinessLayer/"]
COPY ["src/Common/ApartmentsBilling.Common/ApartmentsBilling.Common.csproj", "src/Common/ApartmentsBilling.Common/"]
COPY ["src/Api/Core/ApartmentsBilling.Entity/ApartmentsBilling.Entity.csproj", "src/Api/Core/ApartmentsBilling.Entity/"]
COPY ["src/Projections/ApartmentsBilling.BacGroundJobs/ApartmentsBilling.BackGroundJobs.csproj", "src/Projections/ApartmentsBilling.BacGroundJobs/"]
COPY ["src/Api/Core/ApartmentsBilling.DataAccesLayer/ApartmentsBilling.DataAccesLayer.csproj", "src/Api/Core/ApartmentsBilling.DataAccesLayer/"]
RUN dotnet restore "src/Api/Presentation/ApartmentsBilling.ApiUI/ApartmentsBilling.ApiUI.csproj"
COPY . .
WORKDIR "/src/src/Api/Presentation/ApartmentsBilling.ApiUI"
RUN dotnet build "ApartmentsBilling.ApiUI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "ApartmentsBilling.ApiUI.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ApartmentsBilling.ApiUI.dll"]