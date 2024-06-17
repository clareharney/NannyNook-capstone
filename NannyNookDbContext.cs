using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using NannyNook.Models;

namespace NannyNookcapstone.Data
{
    public class NannyNookDbContext : IdentityDbContext<IdentityUser>
    {
        private readonly IConfiguration _configuration;

        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<Occasion> Occasions { get; set; }
        public DbSet<RSVP> RSVPs { get; set; }
        public DbSet<Category> Categories { get; set; }

        public NannyNookDbContext(DbContextOptions<NannyNookDbContext> options, IConfiguration config) : base(options)
        {
            _configuration = config;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure relationships
            modelBuilder.Entity<RSVP>()
                .HasKey(r => new { r.UserProfileId, r.OccasionId });

            modelBuilder.Entity<RSVP>()
                .HasOne(r => r.UserProfile)
                .WithMany(u => u.RSVPs)
                .HasForeignKey(r => r.UserProfileId);

            modelBuilder.Entity<RSVP>()
                .HasOne(r => r.Occasion)
                .WithMany(o => o.RSVPs)
                .HasForeignKey(r => r.OccasionId);

            modelBuilder.Entity<Occasion>()
                .HasOne(o => o.HostUserProfile)
                .WithMany(u => u.HostedOccasions)
                .HasForeignKey(o => o.HostUserProfileId);

            modelBuilder.Entity<Occasion>()
                .HasOne(o => o.Category)
                .WithMany(c => c.Occasions)
                .HasForeignKey(o => o.CategoryId);

            // Seed Identity Roles
            modelBuilder.Entity<IdentityRole>().HasData(new IdentityRole
            {
                Id = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                Name = "Admin",
                NormalizedName = "admin"
            });

            // Seed Identity Users
            modelBuilder.Entity<IdentityUser>().HasData(new IdentityUser[]
            {
                new IdentityUser
                {
                    Id = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                    UserName = "Administrator",
                    Email = "admina@strator.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
                },
                new IdentityUser
                {
                    Id = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                    UserName = "JohnDoe",
                    Email = "john@doe.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
                },
                new IdentityUser
                {
                    Id = "a7d21fac-3b21-454a-a747-075f072d0cf3",
                    UserName = "JaneSmith",
                    Email = "jane@smith.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
                },
                new IdentityUser
                {
                    Id = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
                    UserName = "AliceJohnson",
                    Email = "alice@johnson.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
                },
                new IdentityUser
                {
                    Id = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
                    UserName = "BobWilliams",
                    Email = "bob@williams.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
                },
                new IdentityUser
                {
                    Id = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
                    UserName = "EveDavis",
                    Email = "eve@davis.comx",
                    PasswordHash = new PasswordHasher<IdentityUser>().HashPassword(null, _configuration["AdminPassword"])
                },
            });

            // Seed Identity User Roles
            modelBuilder.Entity<IdentityUserRole<string>>().HasData(new IdentityUserRole<string>[]
            {
                new IdentityUserRole<string>
                {
                    RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    UserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f"
                },
                new IdentityUserRole<string>
                {
                    RoleId = "c3aaeb97-d2ba-4a53-a521-4eea61e59b35",
                    UserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df"
                },
            });

            // Seed UserProfiles
            modelBuilder.Entity<UserProfile>().HasData(new UserProfile[]
            {
                new UserProfile
                {
                    Id = 1,
                    IdentityUserId = "dbc40bc6-0829-4ac5-a3ed-180f5e916a5f",
                    FirstName = "Admina",
                    LastName = "Strator",
                    UserName = "Admina.Strator",
                    Email = "admina@strator.comx",
                    Location = "New York City",
                    IsNanny = false,
                    IsParent = true,
                    CreateDateTime = new DateTime(2022, 1, 25),
                    ProfileImage = "https://robohash.org/numquamutut.png?size=150x150&set=set1"
                },
                new UserProfile
                {
                    Id = 2,
                    IdentityUserId = "d8d76512-74f1-43bb-b1fd-87d3a8aa36df",
                    FirstName = "John",
                    LastName = "Doe",
                    UserName = "John.Doe",
                    Email = "john@doe.comx",
                    Location = "Los Angeles",
                    IsNanny = true,
                    IsParent = false,
                    CreateDateTime = new DateTime(2023, 2, 2),
                    ProfileImage = "https://robohash.org/nisiautemet.png?size=150x150&set=set1"
                },
                new UserProfile
                {
                    Id = 3,
                    IdentityUserId = "a7d21fac-3b21-454a-a747-075f072d0cf3",
                    FirstName = "Jane",
                    LastName = "Smith",
                    UserName = "Jane.Smith",
                    Email = "jane@smith.comx",
                    Location = "Chicago",
                    IsNanny = true,
                    IsParent = false,
                    CreateDateTime = new DateTime(2022, 3, 15),
                    ProfileImage = "https://robohash.org/ametquisquam.png?size=150x150&set=set1"
                },
                new UserProfile
                {
                    Id = 4,
                    IdentityUserId = "c806cfae-bda9-47c5-8473-dd52fd056a9b",
                    FirstName = "Alice",
                    LastName = "Johnson",
                    UserName = "Alice.Johnson",
                    Email = "alice@johnson.comx",
                    Location = "Houston",
                    IsNanny = false,
                    IsParent = true,
                    CreateDateTime = new DateTime(2021, 11, 8),
                    ProfileImage = "https://robohash.org/quiaquia.png?size=150x150&set=set1"
                },
                new UserProfile
                {
                    Id = 5,
                    IdentityUserId = "9ce89d88-75da-4a80-9b0d-3fe58582b8e2",
                    FirstName = "Bob",
                    LastName = "Williams",
                    UserName = "Bob.Williams",
                    Email = "bob@williams.comx",
                    Location = "Phoenix",
                    IsNanny = false,
                    IsParent = true,
                    CreateDateTime = new DateTime(2021, 10, 15),
                    ProfileImage = "https://robohash.org/quisapientenon.png?size=150x150&set=set1"
                },
                new UserProfile
                {
                    Id = 6,
                    IdentityUserId = "d224a03d-bf0c-4a05-b728-e3521e45d74d",
                    FirstName = "Eve",
                    LastName = "Davis",
                    UserName = "Eve.Davis",
                    Email = "eve@davis.comx",
                    Location = "Philadelphia",
                    IsNanny = true,
                    IsParent = false,
                    CreateDateTime = new DateTime(2021, 9, 22),
                    ProfileImage = "https://robohash.org/voluptatesaspernaturbeatae.png?size=150x150&set=set1"
                }
            });


            // Seed Categories
            modelBuilder.Entity<Category>().HasData(new Category[]
            {
                new Category { Id = 1, Name = "Group Playdate" },
                new Category { Id = 2, Name = "After-work Hangout" },
                new Category { Id = 3, Name = "Long Weekend Retreat" },
                new Category { Id = 4, Name = "Night Out" },
                new Category { Id = 5, Name = "Saturday Lunch" },
                new Category { Id = 6, Name = "Other" }
            });

            // Seed Occasions
            modelBuilder.Entity<Occasion>().HasData(new Occasion[]
            {
                new Occasion
                {
                    Id = 1,
                    Title = "Kids Playdate at the Park",
                    Description = "A fun gathering for kids to play and make new friends at the local park.",
                    State = "New York",
                    City = "New York City",
                    Location = "Central Park",
                    CategoryId = 1,
                    Date = new DateTime(2024, 6, 15, 10, 0, 0),
                    OccasionImage = "https://via.placeholder.com/300",
                    HostUserProfileId = 2
                },
                new Occasion
                {
                    Id = 2,
                    Title = "Happy Hour Networking Event",
                    Description = "Networking event for professionals to connect after work over drinks.",
                    State = "California",
                    City = "Los Angeles",
                    Location = "Local Bar",
                    CategoryId = 2,
                    Date = new DateTime(2024, 6, 20, 18, 0, 0),
                    OccasionImage = "https://via.placeholder.com/300",
                    HostUserProfileId = 3
                },
                new Occasion
                {
                    Id = 3,
                    Title = "Mountain Cabin Getaway",
                    Description = "Escape to a cozy cabin in the mountains for a relaxing weekend retreat.",
                    State = "Colorado",
                    City = "Denver",
                    Location = "Mountain Cabin",
                    CategoryId = 3,
                    Date = new DateTime(2024, 7, 4, 12, 0, 0),
                    OccasionImage = "https://via.placeholder.com/300",
                    HostUserProfileId = 4
                },
                new Occasion
                {
                    Id = 4,
                    Title = "Downtown Bar Crawl",
                    Description = "Explore the nightlife of downtown with friends on this bar crawl adventure.",
                    State = "Texas",
                    City = "Austin",
                    Location = "Downtown Bars",
                    CategoryId = 4,
                    Date = new DateTime(2024, 7, 10, 20, 0, 0),
                    OccasionImage = "https://via.placeholder.com/300",
                    HostUserProfileId = 5
                },
                new Occasion
                {
                    Id = 5,
                    Title = "Saturday Picnic in the Park",
                    Description = "Gather for a relaxing picnic in the park on a sunny Saturday afternoon.",
                    State = "Arizona",
                    City = "Phoenix",
                    Location = "City Park",
                    CategoryId = 5,
                    Date = new DateTime(2024, 7, 17, 12, 0, 0),
                    OccasionImage = "https://via.placeholder.com/300",
                    HostUserProfileId = 6
                },
                new Occasion
                {
                    Id = 6,
                    Title = "Book Club Meeting",
                    Description = "Discuss the latest book selection with fellow book enthusiasts at the local library.",
                    State = "Pennsylvania",
                    City = "Philadelphia",
                    Location = "Local Library",
                    CategoryId = 6,
                    Date = new DateTime(2024, 7, 24, 14, 0, 0),
                    OccasionImage = "https://via.placeholder.com/300",
                    HostUserProfileId = 1
                }
            });

            // Seed RSVPs
            modelBuilder.Entity<RSVP>().HasData(new RSVP[]
            {
                new RSVP
                {
                    UserProfileId = 1,
                    OccasionId = 1
                },
                new RSVP
                {
                    UserProfileId = 2,
                    OccasionId = 2
                },
                new RSVP
                {
                    UserProfileId = 3,
                    OccasionId = 3
                },
                new RSVP
                {
                    UserProfileId = 4,
                    OccasionId = 4
                },
                new RSVP
                {
                    UserProfileId = 5,
                    OccasionId = 5
                }
            });
        }
    }
}
