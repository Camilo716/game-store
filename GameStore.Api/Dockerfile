FROM mcr.microsoft.com/dotnet/sdk:8.0 AS development

#install debugger for NET Core
# RUN apt update
# RUN apt install -y unzip
RUN curl -sSL https://aka.ms/getvsdbgsh | /bin/sh /dev/stdin -v latest -l ~/vsdbg

RUN mkdir /work/
WORKDIR /work/
EXPOSE 5080

COPY . .

RUN dotnet restore

WORKDIR /work/GameStore.Api/
RUN mkdir /out/
RUN dotnet publish GameStore.Api.csproj --no-restore --output /out/ --configuration Release

ENTRYPOINT ["dotnet", "watch", "run" ]
