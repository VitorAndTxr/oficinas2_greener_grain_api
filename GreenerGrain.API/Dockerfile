FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS base

# uncomment if you need to deploy a service account JSON file to access Google Cloud resources
#ARG GOOGLE_APPLICATION_CREDENTIALS_FILE
ARG ASPNETCORE_ENVIRONMENT

# uncomment if you need to deploy a service account JSON file
#ENV GOOGLE_APPLICATION_CREDENTIALS="/app/$GOOGLE_APPLICATION_CREDENTIALS_FILE"
ENV ASPNETCORE_ENVIRONMENT=$ASPNETCORE_ENVIRONMENT

# uncomment if you need to deploy a service account JSON file
#COPY "keys/$GOOGLE_APPLICATION_CREDENTIALS_FILE" "/app/"

WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR /src
# nuget source - restore pacotes
COPY ["nuget.config", "."] 
COPY ["Google.VirtualScheduling.API.sln", "./"]
COPY ["Google.VirtualScheduling.API/Google.VirtualScheduling.API.csproj", "./Google.VirtualScheduling.API/"]
COPY ["Google.VirtualScheduling.Service/Google.VirtualScheduling.Service.csproj", "./Google.VirtualScheduling.Service/"]
COPY ["Google.VirtualScheduling.Data/Google.VirtualScheduling.Data.csproj", "./Google.VirtualScheduling.Data/"]
COPY ["Google.VirtualScheduling.Domain/Google.VirtualScheduling.Domain.csproj", "./Google.VirtualScheduling.Domain/"]
COPY ["Google.VirtualScheduling.Provider/Google.VirtualScheduling.Provider.csproj", "./Google.VirtualScheduling.Provider/"]
COPY ["Google.VirtualScheduling.CrossCutting/Google.VirtualScheduling.CrossCutting.csproj", "./Google.VirtualScheduling.CrossCutting/"]

RUN dotnet restore "Google.VirtualScheduling.API/Google.VirtualScheduling.API.csproj"
COPY . .

WORKDIR /src/Google.VirtualScheduling.Domain
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Google.VirtualScheduling.Provider
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Google.VirtualScheduling.Data
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Google.VirtualScheduling.Service
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Google.VirtualScheduling.CrossCutting
RUN dotnet build -c Release -o /app/build

WORKDIR /src/Google.VirtualScheduling.API
RUN dotnet build -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Google.VirtualScheduling.API.csproj" -c Release -o /app/publish

FROM base AS final
ENV ASPNETCORE_URLS=http://*:${PORT}
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Google.VirtualScheduling.API.dll"]