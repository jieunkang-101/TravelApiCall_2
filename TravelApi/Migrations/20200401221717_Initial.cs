using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace TravelApi.Migrations
{
    public partial class Initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Reviews",
                columns: table => new
                {
                    ReviewId = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Author = table.Column<string>(maxLength: 20, nullable: false),
                    Country = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Landmark = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    Rating = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reviews", x => x.ReviewId);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    Username = table.Column<string>(nullable: true),
                    Password = table.Column<string>(nullable: true),
                    Role = table.Column<string>(nullable: true),
                    Token = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Reviews",
                columns: new[] { "ReviewId", "Author", "City", "Country", "Description", "Landmark", "Rating" },
                values: new object[,]
                {
                    { 1, "user1", "Paris", "France", "I loved this place. If your are an art lover, this place is a paradise for you. One day is not enough to visit the entire museum. But still you can visit the main attractions including the famous Mona Lisa.", "Louvre Museum", 5 },
                    { 2, "user2", "Sydney", "Australia", "Wow! Sydney Opera House is absolutely FABULOUS! What an elegant, gorgeous, impressive display of truly artistic architecture.  The beauty of this site is enhanced with each different view.", "Opera House", 4 },
                    { 3, "user2", "Portland", "USA", "This was a great way to get to know the city of Portland! It was fascinating to learn about one of the first wealthy families to live there. The mansion is really cool and the hike through forest park is a fun way to get there!", "Pittock Mansion", 4 },
                    { 4, "user1", "Bangkok", "Thailand", "A place to be definitely included in must visit tourist destination. Place is well maintained and mermerizing. It is my first time to see buildings crafted with real gold.", "Temple of the Emerald Buddha", 3 },
                    { 5, "user3", "Seoul", "Korea", "It is one of the best places on South Korea to see the old architecture of a palace. Even traditional events are demonstrate times to time. The most interesting thing in this place is that you can take photographs with soldiers with traditional costumes.", "Gyeongbokgung Palace", 5 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "FirstName", "LastName", "Password", "Role", "Token", "Username" },
                values: new object[,]
                {
                    { 1, "Admin", "User", "admin", "Admin", null, "admin" },
                    { 2, "Normal", "User", "user", "User", null, "user" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reviews");

            migrationBuilder.DropTable(
                name: "Users");
        }
    }
}
