using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace EmployeeManegement.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class updateInitDb : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("7eaa3433-27a6-4fa4-963a-2d07263fb4c2"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("8b68641b-9f8e-44aa-8c17-4045448207db"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("bbfb665f-49a9-4cff-bf12-9cf019ffb9b6"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("df9614b6-be5d-41e5-aaca-286ed8529fbf"));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("36cecf00-a31e-4346-ad09-8b019751eb05"), "Finance" },
                    { new Guid("9e7a35f9-7d4f-4c7a-9d78-d3a5858f10c2"), "HR" },
                    { new Guid("dae8e978-cf66-40bb-8f5a-61a7d47e4e85"), "Software Development" },
                    { new Guid("ea612401-ba9c-4f14-b2b1-6f00b5cd37f4"), "Accountant" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("36cecf00-a31e-4346-ad09-8b019751eb05"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("9e7a35f9-7d4f-4c7a-9d78-d3a5858f10c2"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("dae8e978-cf66-40bb-8f5a-61a7d47e4e85"));

            migrationBuilder.DeleteData(
                table: "Departments",
                keyColumn: "Id",
                keyValue: new Guid("ea612401-ba9c-4f14-b2b1-6f00b5cd37f4"));

            migrationBuilder.InsertData(
                table: "Departments",
                columns: new[] { "Id", "Name" },
                values: new object[,]
                {
                    { new Guid("7eaa3433-27a6-4fa4-963a-2d07263fb4c2"), "HR" },
                    { new Guid("8b68641b-9f8e-44aa-8c17-4045448207db"), "Finance" },
                    { new Guid("bbfb665f-49a9-4cff-bf12-9cf019ffb9b6"), "Software Development" },
                    { new Guid("df9614b6-be5d-41e5-aaca-286ed8529fbf"), "Accountant" }
                });
        }
    }
}
