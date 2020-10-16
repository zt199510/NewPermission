﻿// <auto-generated />
using System;
using CardPlatform.MyDBModel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace CardPlatform.Migrations
{
    [DbContext(typeof(MyDbContext))]
    partial class MyDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.8");

            modelBuilder.Entity("CardPlatform.Models.Menu", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("Icon")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(50);

                    b.Property<int>("IndexCode")
                        .HasColumnType("INTEGER");

                    b.Property<int>("MenuType")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("ParentId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Remarks")
                        .HasColumnType("TEXT");

                    b.Property<string>("Url")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.ToTable("Menus");
                });

            modelBuilder.Entity("CardPlatform.Models.PermissionModels", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int?>("PermissionModelsId")
                        .HasColumnType("INTEGER");

                    b.Property<string>("Role")
                        .HasColumnType("TEXT");

                    b.Property<string>("URL")
                        .HasColumnType("TEXT");

                    b.Property<string>("URLName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("PermissionModelsId");

                    b.ToTable("PermissionModels");
                });

            modelBuilder.Entity("CardPlatform.Models.UserInfo", b =>
                {
                    b.Property<string>("UserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("LasLoginTime")
                        .HasColumnType("TEXT");

                    b.Property<string>("Password")
                        .HasColumnType("TEXT");

                    b.Property<string>("RegistTime")
                        .HasColumnType("TEXT");

                    b.Property<bool>("Status")
                        .HasColumnType("INTEGER");

                    b.HasKey("UserName");

                    b.ToTable("UserInfos");
                });

            modelBuilder.Entity("CardPlatform.Models.UserRefreshToken", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("Expires")
                        .HasColumnType("TEXT");

                    b.Property<string>("Token")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserInfoUserName")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserInfoUserName");

                    b.ToTable("UserRefreshToken");
                });

            modelBuilder.Entity("CardPlatform.Models.PermissionModels", b =>
                {
                    b.HasOne("CardPlatform.Models.PermissionModels", null)
                        .WithMany("ChlinPermissionModels")
                        .HasForeignKey("PermissionModelsId");
                });

            modelBuilder.Entity("CardPlatform.Models.UserRefreshToken", b =>
                {
                    b.HasOne("CardPlatform.Models.UserInfo", null)
                        .WithMany("UserRefreshTokens")
                        .HasForeignKey("UserInfoUserName");
                });
#pragma warning restore 612, 618
        }
    }
}
