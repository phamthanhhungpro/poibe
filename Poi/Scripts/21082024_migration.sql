START TRANSACTION;

ALTER TABLE "HrmGiaiTrinhChamCong" ADD "GhiChu" text;

ALTER TABLE "HrmChamCongDiemDanh" ADD "HrmCongXacNhanId" uuid;

CREATE INDEX "IX_HrmChamCongDiemDanh_HrmCongXacNhanId" ON "HrmChamCongDiemDanh" ("HrmCongXacNhanId");

ALTER TABLE "HrmChamCongDiemDanh" ADD CONSTRAINT "FK_HrmChamCongDiemDanh_HrmCongKhaiBao_HrmCongXacNhanId" FOREIGN KEY ("HrmCongXacNhanId") REFERENCES "HrmCongKhaiBao" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240820065906_DiemDanhThuCong', '8.0.4');

COMMIT;

