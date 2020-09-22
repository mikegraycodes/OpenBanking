#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["src/Account/Account.csproj", "src/Account/"]
COPY ["src/EventBus.RabbitMQ/EventBus.RabbitMQ.csproj", "src/EventBus.RabbitMQ/"]
COPY ["src/EventBus/EventBus.csproj", "src/EventBus/"]
RUN dotnet restore "src/Account/Account.csproj"
COPY . .
WORKDIR "/src/src/Account"
RUN dotnet build "Account.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Account.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Account.dll"]
