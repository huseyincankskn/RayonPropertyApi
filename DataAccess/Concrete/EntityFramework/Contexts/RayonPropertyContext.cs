using Entities.Concrete;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace DataAccess.Concrete
{
    public class RayonPropertyContext : DbContext
    {
        public RayonPropertyContext(DbContextOptions<RayonPropertyContext> options) : base(options) { }
        public DbSet<City> City { get; set; }
        public DbSet<District> District { get; set; }
        public DbSet<Town> Town { get; set; }
        public DbSet<Currency> Currency { get; set; }
        public DbSet<Project> Project { get; set; }
        public DbSet<SiteProperty> SiteProperty { get; set; }
        public DbSet<User> User { get; set; }
        public DbSet<Blog> Blog { get; set; }
        public DbSet<BlogCategory> BlogCategory { get; set; }
        public DbSet<BlogFile> BlogFile { get; set; }
        public DbSet<ContactRequest> ContactRequest { get; set; }
        public DbSet<Comment> Comment { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<City>().ToTable(nameof(City), "Address");
            modelBuilder.Entity<Town>().ToTable(nameof(Town), "Address");
            modelBuilder.Entity<District>().ToTable(nameof(District), "Address");
            modelBuilder.Entity<Street>().ToTable(nameof(Street), "Address");

            modelBuilder.Entity<Project>().ToTable(nameof(Project), "Project");

            modelBuilder.Entity<SiteProperty>().ToTable(nameof(SiteProperty), "SiteProperty");

            modelBuilder.Entity<Currency>().ToTable(nameof(Currency), "Currency");

            modelBuilder.Entity<User>().ToTable(nameof(User), "Auth");

            modelBuilder.Entity<Blog>().ToTable(nameof(Blog), "Blog");
            modelBuilder.Entity<BlogCategory>().ToTable(nameof(BlogCategory), "Blog");
            modelBuilder.Entity<BlogFile>().ToTable(nameof(BlogFile), "Blog");
            modelBuilder.Entity<ContactRequest>().ToTable(nameof(ContactRequest), "Contact");
            modelBuilder.Entity<Comment>().ToTable(nameof(Comment), "Comment");

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
