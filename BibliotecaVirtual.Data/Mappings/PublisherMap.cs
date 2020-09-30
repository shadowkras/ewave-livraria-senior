using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class PublisherMap : IEntityTypeConfiguration<Publisher>
    {
        public void Configure(EntityTypeBuilder<Publisher> builder)
        {
            builder.ToTable("Publisher");
            builder.HasKey(p => p.PublisherId);

            builder.Property(p => p.PublisherId);

            builder.Property(p => p.Name)
                   .IsRequired();            
        }
    }
}
