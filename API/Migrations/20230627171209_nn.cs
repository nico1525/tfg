using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class nn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Organizacion_OrganizacionId",
                table: "Vehiculo");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacionId",
                table: "Vehiculo",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Organizacion_OrganizacionId",
                table: "Vehiculo",
                column: "OrganizacionId",
                principalTable: "Organizacion",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Vehiculo_Organizacion_OrganizacionId",
                table: "Vehiculo");

            migrationBuilder.AlterColumn<int>(
                name: "OrganizacionId",
                table: "Vehiculo",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_Vehiculo_Organizacion_OrganizacionId",
                table: "Vehiculo",
                column: "OrganizacionId",
                principalTable: "Organizacion",
                principalColumn: "Id");
        }
    }
}
