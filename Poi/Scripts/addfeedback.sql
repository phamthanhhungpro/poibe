START TRANSACTION;

CREATE TABLE "AFeedbacks" (
    "Id" uuid NOT NULL,
    "UserId" uuid,
    "AppName" text,
    "Tittle" text,
    "MoTa" text,
    "TrangThai" text,
    "Attachments" text,
    "CreatedAt" timestamp with time zone NOT NULL DEFAULT (CURRENT_TIMESTAMP),
    "UpdatedAt" timestamp with time zone,
    "IsDeleted" boolean NOT NULL DEFAULT FALSE,
    "DeletedAt" timestamp with time zone,
    "CreatedBy" uuid,
    CONSTRAINT "PK_AFeedbacks" PRIMARY KEY ("Id")
);

INSERT INTO "__EFMigrationsHistory" ("MigrationId", "ProductVersion")
VALUES ('20240703034538_AddFeedbackTable', '8.0.4');

COMMIT;

