using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class init5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_people_CountryId",
                table: "people",
                column: "CountryId");

            migrationBuilder.AddCheckConstraint(
                name: "Chk_fName",
                table: "people",
                sql: "len(FirstName)>3");

            migrationBuilder.AddForeignKey(
                name: "FK_people_countries_CountryId",
                table: "people",
                column: "CountryId",
                principalTable: "countries",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_people_countries_CountryId",
                table: "people");

            migrationBuilder.DropIndex(
                name: "IX_people_CountryId",
                table: "people");

            migrationBuilder.DropCheckConstraint(
                name: "Chk_fName",
                table: "people");
        }
    }
}
