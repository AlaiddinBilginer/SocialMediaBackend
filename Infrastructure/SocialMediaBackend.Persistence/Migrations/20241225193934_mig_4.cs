using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SocialMediaBackend.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class mig_4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FallowingCount",
                table: "AspNetUsers",
                newName: "FollowingCount");

            migrationBuilder.RenameColumn(
                name: "FallowersCount",
                table: "AspNetUsers",
                newName: "FollowersCount");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "FollowingCount",
                table: "AspNetUsers",
                newName: "FallowingCount");

            migrationBuilder.RenameColumn(
                name: "FollowersCount",
                table: "AspNetUsers",
                newName: "FallowersCount");
        }
    }
}
