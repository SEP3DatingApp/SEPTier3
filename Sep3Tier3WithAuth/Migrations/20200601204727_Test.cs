using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

namespace Sep3Tier3WithAuth.Migrations
{
    public partial class Test : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Interactions",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    InteractionName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interactions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PersonSexualities",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    SexualityName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PersonSexualities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Username = table.Column<string>(nullable: true),
                    PasswordHash = table.Column<byte[]>(nullable: true),
                    PasswordSalt = table.Column<byte[]>(nullable: true),
                    Discriminator = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    Surname = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    Gender = table.Column<string>(nullable: true),
                    PersonSexualityId = table.Column<int>(nullable: true),
                    PicRef = table.Column<string>(nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Users_PersonSexualities_PersonSexualityId",
                        column: x => x.PersonSexualityId,
                        principalTable: "PersonSexualities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LikeReject",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fisher1Id = table.Column<int>(nullable: false),
                    Fisher2Id = table.Column<int>(nullable: false),
                    InteractionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LikeReject", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LikeReject_Users_Fisher1Id",
                        column: x => x.Fisher1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeReject_Users_Fisher2Id",
                        column: x => x.Fisher2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LikeReject_Interactions_InteractionsId",
                        column: x => x.InteractionsId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PeopleWhoMatched",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Fisher1Id = table.Column<int>(nullable: false),
                    Fisher2Id = table.Column<int>(nullable: false),
                    InteractionsId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PeopleWhoMatched", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PeopleWhoMatched_Users_Fisher1Id",
                        column: x => x.Fisher1Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleWhoMatched_Users_Fisher2Id",
                        column: x => x.Fisher2Id,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PeopleWhoMatched_Interactions_InteractionsId",
                        column: x => x.InteractionsId,
                        principalTable: "Interactions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_LikeReject_Fisher1Id",
                table: "LikeReject",
                column: "Fisher1Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikeReject_Fisher2Id",
                table: "LikeReject",
                column: "Fisher2Id");

            migrationBuilder.CreateIndex(
                name: "IX_LikeReject_InteractionsId",
                table: "LikeReject",
                column: "InteractionsId");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleWhoMatched_Fisher1Id",
                table: "PeopleWhoMatched",
                column: "Fisher1Id");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleWhoMatched_Fisher2Id",
                table: "PeopleWhoMatched",
                column: "Fisher2Id");

            migrationBuilder.CreateIndex(
                name: "IX_PeopleWhoMatched_InteractionsId",
                table: "PeopleWhoMatched",
                column: "InteractionsId");

            migrationBuilder.CreateIndex(
                name: "IX_Users_PersonSexualityId",
                table: "Users",
                column: "PersonSexualityId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LikeReject");

            migrationBuilder.DropTable(
                name: "PeopleWhoMatched");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Interactions");

            migrationBuilder.DropTable(
                name: "PersonSexualities");
        }
    }
}
