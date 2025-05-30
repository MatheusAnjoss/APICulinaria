# Receitas API
Desenvolvido por: Matheus Anjos, Karin Pereira, Marcelo Oliveira e Pedro Guilhermem.
Bem-vindo à **Receitas API**, uma aplicação desenvolvida para gerenciar receitas culinárias e suas possíveis alergias. Esta API foi projetada para facilitar o armazenamento, consulta e manipulação de dados relacionados a receitas, com foco em simplicidade e eficiência.

## Funcionalidades

- **Listar Receitas**: Obtenha todas as receitas cadastradas na base de dados.
- **Consultar Receita por ID**: Consulte os detalhes de uma receita específica.
- **Criar Receita**: Adicione novas receitas com informações detalhadas, incluindo alergias.
- **Excluir Receita**: Remova receitas existentes da base de dados.

## Tecnologias Utilizadas

- **ASP.NET Core**: Framework para desenvolvimento da API.
- **Entity Framework Core**: ORM para manipulação da base de dados.
- **SQLite**: Banco de dados leve e eficiente.
- **.NET 9.0**: Versão do framework utilizada.

## Rotas Principais

- `GET /api/receitas`: Retorna todas as receitas.
- `GET /api/receitas/{id}`: Retorna uma receita específica pelo ID.
- `POST /api/receitas`: Adiciona uma nova receita.
- `DELETE /api/receitas/{id}`: Remove uma receita pelo ID.

## Como Executar

1. Clone este repositório:
   ```bash
   git clone https://github.com/seu-usuario/seu-repositorio.git

2. Navegue até o diretório do projeto:
   ```bash
   cd ReceitasApi
   
3. Execute o projeto:
   ```bash
   dotnet run
