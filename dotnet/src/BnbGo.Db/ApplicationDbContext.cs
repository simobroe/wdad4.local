using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OpenIddict;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using BnbGo.Models;
using BnbGo.Models.Security;

namespace BnbGo.Db
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>
    {
        public DbSet<Country> Countries { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<CurrencyType> CurrencyTypes { get; set; }

        public DbSet<Room> Rooms { get; set; }
        public DbSet<Reservation> Reservations { get; set; }

        public DbSet<Image> Images { get; set; }
        public DbSet<ImageType> ImageTypes { get; set; }

        public DbSet<Facility> Facilities { get; set; }
        public DbSet<RoomFacility> RoomFacilities { get; set; }
        public DbSet<HouseType> HouseTypes { get; set; }
        public DbSet<RoomType> RoomTypes { get; set; }
        public DbSet<RentType> RentTypes { get; set; }
        public DbSet<RoomState> RoomStates { get; set; }

        public DbSet<Comment> Comments { get; set; }
        public DbSet<Rating> Ratings { get; set; }
        public DbSet<RatingType> RatingTypes { get; set; }
        
        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
            
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.HasPostgresExtension("uuid-ossp");
            
            base.OnModelCreating(builder);
            /// MODELS 

            // Model: ApplicationUser
            builder.Entity<ApplicationUser>()
                .Property(u => u.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<ApplicationUser>()
                .Property(u => u.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            // Model: Country
            builder.Entity<Country>()
                .HasKey(o => o.Id);

            builder.Entity<Country>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Country>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Country>()
                .Property(o => o.Iso2)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Country>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Country>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: Region
            builder.Entity<Region>()
                .HasKey(o => o.Id);

            builder.Entity<Region>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Region>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Region>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Region>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: City
            builder.Entity<City>()
                .HasKey(o => o.Id);

            builder.Entity<City>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<City>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<City>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<City>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: Location
            builder.Entity<Location>()
                .HasKey(o => o.Id);

            builder.Entity<Location>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Location>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Location>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Location>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: CurrencyType
            builder.Entity<CurrencyType>()
                .HasKey(o => o.Id);

            builder.Entity<CurrencyType>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<CurrencyType>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<CurrencyType>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<CurrencyType>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
                
            // Model: Room
            builder.Entity<Room>()
                .HasKey(o => o.Id);

            builder.Entity<Room>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Room>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Room>()
                .Property(o => o.HouseRules)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Room>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Room>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            // Model: Facility
            builder.Entity<Facility>()
                .HasKey(o => o.Id);

            builder.Entity<Facility>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Facility>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Facility>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Facility>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            // Model: Reservation
            builder.Entity<Reservation>()
                .HasKey(o => o.Id);

            builder.Entity<Reservation>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Reservation>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Reservation>()
                .Property(o => o.Arrival)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Reservation>()
                .Property(o => o.Departure)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Reservation>()
                .Property(o => o.PriceTotal)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Reservation>()
                .Property(o => o.AmountOfGuests)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Reservation>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Reservation>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            // Model: HouseType
            builder.Entity<HouseType>()
                .HasKey(o => o.Id);

            builder.Entity<HouseType>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<HouseType>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<HouseType>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<HouseType>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();

            // Model: RoomType
            builder.Entity<RoomType>()
                .HasKey(o => o.Id);

            builder.Entity<RoomType>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<RoomType>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<RoomType>()
                .Property(o => o.GuestAmount)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<RoomType>()
                .Property(o => o.BedAmount)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<RoomType>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<RoomType>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: RentType
            builder.Entity<RentType>()
                .HasKey(o => o.Id);

            builder.Entity<RentType>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<RentType>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<RentType>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<RentType>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: RoomState
            builder.Entity<RoomState>()
                .HasKey(o => o.Id);

            builder.Entity<RoomState>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<RoomState>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<RoomState>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<RoomState>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: Image
            builder.Entity<Image>()
                .HasKey(o => o.Id);

            builder.Entity<Image>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Image>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Image>()
                .Property(o => o.Link)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Image>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Image>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: ImageType
            builder.Entity<ImageType>()
                .HasKey(o => o.Id);

            builder.Entity<ImageType>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<ImageType>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<ImageType>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<ImageType>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: Comment
            builder.Entity<Comment>()
                .HasKey(o => o.Id);

            builder.Entity<Comment>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Comment>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Comment>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Comment>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: Rating
            builder.Entity<Rating>()
                .HasKey(o => o.Id);

            builder.Entity<Rating>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<Rating>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<Rating>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<Rating>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            
            // Model: RatingType
            builder.Entity<RatingType>()
                .HasKey(o => o.Id);

            builder.Entity<RatingType>()
                .Property(o => o.Name)
                .HasMaxLength(255)
                .IsRequired();

            builder.Entity<RatingType>()
                .Property(o => o.Description)
                .HasMaxLength(511)
                .IsRequired();

            builder.Entity<RatingType>()
                .Property(o => o.CreatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAdd();

            builder.Entity<RatingType>()
                .Property(o => o.UpdatedAt)
                .HasDefaultValueSql("now()")
                .ValueGeneratedOnAddOrUpdate();
            

            /// RELATIONS
            // Relationship between User and Country: 0 to many (n)
            builder.Entity<ApplicationUser>()
                .HasOne(l => l.Country)
                .WithMany(o => o.Users)
                .HasForeignKey(l => l.CountryId);

            // Relationship between Region and Country: 0 to many (n)
            builder.Entity<Region>()
                .HasOne(l => l.Country)
                .WithMany(o => o.Regions)
                .HasForeignKey(l => l.CountryId);

            // Relationship between Country and Currency: 0 to many (n)
            builder.Entity<Country>()
                .HasOne(l => l.CurrencyType)
                .WithMany(o => o.Countries)
                .HasForeignKey(l => l.CurrencyTypeId);

            // Relationship between User and Region: 0 to many (n)
            builder.Entity<ApplicationUser>()
                .HasOne(l => l.Region)
                .WithMany(o => o.Users)
                .HasForeignKey(l => l.RegionId);

            // Relationship between City and Region: 0 to many (n)
            builder.Entity<City>()
                .HasOne(l => l.Region)
                .WithMany(o => o.Cities)
                .HasForeignKey(l => l.RegionId);

            // Relationship between Image and Region: 0 to many (n)
            builder.Entity<Image>()
                .HasOne(l => l.Region)
                .WithMany(o => o.Images)
                .HasForeignKey(l => l.RegionId);

            // Relationship between User and City: 0 to many (n)
            builder.Entity<ApplicationUser>()
                .HasOne(l => l.City)
                .WithMany(o => o.Users)
                .HasForeignKey(l => l.CityId);

            // Relationship between Room and City: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.City)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.CityId);

            // Relationship between Location and City: 0 to many (n)
            builder.Entity<Location>()
                .HasOne(g => g.City)
                .WithMany(l => l.Locations)
                .HasForeignKey(g => g.CityId);

            // Relationship between Room and User: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.User)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.UserId);

            // Relationship between reservation and user: 0 to many (n)
            builder.Entity<Reservation>()
                .HasOne(g => g.User)
                .WithMany(l => l.Reservations)
                .HasForeignKey(g => g.UserId);

            // Relationship between Image and User: 0 to many (n)
            builder.Entity<Image>()
                .HasOne(l => l.User)
                .WithMany(o => o.Images)
                .HasForeignKey(l => l.UserId);

            // Relationship between Rating and User: 0 to many (n)
            builder.Entity<Rating>()
                .HasOne(l => l.User)
                .WithMany(o => o.Ratings)
                .HasForeignKey(l => l.UserId);

            // Relationship between Comment and User: 0 to many (n)
            builder.Entity<Comment>()
                .HasOne(l => l.User)
                .WithMany(o => o.Comments)
                .HasForeignKey(l => l.UserId);

            // Relationship between facility and room: many to many
             builder.Entity<RoomFacility>()
                .HasKey(t => new { t.RoomId, t.FacilityId });

            builder.Entity<RoomFacility>()
                .HasOne(pt => pt.Room)
                .WithMany(p => p.Facilities)
                .HasForeignKey(pt => pt.RoomId);

            builder.Entity<RoomFacility>()
                .HasOne(pt => pt.Facility)
                .WithMany(p => p.Rooms)
                .HasForeignKey(pt => pt.FacilityId);

                // Relationship between room and Locations: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.Location)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.LocationId);

            // Relationship between reservation and room: 0 to many (n)
            builder.Entity<Reservation>()
                .HasOne(g => g.Room)
                .WithMany(l => l.Reservations)
                .HasForeignKey(g => g.RoomId);

            // Relationship between image and room: 0 to many (n)
            builder.Entity<Image>()
                .HasOne(g => g.Room)
                .WithMany(l => l.Images)
                .HasForeignKey(g => g.RoomId);

            // Relationship between ratings and room: 0 to many (n)
            builder.Entity<Rating>()
                .HasOne(g => g.Room)
                .WithMany(l => l.Ratings)
                .HasForeignKey(g => g.RoomId);

            // Relationship between comments and room: 0 to many (n)
            builder.Entity<Comment>()
                .HasOne(g => g.Room)
                .WithMany(l => l.Comments)
                .HasForeignKey(g => g.RoomId);

            // Relationship between room and location: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.Location)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.LocationId);

            // Relationship between room and renttype: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.RentType)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.RentTypeId);

            // Relationship between room and roomtype: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.RoomType)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.RoomTypeId);

            // Relationship between room and housetype: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.HouseType)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.HouseTypeId);

            // Relationship between room and roomstate: 0 to many (n)
            builder.Entity<Room>()
                .HasOne(g => g.RoomState)
                .WithMany(l => l.Rooms)
                .HasForeignKey(g => g.RoomStateId);

            // Relationship between ratings and rating types: 0 to many (n)
            builder.Entity<Rating>()
                .HasOne(g => g.RatingType)
                .WithMany(l => l.Ratings)
                .HasForeignKey(g => g.RatingTypeId);

            // Relationship between images and image types: 0 to many (n)
            builder.Entity<Image>()
                .HasOne(g => g.ImageType)
                .WithMany(l => l.Images)
                .HasForeignKey(g => g.ImageTypeId);

        }
    }
}