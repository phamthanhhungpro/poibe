START TRANSACTION;

ALTER TABLE "HrmChamCongDiemDanh" ADD "GhiChu" text;

ALTER TABLE "HrmChamCongDiemDanh" ADD "LyDo" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240821081239_UpdateCCDD', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "HrmChamCongDiemDanh" DROP CONSTRAINT "FK_HrmChamCongDiemDanh_AspNetUsers_UserId";

UPDATE "HrmChamCongDiemDanh" SET "UserId" = '00000000-0000-0000-0000-000000000000' WHERE "UserId" IS NULL;
ALTER TABLE "HrmChamCongDiemDanh" ALTER COLUMN "UserId" SET NOT NULL;
ALTER TABLE "HrmChamCongDiemDanh" ALTER COLUMN "UserId" SET DEFAULT '00000000-0000-0000-0000-000000000000';

ALTER TABLE "HrmChamCongDiemDanh" ADD "NguoiXacNhanId" uuid;

CREATE INDEX "IX_HrmChamCongDiemDanh_NguoiXacNhanId" ON "HrmChamCongDiemDanh" ("NguoiXacNhanId");

ALTER TABLE "HrmChamCongDiemDanh" ADD CONSTRAINT "FK_HrmChamCongDiemDanh_AspNetUsers_NguoiXacNhanId" FOREIGN KEY ("NguoiXacNhanId") REFERENCES "AspNetUsers" ("Id") ON DELETE RESTRICT;

ALTER TABLE "HrmChamCongDiemDanh" ADD CONSTRAINT "FK_HrmChamCongDiemDanh_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id") ON DELETE RESTRICT;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240821082955_UpdateNguoiXacNhan', '8.0.4');

COMMIT;

