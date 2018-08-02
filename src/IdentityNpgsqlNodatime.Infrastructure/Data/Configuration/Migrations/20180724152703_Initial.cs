﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace IdentityNpgsqlNodatime.Infrastructure.Data.Configuration.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "api_resources",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    display_name = table.Column<string>(maxLength: 200, nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "clients",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(nullable: false),
                    client_id = table.Column<string>(maxLength: 200, nullable: false),
                    protocol_type = table.Column<string>(maxLength: 200, nullable: false),
                    require_client_secret = table.Column<bool>(nullable: false),
                    client_name = table.Column<string>(maxLength: 200, nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    client_uri = table.Column<string>(maxLength: 2000, nullable: true),
                    logo_uri = table.Column<string>(maxLength: 2000, nullable: true),
                    require_consent = table.Column<bool>(nullable: false),
                    allow_remember_consent = table.Column<bool>(nullable: false),
                    always_include_user_claims_in_id_token = table.Column<bool>(nullable: false),
                    require_pkce = table.Column<bool>(nullable: false),
                    allow_plain_text_pkce = table.Column<bool>(nullable: false),
                    allow_access_tokens_via_browser = table.Column<bool>(nullable: false),
                    front_channel_logout_uri = table.Column<string>(maxLength: 2000, nullable: true),
                    front_channel_logout_session_required = table.Column<bool>(nullable: false),
                    back_channel_logout_uri = table.Column<string>(maxLength: 2000, nullable: true),
                    back_channel_logout_session_required = table.Column<bool>(nullable: false),
                    allow_offline_access = table.Column<bool>(nullable: false),
                    identity_token_lifetime = table.Column<int>(nullable: false),
                    access_token_lifetime = table.Column<int>(nullable: false),
                    authorization_code_lifetime = table.Column<int>(nullable: false),
                    consent_lifetime = table.Column<int>(nullable: true),
                    absolute_refresh_token_lifetime = table.Column<int>(nullable: false),
                    sliding_refresh_token_lifetime = table.Column<int>(nullable: false),
                    refresh_token_usage = table.Column<int>(nullable: false),
                    update_access_token_claims_on_refresh = table.Column<bool>(nullable: false),
                    refresh_token_expiration = table.Column<int>(nullable: false),
                    access_token_type = table.Column<int>(nullable: false),
                    enable_local_login = table.Column<bool>(nullable: false),
                    include_jwt_id = table.Column<bool>(nullable: false),
                    always_send_client_claims = table.Column<bool>(nullable: false),
                    client_claims_prefix = table.Column<string>(maxLength: 200, nullable: true),
                    pair_wise_subject_salt = table.Column<string>(maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_clients", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "identity_resources",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    enabled = table.Column<bool>(nullable: false),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    display_name = table.Column<string>(maxLength: 200, nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    required = table.Column<bool>(nullable: false),
                    emphasize = table.Column<bool>(nullable: false),
                    show_in_discovery_document = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_resources", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "api_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(maxLength: 200, nullable: false),
                    api_resource_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_claims_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "api_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_scopes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    name = table.Column<string>(maxLength: 200, nullable: false),
                    display_name = table.Column<string>(maxLength: 200, nullable: true),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    required = table.Column<bool>(nullable: false),
                    emphasize = table.Column<bool>(nullable: false),
                    show_in_discovery_document = table.Column<bool>(nullable: false),
                    api_resource_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_scopes", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_scopes_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "api_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_secrets",
                columns: table => new
                {
                    expiration = table.Column<DateTime>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(maxLength: 1000, nullable: true),
                    value = table.Column<string>(maxLength: 2000, nullable: true),
                    type = table.Column<string>(maxLength: 250, nullable: true),
                    api_resource_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_secrets", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_secrets_api_resources_api_resource_id",
                        column: x => x.api_resource_id,
                        principalTable: "api_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(maxLength: 250, nullable: false),
                    value = table.Column<string>(maxLength: 250, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_claims_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_cors_origins",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    origin = table.Column<string>(maxLength: 150, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_cors_origins", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_cors_origins_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_grant_types",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    grant_type = table.Column<string>(maxLength: 250, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_grant_types", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_grant_types_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_id_prestrictions",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    provider = table.Column<string>(maxLength: 200, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_id_prestrictions", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_id_prestrictions_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_post_logout_redirect_uris",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    post_logout_redirect_uri = table.Column<string>(maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_post_logout_redirect_uris", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_post_logout_redirect_uris_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_properties",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    key = table.Column<string>(maxLength: 250, nullable: false),
                    value = table.Column<string>(maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_properties", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_properties_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_redirect_uris",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    redirect_uri = table.Column<string>(maxLength: 2000, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_redirect_uris", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_redirect_uris_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_scopes",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    scope = table.Column<string>(maxLength: 200, nullable: false),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_scopes", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_scopes_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "client_secrets",
                columns: table => new
                {
                    expiration = table.Column<DateTime>(nullable: true),
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    description = table.Column<string>(maxLength: 2000, nullable: true),
                    value = table.Column<string>(maxLength: 2000, nullable: false),
                    type = table.Column<string>(maxLength: 250, nullable: true),
                    client_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_client_secrets", x => x.id);
                    table.ForeignKey(
                        name: "fk_client_secrets_clients_client_id",
                        column: x => x.client_id,
                        principalTable: "clients",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "identity_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(maxLength: 200, nullable: false),
                    identity_resource_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_identity_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_identity_claims_identity_resources_identity_resource_id",
                        column: x => x.identity_resource_id,
                        principalTable: "identity_resources",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "api_scope_claims",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(maxLength: 200, nullable: false),
                    api_scope_id = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_api_scope_claims", x => x.id);
                    table.ForeignKey(
                        name: "fk_api_scope_claims_api_scopes_api_scope_id",
                        column: x => x.api_scope_id,
                        principalTable: "api_scopes",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "ix_api_claims_api_resource_id",
                table: "api_claims",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_resources_name",
                table: "api_resources",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_scope_claims_api_scope_id",
                table: "api_scope_claims",
                column: "api_scope_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_scopes_api_resource_id",
                table: "api_scopes",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_api_scopes_name",
                table: "api_scopes",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_api_secrets_api_resource_id",
                table: "api_secrets",
                column: "api_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_claims_client_id",
                table: "client_claims",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_cors_origins_client_id",
                table: "client_cors_origins",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_grant_types_client_id",
                table: "client_grant_types",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_id_prestrictions_client_id",
                table: "client_id_prestrictions",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_post_logout_redirect_uris_client_id",
                table: "client_post_logout_redirect_uris",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_properties_client_id",
                table: "client_properties",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_redirect_uris_client_id",
                table: "client_redirect_uris",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_scopes_client_id",
                table: "client_scopes",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_client_secrets_client_id",
                table: "client_secrets",
                column: "client_id");

            migrationBuilder.CreateIndex(
                name: "ix_clients_client_id",
                table: "clients",
                column: "client_id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "ix_identity_claims_identity_resource_id",
                table: "identity_claims",
                column: "identity_resource_id");

            migrationBuilder.CreateIndex(
                name: "ix_identity_resources_name",
                table: "identity_resources",
                column: "name",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "api_claims");

            migrationBuilder.DropTable(
                name: "api_scope_claims");

            migrationBuilder.DropTable(
                name: "api_secrets");

            migrationBuilder.DropTable(
                name: "client_claims");

            migrationBuilder.DropTable(
                name: "client_cors_origins");

            migrationBuilder.DropTable(
                name: "client_grant_types");

            migrationBuilder.DropTable(
                name: "client_id_prestrictions");

            migrationBuilder.DropTable(
                name: "client_post_logout_redirect_uris");

            migrationBuilder.DropTable(
                name: "client_properties");

            migrationBuilder.DropTable(
                name: "client_redirect_uris");

            migrationBuilder.DropTable(
                name: "client_scopes");

            migrationBuilder.DropTable(
                name: "client_secrets");

            migrationBuilder.DropTable(
                name: "identity_claims");

            migrationBuilder.DropTable(
                name: "api_scopes");

            migrationBuilder.DropTable(
                name: "clients");

            migrationBuilder.DropTable(
                name: "identity_resources");

            migrationBuilder.DropTable(
                name: "api_resources");
        }
    }
}
