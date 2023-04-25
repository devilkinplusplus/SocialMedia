using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class followV2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "HasRequest",
                table: "Follows",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "HasRequest",
                table: "Follows");
        }
    }
}
