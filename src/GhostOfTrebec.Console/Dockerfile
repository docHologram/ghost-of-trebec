FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS source
COPY . .

# Need to figure out nuget package restore stuff
ARG NUGET_ACCESSTOKEN
RUN dotnet nuget update source WhateverMySourceHappensToBe -u "docker" -p "${NUGET_ACCESSTOKEN}" --store-password-in-clear-text --valid-authentication-types "basic"
RUN dotnet restore

FROM restore as build
RUN dotnet build -c Release --no-restore

# FROM build as test
RUN dotnet tool install --global dotnet-reportgenerator-globaltool
RUN dotnet test -c Release --logger "trx;LogFileName=testresults.trx" --no-build /p:CollectCoverage=true /p:CoverletOutputFormat=cobertura  /p:Exclude=\"[GhostOfTrebec.Data]GhostOfTrebec.Data.Migrations.*,[*.Tests]*\"
RUN $HOME/.dotnet/tools/reportgenerator -reports:src/**/coverage.cobertura.xml -targetdir:CoverageResults -reporttypes:"HtmlInline_AzurePipelines;Cobertura"

FROM build as migrations
RUN dotnet tool install --global dotnet-ef --version 5.0.0-rc.2.20475.6
RUN $HOME/.dotnet/tools/dotnet-ef migrations script --project src/GhostOfTrebec.Console --output /migrations/migrations.sql --verbose --idempotent --no-build --configuration Release

FROM build as publish
RUN dotnet publish src/GhostOfTrebec.Console -c Release -o /app/publish --no-build

FROM base as final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "ShipmentManagement.Web.dll"]
