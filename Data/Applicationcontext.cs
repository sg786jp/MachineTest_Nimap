using MachineTest.Models;
using Microsoft.EntityFrameworkCore;

namespace MachineTest.Data
{
    public class Applicationcontext:DbContext
    {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
        optionsBuilder.UseSqlServer("Data Source=(LocalDb)\\MSSQLLocalDB;Initial Catalog=mydata;Integrated Security=True");
               
            }
        }
        public Applicationcontext(DbContextOptions<Applicationcontext> options) : base() { }
        public DbSet<product> products { get; set; }
        public DbSet<category> categories { get; set; }

        public DbSet<ViewModel> viewModel { get; set; }
    }
}
