using Microsoft.EntityFrameworkCore;

namespace PrequalificationTool.Models
{
    public class CardApplicationContext : DbContext
    {
        public virtual DbSet<ApplicationResult> ApplicationResults { get; set; }

        public CardApplicationContext(DbContextOptions options) : base(options)
        { }
    }
}
