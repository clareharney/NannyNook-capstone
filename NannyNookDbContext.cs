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
        public DbSet<Resource> Resources { get; set; }
        public DbSet<Job> Jobs { get; set; }

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

            modelBuilder.Entity<Job>().HasData(new Job[]
            {
                new Job
                {
                    Id = 1,
                    Title = "Full-Time Nanny for 2 Kids",
                    Description = "Seeking an experienced nanny to care for our 2 children full-time.",
                    PayRateMin = 15.00M,
                    PayRateMax = 20.00M,
                    NumberOfKids = 2,
                    FullTime = true,
                    ContactInformation = "email@example.com"
                },
                new Job
                {
                    Id = 2,
                    Title = "Part-Time Babysitter Needed",
                    Description = "Looking for a reliable babysitter for occasional evenings and weekends.",
                    PayRateMin = 10.00M,
                    PayRateMax = 15.00M,
                    NumberOfKids = 1,
                    FullTime = false,
                    ContactInformation = "phone@example.com"
                },
                new Job
                {
                    Id = 3,
                    Title = "After-School Nanny",
                    Description = "Need a nanny to pick up kids from school and care for them until 6 PM.",
                    PayRateMin = 12.00M,
                    PayRateMax = 18.00M,
                    NumberOfKids = 3,
                    FullTime = false,
                    ContactInformation = "contact@example.com"
                },
                new Job
                {
                    Id = 4,
                    Title = "Live-In Nanny Position",
                    Description = "Offering a live-in position for a nanny to assist with childcare and household duties.",
                    PayRateMin = 0.00M,  // Assuming live-in position may have different pay structure
                    PayRateMax = 25.00M,
                    NumberOfKids = 4,
                    FullTime = true,
                    ContactInformation = "livein@example.com"
                }
            });
            modelBuilder.Entity<Resource>().HasData(new Resource[]
            {
                // Books
                new Resource
                {
                    Id = 1,
                    Title = "The Whole-Brain Child",
                    Description = "Explains how to nurture children's developing minds and foster healthy emotional and intellectual growth.",
                    Type = "Book",
                    Url = "",
                    Author = "Daniel J. Siegel and Tina Payne Bryson",
                    PublicationDate = new DateTime(2011, 10, 4)
                },
                new Resource
                {
                    Id = 2,
                    Title = "How to Talk So Kids Will Listen & Listen So Kids Will Talk",
                    Description = "Offers effective communication strategies for parents to better connect with their children.",
                    Type = "Book",
                    Url = "",
                    Author = "Adele Faber and Elaine Mazlish",
                    PublicationDate = new DateTime(2012, 2, 7)
                },
                new Resource
                {
                    Id = 3,
                    Title = "Parenting with Love and Logic",
                    Description = "Focuses on raising responsible children through a balance of love and discipline.",
                    Type = "Book",
                    Url = "",
                    Author = "Charles Fay and Foster Cline",
                    PublicationDate = new DateTime(2006, 4, 1)
                },
                new Resource
                {
                    Id = 4,
                    Title = "No-Drama Discipline",
                    Description = "Provides strategies for disciplining children in a way that promotes emotional intelligence and self-discipline.",
                    Type = "Book",
                    Url = "",
                    Author = "Daniel J. Siegel and Tina Payne Bryson",
                    PublicationDate = new DateTime(2014, 9, 23)
                },
                new Resource
                {
                    Id = 5,
                    Title = "The Happiest Baby on the Block",
                    Description = "Shares techniques to soothe and calm infants, promoting better sleep and less crying.",
                    Type = "Book",
                    Url = "",
                    Author = "Harvey Karp",
                    PublicationDate = new DateTime(2002, 5, 27)
                },

                // Websites
                new Resource
                {
                    Id = 6,
                    Title = "Parenting.com",
                    Description = "Offers articles, tips, and advice on various parenting topics including health, behavior, and education.",
                    Type = "Website",
                    Url = "https://www.parenting.com/",
                    Author = "Parenting.com",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 7,
                    Title = "Zero to Three",
                    Description = "Focuses on early childhood development, offering resources for parents of infants and toddlers.",
                    Type = "Website",
                    Url = "https://www.zerotothree.org/",
                    Author = "Zero to Three",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 8,
                    Title = "HealthyChildren.org",
                    Description = "Run by the American Academy of Pediatrics, provides health and safety information for children from infancy through adolescence.",
                    Type = "Website",
                    Url = "https://www.healthychildren.org/",
                    Author = "American Academy of Pediatrics",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 9,
                    Title = "KidsHealth",
                    Description = "Offers reliable information on children's health, behavior, and development, including parenting advice and resources.",
                    Type = "Website",
                    Url = "https://kidshealth.org/",
                    Author = "Nemours Foundation",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 10,
                    Title = "Common Sense Media",
                    Description = "Provides reviews and advice on media content for children, helping parents make informed decisions about screen time and media consumption.",
                    Type = "Website",
                    Url = "https://www.commonsensemedia.org/",
                    Author = "Common Sense Media",
                    PublicationDate = DateTime.UtcNow
                },

                // Podcasts
                new Resource
                {
                    Id = 11,
                    Title = "The Longest Shortest Time",
                    Description = "Discusses various parenting topics, sharing stories and experiences from different perspectives.",
                    Type = "Podcast",
                    Url = "https://www.longestshortesttime.com/",
                    Author = "Hillary Frank",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 12,
                    Title = "Parenting Beyond Discipline",
                    Description = "Offers practical advice on handling various parenting challenges and promoting positive behavior.",
                    Type = "Podcast",
                    Url = "https://www.parentingbeyonddiscipline.com/",
                    Author = "Erin Royer",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 13,
                    Title = "The Mom Hour",
                    Description = "Features conversations on a wide range of parenting topics, with practical tips and relatable stories.",
                    Type = "Podcast",
                    Url = "https://www.themomhour.com/",
                    Author = "Meagan Francis and Sarah Powers",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 14,
                    Title = "Unruffled with Janet Lansbury",
                    Description = "Focuses on respectful parenting approaches and offers solutions to common parenting challenges.",
                    Type = "Podcast",
                    Url = "https://www.janetlansbury.com/podcast/",
                    Author = "Janet Lansbury",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 15,
                    Title = "The Child Repair Guide",
                    Description = "Provides insights from a pediatrician on children's health, development, and parenting strategies.",
                    Type = "Podcast",
                    Url = "https://www.drstevesilvestro.com/podcast",
                    Author = "Dr. Steve Silvestro",
                    PublicationDate = DateTime.UtcNow
                },

                // Online Courses
                new Resource
                {
                    Id = 16,
                    Title = "Positive Parenting Solutions",
                    Description = "Offers online courses and resources to help parents address common behavior problems and improve communication with their children.",
                    Type = "Online Course",
                    Url = "https://www.positiveparentingsolutions.com/",
                    Author = "Amy McCready",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 17,
                    Title = "Big Little Feelings",
                    Description = "Provides online courses focused on toddler behavior and emotional regulation.",
                    Type = "Online Course",
                    Url = "https://www.biglittlefeelings.com/",
                    Author = "Deena Margolin and Kristin Gallant",
                    PublicationDate = DateTime.UtcNow
                },
                new Resource
                {
                    Id = 18,
                    Title = "Coursera",
                    Description = "Features various courses on child development and parenting from reputable institutions.",
                    Type = "Online Course",
                    Url = "https://www.coursera.org/",
                    Author = "Coursera",
                    PublicationDate = DateTime.UtcNow
                },

                // Support Groups and Communities
                new Resource
                {
                    Id = 19,
                    Title = "La Leche League International",
                    Description = "Provides breastfeeding support and resources, including local support groups.",
                    Type = "Support Group",
                    Url = "https://www.llli.org/",
                    Author = "La Leche League International",
                    PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                    Id = 20,
                    Title = "The Bump Community",
                    Description = "Offers forums for parents to share experiences and seek advice on various parenting topics.",
                    Type = "Support Group",
                    Url = "https://community.thebump.com/",
                    Author = "The Bump",
                    PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 21,
                        Title = "Parenting Subreddits",
                        Description = "Communities like r/Parenting and r/ParentingToddlers offer support and advice from fellow parents.",
                        Type = "Support Group",
                        Url = "https://www.reddit.com/r/parenting",
                        Author = "",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 22,
                        Title = "Caring for Your Baby and Young Child: Birth to Age 5",
                        Description = "Comprehensive guide covering development, health, and safety from birth through early childhood.",
                        Type = "Book",
                        Url = "",
                        Author = "American Academy of Pediatrics",
                        PublicationDate = new DateTime(2019, 9, 24)
                    },
                    new Resource
                    {
                        Id = 23,
                        Title = "What to Expect the First Year",
                        Description = "A detailed guide to the first year of a baby's life, including milestones and development.",
                        Type = "Book",
                        Url = "",
                        Author = "Heidi Murkoff",
                        PublicationDate = new DateTime(2014, 10, 7)
                    },
                    new Resource
                    {
                        Id = 24,
                        Title = "CDC’s Developmental Milestones",
                        Description = "Offers a detailed guide on what to expect at each stage of your child's development.",
                        Type = "Website",
                        Url = "https://www.cdc.gov/ncbddd/actearly/milestones/index.html",
                        Author = "Centers for Disease Control and Prevention",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 25,
                        Title = "HealthyChildren.org Developmental Milestones",
                        Description = "Provides information from the American Academy of Pediatrics on developmental stages from infancy through adolescence.",
                        Type = "Website",
                        Url = "https://www.healthychildren.org/English/ages-stages/Pages/default.aspx",
                        Author = "American Academy of Pediatrics",
                        PublicationDate = DateTime.UtcNow
                    },

                    // Healthy Eating for Kids
                    new Resource
                    {
                        Id = 26,
                        Title = "Super Baby Food",
                        Description = "Comprehensive guide on making nutritious baby food and establishing healthy eating habits.",
                        Type = "Book",
                        Url = "",
                        Author = "Ruth Yaron",
                        PublicationDate = new DateTime(2013, 9, 3)
                    },
                    new Resource
                    {
                        Id = 27,
                        Title = "The Pediatrician's Guide to Feeding Babies and Toddlers",
                        Description = "Offers advice on nutrition and feeding practices from infancy through toddlerhood.",
                        Type = "Book",
                        Url = "",
                        Author = "Anthony Porto and Dina DiMaggio",
                        PublicationDate = new DateTime(2016, 9, 6)
                    },
                    new Resource
                    {
                        Id = 28,
                        Title = "Real Food for Healthy Kids",
                        Description = "Provides recipes and tips for feeding kids nutritious meals.",
                        Type = "Book",
                        Url = "",
                        Author = "Tanya Wenman Steel and Tracey Seaman",
                        PublicationDate = new DateTime(2008, 8, 5)
                    },
                    new Resource
                    {
                        Id = 29,
                        Title = "ChooseMyPlate.gov",
                        Description = "Offers guidelines and resources for balanced diets based on the USDA's MyPlate framework.",
                        Type = "Website",
                        Url = "https://www.choosemyplate.gov/",
                        Author = "U.S. Department of Agriculture",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 30,
                        Title = "HealthyChildren.org Nutrition",
                        Description = "Provides articles and advice from the American Academy of Pediatrics on children's nutrition.",
                        Type = "Website",
                        Url = "https://www.healthychildren.org/English/healthy-living/nutrition/Pages/default.aspx",
                        Author = "American Academy of Pediatrics",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 31,
                        Title = "Kids Eat Right",
                        Description = "Offers tips and recipes from the Academy of Nutrition and Dietetics to promote healthy eating in children.",
                        Type = "Website",
                        Url = "https://www.eatright.org/kids",
                        Author = "Academy of Nutrition and Dietetics",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 32,
                        Title = "Ellyn Satter Institute",
                        Description = "Provides resources on child feeding practices and establishing a positive eating environment.",
                        Type = "Website",
                        Url = "https://www.ellynsatterinstitute.org/",
                        Author = "Ellyn Satter Institute",
                        PublicationDate = DateTime.UtcNow
                    },

                    // Podcasts
                    new Resource
                    {
                        Id = 33,
                        Title = "The Nourished Child",
                        Description = "Hosted by registered dietitian Jill Castle, offering advice on child nutrition, feeding practices, and healthy eating habits.",
                        Type = "Podcast",
                        Url = "https://jillcastle.com/podcast/",
                        Author = "Jill Castle",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 34,
                        Title = "Kids Nutrition Podcast",
                        Description = "Offers tips and insights on feeding children nutritious meals and establishing healthy eating patterns.",
                        Type = "Podcast",
                        Url = "https://www.kidsnutritionpodcast.com/",
                        Author = "Katie Goldberg",
                        PublicationDate = DateTime.UtcNow
                    },

                    // Online Courses
                    new Resource
                    {
                        Id = 35,
                        Title = "Stanford Introduction to Food and Health",
                        Description = "While not specific to children, this course offers valuable insights into healthy eating practices.",
                        Type = "Online Course",
                        Url = "https://online.stanford.edu/courses/som-y0010-introduction-food-and-health",
                        Author = "Stanford University",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 36,
                        Title = "Nutrition and Healthy Eating for Kids on Udemy",
                        Description = "Focuses on teaching parents how to create balanced diets and encourage healthy eating habits in children.",
                        Type = "Online Course",
                        Url = "https://www.udemy.com/course/nutrition-and-healthy-eating-for-kids/",
                        Author = "Udemy",
                        PublicationDate = DateTime.UtcNow
                    },

                    // Apps
                    new Resource
                    {
                        Id = 37,
                        Title = "Fooducate",
                        Description = "Helps parents make healthier food choices by providing nutritional information and suggestions.",
                        Type = "App",
                        Url = "https://www.fooducate.com/",
                        Author = "Fooducate Ltd.",
                        PublicationDate = DateTime.UtcNow
                    },
                    new Resource
                    {
                        Id = 38,
                        Title = "Yummly",
                        Description = "Offers a variety of healthy recipes, including options for children, and allows for meal planning.",
                        Type = "App",
                        Url = "https://www.yummly.com/",
                        Author = "Yummly",
                        PublicationDate = DateTime.UtcNow
                    }
            });
        }
    }
}
