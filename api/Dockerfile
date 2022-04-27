# syntax=docker/dockerfile:1
FROM mcr.microsoft.com/dotnet/aspnet:6.0
COPY keep-api/bin/Release/net6.0/publish/ App/
WORKDIR /App
ENTRYPOINT ["dotnet", "KeepApi.dll"]

# docker build -t keep-api .
# docker run -it -p 5051:80 --name keep-api
# docker rm keep-api
