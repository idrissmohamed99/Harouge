FROM mcr.microsoft.com/dotnet/core/aspnet:3.1-buster-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:3.1-buster AS build
WORKDIR /src
COPY ["PathologySystemAPI/HarougeAPI.csproj", "PathologySystemAPI/"]
COPY ["Infra/Infra.csproj", "Infra/"]
COPY ["Repository/Repository.csproj", "Repository/"]
COPY ["Services/Services.csproj", "Services/"]
COPY ["UnitOfWork/UnitOfWork.csproj", "UnitOfWork/"]
RUN dotnet restore "PathologySystemAPI/HarougeAPI.csproj"
COPY . .
WORKDIR "/src/PathologySystemAPI"
RUN dotnet build "HarougeAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN apt-get update -yq \
    && apt-get install curl gnupg -yq \
    && curl -sL https://deb.nodesource.com/setup_14.x | bash \
    && apt-get install nodejs -yq
RUN dotnet publish "HarougeAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HarougeAPI.dll"]
