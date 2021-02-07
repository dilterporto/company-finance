DROP DATABASE IF EXISTS FinanceDb;
CREATE DATABASE FinanceDb;

CREATE TABLE IF NOT EXISTS "__EFMigrationsHistory" (
    "MigrationId" character varying(150) NOT NULL,
    "ProductVersion" character varying(32) NOT NULL,
    CONSTRAINT "PK___EFMigrationsHistory" PRIMARY KEY ("MigrationId")
);

START TRANSACTION;

CREATE TYPE currency AS ENUM ('brl');
CREATE TYPE transaction_type AS ENUM ('credit', 'debit', 'other');

CREATE TABLE "Statements" (
    "Id" uuid NOT NULL,
    "Start" timestamp without time zone NULL,
    "End" timestamp without time zone NULL,
    "Balance" numeric NOT NULL,
    "Transactions" jsonb NULL,
    CONSTRAINT "PK_Statements" PRIMARY KEY ("Id")
);

CREATE TABLE "Transactions" (
    "Id" uuid NOT NULL,
    "Account" text NULL,
    "Currency" currency NOT NULL,
    "Type" transaction_type NOT NULL,
    "Date" timestamp without time zone NULL,
    "Amount" numeric NOT NULL,
    "Memo" text NULL,
    CONSTRAINT "PK_Transactions" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20210124055520_CreateTranscationsAndStatementsTables', '5.0.2');

COMMIT;

