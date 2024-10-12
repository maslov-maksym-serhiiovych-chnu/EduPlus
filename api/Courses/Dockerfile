FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app
COPY PL/PL.csproj PL/
COPY BLL/BLL.csproj BLL/
COPY DAL/DAL.csproj DAL/
RUN dotnet restore PL/PL.csproj
COPY . .
WORKDIR PL
RUN dotnet build PL.csproj -c Release -o /app/build

FROM build AS publish
RUN dotnet publish PL.csproj -c Release -o /app/publish

FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "PL.dll"]