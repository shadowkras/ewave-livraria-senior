using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using BibliotecaVirtual.Data.Entities;

namespace BibliotecaVirtual.Data.Mappings
{
    public class UserBookRentMap : IEntityTypeConfiguration<UserBookRent>
    {
        public void Configure(EntityTypeBuilder<UserBookRent> builder)
        {
            builder.ToTable("UserBookRent");
            builder.HasKey(p => p.UserBookRentId);

            builder.Property(p => p.UserBookRentId);

            builder.Property(p => p.UserId)
                   .IsRequired();

            builder.Property(p => p.BookId)
                   .IsRequired();

            builder.Property(p => p.RentalDate)
                   .IsRequired();

            builder.Property(p => p.ReturnDate)
                   .IsRequired();

            builder.Property(p => p.ReturnedDate);

            builder.HasOne(p => p.User)
                   .WithMany(d => d.RentBooks)
                   .HasForeignKey(p => p.UserId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(p => p.Book)
                   .WithMany(d => d.RentUsers)
                   .HasForeignKey(p => p.BookId)
                   .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
