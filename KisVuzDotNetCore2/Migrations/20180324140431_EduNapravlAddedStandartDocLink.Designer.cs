﻿// <auto-generated />
using KisVuzDotNetCore2.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using System;

namespace KisVuzDotNetCore2.Migrations
{
    [DbContext(typeof(AppIdentityDBContext))]
    [Migration("20180324140431_EduNapravlAddedStandartDocLink")]
    partial class EduNapravlAddedStandartDocLink
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn)
                .HasAnnotation("ProductVersion", "2.0.1-rtm-125");

            modelBuilder.Entity("KisVuzDotNetCore2.Models.AppUser", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<byte[]>("AppUserPhoto");

                    b.Property<DateTime>("Birthdate");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<string>("LastName");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("Patronymic");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduForm", b =>
                {
                    b.Property<int>("EduFormId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EduFormName");

                    b.HasKey("EduFormId");

                    b.ToTable("EduForms");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduLevel", b =>
                {
                    b.Property<int>("EduLevelId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EduLevelName");

                    b.HasKey("EduLevelId");

                    b.ToTable("EduLevels");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduNapravl", b =>
                {
                    b.Property<int>("EduNapravlId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EduNapravCode");

                    b.Property<string>("EduNapravName");

                    b.Property<string>("EduNapravlStandartDocLink");

                    b.Property<int>("EduUgsId");

                    b.HasKey("EduNapravlId");

                    b.HasIndex("EduUgsId");

                    b.ToTable("EduNapravls");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduUgs", b =>
                {
                    b.Property<int>("EduUgsId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EduLevelId");

                    b.Property<string>("EduUgsCode");

                    b.Property<string>("EduUgsName");

                    b.HasKey("EduUgsId");

                    b.HasIndex("EduLevelId");

                    b.ToTable("EduUgses");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.StructOrgUprav", b =>
                {
                    b.Property<int>("StructOrgUpravId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("AddressStr");

                    b.Property<string>("DivisionClauseDocLink");

                    b.Property<string>("Email");

                    b.Property<string>("Fio");

                    b.Property<bool>("IsOrgUprav");

                    b.Property<string>("Post");

                    b.Property<string>("Site");

                    b.Property<string>("StructOrgUpravName");

                    b.HasKey("StructOrgUpravId");

                    b.ToTable("StructOrgUprav");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .IsUnique()
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("RoleId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<string>("UserId")
                        .IsRequired();

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduNapravl", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduUgs", "EduUgs")
                        .WithMany()
                        .HasForeignKey("EduUgsId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduUgs", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduLevel", "EduLevel")
                        .WithMany()
                        .HasForeignKey("EduLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole")
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.AppUser")
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
#pragma warning restore 612, 618
        }
    }
}
