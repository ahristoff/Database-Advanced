using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Forum.Migrations
{
    public partial class Tags : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag");

            migrationBuilder.DropForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag");

            migrationBuilder.RenameTable(
                name: "PostTag",
                newName: "PostsTags");

            migrationBuilder.RenameIndex(
                name: "IX_PostTag_TagId",
                table: "PostsTags",
                newName: "IX_PostsTags_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostsTags",
                table: "PostsTags",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTags_Posts_PostId",
                table: "PostsTags",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostsTags_Tag_TagId",
                table: "PostsTags",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_PostsTags_Posts_PostId",
                table: "PostsTags");

            migrationBuilder.DropForeignKey(
                name: "FK_PostsTags_Tag_TagId",
                table: "PostsTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_PostsTags",
                table: "PostsTags");

            migrationBuilder.RenameTable(
                name: "PostsTags",
                newName: "PostTag");

            migrationBuilder.RenameIndex(
                name: "IX_PostsTags_TagId",
                table: "PostTag",
                newName: "IX_PostTag_TagId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_PostTag",
                table: "PostTag",
                columns: new[] { "PostId", "TagId" });

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Posts_PostId",
                table: "PostTag",
                column: "PostId",
                principalTable: "Posts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_PostTag_Tag_TagId",
                table: "PostTag",
                column: "TagId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
