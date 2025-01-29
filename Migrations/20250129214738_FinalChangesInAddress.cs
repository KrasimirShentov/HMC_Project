using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HMC_Project.Migrations
{
    /// <inheritdoc />
    public partial class FinalChangesInAddress : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Companies_CompanyID",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Companies_CompanyID",
                table: "Addresses",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Addresses_Companies_CompanyID",
                table: "Addresses");

            migrationBuilder.AddForeignKey(
                name: "FK_Addresses_Companies_CompanyID",
                table: "Addresses",
                column: "CompanyID",
                principalTable: "Companies",
                principalColumn: "ID",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
