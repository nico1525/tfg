using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace API.Migrations
{
    /// <inheritdoc />
    public partial class @new : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmisionesFugitivas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    NombreEquipo = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmisionesFugitivas", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmisionesFugitivasConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    Gas = table.Column<string>(type: "longtext", nullable: true),
                    Recarga = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    EmisionesFugitivasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmisionesFugitivasConsumo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FactorEmisionElectricidad",
                columns: table => new
                {
                    Comercializadora = table.Column<string>(type: "longtext", nullable: false),
                    factor = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FactorEmisionesFugitivas",
                columns: table => new
                {
                    Nombre = table.Column<string>(type: "longtext", nullable: false),
                    FormulaQuimica = table.Column<string>(type: "longtext", nullable: true),
                    PCA = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FactorEmisionMaquinaria",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "longtext", nullable: false),
                    agricola = table.Column<string>(type: "longtext", nullable: true),
                    forestal = table.Column<string>(type: "longtext", nullable: true),
                    industrial = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FactorEmisionTransporte",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "longtext", nullable: false),
                    ferrocarril = table.Column<string>(type: "longtext", nullable: true),
                    maritimo = table.Column<string>(type: "longtext", nullable: true),
                    aereo = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FactorEmisionVehiculo",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "longtext", nullable: false),
                    M1 = table.Column<string>(type: "longtext", nullable: true),
                    N1 = table.Column<string>(type: "longtext", nullable: true),
                    N2N3M2M3 = table.Column<string>(type: "longtext", nullable: true),
                    L = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "FactorInstalacionesFijas",
                columns: table => new
                {
                    Tipo = table.Column<string>(type: "longtext", nullable: true),
                    factor = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstalacionesFijas",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    Nombre = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalacionesFijas", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstalacionesFijasConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    CantidadCombustible = table.Column<int>(type: "int", nullable: false),
                    TipoCombustible = table.Column<string>(type: "longtext", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    InstalacionesFijasId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InstalacionesFijasConsumo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Maquinaria",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    Nombre = table.Column<string>(type: "longtext", nullable: true),
                    TipoMaquinaria = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maquinaria", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaquinariaConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    CantidadCombustible = table.Column<int>(type: "int", nullable: false),
                    TipoCombustible = table.Column<string>(type: "longtext", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    MaquinariaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MaquinariaConsumo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Organizacion",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreOrg = table.Column<string>(type: "varchar(255)", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    Direccion = table.Column<string>(type: "longtext", nullable: true),
                    Contraseña = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Organizacion", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "OtrosConsumos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    Nombre = table.Column<string>(type: "longtext", nullable: true),
                    CategoriaActividad = table.Column<string>(type: "longtext", nullable: true),
                    CantidadConsumo = table.Column<int>(type: "int", nullable: false),
                    FactorEmision = table.Column<float>(type: "float", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtrosConsumos", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Transporte",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    TipoTransporte = table.Column<string>(type: "longtext", nullable: true),
                    CombustibleTransporte = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transporte", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransporteConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    CantidadCombustible = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporteConsumo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Usuario",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    NombreApellidos = table.Column<string>(type: "longtext", nullable: true),
                    Email = table.Column<string>(type: "varchar(255)", nullable: true),
                    Contraseña = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false),
                    Role = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuario", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Vehiculo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    Matricula = table.Column<string>(type: "varchar(255)", nullable: true),
                    CategoriaVehiculo = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vehiculo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VehiculoConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    CantidadCombustible = table.Column<int>(type: "int", nullable: false),
                    TipoCombustible = table.Column<string>(type: "longtext", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    VehiculoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_VehiculoConsumo", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Organizacion_Email",
                table: "Organizacion",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Organizacion_NombreOrg",
                table: "Organizacion",
                column: "NombreOrg",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_Matricula",
                table: "Vehiculo",
                column: "Matricula",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EmisionesFugitivas");

            migrationBuilder.DropTable(
                name: "EmisionesFugitivasConsumo");

            migrationBuilder.DropTable(
                name: "FactorEmisionElectricidad");

            migrationBuilder.DropTable(
                name: "FactorEmisionesFugitivas");

            migrationBuilder.DropTable(
                name: "FactorEmisionMaquinaria");

            migrationBuilder.DropTable(
                name: "FactorEmisionTransporte");

            migrationBuilder.DropTable(
                name: "FactorEmisionVehiculo");

            migrationBuilder.DropTable(
                name: "FactorInstalacionesFijas");

            migrationBuilder.DropTable(
                name: "InstalacionesFijas");

            migrationBuilder.DropTable(
                name: "InstalacionesFijasConsumo");

            migrationBuilder.DropTable(
                name: "Maquinaria");

            migrationBuilder.DropTable(
                name: "MaquinariaConsumo");

            migrationBuilder.DropTable(
                name: "Organizacion");

            migrationBuilder.DropTable(
                name: "OtrosConsumos");

            migrationBuilder.DropTable(
                name: "Transporte");

            migrationBuilder.DropTable(
                name: "TransporteConsumo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "VehiculoConsumo");
        }
    }
}
