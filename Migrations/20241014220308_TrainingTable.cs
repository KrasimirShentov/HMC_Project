using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMC_Project.Migrations
{
    /// <inheritdoc />
    public partial class TrainingTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees");

            migrationBuilder.AddColumn<Guid>(
                name: "TrainingID",
                table: "Employees",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TrainingID",
                table: "Employees",
                column: "TrainingID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Training_TrainingID",
                table: "Employees",
                column: "TrainingID",
                principalTable: "Training",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Training_TrainingID",
                table: "Employees");

            migrationBuilder.DropForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees");

            migrationBuilder.DropIndex(
                name: "IX_Employees_TrainingID",
                table: "Employees");

            migrationBuilder.DropColumn(
                name: "TrainingID",
                table: "Employees");

            migrationBuilder.AddForeignKey(
                name: "FK_Employees_Training_TrainingId",
                table: "Employees",
                column: "TrainingId",
                principalTable: "Training",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
