# Inventário

Este é um projeto ASP.NET 8.0 que pode ser executado utilizando Docker Compose ou diretamente em sua máquina local.

## Pré-requisitos

O Projeto é preparado para rodar em ambiente Windows e Linux

### Para rodar em Docker:

- [Docker](https://www.docker.com/get-started)
- [Docker Compose](https://docs.docker.com/compose/install/)
  - Em ambiente Windows, utilizei o Docker Desktop com WSL

### Para rodar na máquina local:

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [MySQL](https://www.mysql.com/products/community/)
  - Ao instalar, crie o banco `InventoryVenturusDb`

## Passo a passo

### Rodar com Docker Compose

#### 1. Clonar o repositório

Clone este repositório para sua máquina local:

```sh
git clone https://github.com/miguelkmarques/Inventory.git
cd Inventory/
```

#### 2. Construir e iniciar os containers

No diretório raiz do projeto, execute os comandos abaixo para rodar o Build e iniciar os Containers:

```sh
docker-compose -f docker-compose.yml -f docker-compose.override.yml build
docker-compose up -d
```

#### 3. Acessar a aplicação

- Após o Build e inicialização, a aplicação estará disponível em `http://localhost:8080`.
- O Swagger da aplicação está disponível em `http://localhost:8080/swagger/index.html`

#### 4. Parar e remover os containers

Para parar e remover os containers, execute:

```sh
docker-compose down
```

### Rodar sem Docker Compose

#### 1. Clonar o repositório

Clone este repositório para sua máquina local:

```sh
git clone https://github.com/miguelkmarques/Inventory.git
cd Inventory/InventoryVenturus/
```

#### 2. Configurar a conexão com MySQL

Certifique-se de que o MySQL está instalado e em execução. Atualize a string de conexão no arquivo `appsettings.Development.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Database=InventoryVenturusDb;User=user;Password=password"
}
```

#### 3. Restaurar pacotes

No diretório InventoryVenturus onde está o `InventoryVenturus.csproj`, execute os seguintes comandos:

```sh
dotnet restore
dotnet build
dotnet run
```

#### 4. Acessar a aplicação

- A aplicação estará disponível em `http://localhost:5129`.
- O Swagger da aplicação está disponível em `http://localhost:5129/swagger/index.html`

#### 5. Rodar Testes Unitários

Volte para a pasta raiz do projeto e execute o comando:

```sh
dotnet test InventoryVenturus.Tests/InventoryVenturus.Tests.csproj
```

## GitHub Action

O projeto possui um workflow do GitHub Actions que executa os testes unitários e gera relatórios de testes e cobertura. Aqui estão os links para a última execução do workflow e seus respectivos relatórios:

- [Última execução](https://github.com/miguelkmarques/Inventory/actions/runs/12341995863)
- [Relatório de Unit Tests](https://github.com/miguelkmarques/Inventory/actions/runs/12341995863/job/34441319693)
- [Relatório de Cobertura](https://github.com/miguelkmarques/Inventory/actions/runs/12341995863/artifacts/2323353250)
  - Para visualizar o relatório de cobertura, extraia os arquivos do .zip e abra o arquivo `index.html` no navegador.

## Decisões Técnicas

### Desenvolvimento do Projeto de Controle de Estoque

- Para o desenvolvimento do Projeto de Controle de Estoque, foi utilizada a biblioteca MediatR para seguir o padrão de arquitetura CQRS. Cada funcionalidade (`Product`, `Stock`, `Transaction`) foi separada em `Commands`, `Dtos`, `Notifications` e `Queries`.
- Foram desenvolvidos Behaviors para serem adicionados na pipeline de execução do MediatR:
  - `ValidationBehavior`: para cada `Command` e `Notification`, o input é validado para garantir a integridade dos dados, usando a biblioteca `FluentValidation`.
  - `RequestResponseLoggingBehavior`: para cada `Command`, é realizado o log da `Request` e da `Response`.
  - `ExceptionLoggingBehavior`: cada `Exception` gerada dentro do MediatR é registrada no banco de dados, juntamente com a `Request` e a `Exception`.
- Foi implementada uma `BaseException` e exceções customizadas para tratar erros originados na lógica da aplicação.
- No middleware do AspNet, foi adicionada uma `GlobalExceptionHandler` para centralizar o tratamento de exceções e padronizar o retorno dos erros para requisições HTTP que não tiveram sucesso. Esse handler incrementa os dados do erro quando se trata de `FluentValidation.ValidationException` e `BaseException`.
- Foi utilizado o Micro ORM Dapper e criada uma pasta de `Repository` para todas as operações de banco de dados.
- Para os Commands que necessitam realizar mais de uma operação, como executar a operação principal e publicar uma notificação que pode desencadear outra operação usando um repositório, foi utilizado o `TransactionScope`. Caso alguma operação falhe dentro do escopo criado, as operações que anteriormente tiveram sucesso são revertidas. Exemplo: ao consumir o estoque de um produto, a operação de atualizar o estoque no repositório é executada e é enviada uma notificação para criar um registro na tabela de `Transactions`. Se a operação de salvar a `Transaction` falhar, o estoque do produto não será atualizado devido ao erro. Isso garante a integridade dos dados.
- Cada componente de código da aplicação foi desenvolvido com foco na testabilidade, facilitando o desenvolvimento de testes unitários e seguindo o padrão TDD.
- Para o escopo deste desafio, os endpoints e as tabelas de banco de dados foram simplificados, apenas para demonstrar a aplicação dos conceitos e tecnologias, e não representam um sistema real.
