#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base
WORKDIR /app
ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS publish
WORKDIR /src
COPY ["RestfulApiDemo/RestfulApiDemo.csproj", "RestfulApiDemo/"]
RUN dotnet restore "RestfulApiDemo/RestfulApiDemo.csproj"
COPY . .
WORKDIR "/src/RestfulApiDemo"
RUN dotnet publish "RestfulApiDemo.csproj" -c Release -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "RestfulApiDemo.dll"]