using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Onym.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "public");

            migrationBuilder.CreateTable(
                name: "media",
                schema: "public",
                columns: table => new
                {
                    media_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    file_link = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_media", x => x.media_id);
                });

            migrationBuilder.CreateTable(
                name: "roles",
                schema: "public",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    role_normalized_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    role_concurrency_stamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_roles", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "statuses",
                schema: "public",
                columns: table => new
                {
                    status_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    status_name = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses", x => x.status_id);
                });

            migrationBuilder.CreateTable(
                name: "tags",
                schema: "public",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    tag_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    tag_rating_counting = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "true")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tags", x => x.tag_id);
                });

            migrationBuilder.CreateTable(
                name: "users",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_profile_picture = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "1"),
                    user_registration_date = table.Column<DateTime>(type: "timestamp(2) without time zone", precision: 2, nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    user_rating_total = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "0"),
                    user_name = table.Column<string>(type: "character varying(20)", maxLength: 20, nullable: false),
                    normalized_user_name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    user_email = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    normalized_user_email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    user_email_confirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    user_password_hash = table.Column<string>(type: "text", nullable: false),
                    user_security_stamp = table.Column<string>(type: "text", nullable: true),
                    user_concurrency_stamp = table.Column<string>(type: "text", nullable: true),
                    user_phone_number = table.Column<string>(type: "text", nullable: true),
                    user_phone_number_confirmed = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    user_two_factors_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    user_lockout_end = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    user_lockout_enabled = table.Column<bool>(type: "boolean", nullable: false, defaultValueSql: "false"),
                    user_access_failed_tally = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users", x => x.user_id);
                    table.ForeignKey(
                        name: "FK_profile_picture",
                        column: x => x.user_profile_picture,
                        principalSchema: "public",
                        principalTable: "media",
                        principalColumn: "media_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "role_claims",
                schema: "public",
                columns: table => new
                {
                    role_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    role_claim_type = table.Column<string>(type: "text", nullable: true),
                    role_claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_role_claims", x => x.role_id);
                    table.ForeignKey(
                        name: "FK_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publications",
                schema: "public",
                columns: table => new
                {
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_name = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                    publication_content = table.Column<string>(type: "character varying(3000)", maxLength: 3000, nullable: false),
                    publication_creation_date = table.Column<DateTime>(type: "timestamp(2) without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    publication_rating_total = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "0"),
                    publication_status = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publications", x => x.publication_id);
                    table.ForeignKey(
                        name: "FK_publication author",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publication_status",
                        column: x => x.publication_status,
                        principalSchema: "public",
                        principalTable: "statuses",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_claims",
                schema: "public",
                columns: table => new
                {
                    user_claim_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    user_claim_type = table.Column<string>(type: "text", nullable: true),
                    user_claim_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_claims", x => x.user_claim_id);
                    table.ForeignKey(
                        name: "FK_user_claims",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_logins",
                schema: "public",
                columns: table => new
                {
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    provider_key = table.Column<string>(type: "text", nullable: false),
                    provider_display_name = table.Column<string>(type: "text", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_logins", x => new { x.login_provider, x.provider_key });
                    table.ForeignKey(
                        name: "FK_user_logins",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_roles",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_roles", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_role_id",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "roles",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user_roles",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_statuses",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    role_id = table.Column<int>(type: "integer", nullable: false),
                    ExpirationTime = table.Column<DateTime>(type: "timestamp(2) without time zone", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_statuses", x => new { x.user_id, x.role_id });
                    table.ForeignKey(
                        name: "FK_status-user",
                        column: x => x.role_id,
                        principalSchema: "public",
                        principalTable: "statuses",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_user-statuses",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "user_tokens",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    login_provider = table.Column<string>(type: "text", nullable: false),
                    user_token_name = table.Column<string>(type: "text", nullable: false),
                    user_token_value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_tokens", x => new { x.user_id, x.login_provider, x.user_token_name });
                    table.ForeignKey(
                        name: "FK_user_tokens",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comments",
                schema: "public",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityAlwaysColumn),
                    parental_comment_id = table.Column<int>(type: "integer", nullable: true),
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    comment_content = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    comment_creation_date = table.Column<DateTime>(type: "timestamp(2) without time zone", nullable: false, defaultValueSql: "CURRENT_TIMESTAMP"),
                    comment_rating_total = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "0"),
                    comment_status = table.Column<int>(type: "integer", nullable: false, defaultValueSql: "1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comments", x => x.comment_id);
                    table.ForeignKey(
                        name: "FK_comment_author",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment_status",
                        column: x => x.comment_status,
                        principalSchema: "public",
                        principalTable: "statuses",
                        principalColumn: "status_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_comment-publication",
                        column: x => x.publication_id,
                        principalSchema: "public",
                        principalTable: "publications",
                        principalColumn: "publication_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_parental_comment",
                        column: x => x.parental_comment_id,
                        principalSchema: "public",
                        principalTable: "comments",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "favorite",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_favorite", x => new { x.user_id, x.publication_id });
                    table.ForeignKey(
                        name: "FK_publications-users_favorite",
                        column: x => x.publication_id,
                        principalSchema: "public",
                        principalTable: "publications",
                        principalColumn: "publication_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users-publication_favorite",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publication_media",
                schema: "public",
                columns: table => new
                {
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    media_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publication_media", x => new { x.publication_id, x.media_id });
                    table.ForeignKey(
                        name: "FK_media-publication",
                        column: x => x.media_id,
                        principalSchema: "public",
                        principalTable: "media",
                        principalColumn: "media_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_publication-media",
                        column: x => x.publication_id,
                        principalSchema: "public",
                        principalTable: "publications",
                        principalColumn: "publication_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publication_rating_tally",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false),
                    publication_rating = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publication_rating_tally", x => new { x.user_id, x.publication_id });
                    table.ForeignKey(
                        name: "FK_publications-users_rating",
                        column: x => x.publication_id,
                        principalSchema: "public",
                        principalTable: "publications",
                        principalColumn: "publication_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users-publications_rating",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "publication_tags",
                schema: "public",
                columns: table => new
                {
                    tag_id = table.Column<int>(type: "integer", nullable: false),
                    publication_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_publication_tags", x => new { x.tag_id, x.publication_id });
                    table.ForeignKey(
                        name: "FK_publication-tag",
                        column: x => x.publication_id,
                        principalSchema: "public",
                        principalTable: "publications",
                        principalColumn: "publication_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_tag-publication",
                        column: x => x.tag_id,
                        principalSchema: "public",
                        principalTable: "tags",
                        principalColumn: "tag_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_media",
                schema: "public",
                columns: table => new
                {
                    comment_id = table.Column<int>(type: "integer", nullable: false),
                    media_id = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment_media", x => new { x.comment_id, x.media_id });
                    table.ForeignKey(
                        name: "FK_comment-media",
                        column: x => x.comment_id,
                        principalSchema: "public",
                        principalTable: "comments",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_media-comment",
                        column: x => x.media_id,
                        principalSchema: "public",
                        principalTable: "media",
                        principalColumn: "media_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "comment_rating_tally",
                schema: "public",
                columns: table => new
                {
                    user_id = table.Column<int>(type: "integer", nullable: false),
                    comment_id = table.Column<int>(type: "integer", nullable: false),
                    comment_rating = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_comment_rating_tally", x => new { x.user_id, x.comment_id });
                    table.ForeignKey(
                        name: "FK_comment-users_rating",
                        column: x => x.comment_id,
                        principalSchema: "public",
                        principalTable: "comments",
                        principalColumn: "comment_id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_users-comment_rating",
                        column: x => x.user_id,
                        principalSchema: "public",
                        principalTable: "users",
                        principalColumn: "user_id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_comment_media_media_id",
                schema: "public",
                table: "comment_media",
                column: "media_id");

            migrationBuilder.CreateIndex(
                name: "IX_comment_rating_tally_comment_id",
                schema: "public",
                table: "comment_rating_tally",
                column: "comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_comments_comment_status",
                schema: "public",
                table: "comments",
                column: "comment_status");

            migrationBuilder.CreateIndex(
                name: "IX_parental_comment_id",
                schema: "public",
                table: "comments",
                column: "parental_comment_id");

            migrationBuilder.CreateIndex(
                name: "IX_publication_id",
                schema: "public",
                table: "comments",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_id2",
                schema: "public",
                table: "comments",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_favorite_publication_id",
                schema: "public",
                table: "favorite",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "IX_publication_media_media_id",
                schema: "public",
                table: "publication_media",
                column: "media_id");

            migrationBuilder.CreateIndex(
                name: "IX_publication_rating_tally_publication_id",
                schema: "public",
                table: "publication_rating_tally",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "IX_publication_tags_publication_id",
                schema: "public",
                table: "publication_tags",
                column: "publication_id");

            migrationBuilder.CreateIndex(
                name: "IX_publication_author",
                schema: "public",
                table: "publications",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_publication_name",
                schema: "public",
                table: "publications",
                column: "publication_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_publication_status",
                schema: "public",
                table: "publications",
                column: "publication_status");

            migrationBuilder.CreateIndex(
                name: "IX_role_id",
                schema: "public",
                table: "role_claims",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_name",
                schema: "public",
                table: "roles",
                column: "role_normalized_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_tag_name",
                schema: "public",
                table: "tags",
                column: "tag_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_id",
                schema: "public",
                table: "user_claims",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_id1",
                schema: "public",
                table: "user_logins",
                column: "user_id");

            migrationBuilder.CreateIndex(
                name: "IX_role_id1",
                schema: "public",
                table: "user_roles",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_statuses_role_id",
                schema: "public",
                table: "user_statuses",
                column: "role_id");

            migrationBuilder.CreateIndex(
                name: "IX_user_email",
                schema: "public",
                table: "users",
                column: "normalized_user_email");

            migrationBuilder.CreateIndex(
                name: "IX_user_email1",
                schema: "public",
                table: "users",
                column: "user_email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_name",
                schema: "public",
                table: "users",
                column: "normalized_user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_name1",
                schema: "public",
                table: "users",
                column: "user_name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_profile_picture",
                schema: "public",
                table: "users",
                column: "user_profile_picture");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "comment_media",
                schema: "public");

            migrationBuilder.DropTable(
                name: "comment_rating_tally",
                schema: "public");

            migrationBuilder.DropTable(
                name: "favorite",
                schema: "public");

            migrationBuilder.DropTable(
                name: "publication_media",
                schema: "public");

            migrationBuilder.DropTable(
                name: "publication_rating_tally",
                schema: "public");

            migrationBuilder.DropTable(
                name: "publication_tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "role_claims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_claims",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_logins",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_statuses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "user_tokens",
                schema: "public");

            migrationBuilder.DropTable(
                name: "comments",
                schema: "public");

            migrationBuilder.DropTable(
                name: "tags",
                schema: "public");

            migrationBuilder.DropTable(
                name: "roles",
                schema: "public");

            migrationBuilder.DropTable(
                name: "publications",
                schema: "public");

            migrationBuilder.DropTable(
                name: "users",
                schema: "public");

            migrationBuilder.DropTable(
                name: "statuses",
                schema: "public");

            migrationBuilder.DropTable(
                name: "media",
                schema: "public");
        }
    }
}
