using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud.Platform.Model.Migrations
{
    /// <inheritdoc />
    public partial class v100 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "SysUser",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Account = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: false, comment: "账号")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Password = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "密码")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    SecurityStamp = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false, comment: "个人秘钥")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    NickName = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "昵称")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Name = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false, comment: "姓名")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Avatar = table.Column<string>(type: "longtext", nullable: true, comment: "头像")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Birthday = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "生日"),
                    Sex = table.Column<int>(type: "int", nullable: false, comment: "性别-男_1、女_2"),
                    Email = table.Column<string>(type: "varchar(50)", maxLength: 50, nullable: true, comment: "邮箱")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Phone = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "手机")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Tel = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "电话")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginIp = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: true, comment: "最后登录IP")
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    LastLoginTime = table.Column<DateTimeOffset>(type: "datetime", nullable: true, comment: "最后登录时间"),
                    AdminType = table.Column<int>(type: "int", nullable: false, comment: "管理员类型 -超级管理员_1、管理员_2、普通账号_3"),
                    Status = table.Column<int>(type: "int", nullable: false, comment: "状态-正常_0、停用_1、删除_2"),
                    EditId = table.Column<long>(type: "bigint", nullable: true),
                    EditTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    CreateId = table.Column<long>(type: "bigint", nullable: false),
                    CreateTime = table.Column<DateTime>(type: "datetime", nullable: false),
                    DeleteId = table.Column<long>(type: "bigint", nullable: true),
                    DeleteTime = table.Column<DateTime>(type: "datetime", nullable: true),
                    IsDelete = table.Column<bool>(type: "tinyint(1)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SysUser", x => x.Id);
                },
                comment: "用户表")
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SysUser");
        }
    }
}
