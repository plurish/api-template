#See https://aka.ms/customizecontainer to learn how to customize your debug container and how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release

WORKDIR /src
COPY ["./src/Plurish.Template.Api/Plurish.Template.Api.csproj", "./Plurish.Template.Api/"]
COPY ["./src/Plurish.Template.Application/Plurish.Template.Application.csproj", "./Plurish.Template.Application/"]
COPY ["./src/Plurish.Template.Domain/Plurish.Template.Domain.csproj", "./Plurish.Template.Domain/"]
COPY ["./src/Plurish.Template.Infra/Plurish.Template.Infra.csproj", "./Plurish.Template.Infra/"]
COPY ["./src/Plurish.Common/Plurish.Common.csproj" "./Plurish.Common/"]
COPY ./Directory.Build.props ./
COPY ./.editorconfig ./

RUN dotnet restore "./Plurish.Template.Api/Plurish.Template.Api.csproj"

COPY ./src/ ./

WORKDIR "/src/Plurish.Template.Api"

RUN dotnet build "./Plurish.Template.Api.csproj" -c $BUILD_CONFIGURATION -o /app/build

# TODO: adicionar dotnet test

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
ARG KEYVAULT_URL

ENV KeyVault__Url=$KEYVAULT_URL

RUN dotnet publish "./Plurish.Template.Api.csproj" \
    -c $BUILD_CONFIGURATION \
    -o /app/publish /p:UseAppHost=false

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS final

EXPOSE 8080
EXPOSE 8081

USER app
WORKDIR /app

COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Plurish.Template.Api.dll"]