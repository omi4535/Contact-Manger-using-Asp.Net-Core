using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Entity.Migrations
{
    /// <inheritdoc />
    public partial class init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPerson = @"
                Create or alter proc GetAllPerson 
                as begin
                Select * from people
                end";
            migrationBuilder.Sql(sp_GetAllPerson);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            string sp_GetAllPerson = @"
                drop proc GetAllPerson ";
            migrationBuilder.Sql(sp_GetAllPerson);
        }
    }
}
