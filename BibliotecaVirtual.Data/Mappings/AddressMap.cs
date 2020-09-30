using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class AddressMap : IEntityTypeConfiguration<Address>
    {
        public void Configure(EntityTypeBuilder<Address> builder)
        {
            builder.ToTable("Address");
            builder.HasKey(p => p.AddressId);

            builder.Property(p => p.AddressId);

            builder.Property(p => p.Endereco)
                   .IsRequired();

            builder.Property(p => p.Numero)
                   .IsRequired();

            builder.Property(p => p.Bairro)
                   .IsRequired();

            builder.Property(p => p.Cidade)
                   .IsRequired();

            builder.Property(p => p.Estado)
                   .IsRequired();

            builder.Property(p => p.Complemento)
                   .IsRequired();
        }
    }
}
