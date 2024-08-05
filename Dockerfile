# Usa la imagen base de .NET SDK para construir la aplicación
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia los archivos de proyecto y restaura las dependencias
COPY ["src/Bext.Reps.Api/Bext.Reps.Api.csproj", "src/Bext.Reps.Api/"]
COPY ["src/Bext.Reps.Business/Bext.Reps.Business.csproj", "src/Bext.Reps.Business/"]
COPY ["src/Bext.Reps.Domain/Bext.Reps.Domain.csproj", "src/Bext.Reps.Domain/"]
COPY ["src/Bext.Reps.Infrastructure/Bext.Reps.Infrastructure.csproj", "src/Bext.Reps.Infrastructure/"]
RUN dotnet restore "src/Bext.Reps.Api/Bext.Reps.Api.csproj"

# Copia el resto de los archivos y construye el proyecto
COPY . .
WORKDIR "/src/src/Bext.Reps.Api"
RUN dotnet build "Bext.Reps.Api.csproj" -c Release -o /app/build

# Publica la aplicación
FROM build AS publish
RUN dotnet publish "Bext.Reps.Api.csproj" -c Release -o /app/publish

# Usa la imagen base de ASP.NET para ejecutar la aplicación
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
WORKDIR /app

# Copia los archivos publicados desde el contenedor de construcción
COPY ["src/sisprodesa", "."]
COPY --from=publish /app/publish .

# Configurar la variable de entorno para el entorno de desarrollo
ENV ASPNETCORE_ENVIRONMENT=Development
ENV ASPNETCORE_URLS=http://+:9000

# Exponer el puerto 5000
EXPOSE 9000

# Establecer el punto de entrada
ENTRYPOINT ["dotnet", "Bext.Reps.Api.dll"]
