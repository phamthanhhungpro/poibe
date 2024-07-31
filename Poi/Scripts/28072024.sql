START TRANSACTION;

ALTER TABLE "PerRoleFunctionScope" ADD "PerEndpointId" uuid;

CREATE INDEX "IX_PerRoleFunctionScope_PerEndpointId" ON "PerRoleFunctionScope" ("PerEndpointId");

ALTER TABLE "PerRoleFunctionScope" ADD CONSTRAINT "FK_PerRoleFunctionScope_PerEndpoint_PerEndpointId" FOREIGN KEY ("PerEndpointId") REFERENCES "PerEndpoint" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240725172838_UpdatePermission', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PerEndpointPerFunction" DROP CONSTRAINT "FK_PerEndpointPerFunction_PerEndpoint_EndpointsId";

ALTER TABLE "PerEndpointPerFunction" DROP CONSTRAINT "FK_PerEndpointPerFunction_PerFunction_FunctionsId";

ALTER TABLE "PerRoleFunctionScope" DROP CONSTRAINT "FK_PerRoleFunctionScope_PerEndpoint_PerEndpointId";

DROP INDEX "IX_PerRoleFunctionScope_PerEndpointId";

ALTER TABLE "PerEndpointPerFunction" DROP CONSTRAINT "PK_PerEndpointPerFunction";

ALTER TABLE "PerRoleFunctionScope" DROP COLUMN "PerEndpointId";

ALTER TABLE "PerEndpointPerFunction" RENAME TO "PerFunctionEndPoint";

ALTER TABLE "PerFunctionEndPoint" RENAME COLUMN "FunctionsId" TO "PerFunctionId";

ALTER INDEX "IX_PerEndpointPerFunction_FunctionsId" RENAME TO "IX_PerFunctionEndPoint_PerFunctionId";

ALTER TABLE "PerFunction" ADD "MainEndPointId" uuid;

ALTER TABLE "PerFunctionEndPoint" ADD CONSTRAINT "PK_PerFunctionEndPoint" PRIMARY KEY ("EndpointsId", "PerFunctionId");

CREATE INDEX "IX_PerFunction_MainEndPointId" ON "PerFunction" ("MainEndPointId");

ALTER TABLE "PerFunction" ADD CONSTRAINT "FK_PerFunction_PerEndpoint_MainEndPointId" FOREIGN KEY ("MainEndPointId") REFERENCES "PerEndpoint" ("Id");

ALTER TABLE "PerFunctionEndPoint" ADD CONSTRAINT "FK_PerFunctionEndPoint_PerEndpoint_EndpointsId" FOREIGN KEY ("EndpointsId") REFERENCES "PerEndpoint" ("Id") ON DELETE CASCADE;

ALTER TABLE "PerFunctionEndPoint" ADD CONSTRAINT "FK_PerFunctionEndPoint_PerFunction_PerFunctionId" FOREIGN KEY ("PerFunctionId") REFERENCES "PerFunction" ("Id") ON DELETE CASCADE;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240725174921_UpdatePermission1', '8.0.4');

COMMIT;

START TRANSACTION;

CREATE TABLE "PrjHoatDong" (
    "Id" uuid NOT NULL,
    "NoiDung" text,
    "TenantId" uuid NOT NULL,
    "CongViecId" uuid NOT NULL,
    "UserName" text,
    "MoreInfo" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PrjHoatDong" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PrjHoatDong_PrjCongViec_CongViecId" FOREIGN KEY ("CongViecId") REFERENCES "PrjCongViec" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PrjHoatDong_Tenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES "Tenants" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_PrjHoatDong_CongViecId" ON "PrjHoatDong" ("CongViecId");

CREATE INDEX "IX_PrjHoatDong_TenantId" ON "PrjHoatDong" ("TenantId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240727160558_AddHoatDongTable', '8.0.4');

COMMIT;

