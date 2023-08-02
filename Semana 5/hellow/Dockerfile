#Base da Imagem
FROM mcr.microsoft.com/dotnet/sdk:6.0 AS build

#Configurar o diretório de trabalho do Container, não é padrão o /app, mas e costume usar, cria a pasta root interna da imagem
WORKDIR /app   

#Migração dos arquivos da app para a imagem, o . é igual a /app ou primeiro nível
COPY hellow.csproj .

#verifica as dependencias do csproj
RUN dotnet restore

#atualiza as dependecias
COPY . /app
#faz o build da imagem
RUN dotnet publish -c release -o out

#Camada de Runtime
FROM mcr.microsoft.com/dotnet/aspnet:6.0 AS runtime

#Configurar o diretorio de trabalho do container
WORKDIR /app

#transferêrencia da build para o ambiente de runtime
COPY --from=build /app/out .

#Entrypoint do nosso app
Entrypoint ["dotnet", "hellow.dll"]