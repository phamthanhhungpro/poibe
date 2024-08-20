START TRANSACTION;

ALTER TABLE "PrjCongViec" ADD "NgayXacNhanHoanThanh" timestamp with time zone;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240819041137_UpdateNgayHoanThanhCongViec', '8.0.4');

COMMIT;

