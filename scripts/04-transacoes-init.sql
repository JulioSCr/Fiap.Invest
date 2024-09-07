\c transacaofiapinvest fiapinvestusertransacao

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240907194641_Inicial') THEN
    CREATE TABLE "Transacoes" (
        "Id" uuid NOT NULL,
        "PortfolioId" uuid NOT NULL,
        "AtivoId" uuid NOT NULL,
        "Tipo" integer NOT NULL,
        "Quantidade" integer NOT NULL,
        "Preco" numeric NOT NULL,
        "DataTransacao" timestamp without time zone NOT NULL,
        CONSTRAINT "PK_Transacoes" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240907194641_Inicial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240907194641_Inicial', '8.0.0');
    END IF;
END $EF$;
COMMIT;

