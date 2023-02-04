#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.
FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base
ENV TZ=Asia/Shanghai
WORKDIR /app
EXPOSE 80

COPY . /app
ENTRYPOINT ["dotnet", "Cloud.Ocelot.Web.dll"]