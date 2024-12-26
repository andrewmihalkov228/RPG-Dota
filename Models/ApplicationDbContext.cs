using Microsoft.EntityFrameworkCore;

namespace RPG_Dota.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<CharacterClass> CharacterClasses { get; set; }
        public DbSet<Character> Characters { get; set; }
        public DbSet<CharacterClassHistory> CharacterClassHistories { get; set; }
        public DbSet<CharacterHistory> CharacterHistories { get; set; }
    }
}
