using Core.Entities;
using Core.Interfaces.Constraints;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace DataBase.Configurations
{
    public class CategoryConfiguration : IEntityTypeConfiguration<CategoryEntity>, ICategoryTitleConstraint
    {
        private readonly int maxTitleLength = ICategoryTitleConstraint.MAX_TITLE_LENGTH;

        public void Configure(EntityTypeBuilder<CategoryEntity> builder)
        {
            builder.ToTable("Categories").HasKey(x => x.Id);

            // Ограничения на поле Title
            builder.Property(x => x.Title)
                .IsRequired()
                .HasMaxLength(maxTitleLength);

            // Индекс для ParentCategoryId
            builder.HasIndex(x => x.ParentCategoryId);
            builder.Property(x => x.ParentCategoryId)
                .IsRequired();

            // Настройка связи "один ко многим" между родительской и дочерними категориями
            builder.HasOne(c => c.ParentCategory)
                .WithMany(c => c.Subcategories)
                .HasForeignKey(c => c.ParentCategoryId)
                .OnDelete(DeleteBehavior.Restrict)
                .IsRequired(false); // Запрет удаления родительской категории, если есть подкатегории
        }
    }
}