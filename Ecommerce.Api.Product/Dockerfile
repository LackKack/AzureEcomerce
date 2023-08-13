#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api.Product/Ecommerce.Api.Products.csproj", "Ecommerce.Api.Product/"]
RUN dotnet restore "Ecommerce.Api.Product/Ecommerce.Api.Products.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api.Product"
RUN dotnet build "Ecommerce.Api.Products.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.Products.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.Products.dll"]