# Fiap.Invest.Auth

> Objetivo

- Autenticação e autorização para segurança dos usuários.

> **Requisitos funcionais**

- **Autenticação** de **usuários cadastrados** através de cpf e senha;
- **Autorização** de **usuários cadastrados** conforme o acesso sistêmico necessário;
- Possibilitar o **cadastro** de novos usuários;
- Possibilitar a utilização de um **refresh token** para minimizar a necessidade de autenticação a todo momento;
- Possibilitar a obtenção dos dados do usuário através do **decryptotoken**;
- A senha deve ser armazenada criptografada.

> **Requisitos não funcionais**

- Utilização de **chaves assíncronas** com exposição de chave pública através de endpoint **jwks** de acordo com as expecificações do **Auth0**;
-  Utilização do PostgreSQL como banco de dados;
-  Banco de dados exclusivo do serviço de autenticação;
-  Arquitetura limpa.

> **Regra de negócio**

- Somente deverão ser aceitos CPFs válidos;
- O nome do usuário deve ter pelo menos 3 letras no primeiro nome e um sobrenome com pelo menos duas letras;
- O nome deve conter o sobrenome da pessoa;
- A senha deve ter 8 caractéres contendo letras minúsculas, maiúsculas, números e caractéres especiais;
- Ao cadastrar um usuário a senha deve ser igual a confirmação de senha.
- O refresh token tem validade de uma hora;
- O access token tem validade de meia hora;

> **Casos de uso**

- `Cadastro de usuário` - A API deve ser capaz de cadastrar um cliente ao ser acionada pelo programa de front, ao ser acionada esta deverá validar os dados a serem inseridos e criptografar senha; posteriormente será registrado no banco de dados nas devidas tabelas.
- `Autenticação` - A Api deve ser capaz de autenticar o acesso de um cliente ao validar a senha e CPF inseridos com a registrada no banco de dados.
- `Refresh token` - Ao receber um token a API deverá informar se o token é válido (*validar se está expirado*) ou não utilizando as validações internas do sistema
- `Decryptotoken` - Ao receber um token válido a API deverá retornar as informações do usuário deste token

> **Itens relacionados**
Para mais informações olhar itens
- [`Autenticação`](https://github.com/JulioSCr/Fiap.Invest/issues/6)
- [`Cadastro`](https://github.com/JulioSCr/Fiap.Invest/issues/5)
