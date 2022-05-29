using Microsoft.EntityFrameworkCore;

namespace Forum.Models
{
    public class ForumContext : DbContext
    {
        public DbSet<User> Users => Set<User>();
        public DbSet<Topic> Topics => Set<Topic>();
        public DbSet<Post> Posts => Set<Post>();
        public DbSet<Role> Roles => Set<Role>();

        public ForumContext(DbContextOptions<ForumContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            string adminRoleName = "admin";
            string userRoleName = "user";
            string adminEmail = "admin@gmail.com";
            string adminPassword = "123456";
            string adminName = "MAKSYM";

            Role adminRole = new Role { Id = 1, Name = adminRoleName };
            Role buyerRole = new Role { Id = 2, Name = userRoleName };
            User adminUser = new User { Id = 1, Email = adminEmail, Password = adminPassword, RoleId = adminRole.Id, Name = adminName };
            Topic main = new Topic { Id = 1, Name = "Main", CreatedDate = DateTime.Now};

            modelBuilder.Entity<Role>().HasData(new Role[] { adminRole, buyerRole });
            modelBuilder.Entity<User>().HasData(new User[] { adminUser });
            modelBuilder.Entity<Topic>().HasData(new Topic[] { main });
            base.OnModelCreating(modelBuilder);
        }
    }
}
