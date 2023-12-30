FROM mcr.microsoft.com/dotnet/sdk:5.0-buster-slim as build
WORKDIR /app
EXPOSE 80
COPY Shared/FreeEducation.Shared/*.csproj Shared/FreeEducation.Shared/
COPY Services/FakePayment/FreeEducation.Services.FakePayment/*.csproj Services/FakePayment/FreeEducation.Services.FakePayment/
RUN dotnet restore Services/FakePayment/FreeEducation.Services.FakePayment/*.csproj
COPY . .
RUN dotnet publish Services/FakePayment/FreeEducation.Services.FakePayment/*.csproj -c Release -o out
FROM mcr.microsoft.com/dotnet/aspnet:5.0-buster-slim as runtime
WORKDIR /app
COPY --from=build /app/out .
ENTRYPOINT [ "dotnet","FreeEducation.Services.FakePayment.dll" ]