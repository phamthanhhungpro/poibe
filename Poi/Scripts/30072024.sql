﻿START TRANSACTION;

ALTER TABLE "PrjHoatDong" ADD "DuanNvChuyenMonId" uuid NOT NULL DEFAULT '00000000-0000-0000-0000-000000000000';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240729161643_UpdateHoatDongTab', '8.0.4');

COMMIT;

