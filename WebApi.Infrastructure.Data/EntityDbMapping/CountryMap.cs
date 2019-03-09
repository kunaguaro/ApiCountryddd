
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using WebApi.Domain.Entities;

namespace WebApi.Infrastructure.Data.EntityDbMapping
{
    public class CountryMap : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.ToTable("Country");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasColumnName("Name")
                .HasColumnType("varchar(150)");

            builder.Property(c => c.Population)
                .IsRequired()
                .HasColumnType("int")
                .HasColumnName("Population");

            builder.Property(c => c.Area)
                .IsRequired()
                .HasColumnType("decimal(14,2)")
                .HasColumnName("Area");

            builder.Property(c => c.ISO3166)
             .IsRequired()
             .HasColumnType("varchar(3)")
             .HasColumnName("ISO3166");

            builder.Property(c => c.DrivingSide)
            .IsRequired()
            .HasColumnType("varchar(50)")
            .HasColumnName("DrivingSide");

            builder.Property(c => c.Capital)
             .IsRequired()
             .HasColumnType("varchar(50)")
             .HasColumnName("Capital");
        }

    }
}
