START TRANSACTION;

ALTER TABLE "PrjCongViec" ADD "NgayGiaHan" timestamp with time zone;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240710070332_AddNgayGiaHanCongViec', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjCongViec" ADD "TrangThaiChoXacNhan" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240710084155_AddTrangThaiChoDuyet', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjComment" ADD "NguoiComment" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240711162450_AddUserComment', '8.0.4');

COMMIT;

