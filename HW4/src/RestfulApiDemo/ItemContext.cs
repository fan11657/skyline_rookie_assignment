using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace RestfulApiDemo
{
    public class ItemContext : DbContext
    {
        public ItemContext()
        {
        }
        public ItemContext(DbContextOptions<ItemContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Item>()
                .Property(item => item.Id)
                .ValueGeneratedOnAdd();
        }
        public DbSet<Item> Items { get; set; }
    }
}
