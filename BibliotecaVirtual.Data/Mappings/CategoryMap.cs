using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class CategoryMap : IEntityTypeConfiguration<Category>
    {
        public void Configure(EntityTypeBuilder<Category> builder)
        {
            builder.ToTable("Category");
            builder.HasKey(p => p.CategoryId);

            builder.Property(p => p.CategoryId);

            builder.Property(p => p.Description)
                   .IsRequired();

            builder.Property(p => p.AboutUrl)
                   .IsRequired();
        }
    }
}
