FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /app

ENV ASPNETCORE_URLS=http://+:8010

EXPOSE 8010

# copy csproj and restore as distinct layers
COPY *.sln .
COPY GhostOfTrebec.Console/*.*.csproj ./GhostOfTrebec.Console/
COPY GhostOfTrebec.Core/*.*.csproj ./GhostOfTrebec.Core/
COPY GhostOfTrebec.Data/*.*.csproj ./GhostOfTrebec.Data/

# copy everything else and build app
COPY GhostOfTrebec.Console/. ./GhostOfTrebec.Console/
COPY GhostOfTrebec.Core/. ./GhostOfTrebec.Core/
COPY GhostOfTrebec.Data/. ./GhostOfTrebec.Data/

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
ENTRYPOINT ["dotnet", "GhostOfTrebec.Web.dll"]
