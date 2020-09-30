using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class UserMap : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("User");
            builder.HasKey(p => p.UserId);

            builder.Property(p => p.UserId);

            builder.Property(p => p.Nome)
                   .IsRequired();

            builder.Property(p => p.Sobrenome)
                   .IsRequired();

            builder.Property(p => p.Telefone)
                   .IsRequired();

            builder.Property(p => p.CPF)
                   .IsRequired();

            builder.Property(p => p.UsuarioIdentityId)
                   .IsRequired();
        }
    }
}
