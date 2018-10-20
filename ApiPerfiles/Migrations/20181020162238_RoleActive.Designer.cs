﻿// <auto-generated />
using System;
using ApiPerfiles.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace ApiPerfiles.Migrations
{
    [DbContext(typeof(PerfilDbContext))]
    [Migration("20181020162238_RoleActive")]
    partial class RoleActive
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "2.1.4-rtm-31024")
                .HasAnnotation("Relational:MaxIdentifierLength", 128)
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("ApiPerfiles.Models.Aplicacion", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronimo")
                        .HasMaxLength(3);

                    b.Property<bool>("Activo");

                    b.Property<string>("ClaveSeguridad");

                    b.Property<string>("Img")
                        .HasMaxLength(125);

                    b.Property<string>("Nombre")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.ToTable("Aplicaciones");
                });

            modelBuilder.Entity("ApiPerfiles.Models.Modulo", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Acronimo")
                        .HasMaxLength(3);

                    b.Property<int>("AplicacionId");

                    b.Property<string>("Nombre")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AplicacionId");

                    b.ToTable("Modulos");
                });

            modelBuilder.Entity("ApiPerfiles.Models.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("Activo");

                    b.Property<int>("AplicacionId");

                    b.Property<string>("Nombre")
                        .HasMaxLength(20);

                    b.HasKey("Id");

                    b.HasIndex("AplicacionId");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("ApiPerfiles.Models.RoleModuloDefault", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<string>("Modulos");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleModulosDefault");
                });

            modelBuilder.Entity("ApiPerfiles.Models.Usuario", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

                    b.Property<bool>("ActiveDirectory");

                    b.Property<string>("Amaterno")
                        .HasMaxLength(20);

                    b.Property<string>("Apaterno")
                        .HasMaxLength(20);

                    b.Property<string>("Correo")
                        .HasMaxLength(30);

                    b.Property<string>("Departamento")
                        .HasMaxLength(30);

                    b.Property<bool>("Estatus");

                    b.Property<DateTime>("FechaBaja");

                    b.Property<DateTime>("FechaMod");

                    b.Property<DateTime>("FechaReg");

                    b.Property<int>("NSandista");

                    b.Property<string>("Nombres")
                        .HasMaxLength(20);

                    b.Property<string>("Password");

                    b.Property<string>("UserName")
                        .HasMaxLength(20);

                    b.Property<int>("UsuarioBaja");

                    b.Property<int>("UsuarioMod");

                    b.Property<int>("UsuarioReg");

                    b.HasKey("Id");

                    b.ToTable("Usuarios");
                });

            modelBuilder.Entity("ApiPerfiles.Models.UsuarioModulo", b =>
                {
                    b.Property<int>("UsuarioId");

                    b.Property<int>("ModuloId");

                    b.Property<string>("Accion");

                    b.HasKey("UsuarioId", "ModuloId");

                    b.HasIndex("ModuloId");

                    b.ToTable("UsuarioModulos");
                });

            modelBuilder.Entity("ApiPerfiles.Models.UsuarioRole", b =>
                {
                    b.Property<int>("UsuarioId");

                    b.Property<int>("RoleId");

                    b.HasKey("UsuarioId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("UsuarioRoles");
                });

            modelBuilder.Entity("ApiPerfiles.Models.Modulo", b =>
                {
                    b.HasOne("ApiPerfiles.Models.Aplicacion", "Aplicacion")
                        .WithMany()
                        .HasForeignKey("AplicacionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiPerfiles.Models.Role", b =>
                {
                    b.HasOne("ApiPerfiles.Models.Aplicacion", "Aplicacion")
                        .WithMany()
                        .HasForeignKey("AplicacionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiPerfiles.Models.RoleModuloDefault", b =>
                {
                    b.HasOne("ApiPerfiles.Models.Role", "Role")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiPerfiles.Models.UsuarioModulo", b =>
                {
                    b.HasOne("ApiPerfiles.Models.Modulo", "Modulo")
                        .WithMany("UsuarioModulos")
                        .HasForeignKey("ModuloId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiPerfiles.Models.Usuario", "Usuario")
                        .WithMany("UsuarioModulos")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("ApiPerfiles.Models.UsuarioRole", b =>
                {
                    b.HasOne("ApiPerfiles.Models.Role", "Role")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("ApiPerfiles.Models.Usuario", "Usuario")
                        .WithMany("UsuarioRoles")
                        .HasForeignKey("UsuarioId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
