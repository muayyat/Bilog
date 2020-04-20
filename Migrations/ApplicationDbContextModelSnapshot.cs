﻿// <auto-generated />
using System;
using Bilog.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace Bilog.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "3.1.2");

            modelBuilder.Entity("Bilog.Data.Models.ApplicationUser", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<int>("AccessFailedCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("DisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<bool>("EmailConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<bool>("LockoutEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<DateTimeOffset?>("LockoutEnd")
                        .HasColumnType("TEXT");

                    b.Property<bool>("NewsletterSubscriber")
                        .HasColumnType("INTEGER");

                    b.Property<string>("NormalizedEmail")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedUserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("PasswordHash")
                        .HasColumnType("TEXT");

                    b.Property<string>("PhoneNumber")
                        .HasColumnType("TEXT");

                    b.Property<bool>("PhoneNumberConfirmed")
                        .HasColumnType("INTEGER");

                    b.Property<string>("SecurityStamp")
                        .HasColumnType("TEXT");

                    b.Property<bool>("TwoFactorEnabled")
                        .HasColumnType("INTEGER");

                    b.Property<string>("UserName")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Bilog.Data.Models.BlogCategory", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BlogId")
                        .HasColumnType("INTEGER");

                    b.Property<int>("CategoryId")
                        .HasColumnType("INTEGER");

                    b.HasKey("Id");

                    b.HasIndex("BlogId");

                    b.HasIndex("CategoryId");

                    b.ToTable("BlogCategory");
                });

            modelBuilder.Entity("Bilog.Data.Models.Blogs", b =>
                {
                    b.Property<int>("BlogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Banner")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlogContent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("BlogDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlogSummary")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlogTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("BlogUserName")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.HasKey("BlogId");

                    b.ToTable("Blogs");
                });

            modelBuilder.Entity("Bilog.Data.Models.Categorys", b =>
                {
                    b.Property<int>("CategoryId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Description")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.HasKey("CategoryId");

                    b.ToTable("Categorys");
                });

            modelBuilder.Entity("Bilog.Data.Models.Comment", b =>
                {
                    b.Property<int>("CommentId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<int>("BlogId")
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CommentCreated")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentIpaddress")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("CommentUpdated")
                        .HasColumnType("TEXT");

                    b.Property<string>("CommentUserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("Content")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .HasColumnType("TEXT");

                    b.Property<int?>("ParentCommentId")
                        .HasColumnType("INTEGER");

                    b.HasKey("CommentId");

                    b.HasIndex("BlogId");

                    b.HasIndex("ParentCommentId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("Bilog.Data.Models.Contacts", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime?>("Date")
                        .HasColumnType("TEXT");

                    b.Property<string>("Details")
                        .HasColumnType("TEXT");

                    b.Property<string>("Email")
                        .HasColumnType("TEXT");

                    b.Property<string>("Fullname")
                        .HasColumnType("TEXT");

                    b.Property<string>("Phone")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Replied")
                        .HasColumnType("TEXT");

                    b.Property<string>("Reply")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("Seen")
                        .HasColumnType("TEXT");

                    b.Property<string>("Source")
                        .HasColumnType("TEXT");

                    b.Property<string>("Title")
                        .HasColumnType("TEXT");

                    b.Property<string>("User")
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.ToTable("Contacts");
                });

            modelBuilder.Entity("Bilog.Data.Models.Files", b =>
                {
                    b.Property<int>("FileId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<DateTime>("CreateDate")
                        .HasColumnType("TEXT");

                    b.Property<int>("DownloadCount")
                        .HasColumnType("INTEGER");

                    b.Property<string>("FileName")
                        .HasColumnType("TEXT");

                    b.Property<string>("FilePath")
                        .HasColumnType("TEXT");

                    b.HasKey("FileId");

                    b.ToTable("Files");
                });

            modelBuilder.Entity("Bilog.Data.Models.Logs", b =>
                {
                    b.Property<int>("LogId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("LogAction")
                        .HasColumnType("TEXT");

                    b.Property<DateTime>("LogDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogIpaddress")
                        .HasColumnType("TEXT");

                    b.Property<string>("LogUserName")
                        .HasColumnType("TEXT");

                    b.HasKey("LogId");

                    b.ToTable("Logs");
                });

            modelBuilder.Entity("Bilog.Data.Models.Projects", b =>
                {
                    b.Property<int>("ProjectId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("Banner")
                        .HasColumnType("TEXT");

                    b.Property<string>("ELink")
                        .HasColumnType("TEXT");

                    b.Property<string>("Link")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectCategory")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectClient")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectContent")
                        .HasColumnType("TEXT");

                    b.Property<DateTime?>("ProjectDate")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectSubTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectTitle")
                        .HasColumnType("TEXT");

                    b.Property<string>("ProjectUserName")
                        .HasColumnType("TEXT");

                    b.HasKey("ProjectId");

                    b.ToTable("Projects");
                });

            modelBuilder.Entity("Bilog.Data.Models.Settings", b =>
                {
                    b.Property<int>("SettingId")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("SettingName")
                        .HasColumnType("TEXT");

                    b.Property<string>("SettingValue")
                        .HasColumnType("TEXT");

                    b.HasKey("SettingId");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRole", b =>
                {
                    b.Property<string>("Id")
                        .HasColumnType("TEXT");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken()
                        .HasColumnType("TEXT");

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(256);

                    b.Property<string>("NormalizedName")
                        .HasColumnType("TEXT")
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
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("INTEGER");

                    b.Property<string>("ClaimType")
                        .HasColumnType("TEXT");

                    b.Property<string>("ClaimValue")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderKey")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("ProviderDisplayName")
                        .HasColumnType("TEXT");

                    b.Property<string>("UserId")
                        .IsRequired()
                        .HasColumnType("TEXT");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("RoleId")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.Property<string>("UserId")
                        .HasColumnType("TEXT");

                    b.Property<string>("LoginProvider")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Name")
                        .HasColumnType("TEXT")
                        .HasMaxLength(128);

                    b.Property<string>("Value")
                        .HasColumnType("TEXT");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("Bilog.Data.Models.BlogCategory", b =>
                {
                    b.HasOne("Bilog.Data.Models.Blogs", "Blog")
                        .WithMany("BlogCategory")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bilog.Data.Models.Categorys", "Category")
                        .WithMany("BlogCategory")
                        .HasForeignKey("CategoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Bilog.Data.Models.Comment", b =>
                {
                    b.HasOne("Bilog.Data.Models.Blogs", "Blog")
                        .WithMany("Comment")
                        .HasForeignKey("BlogId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bilog.Data.Models.Comment", "ParentComment")
                        .WithMany("InverseParentComment")
                        .HasForeignKey("ParentCommentId");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityRoleClaim<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserClaim<string>", b =>
                {
                    b.HasOne("Bilog.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserLogin<string>", b =>
                {
                    b.HasOne("Bilog.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserRole<string>", b =>
                {
                    b.HasOne("Microsoft.AspNetCore.Identity.IdentityRole", null)
                        .WithMany()
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Bilog.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.IdentityUserToken<string>", b =>
                {
                    b.HasOne("Bilog.Data.Models.ApplicationUser", null)
                        .WithMany()
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });
#pragma warning restore 612, 618
        }
    }
}