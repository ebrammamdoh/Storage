using Infrastructure.Data.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Storage.Data.Configurations.Base;

public class BaseLookupConfiguration<T> : IEntityTypeConfiguration<T> where T : BaseLookup
{
    public void Configure(EntityTypeBuilder<T> builder)
    {
        builder.Property(x => x.Name).HasMaxLength(200);
        builder.Property(x => x.NameAr).HasMaxLength(200);
        builder.Property(x => x.CreatedAt).HasDefaultValueSql("GETDATE()");
        builder.Property(x => x.IsActive).HasDefaultValue(true);


        ConfigureEntity(builder);
    }

    protected virtual void ConfigureEntity(EntityTypeBuilder<T> builder)
    {
    }

}
