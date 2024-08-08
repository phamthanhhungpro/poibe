START TRANSACTION;

ALTER TABLE "PrjDuAnNvChuyenMon" ADD "IsClosed" boolean NOT NULL DEFAULT FALSE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240806084655_UpdateDuanTable', '8.0.4');

COMMIT;

