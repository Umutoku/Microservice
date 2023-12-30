FROM mcr.microsoft.com/dotnet/sdk:3.1 as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeEducation.Shared/*.csproj Shared/FreeEducation.Shared/
COPY IdentityServer/FreeEducation.IdentityServer/*.csproj IdentityServer/FreeEducation.IdentityServer
RUN dotnet restore IdentityServer/FreeEducation.IdentityServer/*.csproj
COPY . .
RUN dotnet publish IdentityServer/FreeEducation.IdentityServer/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:3.1 as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeEducation.IdentityServer.dll" ]