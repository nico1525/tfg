﻿// <auto-generated />
using System;
using API.Models.Context;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
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
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("API.Models.Consumos.ElectricidadConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Comercializadora")
                        .HasColumnType("longtext");

                    b.Property<int>("ComercializadoraId")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<int>("ElectricidadId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("Kwh")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("ElectricidadId");

                    b.ToTable("ElectricidadConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.EmisionesFugitivasConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<int>("EmisionesFugitivasId")
                        .HasColumnType("int");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Gas")
                        .HasColumnType("longtext");

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

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("InstalacionesFijasId")
                        .HasColumnType("int");

                    b.Property<string>("TipoCombustible")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("InstalacionesFijasId");

                    b.ToTable("InstalacionesFijasConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.MaquinariaConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<int>("MaquinariaId")
                        .HasColumnType("int");

                    b.Property<string>("TipoCombustible")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("MaquinariaId");

                    b.ToTable("MaquinariaConsumo");
                });

            modelBuilder.Entity("API.Models.Consumos.OtrosConsumos", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadConsumo")
                        .HasColumnType("int");

                    b.Property<string>("CategoriaActividad")
                        .HasColumnType("longtext");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<float>("FactorEmision")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("OtrosConsumos");
                });

            modelBuilder.Entity("API.Models.Consumos.TransporteConsumo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

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

                    b.Property<int>("CantidadCombustible")
                        .HasColumnType("int");

                    b.Property<float>("Consumo")
                        .HasColumnType("float");

                    b.Property<DateTime>("FechaFin")
                        .HasColumnType("datetime(6)");

                    b.Property<DateTime>("FechaInicio")
                        .HasColumnType("datetime(6)");

                    b.Property<string>("TipoCombustible")
                        .HasColumnType("longtext");

                    b.Property<int>("VehiculoId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("VehiculoId");

                    b.ToTable("VehiculoConsumo");
                });

            modelBuilder.Entity("API.Models.Electricidad", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Descripcion")
                        .HasColumnType("longtext");

                    b.Property<string>("Dispositivo")
                        .HasColumnType("longtext");

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Electricidad");
                });

            modelBuilder.Entity("API.Models.EmisionesFugitivas", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<string>("NombreEquipo")
                        .HasColumnType("longtext");

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

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

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

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .HasColumnType("longtext");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.Property<string>("TipoMaquinaria")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Maquinaria");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionElectricidad", b =>
                {
                    b.Property<string>("Comercializadora")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<int>("Id")
                        .HasColumnType("int");

                    b.Property<string>("factor")
                        .HasColumnType("longtext");

                    b.ToTable("FactorEmisionElectricidad");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionMaquinaria", b =>
                {
                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("agricola")
                        .HasColumnType("longtext");

                    b.Property<string>("forestal")
                        .HasColumnType("longtext");

                    b.Property<string>("industrial")
                        .HasColumnType("longtext");

                    b.ToTable("FactorEmisionMaquinaria");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionTransporte", b =>
                {
                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("aereo")
                        .HasColumnType("longtext");

                    b.Property<string>("ferrocarril")
                        .HasColumnType("longtext");

                    b.Property<string>("maritimo")
                        .HasColumnType("longtext");

                    b.ToTable("FactorEmisionTransporte");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionVehiculo", b =>
                {
                    b.Property<string>("L")
                        .HasColumnType("longtext");

                    b.Property<string>("M1")
                        .HasColumnType("longtext");

                    b.Property<string>("N1")
                        .HasColumnType("longtext");

                    b.Property<string>("N2N3M2M3")
                        .HasColumnType("longtext");

                    b.Property<string>("Tipo")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.ToTable("FactorEmisionVehiculo");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorEmisionesFugitivas", b =>
                {
                    b.Property<string>("FormulaQuimica")
                        .HasColumnType("longtext");

                    b.Property<string>("Nombre")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("PCA")
                        .HasColumnType("longtext");

                    b.ToTable("FactorEmisionesFugitivas");
                });

            modelBuilder.Entity("API.Models.Master_Tables.FactorInstalacionesFijas", b =>
                {
                    b.Property<string>("Tipo")
                        .HasColumnType("longtext");

                    b.Property<string>("factor")
                        .HasColumnType("longtext");

                    b.ToTable("FactorInstalacionesFijas");
                });

            modelBuilder.Entity("API.Models.Organizacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contraseña")
                        .HasColumnType("longtext");

                    b.Property<string>("Direccion")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NombreOrg")
                        .HasColumnType("varchar(255)");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("NombreOrg")
                        .IsUnique();

                    b.ToTable("Organizacion");
                });

            modelBuilder.Entity("API.Models.Transporte", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CombustibleTransporte")
                        .HasColumnType("longtext");

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.Property<string>("TipoTransporte")
                        .HasColumnType("longtext");

                    b.HasKey("Id");

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Transporte");
                });

            modelBuilder.Entity("API.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("Contraseña")
                        .HasColumnType("longtext");

                    b.Property<string>("Email")
                        .HasColumnType("varchar(255)");

                    b.Property<string>("NombreApellidos")
                        .HasColumnType("longtext");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.Property<int>("Role")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Email")
                        .IsUnique();

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Usuario");
                });

            modelBuilder.Entity("API.Models.Vehiculo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int");

                    b.Property<string>("CategoriaVehiculo")
                        .HasColumnType("longtext");

                    b.Property<string>("Edificio")
                        .HasColumnType("longtext");

                    b.Property<string>("Matricula")
                        .HasColumnType("varchar(255)");

                    b.Property<int>("OrganizacionId")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.HasIndex("Matricula")
                        .IsUnique();

                    b.HasIndex("OrganizacionId");

                    b.ToTable("Vehiculo");
                });

            modelBuilder.Entity("API.Models.Consumos.ElectricidadConsumo", b =>
                {
                    b.HasOne("API.Models.Electricidad", "Electricidad")
                        .WithMany()
                        .HasForeignKey("ElectricidadId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Electricidad");
                });

            modelBuilder.Entity("API.Models.Consumos.EmisionesFugitivasConsumo", b =>
                {
                    b.HasOne("API.Models.EmisionesFugitivas", "EmisionesFugitivas")
                        .WithMany()
                        .HasForeignKey("EmisionesFugitivasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("EmisionesFugitivas");
                });

            modelBuilder.Entity("API.Models.Consumos.InstalacionesFijasConsumo", b =>
                {
                    b.HasOne("API.Models.InstalacionesFijas", "InstalacionesFijas")
                        .WithMany()
                        .HasForeignKey("InstalacionesFijasId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("InstalacionesFijas");
                });

            modelBuilder.Entity("API.Models.Consumos.MaquinariaConsumo", b =>
                {
                    b.HasOne("API.Models.Maquinaria", "Maquinaria")
                        .WithMany()
                        .HasForeignKey("MaquinariaId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Maquinaria");
                });

            modelBuilder.Entity("API.Models.Consumos.OtrosConsumos", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.Consumos.TransporteConsumo", b =>
                {
                    b.HasOne("API.Models.Transporte", "Tranporte")
                        .WithMany()
                        .HasForeignKey("TransporteId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Tranporte");
                });

            modelBuilder.Entity("API.Models.Consumos.VehiculoConsumo", b =>
                {
                    b.HasOne("API.Models.Vehiculo", "Vehiculo")
                        .WithMany()
                        .HasForeignKey("VehiculoId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Vehiculo");
                });

            modelBuilder.Entity("API.Models.Electricidad", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.EmisionesFugitivas", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.InstalacionesFijas", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.Maquinaria", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.Transporte", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.Usuario", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });

            modelBuilder.Entity("API.Models.Vehiculo", b =>
                {
                    b.HasOne("API.Models.Organizacion", "Organizacion")
                        .WithMany()
                        .HasForeignKey("OrganizacionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Organizacion");
                });
#pragma warning restore 612, 618
        }
    }
}
