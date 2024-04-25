using BDWalks.Api.Models.Domain;
using Microsoft.EntityFrameworkCore;

namespace BDWalks.Api.Data
{
    public class BdWalksDbContext: DbContext
    {
        public BdWalksDbContext(DbContextOptions dbContextOptions) : base(dbContextOptions) 
        { 
        }

        public DbSet<Difficulty> Difficulties { get; set; }
        public DbSet<Region> Regions { get; set; }
        public DbSet<Walk> Walks { get; set; }
    }
}
