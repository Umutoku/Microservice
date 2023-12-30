FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeEducation.Shared/*.csproj Shared/FreeEducation.Shared/

COPY Services/Order/FreeEducation.Services.Order.Application/*.csproj Services/Order/FreeEducation.Services.Order.Application/

COPY Services/Order/FreeEducation.Services.Order.Domain/*.csproj Services/Order/FreeEducation.Services.Order.Domain/

COPY Services/Order/FreeEducation.Services.Order.Domain.Core/*.csproj Services/Order/FreeEducation.Services.Order.Domain.Core/

COPY Services/Order/FreeEducation.Services.Order.Infrastructure/*.csproj Services/Order/FreeEducation.Services.Order.Infrastructure/

COPY Services/Order/FreeEducation.Services.Order.API/*.csproj Services/Order/FreeEducation.Services.Order.API/
RUN dotnet restore Services/Order/FreeEducation.Services.Order.API/*.csproj
COPY . .
RUN dotnet publish Services/Order/FreeEducation.Services.Order.API/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeEducation.Services.Order.API.dll" ]