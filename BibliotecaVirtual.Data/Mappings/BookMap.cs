using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class BookMap : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Book");
            builder.HasKey(p => p.BookId);

            builder.Property(p => p.BookId);

            builder.Property(p => p.Title)
                   .IsRequired();

            builder.Property(p => p.Description)
                   .IsRequired();

            builder.Property(p => p.Sinopsis)
                   .IsRequired();

            builder.Property(p => p.AuthorId)
                   .IsRequired();

            builder.Property(p => p.PublishDate)
                   .IsRequired();

            builder.Property(p => p.PublisherId)
                   .IsRequired();            

            builder.Property(p => p.Pages)
                   .IsRequired();

            builder.Property(p => p.CoverImage)
                   .IsRequired();

            builder.Property(p => p.Title)
                   .IsRequired();

            builder.HasOne(p => p.Author)
                   .WithMany(d => d.Books)
                   .HasForeignKey(p => p.AuthorId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Publisher)
                   .WithMany(d => d.Books)
                   .HasForeignKey(p => p.PublisherId)
                   .OnDelete(DeleteBehavior.Cascade);            
        }
    }
}
