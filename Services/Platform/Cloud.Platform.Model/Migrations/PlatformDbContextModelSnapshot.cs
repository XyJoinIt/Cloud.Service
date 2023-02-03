﻿// <auto-generated />
using System;
using Cloud.Platform.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace Cloud.Platform.Model.Migrations
{
    [DbContext(typeof(PlatformDbContext))]
    partial class PlatformDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.1")
                .HasAnnotation("Relational:MaxIdentifierLength", 64);

            modelBuilder.Entity("Cloud.Platform.Model.Entity.SysOrg", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CreateId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("DeleteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("EditId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EditTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(64)
                        .HasColumnType("varchar(64)");

                    b.Property<int>("Order")
                        .HasColumnType("int");

                    b.Property<long>("Pid")
                        .HasColumnType("bigint");

                    b.Property<string>("Remark")
                        .HasMaxLength(128)
                        .HasColumnType("varchar(128)");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SysOrg", t =>
                        {
                            t.HasComment("机构表");
                        });
                });

            modelBuilder.Entity("Cloud.Platform.Model.Entity.SysRole", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<long>("CreateId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("DeleteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("EditId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EditTime")
                        .HasColumnType("datetime");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("longtext");

                    b.Property<string>("Remark")
                        .HasColumnType("longtext");

                    b.Property<int>("Sort")
                        .HasColumnType("int");

                    b.Property<int>("Status")
                        .HasColumnType("int");

                    b.HasKey("Id");

                    b.ToTable("SysRole", t =>
                        {
                            t.HasComment("角色表");
                        });
                });

            modelBuilder.Entity("Cloud.Platform.Model.Entity.SysUser", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("bigint");

                    b.Property<string>("Account")
                        .IsRequired()
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("账号");

                    b.Property<int>("AdminType")
                        .HasColumnType("int")
                        .HasComment("管理员类型 -超级管理员_1、管理员_2、普通账号_3");

                    b.Property<string>("Avatar")
                        .HasColumnType("longtext")
                        .HasComment("头像");

                    b.Property<DateTime?>("Birthday")
                        .HasColumnType("datetime")
                        .HasComment("生日");

                    b.Property<long>("CreateId")
                        .HasColumnType("bigint");

                    b.Property<DateTime>("CreateTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("DeleteId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("DeleteTime")
                        .HasColumnType("datetime");

                    b.Property<long?>("EditId")
                        .HasColumnType("bigint");

                    b.Property<DateTime?>("EditTime")
                        .HasColumnType("datetime");

                    b.Property<string>("Email")
                        .HasMaxLength(50)
                        .HasColumnType("varchar(50)")
                        .HasComment("邮箱");

                    b.Property<bool>("IsDelete")
                        .HasColumnType("tinyint(1)");

                    b.Property<string>("LastLoginIp")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("最后登录IP");

                    b.Property<DateTime?>("LastLoginTime")
                        .HasColumnType("datetime")
                        .HasComment("最后登录时间");

                    b.Property<string>("Name")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("姓名");

                    b.Property<string>("NickName")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("昵称");

                    b.Property<string>("Password")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("密码");

                    b.Property<string>("Phone")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("手机");

                    b.Property<string>("SecurityStamp")
                        .IsRequired()
                        .HasMaxLength(100)
                        .HasColumnType("varchar(100)")
                        .HasComment("加密盐");

                    b.Property<int>("Sex")
                        .HasColumnType("int")
                        .HasComment("性别-男_1、女_2");

                    b.Property<int>("Status")
                        .HasColumnType("int")
                        .HasComment("状态-正常_0、停用_1、删除_2");

                    b.Property<string>("Tel")
                        .HasMaxLength(20)
                        .HasColumnType("varchar(20)")
                        .HasComment("电话");

                    b.HasKey("Id");

                    b.ToTable("SysUser", t =>
                        {
                            t.HasComment("用户表");
                        });
                });
#pragma warning restore 612, 618
        }
    }
}
