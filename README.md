# API de Gerenciamento de Funcionários

Essa é uma API de CRUD (Create, Read, Update, Delete) para gerenciamento de funcionários, desenvolvida com .NET Core 9.

## Funcionalidades

*   Criação de funcionários
*   Leitura de funcionários (por ID e lista)
*   Atualização de funcionários
*   Exclusão de funcionários
*   Lista Lideres
*   Autenticação e autorização com JWT

## Tecnologias Utilizadas

*   .NET Core 9
*   ASP.NET Core
*   Entity Framework Core
*   PostgreSQL
*   Swagger
*   JWT

## Instalação

1.  Clone o repositório: `git clone https://github.com/seu-repositorio/gerenciamento-funcionarios.git`
2.  Instale as dependências: `dotnet restore`
3.  Configure a conexão com o banco de dados em `appsettings.json`
4.  Execute a aplicação: `dotnet run`

## Uso

*   Acesse a API em `http://localhost:5200/swagger` para visualizar a documentação
*   Utilize as rotas abaixo para interagir com a API:

    *   `POST /funcionario` - Cria um novo funcionário
    *   `GET /funcionario` - Lista todos os funcionários
    *   `GET /funcionario/{id}` - Busca um funcionário por ID
    *   `PUT /funcionario/{id}` - Atualiza um funcionário
    *   `DELETE /funcionario/{id}` - Exclui um funcionário
    *   `GET /funcionario/lider-e-diretor` - Obtém todos os lideres e diretores

## Autenticação e Autorização

*   A API utiliza autenticação e autorização com JWT
*   Para utilizar as rotas protegidas, é necessário obter um token de acesso
*   O token de acesso pode ser obtido enviando uma requisição `POST /login` com as credenciais de usuário

