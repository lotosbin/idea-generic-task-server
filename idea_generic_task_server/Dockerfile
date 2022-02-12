FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
COPY ["idea_generic_task_server/idea_generic_task_server.csproj", "idea_generic_task_server/"]
RUN dotnet restore "idea_generic_task_serveridea_generic_task_server.csproj"
COPY . .
WORKDIR "/src/idea_generic_task_server"
RUN dotnet build "idea_generic_task_server.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "idea_generic_task_server.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "idea_generic_task_server.dll"]
