using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventAssos.Infra.Migrations
{
    /// <inheritdoc />
    public partial class EventSeed : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "InscriptionId",
                table: "Inscriptions",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<bool>(
                name: "listeAttenteActive",
                table: "Inscriptions",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "InscriptionId",
                table: "Inscriptions");

            migrationBuilder.DropColumn(
                name: "listeAttenteActive",
                table: "Inscriptions");
        }
    }
}
