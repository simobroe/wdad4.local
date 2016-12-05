using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using BnbGo.Db;

namespace BnbGo.Db.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20161205130306_Initial Commit")]
    partial class InitialCommit
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("Npgsql:PostgresExtension:.uuid-ossp", "'uuid-ossp', '', ''")
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431");

            modelBuilder.Entity("BnbGo.Models.City", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Postal");

                    b.Property<int>("RegionId");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("RegionId");

                    b.ToTable("Cities");
                });

            modelBuilder.Entity("BnbGo.Models.Comment", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Content");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<long>("RoomId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Comment");
                });

            modelBuilder.Entity("BnbGo.Models.Country", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<short>("CurrencyTypeId");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Iso2")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("CurrencyTypeId");

                    b.ToTable("Countries");
                });

            modelBuilder.Entity("BnbGo.Models.CurrencyType", b =>
                {
                    b.Property<short>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("CurrencyTypes");
                });

            modelBuilder.Entity("BnbGo.Models.Facility", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("Facilities");
                });

            modelBuilder.Entity("BnbGo.Models.HouseType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("HouseTypes");
                });

            modelBuilder.Entity("BnbGo.Models.Image", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<int?>("ImageTypeId");

                    b.Property<string>("Link")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<long>("RoomId");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("ImageTypeId");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId")
                        .IsUnique();

                    b.ToTable("Images");
                });

            modelBuilder.Entity("BnbGo.Models.ImageType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.ToTable("ImageType");
                });

            modelBuilder.Entity("BnbGo.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("HouseNumber");

                    b.Property<string>("Name");

                    b.Property<long>("RoomId");

                    b.Property<string>("Street");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("RoomId")
                        .IsUnique();

                    b.ToTable("Location");
                });

            modelBuilder.Entity("BnbGo.Models.Rating", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("Amount");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<long>("RoomId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Rating");
                });

            modelBuilder.Entity("BnbGo.Models.Region", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<long>("ImageId");

                    b.Property<int?>("ImageId1");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.HasIndex("CountryId");

                    b.HasIndex("ImageId1");

                    b.ToTable("Regions");
                });

            modelBuilder.Entity("BnbGo.Models.RentType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("RentTypes");
                });

            modelBuilder.Entity("BnbGo.Models.Reservation", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AmountOfGuests")
                        .HasAnnotation("MaxLength", 511);

                    b.Property<DateTime>("Arrival")
                        .HasAnnotation("MaxLength", 511);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<DateTime>("Departure")
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<int>("PriceTotal")
                        .HasAnnotation("MaxLength", 511);

                    b.Property<long>("RoomId");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("RoomId");

                    b.HasIndex("UserId");

                    b.ToTable("Reservations");
                });

            modelBuilder.Entity("BnbGo.Models.ReservationDay", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime>("Day");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description");

                    b.Property<string>("Name");

                    b.Property<long>("ReservationId");

                    b.Property<long>("RoomId");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("ReservationId");

                    b.HasIndex("RoomId");

                    b.ToTable("ReservationDay");
                });

            modelBuilder.Entity("BnbGo.Models.Room", b =>
                {
                    b.Property<long>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<long>("CityId");

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("HouseRules")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<int>("HouseTypeId");

                    b.Property<long>("LocationId");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<int>("PriceBase");

                    b.Property<int>("PriceExtraPerPerson");

                    b.Property<int>("PricePerNight");

                    b.Property<int>("RentTypeId");

                    b.Property<int>("RoomStateId");

                    b.Property<int>("RoomTypeId");

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("HouseTypeId");

                    b.HasIndex("RentTypeId");

                    b.HasIndex("RoomStateId");

                    b.HasIndex("RoomTypeId");

                    b.HasIndex("UserId");

                    b.ToTable("Rooms");
                });

            modelBuilder.Entity("BnbGo.Models.RoomFacility", b =>
                {
                    b.Property<long>("RoomId");

                    b.Property<long>("FacilityId");

                    b.HasKey("RoomId", "FacilityId");

                    b.HasIndex("FacilityId");

                    b.HasIndex("RoomId");

                    b.ToTable("RoomFacility");
                });

            modelBuilder.Entity("BnbGo.Models.RoomState", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("RoomStates");
                });

            modelBuilder.Entity("BnbGo.Models.RoomType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<short>("BedAmount")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime>("CreatedAt")
                        .ValueGeneratedOnAdd()
                        .HasDefaultValueSql("now()");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 511);

                    b.Property<short>("GuestAmount")
                        .HasAnnotation("MaxLength", 255);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 255);

                    b.Property<DateTime?>("UpdatedAt")
                        .ValueGeneratedOnAddOrUpdate()
                        .HasDefaultValueSql("now()");

                    b.HasKey("Id");

                    b.ToTable("RoomTypes");
                });

            modelBuilder.Entity("BnbGo.Models.Security.ApplicationRole", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("RoleDescription");

                    b.Property<string>("RoleName");

                    b.Property<DateTime?>("UpdatedAt");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("AspNetRoles");
                });

            modelBuilder.Entity("BnbGo.Models.Security.ApplicationUser", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<long>("CityId");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<int>("CountryId");

                    b.Property<DateTime>("CreatedAt");

                    b.Property<DateTime?>("DayOfBirth");

                    b.Property<DateTime?>("DeletedAt");

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<string>("FirstName");

                    b.Property<byte>("Gender");

                    b.Property<long>("ImageId");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<int>("RegionId");

                    b.Property<string>("SecurityStamp");

                    b.Property<string>("SurName");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<DateTime?>("UpdatedAt");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("CityId");

                    b.HasIndex("CountryId");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.HasIndex("RegionId");

                    b.ToTable("AspNetUsers");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("AspNetRoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<Guid>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.Property<string>("LoginProvider");

                    b.Property<string>("ProviderKey");

                    b.Property<string>("ProviderDisplayName");

                    b.Property<Guid>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<Guid>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("AspNetUserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<System.Guid>", b =>
                {
                    b.Property<Guid>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("OpenIddict.OpenIddictApplication<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClientId");

                    b.Property<string>("ClientSecret");

                    b.Property<string>("DisplayName");

                    b.Property<string>("LogoutRedirectUri");

                    b.Property<string>("RedirectUri");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ClientId")
                        .IsUnique();

                    b.ToTable("OpenIddictApplications");
                });

            modelBuilder.Entity("OpenIddict.OpenIddictAuthorization<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Scope");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictAuthorizations");
                });

            modelBuilder.Entity("OpenIddict.OpenIddictScope<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description");

                    b.HasKey("Id");

                    b.ToTable("OpenIddictScopes");
                });

            modelBuilder.Entity("OpenIddict.OpenIddictToken<System.Guid>", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<Guid?>("ApplicationId");

                    b.Property<Guid?>("AuthorizationId");

                    b.Property<string>("Type");

                    b.HasKey("Id");

                    b.HasIndex("ApplicationId");

                    b.HasIndex("AuthorizationId");

                    b.ToTable("OpenIddictTokens");
                });

            modelBuilder.Entity("BnbGo.Models.City", b =>
                {
                    b.HasOne("BnbGo.Models.Region", "Region")
                        .WithMany("Cities")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Comment", b =>
                {
                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithMany("Comments")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Security.ApplicationUser", "User")
                        .WithMany("Comments")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Country", b =>
                {
                    b.HasOne("BnbGo.Models.CurrencyType", "CurrencyType")
                        .WithMany("Countries")
                        .HasForeignKey("CurrencyTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Image", b =>
                {
                    b.HasOne("BnbGo.Models.ImageType", "ImageType")
                        .WithMany("Images")
                        .HasForeignKey("ImageTypeId");

                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithMany("Images")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Security.ApplicationUser", "User")
                        .WithOne("Image")
                        .HasForeignKey("BnbGo.Models.Image", "UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Location", b =>
                {
                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithOne("Location")
                        .HasForeignKey("BnbGo.Models.Location", "RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Rating", b =>
                {
                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithMany("Ratings")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Security.ApplicationUser", "User")
                        .WithMany("Ratings")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Region", b =>
                {
                    b.HasOne("BnbGo.Models.Country", "Country")
                        .WithMany("Regions")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Image", "Image")
                        .WithMany()
                        .HasForeignKey("ImageId1");
                });

            modelBuilder.Entity("BnbGo.Models.Reservation", b =>
                {
                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithMany("Reservations")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Security.ApplicationUser", "User")
                        .WithMany("Reservations")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.ReservationDay", b =>
                {
                    b.HasOne("BnbGo.Models.Reservation", "Reservation")
                        .WithMany("ReservationDays")
                        .HasForeignKey("ReservationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithMany("ReservationDays")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Room", b =>
                {
                    b.HasOne("BnbGo.Models.City", "City")
                        .WithMany("Rooms")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.HouseType", "HouseType")
                        .WithMany("Rooms")
                        .HasForeignKey("HouseTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.RentType", "RentType")
                        .WithMany("Rooms")
                        .HasForeignKey("RentTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.RoomState", "RoomsState")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomStateId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.RoomType", "RoomType")
                        .WithMany("Rooms")
                        .HasForeignKey("RoomTypeId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Security.ApplicationUser", "User")
                        .WithMany("Rooms")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.RoomFacility", b =>
                {
                    b.HasOne("BnbGo.Models.Facility", "Facility")
                        .WithMany("Rooms")
                        .HasForeignKey("FacilityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Room", "Room")
                        .WithMany("Facilities")
                        .HasForeignKey("RoomId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("BnbGo.Models.Security.ApplicationUser", b =>
                {
                    b.HasOne("BnbGo.Models.City", "City")
                        .WithMany("Users")
                        .HasForeignKey("CityId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Country", "Country")
                        .WithMany("Users")
                        .HasForeignKey("CountryId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Region", "Region")
                        .WithMany("Users")
                        .HasForeignKey("RegionId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<System.Guid>", b =>
                {
                    b.HasOne("BnbGo.Models.Security.ApplicationRole")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<System.Guid>", b =>
                {
                    b.HasOne("BnbGo.Models.Security.ApplicationUser")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<System.Guid>", b =>
                {
                    b.HasOne("BnbGo.Models.Security.ApplicationUser")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<System.Guid>", b =>
                {
                    b.HasOne("BnbGo.Models.Security.ApplicationRole")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("BnbGo.Models.Security.ApplicationUser")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("OpenIddict.OpenIddictToken<System.Guid>", b =>
                {
                    b.HasOne("OpenIddict.OpenIddictApplication<System.Guid>")
                        .WithMany("Tokens")
                        .HasForeignKey("ApplicationId");

                    b.HasOne("OpenIddict.OpenIddictAuthorization<System.Guid>")
                        .WithMany("Tokens")
                        .HasForeignKey("AuthorizationId");
                });
        }
    }
}
