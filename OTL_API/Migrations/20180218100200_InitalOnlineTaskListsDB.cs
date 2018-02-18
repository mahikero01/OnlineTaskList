using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace OTL_API.Migrations
{
    public partial class InitalOnlineTaskListsDB : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UserTasks",
                columns: table => new
                {
                    UserTaskID = table.Column<Guid>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    Description = table.Column<string>(maxLength: 50, nullable: true),
                    IsDone = table.Column<bool>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    UserID = table.Column<string>(maxLength: 450, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTasks", x => x.UserTaskID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserTasks");
        }
    }
}
