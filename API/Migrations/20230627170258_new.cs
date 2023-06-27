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
                name: "FactorEmisionElectricidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false),
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
                name: "Electricidad",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Edificio = table.Column<string>(type: "longtext", nullable: true),
                    Dispositivo = table.Column<string>(type: "longtext", nullable: true),
                    Descripcion = table.Column<string>(type: "longtext", nullable: true),
                    OrganizacionId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Electricidad", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Electricidad_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
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
                    table.ForeignKey(
                        name: "FK_EmisionesFugitivas_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_InstalacionesFijas_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Maquinaria_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_OtrosConsumos_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Transporte_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Usuario_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
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
                    table.ForeignKey(
                        name: "FK_Vehiculo_Organizacion_OrganizacionId",
                        column: x => x.OrganizacionId,
                        principalTable: "Organizacion",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "ElectricidadConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Kwh = table.Column<int>(type: "int", nullable: false),
                    ComercializadoraId = table.Column<int>(type: "int", nullable: false),
                    Comercializadora = table.Column<string>(type: "longtext", nullable: true),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    ElectricidadId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ElectricidadConsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ElectricidadConsumo_Electricidad_ElectricidadId",
                        column: x => x.ElectricidadId,
                        principalTable: "Electricidad",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "EmisionesFugitivasConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
                    table.ForeignKey(
                        name: "FK_EmisionesFugitivasConsumo_EmisionesFugitivas_EmisionesFugiti~",
                        column: x => x.EmisionesFugitivasId,
                        principalTable: "EmisionesFugitivas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "InstalacionesFijasConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
                    table.ForeignKey(
                        name: "FK_InstalacionesFijasConsumo_InstalacionesFijas_InstalacionesFi~",
                        column: x => x.InstalacionesFijasId,
                        principalTable: "InstalacionesFijas",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "MaquinariaConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
                    table.ForeignKey(
                        name: "FK_MaquinariaConsumo_Maquinaria_MaquinariaId",
                        column: x => x.MaquinariaId,
                        principalTable: "Maquinaria",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "TransporteConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    CantidadCombustible = table.Column<int>(type: "int", nullable: false),
                    FechaInicio = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    FechaFin = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Consumo = table.Column<float>(type: "float", nullable: false),
                    TransporteId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransporteConsumo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransporteConsumo_Transporte_TransporteId",
                        column: x => x.TransporteId,
                        principalTable: "Transporte",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "VehiculoConsumo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
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
                    table.ForeignKey(
                        name: "FK_VehiculoConsumo_Vehiculo_VehiculoId",
                        column: x => x.VehiculoId,
                        principalTable: "Vehiculo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Electricidad_OrganizacionId",
                table: "Electricidad",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_ElectricidadConsumo_ElectricidadId",
                table: "ElectricidadConsumo",
                column: "ElectricidadId");

            migrationBuilder.CreateIndex(
                name: "IX_EmisionesFugitivas_OrganizacionId",
                table: "EmisionesFugitivas",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_EmisionesFugitivasConsumo_EmisionesFugitivasId",
                table: "EmisionesFugitivasConsumo",
                column: "EmisionesFugitivasId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalacionesFijas_OrganizacionId",
                table: "InstalacionesFijas",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_InstalacionesFijasConsumo_InstalacionesFijasId",
                table: "InstalacionesFijasConsumo",
                column: "InstalacionesFijasId");

            migrationBuilder.CreateIndex(
                name: "IX_Maquinaria_OrganizacionId",
                table: "Maquinaria",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_MaquinariaConsumo_MaquinariaId",
                table: "MaquinariaConsumo",
                column: "MaquinariaId");

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
                name: "IX_OtrosConsumos_OrganizacionId",
                table: "OtrosConsumos",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Transporte_OrganizacionId",
                table: "Transporte",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_TransporteConsumo_TransporteId",
                table: "TransporteConsumo",
                column: "TransporteId");

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_Email",
                table: "Usuario",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Usuario_OrganizacionId",
                table: "Usuario",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_Matricula",
                table: "Vehiculo",
                column: "Matricula",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Vehiculo_OrganizacionId",
                table: "Vehiculo",
                column: "OrganizacionId");

            migrationBuilder.CreateIndex(
                name: "IX_VehiculoConsumo_VehiculoId",
                table: "VehiculoConsumo",
                column: "VehiculoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ElectricidadConsumo");

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
                name: "InstalacionesFijasConsumo");

            migrationBuilder.DropTable(
                name: "MaquinariaConsumo");

            migrationBuilder.DropTable(
                name: "OtrosConsumos");

            migrationBuilder.DropTable(
                name: "TransporteConsumo");

            migrationBuilder.DropTable(
                name: "Usuario");

            migrationBuilder.DropTable(
                name: "VehiculoConsumo");

            migrationBuilder.DropTable(
                name: "Electricidad");

            migrationBuilder.DropTable(
                name: "EmisionesFugitivas");

            migrationBuilder.DropTable(
                name: "InstalacionesFijas");

            migrationBuilder.DropTable(
                name: "Maquinaria");

            migrationBuilder.DropTable(
                name: "Transporte");

            migrationBuilder.DropTable(
                name: "Vehiculo");

            migrationBuilder.DropTable(
                name: "Organizacion");
        }
    }
}
