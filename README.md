# Cash Flow

### 1 - Documentação do Projeto

A documentação do projeto está na pasta __doc__ no arquivo __document.md__.

### 2 - Como Rodar o Projeto?

Para executar o projeto na máquina, basta entrar na pasta __src/API__, rodar os comandos __dotnet build__, __dotnet run__ e quando o projeto estiver rodando, acessar o link __http://localhost:5290/swagger__ ou  __http://localhost:5290/api-docs__ (porém, no segundo link não será possível executar os endpoints via web).

### 3 - Pontos de Melhoria

- Versionamento dos endpoints
- Criar mais tests unitários
- Implementar o uso do CorrelationId nas requisições para poder ter todo o rastreio da requisição em uma análise
- Implementar a paginação no endpoint GetAll
- Caso necessário, podemos implementar o Event Sourcing para ter toda linha do tempo dos dados
- O método EntryIsNull nas classes UpdateEntryHanlder e DeleteEntryHandler pode estar centralizado em uma __CommonValidation__
- Criar os diagramas de sequência ou caso de uso dos outros endpoints