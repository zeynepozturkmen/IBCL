using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBCL.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionTypeColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "TransactionType",
                table: "Position",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "TransactionType",
                table: "Position");
        }
    }
}
