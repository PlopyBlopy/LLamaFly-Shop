using Core.Entities;
using Core.Interfaces.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<ProductEntity>, IProductTitleConstraint, IProductDescriptionConstraint
    {
        private readonly int maxTitleLength = IProductTitleConstraint.MAX_TITLE_LENGTH;
        private readonly int maxDescriptionLength = IProductDescriptionConstraint.MAX_Description_LENGTH;

        public void Configure(EntityTypeBuilder<ProductEntity> builder)
        {
            builder.ToTable("Products").HasKey(x => x.Id);

            builder.HasIndex(x => x.Title);
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(maxTitleLength);

            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(maxDescriptionLength);

            builder.HasIndex(x => x.Price);
            builder.Property(x => x.Price)
                .IsRequired()
                .HasColumnType("numeric(8, 0)");

            builder.HasIndex(x => x.Rating);
            builder.Property(x => x.Rating)
                .IsRequired()
                .HasColumnType("numeric(2, 1)");

            builder.HasIndex(x => x.CreateAt);
            builder.Property(x => x.CreateAt)
                .IsRequired();

            builder.HasIndex(x => x.CategoryId);
            builder.Property(x => x.CategoryId)
                .IsRequired();

            builder.HasIndex(x => x.SellerId);
            builder.Property(x => x.SellerId)
                .IsRequired();
        }
    }
}