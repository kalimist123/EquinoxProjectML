using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Equinox.Domain.Models;

namespace Equinox.Infra.Data.Mappings
{    
    public class BongMap : IEntityTypeConfiguration<Bong>
    {
        public void Configure(EntityTypeBuilder<Bong> builder)
        {
            builder.Property(c => c.Id)
                .HasColumnName("Id");

            builder.Property(c => c.Name)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(c => c.ReferenceNo)
                .HasColumnType("varchar(100)")
                .HasMaxLength(100)
                .IsRequired();   
        }
    }
}
