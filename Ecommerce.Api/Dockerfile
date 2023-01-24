FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["Ecommerce.Api/Ecommerce.Api.csproj", "Ecommerce.Api/"]
RUN dotnet restore "Ecommerce.Api/Ecommerce.Api.csproj"
COPY . .
WORKDIR "/src/Ecommerce.Api"
RUN dotnet build "Ecommerce.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Ecommerce.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Ecommerce.Api.dll"]
