# Pós Tech FIAP - Tech Challenge - Fase 5 📜

Aplicação **.NET** para simular um sistema de gestão de investimentos.

## Fiap.Invest.Auth

> Descrição: API de autenticação do sistema Fiap.Invest, para mais detalhes [clique aqui](https://github.com/JulioSCr/Fiap.Invest/blob/develop/src/services/Auth/README.md).

## Fiap.Invest.Ativos

> Descrição: API de ativos do sistema Fiap.Invest, para mais detalhes [clique aqui](https://github.com/JulioSCr/Fiap.Invest/blob/develop/src/services/Ativo/README.md).

## Fiap.Invest.Portfolios

> Descrição: API de portfólios do sistema Fiap.Invest, para mais detalhes [clique aqui](https://github.com/JulioSCr/Fiap.Invest/tree/develop/src/services/Portfolio#readme).

## Fiap.Invest.Transacoes

> Descrição: API de transações do sistema Fiap.Invest, para mais detalhes [clique aqui](https://github.com/JulioSCr/Fiap.Invest/blob/develop/src/services/Transacao/README.md).

## Fiap.Invest.Blazor.Web

> Descrição: Plataforma Web para executar os serviços da Fiap.Invest através de UI.

## Execução
Para fazer a execução basta executar o comando `docker-compose up -d` que fará a geração do banco e a criação das tabelas.
Esse comando também sobe a Fiap.Invest.Auth na porta `8081`.
Em seguida basta executar o comando `dotnet run` para cada um dos projetos.
Já existe um usuário administrador, cujo qual, foi criado no momento do up do docker copose, esté usuário possui CPF 001 (*inválido porém aceito pelo sistema, único nessa condição*) e senha Teste@01.
