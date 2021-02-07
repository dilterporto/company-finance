using Company.Finance.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Company.Finance.Persistence.ModelConfiguration
{
    public class StatementModelConfiguration : IEntityTypeConfiguration<Statement>
    {
        public void Configure(EntityTypeBuilder<Statement> builder)
        {
            builder.ToTable("Statements");
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Transactions)
                .HasJsonConversion()
                .HasColumnType("jsonb");
        }
    }
}