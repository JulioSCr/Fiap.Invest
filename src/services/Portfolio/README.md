# Fiap.Invest.Portfolios

> **Objetivo**

- Manutenção, listagem e detalhamento de portfólios.

> **Requisitos funcionais**

- **Cadastro** de portfólios;
- **Listagem** de portfólios por usuário;
- **Autenticação** de usuários para utilização dos serviços fornecidos pela API.

> **Requisitos não funcionais**

- Utilização do PostgreSQL como banco de dados;
- Banco de dados exclusivo do serviço de portfólios;
- Arquitetura limpa;
- Registro de eventos (LOGs);
- Testes unitários utilizando a biblioteca XUnit;
- Cobertura mínima de testes em 50%.

> **Regra de negócio**

- O portfólio deve possuir um nome, com no mínimo 5 letras e no máximo 20;
- O portfólio pode possuir uma descrição de até 500 caractéres;
- Apenas usuários autenticados podem fazer cadastro de novos portfólios;
- Os dados do usuário dono do portfólio deve ser obtido através do usuário autenticado.

> **Casos de uso**

- `Listagem de portfólios por usuário` - A API deve ser capaz de listar os portfólios pertencentes a determinado usuário, a partir da autenticação do mesmo.
- `Cadastro` - Um usuário com autenticado pode fazer cadastro de novos portfólio, todo novo portfólio fica atrelado a este usuário a partir de sua autenticação.

> **Itens relacionados**
Para mais informações olhar itens
- [`Criação de portfólios`](https://github.com/JulioSCr/Fiap.Invest/issues/1)
- [`Listagem de portfólios`](https://github.com/JulioSCr/Fiap.Invest/issues/3)
- [`Transação de ativos`](https://github.com/JulioSCr/Fiap.Invest/issues/2)
- [`Saldo de ativos`](https://github.com/JulioSCr/Fiap.Invest/issues/4)
