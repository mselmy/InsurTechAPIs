using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace InsurTech.Repository.Data.Migrations
{
    /// <inheritdoc />
    public partial class addFeedBack : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans");

            migrationBuilder.AddColumn<int>(
                name: "InsurancePlanId",
                table: "Feedbacks",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "5366c00e-68ac-40a7-999d-6000b1e340e3", "AQAAAAIAAYagAAAAEIuODIp9Np9wmvIUhI50lgm8/fNSDbhongpExK9IkOnUlgv2fPLfUyhR+fWEmCtTuA==", "91e41cda-e331-4026-8ac6-e848ae6c9ba1" });

            migrationBuilder.CreateIndex(
                name: "IX_Feedbacks_InsurancePlanId",
                table: "Feedbacks",
                column: "InsurancePlanId");

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_InsurancePlans_InsurancePlanId",
                table: "Feedbacks",
                column: "InsurancePlanId",
                principalTable: "InsurancePlans",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_AspNetUsers_CustomerId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_Feedbacks_InsurancePlans_InsurancePlanId",
                table: "Feedbacks");

            migrationBuilder.DropForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans");

            migrationBuilder.DropIndex(
                name: "IX_Feedbacks_InsurancePlanId",
                table: "Feedbacks");

            migrationBuilder.DropColumn(
                name: "InsurancePlanId",
                table: "Feedbacks");

            migrationBuilder.UpdateData(
                table: "AspNetUsers",
                keyColumn: "Id",
                keyValue: "1",
                columns: new[] { "ConcurrencyStamp", "PasswordHash", "SecurityStamp" },
                values: new object[] { "8324d19e-4160-402a-a273-cb90454927aa", "AQAAAAIAAYagAAAAEC/72deuZeyFDEpD1zidHaD2yy5FU1DQSPNefls5l5RGmNKH5+Lu24OVbRU2yfDMtw==", "2139de8f-9665-4ed2-9a26-950172903dfc" });

            migrationBuilder.AddForeignKey(
                name: "FK_Feedbacks_AspNetUsers_CustomerId",
                table: "Feedbacks",
                column: "CustomerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_InsurancePlans_AspNetUsers_CompanyId",
                table: "InsurancePlans",
                column: "CompanyId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
