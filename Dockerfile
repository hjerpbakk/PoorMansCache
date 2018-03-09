FROM microsoft/aspnetcore-build:2 AS builder
WORKDIR /source
COPY ./PoorMansCache.sln .

COPY ./PoorMansCache/*.csproj ./PoorMansCache/
RUN dotnet restore

COPY ./PoorMansCache ./PoorMansCache

RUN dotnet publish "./PoorMansCache/PoorMansCache.csproj" --output "../dist" --configuration Release --no-restore

FROM microsoft/aspnetcore:2
WORKDIR /app
COPY --from=builder /source/dist .
EXPOSE 5000
ENTRYPOINT ["dotnet", "PoorMansCache.dll"]