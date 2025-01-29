using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMC_Project.Migrations
{
    /// <inheritdoc />
    public partial class AddressIDChange : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentAddresses_Addresses_AddressName",
                table: "DepartmentAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAddress_Addresses_AddressName",
                table: "EmployeeAddress");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAddress_AddressName",
                table: "EmployeeAddress");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentAddresses_AddressName",
                table: "DepartmentAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "EmployeeAddress");

            migrationBuilder.DropColumn(
                name: "AddressName",
                table: "DepartmentAddresses");

            migrationBuilder.AddColumn<Guid>(
                name: "ID",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "ID");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAddress_AddressID",
                table: "EmployeeAddress",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAddresses_AddressID",
                table: "DepartmentAddresses",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentAddresses_Addresses_AddressID",
                table: "DepartmentAddresses",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAddress_Addresses_AddressID",
                table: "EmployeeAddress",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_DepartmentAddresses_Addresses_AddressID",
                table: "DepartmentAddresses");

            migrationBuilder.DropForeignKey(
                name: "FK_EmployeeAddress_Addresses_AddressID",
                table: "EmployeeAddress");

            migrationBuilder.DropIndex(
                name: "IX_EmployeeAddress_AddressID",
                table: "EmployeeAddress");

            migrationBuilder.DropIndex(
                name: "IX_DepartmentAddresses_AddressID",
                table: "DepartmentAddresses");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "ID",
                table: "Addresses");

            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "EmployeeAddress",
                type: "character varying(255)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AddressName",
                table: "DepartmentAddresses",
                type: "character varying(255)",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Addresses",
                table: "Addresses",
                column: "AddressName");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAddress_AddressName",
                table: "EmployeeAddress",
                column: "AddressName");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentAddresses_AddressName",
                table: "DepartmentAddresses",
                column: "AddressName");

            migrationBuilder.AddForeignKey(
                name: "FK_DepartmentAddresses_Addresses_AddressName",
                table: "DepartmentAddresses",
                column: "AddressName",
                principalTable: "Addresses",
                principalColumn: "AddressName");

            migrationBuilder.AddForeignKey(
                name: "FK_EmployeeAddress_Addresses_AddressName",
                table: "EmployeeAddress",
                column: "AddressName",
                principalTable: "Addresses",
                principalColumn: "AddressName");
        }
    }
}
