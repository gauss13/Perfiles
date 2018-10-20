using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiPerfiles.Migrations
{
    public partial class Inicial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aplicaciones",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    Acronimo = table.Column<string>(nullable: true),
                    ClaveSeguridad = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aplicaciones", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    UserName = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Nombres = table.Column<string>(nullable: true),
                    Apaterno = table.Column<string>(nullable: true),
                    Amaterno = table.Column<string>(nullable: true),
                    Correo = table.Column<string>(nullable: true),
                    Departamento = table.Column<string>(nullable: true),
                    NSandista = table.Column<int>(nullable: false),
                    ActiveDirectory = table.Column<bool>(nullable: false),
                    Estatus = table.Column<bool>(nullable: false),
                    FechaReg = table.Column<DateTime>(nullable: false),
                    UsuarioReg = table.Column<int>(nullable: false),
                    FechaMod = table.Column<DateTime>(nullable: false),
                    UsuarioMod = table.Column<int>(nullable: false),
                    FechaBaja = table.Column<DateTime>(nullable: false),
                    UsuarioBaja = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Modulos",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    AplicacionId = table.Column<int>(nullable: false),
                    Nombre = table.Column<string>(nullable: true),
                    Acronimo = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Modulos", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Modulos_Aplicaciones_AplicacionId",
                        column: x => x.AplicacionId,
                        principalTable: "Aplicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Roles",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nombre = table.Column<string>(nullable: true),
                    AplicacionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Roles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Roles_Aplicaciones_AplicacionId",
                        column: x => x.AplicacionId,
                        principalTable: "Aplicaciones",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioModulos",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false),
                    ModuloId = table.Column<int>(nullable: false),
                    Accion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioModulos", x => new { x.UsuarioId, x.ModuloId });
                    table.ForeignKey(
                        name: "FK_UsuarioModulos_Modulos_ModuloId",
                        column: x => x.ModuloId,
                        principalTable: "Modulos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioModulos_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleModulosDefault",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<int>(nullable: false),
                    Modulos = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleModulosDefault", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleModulosDefault_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UsuarioRoles",
                columns: table => new
                {
                    UsuarioId = table.Column<int>(nullable: false),
                    RoleId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsuarioRoles", x => new { x.UsuarioId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Roles_RoleId",
                        column: x => x.RoleId,
                        principalTable: "Roles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UsuarioRoles_Usuarios_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "Usuarios",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Modulos_AplicacionId",
                table: "Modulos",
                column: "AplicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_RoleModulosDefault_RoleId",
                table: "RoleModulosDefault",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Roles_AplicacionId",
                table: "Roles",
                column: "AplicacionId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioModulos_ModuloId",
                table: "UsuarioModulos",
                column: "ModuloId");

            migrationBuilder.CreateIndex(
                name: "IX_UsuarioRoles_RoleId",
                table: "UsuarioRoles",
                column: "RoleId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RoleModulosDefault");

            migrationBuilder.DropTable(
                name: "UsuarioModulos");

            migrationBuilder.DropTable(
                name: "UsuarioRoles");

            migrationBuilder.DropTable(
                name: "Modulos");

            migrationBuilder.DropTable(
                name: "Roles");

            migrationBuilder.DropTable(
                name: "Usuarios");

            migrationBuilder.DropTable(
                name: "Aplicaciones");
        }
    }
}
