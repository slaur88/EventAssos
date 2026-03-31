using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAssos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class AddImgToEvent : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "start",
                table: "Events",
                newName: "Start");

            migrationBuilder.RenameColumn(
                name: "lieu",
                table: "Events",
                newName: "Lieu");

            migrationBuilder.RenameColumn(
                name: "end",
                table: "Events",
                newName: "End");

            migrationBuilder.AddColumn<byte[]>(
                name: "Img",
                table: "Events",
                type: "varbinary(max)",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Img",
                table: "Events");

            migrationBuilder.RenameColumn(
                name: "Start",
                table: "Events",
                newName: "start");

            migrationBuilder.RenameColumn(
                name: "Lieu",
                table: "Events",
                newName: "lieu");

            migrationBuilder.RenameColumn(
                name: "End",
                table: "Events",
                newName: "end");
        }
    }
}
