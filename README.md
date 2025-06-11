# Challenge B3 - Simulador de CDB

Projeto de avaliação para desenvolvedores — Diretoria de Desenvolvimento de Sistemas de Middle e Back Office  
Repositório público: [https://github.com/leo-oliveira-eng/challenge-b3](https://github.com/leo-oliveira-eng/challenge-b3)

---

## Objetivo

Avaliar a capacidade de análise e implementação de soluções seguindo princípios **SOLID**, testes unitários, boas práticas de performance e Clean Architecture.

---

## Desafio

O desafio consiste na implementação de:

1. **Web API (.NET 8):**  
   - Calcula o rendimento de um investimento em CDB, conforme as regras detalhadas no enunciado.
   - Possui cobertura de testes unitários superior a 90% na camada de lógica.
   - Exposta via Docker.

2. **Frontend Angular:**  
   - Tela web para informar o valor inicial do investimento e o prazo (em meses).
   - Exibe resultado bruto, líquido e simula todos os cálculos conforme especificação.
   - Desenvolvida com Angular Standalone Components (Angular CLI 17+), Bootstrap 5 e responsiva.
   - Exposta via Docker.

---

## Regras de Negócio

- **Fórmula do CDB:**  
  VF = VI × [1 + (CDI × TB)]<sup>n</sup>  
  Onde:  
  - **VF** = valor final  
  - **VI** = valor inicial  
  - **CDI** = 0,9% ao mês  
  - **TB** = 108%  
  - **n** = número de meses (rendimentos mensais são compostos mês a mês)

- **Tabela de Imposto de Renda:**  
  | Prazo               | Alíquota IR  |
  |---------------------|--------------|
  | Até 6 meses         | 22,5%        |
  | Até 12 meses        | 20%          |
  | Até 24 meses        | 17,5%        |
  | Acima de 24 meses   | 15%          |

---

## Como Executar Localmente

### Pré-requisitos

- Docker e Docker Compose instalados
- (Opcional) .NET 8 SDK e Node 18+ para desenvolvimento local

### Build & Run com Docker Compose

1. Clone o repositório:
   ```bash
   git clone https://github.com/leo-oliveira-eng/challenge-b3.git
   cd challenge-b3
   ```

2. Suba ambos os containers (API e UI):
    ```bash
    docker-compose up --build
    ```

3. Acesse:

- Frontend Angular: http://localhost:4200

- Backend API: http://localhost:8085/swagger/index.html


### Execução Manual (Desenvolvimento)

- API (.NET)
```bash
cd api/src/Api
dotnet run
```

- Frontend (Angular):
```bash
cd ui
npm install
npm start
```
O Angular estará disponível em http://localhost:4200.

## Testes
- Testes unitários (.NET) podem ser executados via:
```bash
cd api/tests
dotnet test cdb-yield-simulator.sln --collect:"XPlat Code Coverage"
```

O .NET por padrão gera cobertura de testes no formato .cobertura.xml com o parâmetro `--collect:"XPlat Code Coverage"`. Para gerar um relatório HTML legível, recomendo usar a ferramenta ReportGenerator, que transforma o arquivo Cobertura em um HTML.

Para instalar o ReportGenerator basta rodar o comando `dotnet tool install -g dotnet-reportgenerator-globaltool`

Para gerar o relatório basta executar o comando abaixo:
```bash
reportgenerator -reports:"**/coverage.cobertura.xml" -targetdir:"coveragereport" -reporttypes:Html -assemblyfilters:"-*.Tests;-Test*" 
```
Isso vai criar uma pasta chamada coveragereport com um arquivo index.html.
Abra esse arquivo no navegador e visualize o relatório!


## Decisões Técnicas do Projeto
### Clean Architecture
O projeto segue o padrão Clean Architecture para garantir uma separação clara de responsabilidades entre as camadas de domínio, aplicação e infraestrutura.
Isso facilita a manutenção, escalabilidade e testabilidade do código, além de permitir evoluções futuras (como troca de banco ou de framework) com mínimo impacto na lógica de negócio.

Por quê?

    - Facilita testes automatizados (mock de dependências).

    - Favorece reuso do domínio em outros contextos (ex: APIs, worker, console app).

    - Reduz acoplamento entre lógica de negócio e detalhes de implementação.

### Princípios SOLID: 
Seguidos em toda a estrutura do backend.
- S: Responsabilidades únicas (SRP) para handlers, services e entidades.
- O: Serviços são facilmente extensíveis sem alterar o código já existente.
- L: Uso de interfaces/classes abstratas com troca segura de implementações.
- I: Interfaces segregadas (nenhum serviço foi obrigado a implementar nada fora de seu contexto).
- D: Injetei todas as dependências, evitando acoplamento direto a implementações.

### Cobertura de Testes: 
100% de cobertura na lógica de negócio.
O projeto inclui tanto testes unitários quanto testes de integração:
    - Testes unitários: Focados na lógica de negócio isolada, utilizando mocks e fakes para garantir que regras como aplicação da tabela de IR, cálculo de juros compostos e validações de entrada estejam corretas.
    - Testes de integração: Validei o funcionamento conjunto das camadas da aplicação, incluindo chamadas reais à API e cenários completos, desde a requisição até o retorno dos resultados de simulação. Isso garante que a integração entre os componentes do backend e, quando necessário, com dependências externas (como banco de dados ou serviços, que acabou não sendo o caso desse desafio), está funcionando conforme esperado.

Combinando ambos os tipos de teste, busquei garantir robustez, confiabilidade e segurança para a aplicação, permitindo evolução futura com confiança.

### Docker: 
O uso de Docker permite isolar o ambiente da aplicação, facilitando testes locais, CI/CD e deploy.
O docker-compose foi adotado para facilitar a subida simultânea do frontend (Angular) e backend (.NET), simulando o ambiente produtivo.

- SonarLint: Projeto limpo de alertas padrão do Sonar e análise estática do Visual Studio.

### Trade-offs e Pontos de Atenção
Preferi manter CDI e TB fixos obedecendo a regra de negócio do desafio (em cenário real, poderiam ser dinâmicos via endpoint, vir de uma coleção em database, injetados via settings, etc). 
O frontend não usa state management pois o fluxo é simples e direto.

### Escolha de Bibliotecas
Para garantir produtividade, manutenibilidade e adoção de boas práticas de desenvolvimento, optei por utilizar as seguintes bibliotecas reconhecidas no ecossistema .NET:

`FluentValidation`: validação de comandos e DTOs de maneira fluente e desacoplada, promovendo código limpo e fácil manutenção.

`MediatR`: implementação do padrão Mediator, separando comandos, queries e handlers, facilitando a escalabilidade da aplicação.

`xUnit`: framework moderno e flexível para testes automatizados.

`Shouldly`: sintaxe mais legível para asserções em testes, tornando o feedback de falhas mais intuitivo.

`Moq`: criação de mocks para isolar dependências em testes unitários.

`Bogus`: geração de dados fake realistas para cenários de testes.

`Reqnroll (antigo SpecFlow)`: automação de testes de aceitação/BDD, permitindo especificação de cenários em linguagem natural.

Além dessas, utilizei duas bibliotecas de minha autoria:

[DomAid](https://github.com/leo-oliveira-eng/DomAid): biblioteca utilitária para domínio, que oferece integração facilitada com o padrão Mediator, abstrações para comandos/queries e extensões úteis para código limpo e desacoplado.

[Funcfy](https://github.com/leo-oliveira-eng/funcfy): biblioteca funcional para C#, trazendo padrões como o Result Pattern e monads, que facilitam a modelagem explícita do sucesso/fracasso das operações e promovem tratamento robusto de erros.

O uso do Result Pattern nessas bibliotecas garante que operações retornem estados explícitos (sucesso/falha), facilitando o fluxo de mensagens e a integração entre as camadas, além de simplificar a manipulação de erros.

Essas bibliotecas ainda estão em desenvolvimento mas serviram a uma combinação de ferramentas que permitiu uma arquitetura robusta, fácil de testar e evoluir, atendendo aos critérios de qualidade e boas práticas do desafio.

## Observações
Todos os cálculos seguem fielmente as regras do enunciado.

Projeto preparado para fácil avaliação: solução única, documentação clara e cobertura de testes verificada.

Repositório público: https://github.com/leo-oliveira-eng/challenge-b3
