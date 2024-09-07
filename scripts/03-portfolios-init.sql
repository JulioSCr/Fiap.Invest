\c portfoliofiapinvest fiapinvestuserportfolio

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240907194253_Inicial') THEN
    CREATE TABLE "Portfolios" (
        "Id" uuid NOT NULL,
        "UsuarioId" uuid NOT NULL,
        "Nome" varchar(20) NOT NULL,
        "Descricao" varchar(500) NOT NULL,
        CONSTRAINT "PK_Portfolios" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240907194253_Inicial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240907194253_Inicial', '8.0.0');
    END IF;
END $EF$;
COMMIT;

