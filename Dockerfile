FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build-env
WORKDIR /app

RUN dotnet --version

COPY ./src/Company.Finance.Api/Company.Finance.Api.csproj ./
RUN dotnet restore

COPY ./src/Company.Finance.Api ./
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine
WORKDIR /app
COPY --from=build-env /app/out .

EXPOSE 5002

ENTRYPOINT ["dotnet", "Company.Finance.Api.dll"]