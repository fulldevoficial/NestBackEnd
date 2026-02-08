using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Data.Migrations
{
    /// <inheritdoc />
    public partial class v0001 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "dbo");

            migrationBuilder.CreateTable(
                name: "courses",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    description = table.Column<string>(type: "character varying(2000)", maxLength: 2000, nullable: true),
                    thumbnail = table.Column<string>(type: "character varying(1000)", maxLength: 1000, nullable: true),
                    selo = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    precoatual = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    precoantigo = table.Column<decimal>(type: "numeric(18,2)", precision: 18, scale: 2, nullable: true),
                    category = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: true),
                    tags = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    isdestaque = table.Column<bool>(type: "boolean", nullable: false),
                    criadoem = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    atualizadoem = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_courses", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "usuario",
                schema: "dbo",
                columns: table => new
                {
                    id = table.Column<Guid>(type: "uuid", nullable: false),
                    codigousuario = table.Column<int>(type: "integer", nullable: false),
                    nome = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: false),
                    email = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    Password = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_usuario", x => x.id);
                });

            migrationBuilder.CreateIndex(
                name: "ix_courses_category",
                schema: "dbo",
                table: "courses",
                column: "category");

            migrationBuilder.CreateIndex(
                name: "ix_courses_title",
                schema: "dbo",
                table: "courses",
                column: "title");

            migrationBuilder.CreateIndex(
                name: "uk_usuarios_codigo",
                schema: "dbo",
                table: "usuario",
                column: "codigousuario",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "uk_usuarios_email",
                schema: "dbo",
                table: "usuario",
                column: "email",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "courses",
                schema: "dbo");

            migrationBuilder.DropTable(
                name: "usuario",
                schema: "dbo");
        }
    }
}
