using MachineTest.Models;
using Microsoft.Data.SqlClient;
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
        public List<ViewModel> GetPagedData(int pageindex, int Pagesize, out int totalRecords)
        {
            var totalRecordsParam = new SqlParameter("@TotalRecords", System.Data.SqlDbType.Int)
            {
                Direction = System.Data.ParameterDirection.Output
            };

            var parameters = new[]
            {
            new SqlParameter("@pageindex", pageindex),
            new SqlParameter("@Pagesize", Pagesize),
            totalRecordsParam
        };

            var pagedData = this.Set<ViewModel>()
                .FromSqlRaw("EXEC SP_products_category @pageindex, @Pagesize, @TotalRecords OUTPUT", parameters)
                .ToList();

            totalRecords = (int)totalRecordsParam.Value;

            return pagedData;
        }
    }
}
