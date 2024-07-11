ENV ASPNETCORE_URLS=http://+:5000
EXPOSE 5000

FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS build
WORKDIR /src
COPY ["src/Bext.Reps.Api/Bext.Reps.Api.csproj", "src/Bext.Reps.Api/"]
COPY ["src/Bext.Reps.Business/Bext.Reps.Business.csproj", "src/Bext.Reps.Business/"]
COPY ["src/Bext.Reps.Domain/Bext.Reps.Domain.csproj", "src/Bext.Reps.Domain/"]
COPY ["src/Bext.Reps.Infrastructure/Bext.Reps.Infrastructure.csproj", "src/Bext.Reps.Infrastructure/"]
RUN dotnet restore "src/WebUI/WebUI.csproj"
COPY . .
WORKDIR "/src/src/Bext.Reps.Api"
RUN dotnet build "Bext.Reps.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "Bext.Reps.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "Bext.Reps.Api.dll"]