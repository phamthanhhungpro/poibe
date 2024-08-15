START TRANSACTION;

ALTER TABLE "PrjCongViec" ADD "DGChapHanhCheDoThongTinBaoCao" text;

ALTER TABLE "PrjCongViec" ADD "DGChapHanhDieuDongLamThemGio" text;

ALTER TABLE "PrjCongViec" ADD "DGChatLuongHieuQua" text;

ALTER TABLE "PrjCongViec" ADD "DGTienDo" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240812070432_UpdateDanhGiaCv', '8.0.4');

COMMIT;

