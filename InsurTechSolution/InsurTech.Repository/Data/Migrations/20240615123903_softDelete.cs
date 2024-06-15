using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class softDelete : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "AspNetUsers",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "IsDeleted", "PasswordHash", "SecurityStamp" },
                values: new object[] { "cf7befa0-00b2-4fcc-9967-ee55dc294434", false, "AQAAAAIAAYagAAAAED2Zjkb2/oe00r/SdpAkR43+JJrD+QqWNIPf544uIoeE73DiAmfAhxNYSMcUXFON7w==", "dc96a64d-7b43-4883-8cae-c2d473e0b7de" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsDeleted",
                table: "AspNetUsers");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "7d153a61-62c7-4930-9205-d6ae96f3ddc4", "AQAAAAIAAYagAAAAEHi/E+L4OXmOldtVi3OOB5rzMhSrtADJM6bRpOcv9hJfoaEMyVOm2ITFYJruYVRHoQ==", "9128a16c-067e-4701-a37d-b312d384a5bd" });
        }
    }
}
