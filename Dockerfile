FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build
WORKDIR preferenceservice

EXPOSE 7175
EXPOSE 80

WORKDIR /src
COPY ["PreferencesMicroService/PreferencesMicroService.csproj", "PreferencesMicroService/"]
COPY ["Application/Application.csproj", "Application/"]
COPY ["Infrastructure/Infrastructure.csproj", "Infrastructure/"]
COPY ["Domain/Domain.csproj", "Domain/"]

RUN dotnet restore "./PreferencesMicroService/PreferencesMicroService.csproj"

COPY . .
WORKDIR "/src/PreferencesMicroService"
RUN dotnet build "PreferencesMicroService.csproj" -c Release -o /preferenceservice/build

FROM build AS publish
RUN dotnet publish "PreferencesMicroService.csproj" -c Release -o /preferenceservice/publish

FROM mcr.microsoft.com/dotnet/sdk:6.0
WORKDIR /preferenceservice
COPY --from=publish /preferenceservice/publish .
ENTRYPOINT ["dotnet", "PreferencesMicroService.dll"]