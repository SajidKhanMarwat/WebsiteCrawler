using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    LastName = table.Column<string>(type: "varchar(30)", unicode: false, maxLength: 30, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Password = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "PaymentStatus",
                columns: table => new
                {
                    paymentID = table.Column<int>(type: "int", nullable: false),
                    Free = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    Paid = table.Column<string>(type: "varchar(10)", unicode: false, maxLength: 10, nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__PaymentS__A0D9EFA6E7C2C12A", x => x.paymentID);
                    table.ForeignKey(
                        name: "FK__PaymentSt__UserI__286302EC",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "UsersData",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false),
                    URLs = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    StatusCode = table.Column<int>(type: "int", nullable: true),
                    UserID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UsersData", x => x.ID);
                    table.ForeignKey(
                        name: "FK__UsersData__UserI__29572725",
                        column: x => x.UserID,
                        principalTable: "Users",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_PaymentStatus_UserID",
                table: "PaymentStatus",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_UsersData_UserID",
                table: "UsersData",
                column: "UserID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PaymentStatus");

            migrationBuilder.DropTable(
                name: "UsersData");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
