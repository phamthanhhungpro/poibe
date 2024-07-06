START TRANSACTION;

CREATE TABLE "TokenExpired" (
    "Id" uuid NOT NULL,
    "Token" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_TokenExpired" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240703105152_AddTokenExpired', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjDuAnSetting" ADD "JsonValue" jsonb;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240704185010_JsonValueSt', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjKanban" ALTER COLUMN "TrangThaiCongViec" TYPE text;
ALTER TABLE "PrjKanban" ALTER COLUMN "TrangThaiCongViec" DROP NOT NULL;

ALTER TABLE "PrjCongViec" ALTER COLUMN "TrangThai" TYPE text;
ALTER TABLE "PrjCongViec" ALTER COLUMN "TrangThai" DROP NOT NULL;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240705173937_TrangThaiCongViecString', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjTagCongViec" ADD "MauSac" text;

ALTER TABLE "PrjTagComment" ADD "MauSac" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240706162714_UpdateTagMauSac', '8.0.4');

COMMIT;

