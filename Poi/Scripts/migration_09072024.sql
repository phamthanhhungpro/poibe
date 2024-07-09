START TRANSACTION;

ALTER TABLE "PrjCongViec" DROP CONSTRAINT "FK_PrjCongViec_AspNetUsers_NguoiThucHienId";

ALTER TABLE "PrjCongViec" RENAME COLUMN "NguoiThucHienId" TO "NguoiDuocGiaoId";

ALTER INDEX "IX_PrjCongViec_NguoiThucHienId" RENAME TO "IX_PrjCongViec_NguoiDuocGiaoId";

CREATE TABLE "PrjCongViecNguoiThucHien" (
    "NguoiThucHienId" uuid NOT NULL,
    "PrjCongViec1Id" uuid NOT NULL,
    CONSTRAINT "PK_PrjCongViecNguoiThucHien" PRIMARY KEY ("NguoiThucHienId", "PrjCongViec1Id"),
    CONSTRAINT "FK_PrjCongViecNguoiThucHien_AspNetUsers_NguoiThucHienId" FOREIGN KEY ("NguoiThucHienId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PrjCongViecNguoiThucHien_PrjCongViec_PrjCongViec1Id" FOREIGN KEY ("PrjCongViec1Id") REFERENCES "PrjCongViec" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_PrjCongViecNguoiThucHien_PrjCongViec1Id" ON "PrjCongViecNguoiThucHien" ("PrjCongViec1Id");

ALTER TABLE "PrjCongViec" ADD CONSTRAINT "FK_PrjCongViec_AspNetUsers_NguoiDuocGiaoId" FOREIGN KEY ("NguoiDuocGiaoId") REFERENCES "AspNetUsers" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240707162217_AddNguoiThucHien', '8.0.4');

COMMIT;

START TRANSACTION;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240709163102_UpdateTableCongViec', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjCongViec" ADD "Attachments" text;

ALTER TABLE "PrjCongViec" ADD "MucDoUuTien" text;

ALTER TABLE "PrjCongViec" ADD "ThoiGianDuKien" text;

ALTER TABLE "PrjCongViec" ADD "TrangThaiChiTiet" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240709164120_UpdateConViecColumn', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PrjCongViec" ALTER COLUMN "TrangThaiChiTiet" SET DEFAULT 'READY';

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240709164346_UpdateConViecColumn1', '8.0.4');

COMMIT;

