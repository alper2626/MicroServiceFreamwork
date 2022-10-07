using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SSTTEK.Contact.DataAccess.Migrations
{
    public partial class removepropadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "Contacts",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "IsRemoved",
                table: "ContactInformations",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "Contacts");

            migrationBuilder.DropColumn(
                name: "IsRemoved",
                table: "ContactInformations");
        }
    }
}
