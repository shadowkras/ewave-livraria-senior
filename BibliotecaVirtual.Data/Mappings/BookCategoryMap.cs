using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class BookCategoryMap : IEntityTypeConfiguration<BookCategory>
    {
        public void Configure(EntityTypeBuilder<BookCategory> builder)
        {
            builder.ToTable("BookCategory");
            builder.HasKey(p => new { p.BookId, p.CategoryId } );

            builder.Property(p => p.BookId);
            builder.Property(p => p.CategoryId);

            builder.HasOne(p => p.Book)
                   .WithMany(d => d.Categories)
                   .HasForeignKey(p => p.BookId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Category)
                   .WithMany(d => d.Books)
                   .HasForeignKey(p => p.CategoryId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
