# Fiap.Invest.Ativos

> **Objetivo**

- Manutenção, listagem e detalhamento de ativos.

> **Requisitos funcionais**

- **Cadastro** de ativos;
- **Listagem** de ativos;
- **Busca** de ativo por ID;
- Cadastro apenas acessível para usuários **autenticados** e com **autorização** de ADMIN.

> **Requisitos não funcionais**

- Utilização do PostgreSQL como banco de dados;
- Banco de dados exclusivo do serviço de ativos;
- Arquitetura limpa;
- Registro de eventos (LOGs);
- Testes unitários utilizando a biblioteca XUnit;
- Cobertura mínima de testes em 50%.

> **Regra de negócio**

- O ativo deve possuir um nome;
- O ativo pode possuir uma descrição;
- Apenas usuários com permissão de administradores podem fazer cadastro de novos ativos;
- Os ativos, quando numa busca geral, deve utilizar paginação.

> **Casos de uso**

- `Listagem de ativos` - A API deve ser capaz de listar os ativos através de paginação, essa listagem não requer autenticação alguma.
- `Busca` - Quando passado um id de um ativo deve ser retornado os detalhes desse ativo, essa busca não requer autenticação alguma.
- `Cadastro` - Um usuário com permissão de ADMIN pode fazer cadastro de novos ativos, fornecendo obrigatoriamente um nome caso queira uma descrição.

> **Itens relacionados**
Para mais informações olhar itens
- [`Transação de ativos`](https://github.com/JulioSCr/Fiap.Invest/issues/2)
- [`Saldo de ativos`](https://github.com/JulioSCr/Fiap.Invest/issues/4)
