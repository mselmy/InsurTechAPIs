using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class editInsuranceTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "AvailableInsurance",
                table: "InsurancePlans",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableInsurance",
                table: "InsurancePlans");
        }
    }
}
