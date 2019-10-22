using Microsoft.EntityFrameworkCore.Migrations;

namespace BddTodo.Data.Migrations
{
    public partial class TodoLists : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TodoListId",
                table: "Todo",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "TodoList",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TodoList", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TodoList_User_UserId",
                        column: x => x.UserId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Todo_TodoListId",
                table: "Todo",
                column: "TodoListId");

            migrationBuilder.CreateIndex(
                name: "IX_TodoList_UserId",
                table: "TodoList",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Todo_TodoList_TodoListId",
                table: "Todo",
                column: "TodoListId",
                principalTable: "TodoList",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Todo_TodoList_TodoListId",
                table: "Todo");

            migrationBuilder.DropTable(
                name: "TodoList");

            migrationBuilder.DropIndex(
                name: "IX_Todo_TodoListId",
                table: "Todo");

            migrationBuilder.DropColumn(
                name: "TodoListId",
                table: "Todo");
        }
    }
}
