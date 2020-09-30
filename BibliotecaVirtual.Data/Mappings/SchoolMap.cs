using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class SchoolMap : IEntityTypeConfiguration<School>
    {
        public void Configure(EntityTypeBuilder<School> builder)
        {
            builder.ToTable("School");
            builder.HasKey(p => p.SchoolId);

            builder.Property(p => p.SchoolId);

            builder.Property(p => p.Name)
                   .IsRequired();

            builder.Property(p => p.CNPJ)
                   .IsRequired();

            builder.Property(p => p.Telefone)
                   .IsRequired();
        }
    }
}
