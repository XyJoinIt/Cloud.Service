FROM mcr.microsoft.com/dotnet/aspnet:7.0 AS base

ARG agree
ARG ip
ARG port
ARG weight

ENV TZ=Asia/Shanghai
ENV agree=${agree}
ENV ip=${ip}
ENV port=${port}
ENV weight=${weight}

WORKDIR /app
EXPOSE 80

COPY . /app
ENTRYPOINT ["dotnet", "Cloud.Platform.Web.dll"]