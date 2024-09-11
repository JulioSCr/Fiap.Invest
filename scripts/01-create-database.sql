CREATE USER fiapinvestuserativo With PASSWORD 'PostgresFiapInvestLocalAtivo';
CREATE USER fiapinvestuserportfolio With PASSWORD 'PostgresFiapInvestLocalPortfolio';
CREATE USER fiapinvestusertransacao With PASSWORD 'PostgresFiapInvestLocalTransacao';
CREATE USER fiapinvestuserauth With PASSWORD 'PostgresFiapInvestLocalAuth';
CREATE DATABASE ativofiapinvest;
CREATE DATABASE portfoliofiapinvest;
CREATE DATABASE transacaofiapinvest;
CREATE DATABASE authfiapinvest;
GRANT ALL PRIVILEGES ON DATABASE ativofiapinvest TO fiapinvestuserativo;
GRANT ALL PRIVILEGES ON DATABASE portfoliofiapinvest TO fiapinvestuserportfolio;
GRANT ALL PRIVILEGES ON DATABASE transacaofiapinvest TO fiapinvestusertransacao;
GRANT ALL PRIVILEGES ON DATABASE authfiapinvest TO fiapinvestuserauth;
\c ativofiapinvest
GRANT ALL ON SCHEMA public TO fiapinvestuserativo;
\c portfoliofiapinvest
GRANT ALL ON SCHEMA public TO fiapinvestuserportfolio;
\c transacaofiapinvest
GRANT ALL ON SCHEMA public TO fiapinvestusertransacao;
\c authfiapinvest
GRANT ALL ON SCHEMA public TO fiapinvestuserauth;