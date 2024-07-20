using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PM.Gallery.Domain.Entities;

namespace PM.Gallery.Infrastructure.EntityFrameworkCore.Configurations;

public class ImageConfiguration : IEntityTypeConfiguration<Image>
{
    public void Configure(EntityTypeBuilder<Image> builder)
    {
        builder.HasKey(i => i.Id);

        builder.Property(i => i.ImageData).HasColumnType("mediumblob");

        //值对象，均使用AutoInclude(false)进行延迟加载
        //This allows eager loading them via Include / ThenInclude. But due to EF Core
        //implementation specifics, owned entity types cannot use explicit/lazy loading.
        //Trying to do so leads to runtime exception. 
        //图像具有多个评论
        builder.OwnsMany(i => i.Comments, c =>
            {
                c.WithOwner().HasForeignKey("ImageId");
                c.Property<int>("Id");
                c.HasKey("Id");
                c.ToTable("Comments");
            })
            .Navigation(i => i.Comments).AutoInclude(false);

        //图像具有多个评分
        builder.OwnsMany(i => i.Ratings, r =>
            {
                r.WithOwner().HasForeignKey("ImageId");
                r.Property<int>("Id");
                r.HasKey("Id");
                r.ToTable("Ratings");
            })
            .Navigation(i => i.Ratings).AutoInclude(false);

        //图像具有多个标签
        builder.OwnsMany(i => i.Tags, t =>
            {
                t.WithOwner().HasForeignKey("ImageId");
                t.Property<int>("Id");
                t.HasKey("Id");
                t.ToTable("Tags");
            })
            .Navigation(i => i.Tags).AutoInclude(false);
    }
}