using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PrequalificationTool.Models
{
    public class CardApplicationContext : DbContext
    {
        public virtual DbSet<ApplicationResult> ApplicationResults { get; set; }

        public CardApplicationContext(DbContextOptions<CardApplicationContext> options) : base(options)
        { }
    }
}
