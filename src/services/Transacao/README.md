# Fiap.Invest.Transacoes

> **Objetivo**

- Compra, venda e saldo de ativos através de transações.

> **Requisitos funcionais**

- **Fazer transação** de compra e venda de ativos para portfólios;
- **Obtenção de Saldo** de ativos do portfólio;
- **Autenticação** de usuários para utilização dos serviços fornecidos pela API.

> **Requisitos não funcionais**

- Utilização do PostgreSQL como banco de dados;
- Banco de dados exclusivo do serviço de transações;
- Arquitetura limpa;
- Registro de eventos (LOGs);
- Testes unitários utilizando a biblioteca XUnit;
- Cobertura mínima de testes em 50%.

> **Regra de negócio**

- Para fazer uma venda o usuário deve possuir saldo positivo para o ativo dentro do portfólio;
- O valor mínimo para compra e para venda é de 0,1 R$;
- A quantidade mínima para compra ou venda é 1.
- Os dados do usuário a realizar transação devem ser obtidos através do usuário autenticado.

> **Casos de uso**

- `Saldo de transações de ativos por portfólio` - A API deve ser capaz de listar as transações e calcular o saldo da quantidade de ativos existentes para o usuário dentro do portfólio a partir de um id de um portfólio.
- `Transação` - Um usuário autenticado pode fazer compra ou venda de ativos dentro de um portfólio.

> **Itens relacionados**
Para mais informações olhar itens
- [`Criação de portfólios`](https://github.com/JulioSCr/Fiap.Invest/issues/1)
- [`Listagem de portfólios`](https://github.com/JulioSCr/Fiap.Invest/issues/3)
- [`Transação de ativos`](https://github.com/JulioSCr/Fiap.Invest/issues/2)
- [`Saldo de ativos`](https://github.com/JulioSCr/Fiap.Invest/issues/4)
