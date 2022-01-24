#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
RUN groupadd -r user -g 443 && \
    useradd -u 101 -r -g user newuser
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["backend/backend.csproj", "backend/"]
RUN dotnet restore "backend/backend.csproj"

WORKDIR "/src/backend"
COPY "/backend" .
RUN dotnet build "backend.csproj" -c Release -o /app/build

FROM build AS publish
ENV PATH $PATH:/root/.dotnet/tools
RUN dotnet tool install -g dotnet-ef && \
    dotnet publish "backend.csproj" -c Release -o /app/publish && \
    dotnet-ef migrations script -p "backend.csproj" -o /app/scripts/init.sql

FROM base AS final
USER newuser
WORKDIR /app
COPY --from=publish /app .
WORKDIR /app/publish
VOLUME /app/scripts
ENTRYPOINT ["dotnet", "backend.dll"]