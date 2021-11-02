using backend.Model;
using backend.Model.ModelConfiguration;
using Microsoft.EntityFrameworkCore;

namespace backend.DAL
{
    public class DrugStoreContext : DbContext
    {
        public DrugStoreContext(DbContextOptions<DrugStoreContext> options) : base(options) { }

        public DbSet<Medicine> Medicine { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new MedicineConfiguration());
        }
    }
}
