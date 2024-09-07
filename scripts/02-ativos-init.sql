\c ativofiapinvest fiapinvestuserativo

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;


DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240907190818_Inicial') THEN
    CREATE TABLE "Ativos" (
        "Id" uuid NOT NULL,
        "Tipo" integer NOT NULL,
        "Nome" varchar(100) NOT NULL,
        "Codigo" varchar(100) NOT NULL,
        CONSTRAINT "PK_Ativos" PRIMARY KEY ("Id")
    );
    END IF;
END $EF$;

DO $EF$
BEGIN
    IF NOT EXISTS(SELECT 1 FROM "__EFMigrationsHistory" WHERE "MigrationId" = '20240907190818_Inicial') THEN
    INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
    VALUES ('20240907190818_Inicial', '8.0.0');
    END IF;
END $EF$;
COMMIT;