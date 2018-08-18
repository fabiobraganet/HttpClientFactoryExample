FROM microsoft/dotnet:2.1-aspnetcore-runtime AS base
WORKDIR /app
EXPOSE 65069
EXPOSE 44394

FROM microsoft/dotnet:2.1-sdk AS build
WORKDIR /src
COPY ../src/EndPointsExamples/EndPointsExamples.csproj ../src/EndPointsExamples/
RUN dotnet restore ../src/EndPointsExamples/EndPointsExamples.csproj
COPY . .
WORKDIR /src/../src/EndPointsExamples
RUN dotnet build EndPointsExamples.csproj -c Release -o /app

FROM build AS publish
RUN dotnet publish EndPointsExamples.csproj -c Release -o /app

FROM base AS final
WORKDIR /app
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "EndPointsExamples.dll"]
