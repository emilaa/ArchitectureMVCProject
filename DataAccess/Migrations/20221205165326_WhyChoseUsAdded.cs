using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    public partial class WhyChoseUsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ProfessionalDoctors",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "Quality",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "SatisifiedPatients",
                table: "WhyChooseUs");

            migrationBuilder.DropColumn(
                name: "YearExperience",
                table: "WhyChooseUs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ProfessionalDoctors",
                table: "WhyChooseUs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Quality",
                table: "WhyChooseUs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "SatisifiedPatients",
                table: "WhyChooseUs",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "YearExperience",
                table: "WhyChooseUs",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
