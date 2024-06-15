using LocalFriendzApi.Core.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LocalFriendzApi.Infrastructure.Data.Mappings
{
    public class ContactMapping : IEntityTypeConfiguration<Contact>
    {
        public void Configure(EntityTypeBuilder<Contact> builder)
        {
            builder.ToTable("TB_CONTACT");
            builder.HasKey(c => c.Id)
                    .HasName("id_contact");

            builder.Property(c => c.Id)
                   .HasColumnName("id_contact");

            builder.Property(c => c.Name)
                .IsRequired(true)
                .HasColumnName("name")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(100);

            builder.Property(c => c.Phone)
                .IsRequired(true)
                .HasColumnName("phone")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(20);

            builder.Property(c => c.Email)
                .IsRequired(true)
                .HasColumnName("email")
                .HasColumnType("NVARCHAR")
                .HasMaxLength(40);

            builder.Property(c => c.AreaCodeId)
              .HasColumnName("fk_id_area_code");

            builder.HasOne(c => c.AreaCode)
              .WithMany()
              .HasForeignKey(c => c.AreaCodeId)
              .OnDelete(DeleteBehavior.Cascade)
              .HasConstraintName("FK_Contact_AreaCode");
        }
    }
}
