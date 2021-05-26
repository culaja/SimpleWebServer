FROM mcr.microsoft.com/dotnet/sdk:5.0-alpine AS build
WORKDIR /app

COPY . ./

RUN dotnet restore
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0-alpine AS runtime
WORKDIR /app

COPY --from=build /app/out /app/

ENV ASPNETCORE_URLS=http://+:5000 
EXPOSE 5000
ENTRYPOINT ["dotnet", "WebApplication.dll"]
