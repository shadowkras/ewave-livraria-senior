using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class AuthorMap : IEntityTypeConfiguration<Author>
    {
        public void Configure(EntityTypeBuilder<Author> builder)
        {
            builder.ToTable("Author");
            builder.HasKey(p => p.AuthorId);

            builder.Property(p => p.AuthorId);

            builder.Property(p => p.Name)
                   .IsRequired();            
        }
    }
}
