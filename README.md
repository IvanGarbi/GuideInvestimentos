# GuideInvestimentos

# API REST - Documentação Swagger
CRUD de ações da bolsa com informações consumidas da API do Yahoo e retorno de varição do preço da ação por pregão. 
<br>

# Modo de usar:
* Inserir nos campos o nome da ação desejada. Por exemplo: wege3, mglu3.
* No POST a ação é salva no banco de dados.
* No GET é devolvido a ação com os cálculos de variação de preço de cada pregão.
* Obs: não é preciso adicionar .SA nas ações.

# Tecnologias
* .NET 7
* EntityFramework
* FluentValidation
* Moq
* SQL Server
* AutoMapper
* xUnit.NET
