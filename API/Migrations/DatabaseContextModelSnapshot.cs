﻿// <auto-generated />
using System;
using API.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace API.Migrations
{
    [DbContext(typeof(DatabaseContext))]
    partial class DatabaseContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.5")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("API.Models.Consumos.EmisionesFugitivasConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<float>("Consumo")
                        .HasColumnType("real");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("EmisionesFugitivasId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("Gas")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("Recarga")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("EmisionesFugitivasId");

                    b.ToTable("EmisionesFugitivasConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.InstalacionesFijasConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("real");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("InstalacionesFijasId")
                        .HasColumnType("int");

                    b.Property<string>("TipoCombustible")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("InstalacionesFijasId");

                    b.ToTable("InstalacionesFijasConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.MaquinariaConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("real");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("MaquinariaId")
                        .HasColumnType("int");

                    b.Property<string>("TipoCombustible")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("MaquinariaId");

                    b.ToTable("MaquinariaConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.TransporteConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("real");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<int>("TransporteId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("TransporteId");

                    b.ToTable("TransporteConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.VehiculoConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("real");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime2");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime2");

                    b.Property<string>("TipoCombustible")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("VehiculoConsumo");
                });

            modelBuilder.Entity("API.Models.EmisionesFugitivas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("NombreEquipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("EmisionesFugitivas");
                });

            modelBuilder.Entity("API.Models.InstalacionesFijas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("InstalacionesFijas");
                });

            modelBuilder.Entity("API.Models.Maquinaria", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.Property<string>("TipoMaquinaria")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Maquinaria");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionMaquinaria", b =>
                {
                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("agricola")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("forestal")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("industrial")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FactorEmisionMaquinaria");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionTransporte", b =>
                {
                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("aereo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("ferrocarril")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("maritimo")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FactorEmisionTransporte");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionVehiculo", b =>
                {
                    b.Property<string>("L")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("M1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N1")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("N2N3M2M3")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FactorEmisionVehiculo");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionesFugitivas", b =>
                {
                    b.Property<string>("FormulaQuimica")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("PCA")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FactorEmisionesFugitivas");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorInstalacionesFijas", b =>
                {
                    b.Property<string>("Tipo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("factor")
                        .HasColumnType("nvarchar(max)");

                    b.ToTable("FactorInstalacionesFijas");
                });

            modelBuilder.Entity("API.Models.Organizacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contraseña")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Direccion")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NombreOrg")
                        .HasColumnType("nvarchar(450)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("NombreOrg")
                        .IsUnique()
                        .HasFilter("[NombreOrg] IS NOT NULL");

                    b.ToTable("Organizacion");
                });

            modelBuilder.Entity("API.Models.Transporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CombustibleTransporte")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.Property<string>("TipoTransporte")
                        .HasColumnType("nvarchar(max)");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Transporte");
                });

            modelBuilder.Entity("API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Contraseña")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Email")
                        .HasColumnType("nvarchar(450)");

                    b.Property<string>("NombreApellidos")
                        .HasColumnType("nvarchar(max)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique()
                        .HasFilter("[Email] IS NOT NULL");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("API.Models.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("CategoriaVehiculo")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Edificio")
                        .HasColumnType("nvarchar(max)");

                    b.Property<string>("Matricula")
                        .HasColumnType("nvarchar(450)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Matricula")
                        .IsUnique()
                        .HasFilter("[Matricula] IS NOT NULL");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Vehiculo");
                });

            modelBuilder.Entity("API.Models.Consumos.EmisionesFugitivasConsumo", b =>
                {
                    b.HasOne("API.Models.EmisionesFugitivas", "EmisionesFugitivasRef")
                        .WithMany()
                        .HasForeignKey("EmisionesFugitivasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmisionesFugitivasRef");
                });

            modelBuilder.Entity("API.Models.Consumos.InstalacionesFijasConsumo", b =>
                {
                    b.HasOne("API.Models.InstalacionesFijas", "InstalacionesFijasRef")
                        .WithMany()
                        .HasForeignKey("InstalacionesFijasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstalacionesFijasRef");
                });

            modelBuilder.Entity("API.Models.Consumos.MaquinariaConsumo", b =>
                {
                    b.HasOne("API.Models.Maquinaria", "MaquinariaRef")
                        .WithMany()
                        .HasForeignKey("MaquinariaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("MaquinariaRef");
                });

            modelBuilder.Entity("API.Models.Consumos.TransporteConsumo", b =>
                {
                    b.HasOne("API.Models.Transporte", "TransporteRef")
                        .WithMany()
                        .HasForeignKey("TransporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("TransporteRef");
                });

            modelBuilder.Entity("API.Models.Consumos.VehiculoConsumo", b =>
                {
                    b.HasOne("API.Models.Vehiculo", "VehiculoRef")
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("VehiculoRef");
                });

            modelBuilder.Entity("API.Models.EmisionesFugitivas", b =>
                {
                    b.HasOne("API.Models.Organizacion", "OrganizacionRef")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizacionRef");
                });

            modelBuilder.Entity("API.Models.InstalacionesFijas", b =>
                {
                    b.HasOne("API.Models.Organizacion", "OrganizacionRef")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizacionRef");
                });

            modelBuilder.Entity("API.Models.Maquinaria", b =>
                {
                    b.HasOne("API.Models.Organizacion", "OrganizacionRef")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizacionRef");
                });

            modelBuilder.Entity("API.Models.Transporte", b =>
                {
                    b.HasOne("API.Models.Organizacion", "OrganizacionRef")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizacionRef");
                });

            modelBuilder.Entity("API.Models.Usuario", b =>
                {
                    b.HasOne("API.Models.Organizacion", "OrganizacionRef")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizacionRef");
                });

            modelBuilder.Entity("API.Models.Vehiculo", b =>
                {
                    b.HasOne("API.Models.Organizacion", "OrganizacionRef")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("OrganizacionRef");
                });
#pragma warning restore 612, 618
        }
    }
}
