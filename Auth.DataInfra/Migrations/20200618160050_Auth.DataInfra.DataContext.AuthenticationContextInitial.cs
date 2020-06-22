using Microsoft.EntityFrameworkCore.Migrations;

namespace Auth.DataInfra.Migrations
{
    public partial class AuthDataInfraDataContextAuthenticationContextInitial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LoginDetails",
                columns: table => new
                {
                    Email = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(20)", nullable: false),
                    LoginIndex = table.Column<string>(type: "nvarchar(MAX)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoginDetails", x => x.Email);
                });

            migrationBuilder.InsertData(
                table: "LoginDetails",
                columns: new[] { "Email", "LoginIndex", "Password" },
                values: new object[] { "test@gmail.com", "06/29/2020 05:50", "pwd" });

            migrationBuilder.InsertData(
                table: "LoginDetails",
                columns: new[] { "Email", "LoginIndex", "Password" },
                values: new object[] { "test@yahoo.com", "06/29/2020 05:50", "pwd" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LoginDetails");
        }
    }
}
