using System.Reflection;
using Company.Finance.Models.Entities;
using Company.Finance.Models.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Npgsql;

namespace Company.Finance.Persistence
{
    public class FinanceDbContext : DbContext 
    {
        public FinanceDbContext(DbContextOptions builderOptions) 
            : base(builderOptions)
        {
            NpgsqlConnection.GlobalTypeMapper.MapEnum<Currency>();
            NpgsqlConnection.GlobalTypeMapper.MapEnum<TransactionType>();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // register all model configurations automatically
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            modelBuilder.HasPostgresEnum<Currency>();
            modelBuilder.HasPostgresEnum<TransactionType>();
            
            base.OnModelCreating(modelBuilder);
        }
    }
}