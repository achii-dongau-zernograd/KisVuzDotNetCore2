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
    partial class AppIdentityDBContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduAccred", b =>
                {
                    b.Property<int>("EduAccredId")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("EduAccredDate");

                    b.Property<DateTime>("EduAccredDateExpiration");

                    b.Property<int>("EduAccredFileId");

                    b.Property<string>("EduAccredNumber");

                    b.HasKey("EduAccredId");

                    b.HasIndex("EduAccredFileId");

                    b.ToTable("EduAccred");
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

                    b.Property<int?>("EduAccredId");

                    b.Property<int>("EduLevelId");

                    b.Property<string>("EduUgsCode");

                    b.Property<string>("EduUgsName");

                    b.HasKey("EduUgsId");

                    b.HasIndex("EduAccredId");

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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileDataType", b =>
                {
                    b.Property<int>("FileDataTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FileDataTypeGroupId");

                    b.Property<string>("FileDataTypeName");

                    b.HasKey("FileDataTypeId");

                    b.HasIndex("FileDataTypeGroupId");

                    b.ToTable("FileDataTypes");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileDataTypeGroup", b =>
                {
                    b.Property<int>("FileDataTypeGroupId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FileDataTypeGroupName");

                    b.HasKey("FileDataTypeGroupId");

                    b.ToTable("FileDataTypeGroups");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileModel", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ContentType");

                    b.Property<string>("FileName");

                    b.Property<string>("Name");

                    b.Property<string>("Path");

                    b.Property<DateTime>("UploadDate");

                    b.HasKey("Id");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileToFileType", b =>
                {
                    b.Property<int>("FileToFileTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("FileDataTypeId");

                    b.Property<int>("FileModelId");

                    b.HasKey("FileToFileTypeId");

                    b.HasIndex("FileDataTypeId");

                    b.HasIndex("FileModelId");

                    b.ToTable("FileToFileTypes");
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

                    b.Property<int?>("StructUniversityId");

                    b.HasKey("EmailId");

                    b.HasIndex("StructInstituteId");

                    b.HasIndex("StructUniversityId");

                    b.ToTable("Emails");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Fax", b =>
                {
                    b.Property<int>("FaxId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("FaxComment");

                    b.Property<string>("FaxValue");

                    b.Property<int?>("StructInstituteId");

                    b.Property<int?>("StructUniversityId");

                    b.HasKey("FaxId");

                    b.HasIndex("StructInstituteId");

                    b.HasIndex("StructUniversityId");

                    b.ToTable("Faxes");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Post", b =>
                {
                    b.Property<int>("PostId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("PostName");

                    b.HasKey("PostId");

                    b.ToTable("Posts");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructFacultet", b =>
                {
                    b.Property<int>("StructFacultetId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StructInstituteId");

                    b.Property<int>("StructSubvisionId");

                    b.HasKey("StructFacultetId");

                    b.HasIndex("StructInstituteId");

                    b.HasIndex("StructSubvisionId");

                    b.ToTable("StructFacultets");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructInstitute", b =>
                {
                    b.Property<int>("StructInstituteId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<bool>("ExistenceOfFilials");

                    b.Property<string>("StructInstituteName");

                    b.Property<int>("UniversityId");

                    b.Property<string>("WorkingRegime");

                    b.Property<string>("WorkingSchedule");

                    b.HasKey("StructInstituteId");

                    b.HasIndex("AddressId");

                    b.HasIndex("UniversityId");

                    b.ToTable("StructInstitutes");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructKaf", b =>
                {
                    b.Property<int>("StructKafId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("StructFacultetId");

                    b.Property<int>("StructSubvisionId");

                    b.HasKey("StructKafId");

                    b.HasIndex("StructFacultetId");

                    b.HasIndex("StructSubvisionId");

                    b.ToTable("StructKafs");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructSubvision", b =>
                {
                    b.Property<int>("StructSubvisionId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("StructStandingOrderId");

                    b.Property<int>("StructSubvisionAdressId");

                    b.Property<int?>("StructSubvisionEmailId");

                    b.Property<string>("StructSubvisionFioChief");

                    b.Property<string>("StructSubvisionName");

                    b.Property<int>("StructSubvisionPostChiefId");

                    b.Property<string>("StructSubvisionSite");

                    b.Property<int>("StructSubvisionTypeId");

                    b.HasKey("StructSubvisionId");

                    b.HasIndex("StructStandingOrderId");

                    b.HasIndex("StructSubvisionAdressId");

                    b.HasIndex("StructSubvisionEmailId");

                    b.HasIndex("StructSubvisionPostChiefId");

                    b.HasIndex("StructSubvisionTypeId");

                    b.ToTable("StructSubvisions");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructSubvisionType", b =>
                {
                    b.Property<int>("StructSubvisionTypeId")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("StructSubvisionTypeName");

                    b.HasKey("StructSubvisionTypeId");

                    b.ToTable("StructSubvisionTypes");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructUniversity", b =>
                {
                    b.Property<int>("StructUniversityId")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AddressId");

                    b.Property<DateTime>("DateOfCreation");

                    b.Property<bool>("ExistenceOfFilials");

                    b.Property<string>("StructUniversityName");

                    b.Property<string>("WorkingRegime");

                    b.Property<string>("WorkingSchedule");

                    b.HasKey("StructUniversityId");

                    b.HasIndex("AddressId");

                    b.ToTable("StructUniversities");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Telephone", b =>
                {
                    b.Property<int>("TelephoneId")
                        .ValueGeneratedOnAdd();

                    b.Property<int?>("StructInstituteId");

                    b.Property<int?>("StructUniversityId");

                    b.Property<string>("TelephoneComment");

                    b.Property<string>("TelephoneNumber");

                    b.HasKey("TelephoneId");

                    b.HasIndex("StructInstituteId");

                    b.HasIndex("StructUniversityId");

                    b.ToTable("Telephones");
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

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduAccred", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.FileModel", "EduAccredFile")
                        .WithMany()
                        .HasForeignKey("EduAccredFileId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduNapravl", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduUgs", "EduUgs")
                        .WithMany("EduNapravls")
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
                        .WithMany("EduProfiles")
                        .HasForeignKey("EduNapravlId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Education.EduUgs", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduAccred", "EduAccred")
                        .WithMany("EduAccredUgses")
                        .HasForeignKey("EduAccredId");

                    b.HasOne("KisVuzDotNetCore2.Models.Education.EduLevel", "EduLevel")
                        .WithMany("EduUgses")
                        .HasForeignKey("EduLevelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileDataType", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.FileDataTypeGroup", "FileDataTypeGroup")
                        .WithMany("FileDataTypes")
                        .HasForeignKey("FileDataTypeGroupId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.FileToFileType", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.FileDataType", "FileDataType")
                        .WithMany("FileToFileTypes")
                        .HasForeignKey("FileDataTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.FileModel", "FileModel")
                        .WithMany("FileToFileTypes")
                        .HasForeignKey("FileModelId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Email", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute")
                        .WithMany("Emailes")
                        .HasForeignKey("StructInstituteId");

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructUniversity")
                        .WithMany("Emailes")
                        .HasForeignKey("StructUniversityId");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Fax", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute")
                        .WithMany("Faxes")
                        .HasForeignKey("StructInstituteId");

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructUniversity")
                        .WithMany("Faxes")
                        .HasForeignKey("StructUniversityId");
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructFacultet", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute", "StructInstitute")
                        .WithMany("StructFacultets")
                        .HasForeignKey("StructInstituteId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructSubvision", "StructSubvision")
                        .WithMany()
                        .HasForeignKey("StructSubvisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructInstitute", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructUniversity", "University")
                        .WithMany("StructInstitutes")
                        .HasForeignKey("UniversityId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructKaf", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructFacultet", "StructFacultet")
                        .WithMany()
                        .HasForeignKey("StructFacultetId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructSubvision", "StructSubvision")
                        .WithMany()
                        .HasForeignKey("StructSubvisionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructSubvision", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.FileModel", "StructStandingOrder")
                        .WithMany()
                        .HasForeignKey("StructStandingOrderId");

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.Address", "StructSubvisionAdress")
                        .WithMany()
                        .HasForeignKey("StructSubvisionAdressId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.Email", "StructSubvisionEmail")
                        .WithMany()
                        .HasForeignKey("StructSubvisionEmailId");

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.Post", "StructSubvisionPostChief")
                        .WithMany()
                        .HasForeignKey("StructSubvisionPostChiefId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructSubvisionType", "StructSubvisionType")
                        .WithMany("StructSubvisions")
                        .HasForeignKey("StructSubvisionTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.StructUniversity", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.Address", "Address")
                        .WithMany()
                        .HasForeignKey("AddressId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("KisVuzDotNetCore2.Models.Struct.Telephone", b =>
                {
                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructInstitute")
                        .WithMany("Telephones")
                        .HasForeignKey("StructInstituteId");

                    b.HasOne("KisVuzDotNetCore2.Models.Struct.StructUniversity")
                        .WithMany("Telephones")
                        .HasForeignKey("StructUniversityId");
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
