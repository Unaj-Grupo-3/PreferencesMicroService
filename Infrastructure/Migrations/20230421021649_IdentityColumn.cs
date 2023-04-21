using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class IdentityColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "GenderPreference",
                columns: table => new
                {
                    GenderPreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    GenderId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenderPreference", x => x.GenderPreferenceId);
                });

            migrationBuilder.CreateTable(
                name: "InterestCategory",
                columns: table => new
                {
                    InterestCategoryId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InterestCategory", x => x.InterestCategoryId);
                });

            migrationBuilder.CreateTable(
                name: "OverallPreference",
                columns: table => new
                {
                    OverallPreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    SinceAge = table.Column<int>(type: "int", nullable: false),
                    UntilAge = table.Column<int>(type: "int", nullable: false),
                    Distance = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OverallPreference", x => x.OverallPreferenceId);
                });

            migrationBuilder.CreateTable(
                name: "Interest",
                columns: table => new
                {
                    InterestId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InterestCategoryId = table.Column<int>(type: "int", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(50)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Interest", x => x.InterestId);
                    table.ForeignKey(
                        name: "FK_Interest_InterestCategory_InterestCategoryId",
                        column: x => x.InterestCategoryId,
                        principalTable: "InterestCategory",
                        principalColumn: "InterestCategoryId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Preference",
                columns: table => new
                {
                    PreferenceId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(type: "int", nullable: false),
                    InterestId = table.Column<int>(type: "int", nullable: false),
                    OwnInterest = table.Column<bool>(type: "bit", nullable: false),
                    Like = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Preference", x => x.PreferenceId);
                    table.ForeignKey(
                        name: "FK_Preference_Interest_InterestId",
                        column: x => x.InterestId,
                        principalTable: "Interest",
                        principalColumn: "InterestId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Interest_InterestCategoryId",
                table: "Interest",
                column: "InterestCategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Preference_InterestId",
                table: "Preference",
                column: "InterestId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GenderPreference");

            migrationBuilder.DropTable(
                name: "OverallPreference");

            migrationBuilder.DropTable(
                name: "Preference");

            migrationBuilder.DropTable(
                name: "Interest");

            migrationBuilder.DropTable(
                name: "InterestCategory");
        }
    }
}
