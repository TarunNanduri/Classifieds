using Classifieds.App.Models;
using Microsoft.EntityFrameworkCore;

namespace Classifieds.App.Services
{
    public class ClassifiedsContext : DbContext
    {
        public ClassifiedsContext(DbContextOptions<ClassifiedsContext> options)
            : base(options)
        {
        }

        public DbSet<Advertisement> Advertisement { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Status> Status { get; set; }
        public DbSet<Report> Report { get; set; }
        public DbSet<Offer> Offer { get; set; }
        public DbSet<Location> Location { get; set; }
        public DbSet<Inbox> InBox { get; set; }
        public DbSet<Image> Image { get; set; }
        public DbSet<Comment> Comment { get; set; }
        public DbSet<ChatBox> ChatBox { get; set; }
        public DbSet<Attribute> Attribute { get; set; }
        public DbSet<AdType> AdType { get; set; }
        public DbSet<Category> Category { get; set; }
        public DbSet<AttributeDetail> AttributeDetail { get; set; }
    }
}