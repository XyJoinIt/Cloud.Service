using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Cloud.Platform.Model.Migrations
{
    /// <inheritdoc />
    public partial class v104 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "userInfo_Tel",
                table: "SysUser",
                newName: "Tel");

            migrationBuilder.RenameColumn(
                name: "userInfo_Status",
                table: "SysUser",
                newName: "Status");

            migrationBuilder.RenameColumn(
                name: "userInfo_Sex",
                table: "SysUser",
                newName: "Sex");

            migrationBuilder.RenameColumn(
                name: "userInfo_SecurityStamp",
                table: "SysUser",
                newName: "SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "userInfo_Phone",
                table: "SysUser",
                newName: "Phone");

            migrationBuilder.RenameColumn(
                name: "userInfo_Password",
                table: "SysUser",
                newName: "Password");

            migrationBuilder.RenameColumn(
                name: "userInfo_NickName",
                table: "SysUser",
                newName: "NickName");

            migrationBuilder.RenameColumn(
                name: "userInfo_Name",
                table: "SysUser",
                newName: "Name");

            migrationBuilder.RenameColumn(
                name: "userInfo_LastLoginTime",
                table: "SysUser",
                newName: "LastLoginTime");

            migrationBuilder.RenameColumn(
                name: "userInfo_LastLoginIp",
                table: "SysUser",
                newName: "LastLoginIp");

            migrationBuilder.RenameColumn(
                name: "userInfo_Email",
                table: "SysUser",
                newName: "Email");

            migrationBuilder.RenameColumn(
                name: "userInfo_Birthday",
                table: "SysUser",
                newName: "Birthday");

            migrationBuilder.RenameColumn(
                name: "userInfo_Avatar",
                table: "SysUser",
                newName: "Avatar");

            migrationBuilder.RenameColumn(
                name: "userInfo_AdminType",
                table: "SysUser",
                newName: "AdminType");

            migrationBuilder.RenameColumn(
                name: "userInfo_Account",
                table: "SysUser",
                newName: "Account");

            migrationBuilder.AlterColumn<int>(
                name: "Status",
                table: "SysUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "状态-正常_0、停用_1、删除_2",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "状态-正常_0、停用_1、删除_2");

            migrationBuilder.AlterColumn<int>(
                name: "Sex",
                table: "SysUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "性别-男_1、女_2",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "性别-男_1、女_2");

            migrationBuilder.UpdateData(
                table: "SysUser",
                keyColumn: "SecurityStamp",
                keyValue: null,
                column: "SecurityStamp",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "SecurityStamp",
                table: "SysUser",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "加密盐",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "加密盐")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.UpdateData(
                table: "SysUser",
                keyColumn: "Password",
                keyValue: null,
                column: "Password",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Password",
                table: "SysUser",
                type: "varchar(100)",
                maxLength: 100,
                nullable: false,
                comment: "密码",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldNullable: true,
                oldComment: "密码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "AdminType",
                table: "SysUser",
                type: "int",
                nullable: false,
                defaultValue: 0,
                comment: "管理员类型 -超级管理员_1、管理员_2、普通账号_3",
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true,
                oldComment: "管理员类型 -超级管理员_1、管理员_2、普通账号_3");

            migrationBuilder.UpdateData(
                table: "SysUser",
                keyColumn: "Account",
                keyValue: null,
                column: "Account",
                value: "");

            migrationBuilder.AlterColumn<string>(
                name: "Account",
                table: "SysUser",
                type: "varchar(50)",
                maxLength: 50,
                nullable: false,
                comment: "账号",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldNullable: true,
                oldComment: "账号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Tel",
                table: "SysUser",
                newName: "userInfo_Tel");

            migrationBuilder.RenameColumn(
                name: "Status",
                table: "SysUser",
                newName: "userInfo_Status");

            migrationBuilder.RenameColumn(
                name: "Sex",
                table: "SysUser",
                newName: "userInfo_Sex");

            migrationBuilder.RenameColumn(
                name: "SecurityStamp",
                table: "SysUser",
                newName: "userInfo_SecurityStamp");

            migrationBuilder.RenameColumn(
                name: "Phone",
                table: "SysUser",
                newName: "userInfo_Phone");

            migrationBuilder.RenameColumn(
                name: "Password",
                table: "SysUser",
                newName: "userInfo_Password");

            migrationBuilder.RenameColumn(
                name: "NickName",
                table: "SysUser",
                newName: "userInfo_NickName");

            migrationBuilder.RenameColumn(
                name: "Name",
                table: "SysUser",
                newName: "userInfo_Name");

            migrationBuilder.RenameColumn(
                name: "LastLoginTime",
                table: "SysUser",
                newName: "userInfo_LastLoginTime");

            migrationBuilder.RenameColumn(
                name: "LastLoginIp",
                table: "SysUser",
                newName: "userInfo_LastLoginIp");

            migrationBuilder.RenameColumn(
                name: "Email",
                table: "SysUser",
                newName: "userInfo_Email");

            migrationBuilder.RenameColumn(
                name: "Birthday",
                table: "SysUser",
                newName: "userInfo_Birthday");

            migrationBuilder.RenameColumn(
                name: "Avatar",
                table: "SysUser",
                newName: "userInfo_Avatar");

            migrationBuilder.RenameColumn(
                name: "AdminType",
                table: "SysUser",
                newName: "userInfo_AdminType");

            migrationBuilder.RenameColumn(
                name: "Account",
                table: "SysUser",
                newName: "userInfo_Account");

            migrationBuilder.AlterColumn<int>(
                name: "userInfo_Status",
                table: "SysUser",
                type: "int",
                nullable: true,
                comment: "状态-正常_0、停用_1、删除_2",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "状态-正常_0、停用_1、删除_2");

            migrationBuilder.AlterColumn<int>(
                name: "userInfo_Sex",
                table: "SysUser",
                type: "int",
                nullable: true,
                comment: "性别-男_1、女_2",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "性别-男_1、女_2");

            migrationBuilder.AlterColumn<string>(
                name: "userInfo_SecurityStamp",
                table: "SysUser",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "加密盐",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldComment: "加密盐")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<string>(
                name: "userInfo_Password",
                table: "SysUser",
                type: "varchar(100)",
                maxLength: 100,
                nullable: true,
                comment: "密码",
                oldClrType: typeof(string),
                oldType: "varchar(100)",
                oldMaxLength: 100,
                oldComment: "密码")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.AlterColumn<int>(
                name: "userInfo_AdminType",
                table: "SysUser",
                type: "int",
                nullable: true,
                comment: "管理员类型 -超级管理员_1、管理员_2、普通账号_3",
                oldClrType: typeof(int),
                oldType: "int",
                oldComment: "管理员类型 -超级管理员_1、管理员_2、普通账号_3");

            migrationBuilder.AlterColumn<string>(
                name: "userInfo_Account",
                table: "SysUser",
                type: "varchar(50)",
                maxLength: 50,
                nullable: true,
                comment: "账号",
                oldClrType: typeof(string),
                oldType: "varchar(50)",
                oldMaxLength: 50,
                oldComment: "账号")
                .Annotation("MySql:CharSet", "utf8mb4")
                .OldAnnotation("MySql:CharSet", "utf8mb4");
        }
    }
}
