﻿FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /app

RUN apt-get update
RUN apt-get -y install protobuf-compiler-grpc
RUN export PROTOBUF_PROTOC=/usr/bin/protoc

COPY . ./

RUN dotnet restore
RUN cp /usr/bin/protoc /root/.nuget/packages/grpc.tools/2.37.0/tools/linux_arm64/protoc
RUN dotnet publish -c Release -o out

FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS runtime
WORKDIR /app

COPY --from=build /app/out /app/

ENV ASPNETCORE_URLS=http://+:5001 
EXPOSE 5001
ENTRYPOINT ["dotnet", "GrpcService.dll"]
