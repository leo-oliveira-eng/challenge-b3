FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build

ARG BUILD_CONFIGURATION=Release

WORKDIR /src

COPY ["src/Api/Api.csproj", "src/Api/"]
COPY ["src/Application/Application.csproj", "src/Application/"]
COPY ["src/DataContracts/DataContracts.csproj", "src/DataContracts/"]
COPY ["src/Domain/Domain.csproj", "src/Domain/"]
COPY ["src/Infra.CrossCutting/Infra.CrossCutting.csproj", "src/Infra.CrossCutting/"]
COPY ["src/Infra.Persistence/Infra.Persistence.csproj", "src/Infra.Persistence/"]

RUN dotnet restore "src/Api/Api.csproj"

COPY . .

WORKDIR "/src/src/Api"

RUN dotnet publish "Api.csproj" -c $BUILD_CONFIGURATION -o /app/publish -r linux-musl-x64 --self-contained

FROM mcr.microsoft.com/dotnet/runtime-deps:8.0-alpine AS final

WORKDIR /app

ENV DOTNET_SYSTEM_GLOBALIZATION_INVARIANT=false

RUN apk update --no-cache && \
    apk add --no-cache icu-libs

COPY --from=build /app/publish .

ENTRYPOINT ["/app/Api"]
