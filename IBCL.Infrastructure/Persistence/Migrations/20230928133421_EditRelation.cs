using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IBCL.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class EditRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Asset_Account_AccountId",
                table: "Asset");

            migrationBuilder.DropIndex(
                name: "IX_Asset_AccountId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "AccountId",
                table: "Asset");

            migrationBuilder.DropColumn(
                name: "Amount",
                table: "Asset");

            migrationBuilder.AddColumn<Guid>(
                name: "AssetId",
                table: "Position",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Position_AssetId",
                table: "Position",
                column: "AssetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Position_Asset_AssetId",
                table: "Position",
                column: "AssetId",
                principalTable: "Asset",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Position_Asset_AssetId",
                table: "Position");

            migrationBuilder.DropIndex(
                name: "IX_Position_AssetId",
                table: "Position");

            migrationBuilder.DropColumn(
                name: "AssetId",
                table: "Position");

            migrationBuilder.AddColumn<Guid>(
                name: "AccountId",
                table: "Asset",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<decimal>(
                name: "Amount",
                table: "Asset",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_Asset_AccountId",
                table: "Asset",
                column: "AccountId");

            migrationBuilder.AddForeignKey(
                name: "FK_Asset_Account_AccountId",
                table: "Asset",
                column: "AccountId",
                principalTable: "Account",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
