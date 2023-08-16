using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMedia.Persistance.Migrations
{
    /// <inheritdoc />
    public partial class ranks_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRank_AspNetUsers_UserId",
                table: "UserRank");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRank_Ranks_RankId",
                table: "UserRank");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRank",
                table: "UserRank");

            migrationBuilder.RenameTable(
                name: "UserRank",
                newName: "UserRanks");

            migrationBuilder.RenameIndex(
                name: "IX_UserRank_UserId",
                table: "UserRanks",
                newName: "IX_UserRanks_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRanks",
                table: "UserRanks",
                columns: new[] { "RankId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRanks_AspNetUsers_UserId",
                table: "UserRanks",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRanks_Ranks_RankId",
                table: "UserRanks",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_UserRanks_AspNetUsers_UserId",
                table: "UserRanks");

            migrationBuilder.DropForeignKey(
                name: "FK_UserRanks_Ranks_RankId",
                table: "UserRanks");

            migrationBuilder.DropPrimaryKey(
                name: "PK_UserRanks",
                table: "UserRanks");

            migrationBuilder.RenameTable(
                name: "UserRanks",
                newName: "UserRank");

            migrationBuilder.RenameIndex(
                name: "IX_UserRanks_UserId",
                table: "UserRank",
                newName: "IX_UserRank_UserId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_UserRank",
                table: "UserRank",
                columns: new[] { "RankId", "UserId" });

            migrationBuilder.AddForeignKey(
                name: "FK_UserRank_AspNetUsers_UserId",
                table: "UserRank",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_UserRank_Ranks_RankId",
                table: "UserRank",
                column: "RankId",
                principalTable: "Ranks",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
