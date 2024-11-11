using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMC_Project.Migrations
{
    /// <inheritdoc />
    public partial class UserAddressConnection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Departments_Addresses_AddressID",
                table: "Departments");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Addresses_AddressID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Users_Addresses_AddressID",
                table: "Users");

            migrationBuilder.DropTable(
                name: "UserAddress");

            migrationBuilder.DropIndex(
                name: "IX_Users_AddressID",
                table: "Users");

            migrationBuilder.DropIndex(
                name: "IX_Employees_AddressID",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Departments_AddressID",
                table: "Departments");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Users");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "AddressID",
                table: "Departments");

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                table: "Addresses",
                type: "character varying(255)",
                maxLength: 255,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "text");

            migrationBuilder.AddColumn<Guid>(
                name: "UserID",
                table: "Addresses",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses",
                column: "UserID");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Users_UserID",
                table: "Addresses",
                column: "UserID",
                principalTable: "Users",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.SetNull);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Users_UserID",
                table: "Addresses");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Addresses_UserID",
                table: "Addresses");

            migrationBuilder.DropColumn(
                name: "UserID",
                table: "Addresses");

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Users",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AddressID",
                table: "Departments",
                type: "uuid",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "AddressName",
                table: "Addresses",
                type: "text",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(255)",
                oldMaxLength: 255);

            migrationBuilder.CreateTable(
                name: "UserAddress",
                columns: table => new
                {
                    UserID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserAddressID = table.Column<Guid>(type: "uuid", nullable: false),
                    AddressID = table.Column<Guid>(type: "uuid", nullable: false),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserAddress", x => new { x.UserID, x.UserAddressID });
                    table.ForeignKey(
                        name: "FK_UserAddress_Addresses_AddressID",
                        column: x => x.AddressID,
                        principalTable: "Addresses",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserAddress_Users_UserID",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Users_AddressID",
                table: "Users",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_AddressID",
                table: "Employees",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AddressID",
                table: "Departments",
                column: "AddressID");

            migrationBuilder.CreateIndex(
                name: "IX_UserAddress_AddressID",
                table: "UserAddress",
                column: "AddressID");

            migrationBuilder.AddForeignKey(
                name: "FK_Departments_Addresses_AddressID",
                table: "Departments",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Addresses_AddressID",
                table: "Employees",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Users_Addresses_AddressID",
                table: "Users",
                column: "AddressID",
                principalTable: "Addresses",
                principalColumn: "ID");
        }
    }
}
