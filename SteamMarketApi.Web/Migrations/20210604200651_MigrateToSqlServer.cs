using Microsoft.EntityFrameworkCore.Migrations;

namespace SteamMarketApi.Web.Migrations
{
    public partial class MigrateToSqlServer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SteamItems",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    NormalizedName = table.Column<string>(nullable: true),
                    MarketName = table.Column<string>(nullable: true),
                    MarketHashName = table.Column<string>(nullable: true),
                    Image = table.Column<string>(nullable: true),
                    LowestPrice = table.Column<decimal>(nullable: false),
                    MedianPrice = table.Column<decimal>(nullable: false),
                    Game = table.Column<int>(nullable: false),
                    AppId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SteamItems", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SteamItems");
        }
    }
}
