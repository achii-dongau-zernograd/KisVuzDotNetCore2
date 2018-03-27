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
    [Migration("20180327094526_StructInstituteModelChanging")]
    partial class StructInstituteModelChanging
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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduPlan", b =>
                {
                    b.Property<int>("EduPlanId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EduFormId");

                    b.Property<int>("EduProfileId");

                    b.Property<int?>("EduYearBeginningTrainingId");

                    b.Property<int?>("EduYearId");

                    b.Property<int>("StructKafId");

                    b.HasKey("EduPlanId");

                    b.HasIndex("EduFormId");

                    b.HasIndex("EduProfileId");

                    b.HasIndex("EduYearBeginningTrainingId");

                    b.HasIndex("EduYearId");

                    b.HasIndex("StructKafId");

                    b.ToTable("EduPlans");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduProfile", b =>
                {
                    b.Property<int>("EduProfileId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("EduNapravlId");

                    b.Property<string>("EduProfileName");

                    b.HasKey("EduProfileId");

                    b.HasIndex("EduNapravlId");

                    b.ToTable("EduProfiles");
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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduYear", b =>
                {
                    b.Property<int>("EduYearId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EduYearName");

                    b.HasKey("EduYearId");

                    b.ToTable("EduYears");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduYearBeginningTraining", b =>
                {
                    b.Property<int>("EduYearBeginningTrainingId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EduYearBeginningTrainingName");

                    b.HasKey("EduYearBeginningTrainingId");

                    b.ToTable("EduYearBeginningTrainings");
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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Address", b =>
                {
                    b.Property<int>("AddressId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Country");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("PostCode");

                    b.Property<string>("Region");

                    b.Property<string>("Settlement");

                    b.Property<string>("Street");

                    b.HasKey("AddressId");

                    b.ToTable("Addresses");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Email", b =>
                {
                    b.Property<int>("EmailId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("EmailComment");

                    b.Property<string>("EmailValue");

                    b.Property<int?>("StructInstituteId");

                    b.HasKey("EmailId");

                    b.HasIndex("StructInstituteId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Fax", b =>
                {
                    b.Property<int>("FaxId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FaxComment");

                    b.Property<string>("FaxValue");

                    b.Property<int?>("StructInstituteId");

                    b.HasKey("FaxId");

                    b.HasIndex("StructInstituteId");

                    b.ToTable("Faxes");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructFacultet", b =>
                {
                    b.Property<int>("StructFacultetId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StructFacultetName");

                    b.Property<int>("StructInstituteId");

                    b.HasKey("StructFacultetId");

                    b.HasIndex("StructInstituteId");

                    b.ToTable("StructFacultets");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructInstitute", b =>
                {
                    b.Property<int>("StructInstituteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("AddressId");

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<bool>("ExistenceOfFilials");

                    b.Property<string>("StructInstituteName");

                    b.Property<string>("WorkingRegime");

                    b.Property<string>("WorkingSchedule");

                    b.HasKey("StructInstituteId");

                    b.HasIndex("AddressId");

                    b.ToTable("StructInstitutes");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructKaf", b =>
                {
                    b.Property<int>("StructKafId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StructFacultetId");

                    b.Property<int>("StructKafName");

                    b.Property<int>("StructKafNameSokr");

                    b.HasKey("StructKafId");

                    b.HasIndex("StructFacultetId");

                    b.ToTable("StructKafs");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Telephone", b =>
                {
                    b.Property<int>("TelephoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("StructInstituteId");

                    b.Property<string>("TelephoneComment");

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("TelephoneId");

                    b.HasIndex("StructInstituteId");

                    b.ToTable("Telephones");
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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduPlan", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduForm", "EduForm")
                        .WithMany()
                        .HasForeignKey("EduFormId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduProfile", "EduProfile")
                        .WithMany()
                        .HasForeignKey("EduProfileId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduYearBeginningTraining", "EduYearBeginningTraining")
                        .WithMany()
                        .HasForeignKey("EduYearBeginningTrainingId");

                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduYear", "EduYear")
                        .WithMany()
                        .HasForeignKey("EduYearId");

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructKaf", "StructKaf")
                        .WithMany()
                        .HasForeignKey("StructKafId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduProfile", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduNapravl", "EduNapravl")
                        .WithMany()
                        .HasForeignKey("EduNapravlId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduUgs", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduLevel", "EduLevel")
                        .WithMany()
                        .HasForeignKey("EduLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Email", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute")
                        .WithMany("Emailes")
                        .HasForeignKey("StructInstituteId");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Fax", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute")
                        .WithMany("Faxes")
                        .HasForeignKey("StructInstituteId");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructFacultet", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute", "StructInstitute")
                        .WithMany("StructFacultets")
                        .HasForeignKey("StructInstituteId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructInstitute", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructKaf", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructFacultet", "StructFacultet")
                        .WithMany()
                        .HasForeignKey("StructFacultetId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Telephone", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute")
                        .WithMany("Telephones")
                        .HasForeignKey("StructInstituteId");
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
