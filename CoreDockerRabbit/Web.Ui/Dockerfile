#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:5.0.202 AS build
WORKDIR /src
COPY ["Web.UI/Web.Ui.csproj", "Web.UI/"]
COPY ["Web.Common/Web.Common.csproj", "Web.Common/"]

RUN dotnet restore "Web.UI/Web.Ui.csproj"
COPY . .
WORKDIR "/src/Web.UI"
RUN dotnet build "Web.Ui.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Web.Ui.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Web.Ui.dll"]