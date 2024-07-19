START TRANSACTION;

CREATE TABLE "PerEndpoint" (
    "Id" uuid NOT NULL,
    "Name" text,
    "Description" text,
    "Method" text,
    "Path" text,
    "IsPublic" boolean NOT NULL,
    "AppCode" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PerEndpoint" PRIMARY KEY ("Id")
);

CREATE TABLE "PerGroupFunction" (
    "Id" uuid NOT NULL,
    "Name" text,
    "Description" text,
    "AppCode" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PerGroupFunction" PRIMARY KEY ("Id")
);

CREATE TABLE "PerRole" (
    "Id" uuid NOT NULL,
    "Name" text,
    "Description" text,
    "TenantId" uuid NOT NULL,
    "AppCode" text,
    "UserId" uuid,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PerRole" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PerRole_AspNetUsers_UserId" FOREIGN KEY ("UserId") REFERENCES "AspNetUsers" ("Id"),
    CONSTRAINT "FK_PerRole_Tenants_TenantId" FOREIGN KEY ("TenantId") REFERENCES "Tenants" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PerScope" (
    "Id" uuid NOT NULL,
    "Name" text,
    "Description" text,
    "AppCode" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PerScope" PRIMARY KEY ("Id")
);

CREATE TABLE "PerFunction" (
    "Id" uuid NOT NULL,
    "Name" text,
    "Description" text,
    "IsPublic" boolean NOT NULL,
    "GroupFunctionId" uuid NOT NULL,
    "AppCode" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PerFunction" PRIMARY KEY ("Id"),
    CONSTRAINT "FK_PerFunction_PerGroupFunction_GroupFunctionId" FOREIGN KEY ("GroupFunctionId") REFERENCES "PerGroupFunction" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PerEndpointPerFunction" (
    "EndpointsId" uuid NOT NULL,
    "FunctionsId" uuid NOT NULL,
    CONSTRAINT "PK_PerEndpointPerFunction" PRIMARY KEY ("EndpointsId", "FunctionsId"),
    CONSTRAINT "FK_PerEndpointPerFunction_PerEndpoint_EndpointsId" FOREIGN KEY ("EndpointsId") REFERENCES "PerEndpoint" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PerEndpointPerFunction_PerFunction_FunctionsId" FOREIGN KEY ("FunctionsId") REFERENCES "PerFunction" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PerFunctionPerRole" (
    "FunctionsId" uuid NOT NULL,
    "RolesId" uuid NOT NULL,
    CONSTRAINT "PK_PerFunctionPerRole" PRIMARY KEY ("FunctionsId", "RolesId"),
    CONSTRAINT "FK_PerFunctionPerRole_PerFunction_FunctionsId" FOREIGN KEY ("FunctionsId") REFERENCES "PerFunction" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PerFunctionPerRole_PerRole_RolesId" FOREIGN KEY ("RolesId") REFERENCES "PerRole" ("Id") ON DELETE CASCADE
);

CREATE TABLE "PerFunctionPerScope" (
    "FunctionsId" uuid NOT NULL,
    "ScopesId" uuid NOT NULL,
    CONSTRAINT "PK_PerFunctionPerScope" PRIMARY KEY ("FunctionsId", "ScopesId"),
    CONSTRAINT "FK_PerFunctionPerScope_PerFunction_FunctionsId" FOREIGN KEY ("FunctionsId") REFERENCES "PerFunction" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PerFunctionPerScope_PerScope_ScopesId" FOREIGN KEY ("ScopesId") REFERENCES "PerScope" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_PerEndpointPerFunction_FunctionsId" ON "PerEndpointPerFunction" ("FunctionsId");

CREATE INDEX "IX_PerFunction_GroupFunctionId" ON "PerFunction" ("GroupFunctionId");

CREATE INDEX "IX_PerFunctionPerRole_RolesId" ON "PerFunctionPerRole" ("RolesId");

CREATE INDEX "IX_PerFunctionPerScope_ScopesId" ON "PerFunctionPerScope" ("ScopesId");

CREATE INDEX "IX_PerRole_TenantId" ON "PerRole" ("TenantId");

CREATE INDEX "IX_PerRole_UserId" ON "PerRole" ("UserId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240716074418_AddPermissionTable', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PerScope" ADD "Code" text;

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240717083613_UpdatePermissionRole', '8.0.4');

COMMIT;

START TRANSACTION;

DROP TABLE "PerFunctionPerRole";

CREATE TABLE "PerRoleFunctionScope" (
    "PerRoleId" uuid NOT NULL,
    "PerFunctionId" uuid NOT NULL,
    "PerScopeId" uuid NOT NULL,
    "Id" uuid NOT NULL,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_PerRoleFunctionScope" PRIMARY KEY ("PerRoleId", "PerFunctionId"),
    CONSTRAINT "FK_PerRoleFunctionScope_PerFunction_PerFunctionId" FOREIGN KEY ("PerFunctionId") REFERENCES "PerFunction" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PerRoleFunctionScope_PerRole_PerRoleId" FOREIGN KEY ("PerRoleId") REFERENCES "PerRole" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PerRoleFunctionScope_PerScope_PerScopeId" FOREIGN KEY ("PerScopeId") REFERENCES "PerScope" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_PerRoleFunctionScope_PerFunctionId" ON "PerRoleFunctionScope" ("PerFunctionId");

CREATE INDEX "IX_PerRoleFunctionScope_PerScopeId" ON "PerRoleFunctionScope" ("PerScopeId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240718110320_AddRoleScope', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PerRoleFunctionScope" DROP CONSTRAINT "FK_PerRoleFunctionScope_PerScope_PerScopeId";

ALTER TABLE "PerRoleFunctionScope" ALTER COLUMN "PerScopeId" DROP NOT NULL;

ALTER TABLE "PerRoleFunctionScope" ADD CONSTRAINT "FK_PerRoleFunctionScope_PerScope_PerScopeId" FOREIGN KEY ("PerScopeId") REFERENCES "PerScope" ("Id");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240718162004_AddRoleScope1', '8.0.4');

COMMIT;

START TRANSACTION;

ALTER TABLE "PerRole" DROP CONSTRAINT "FK_PerRole_AspNetUsers_UserId";

DROP INDEX "IX_PerRole_UserId";

ALTER TABLE "PerRole" DROP COLUMN "UserId";

CREATE TABLE "PerRoleUser" (
    "PerRolesId" uuid NOT NULL,
    "UsersId" uuid NOT NULL,
    CONSTRAINT "PK_PerRoleUser" PRIMARY KEY ("PerRolesId", "UsersId"),
    CONSTRAINT "FK_PerRoleUser_AspNetUsers_UsersId" FOREIGN KEY ("UsersId") REFERENCES "AspNetUsers" ("Id") ON DELETE CASCADE,
    CONSTRAINT "FK_PerRoleUser_PerRole_PerRolesId" FOREIGN KEY ("PerRolesId") REFERENCES "PerRole" ("Id") ON DELETE CASCADE
);

CREATE INDEX "IX_PerRoleUser_UsersId" ON "PerRoleUser" ("UsersId");

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240719022655_AddRoleScope2', '8.0.4');

COMMIT;

