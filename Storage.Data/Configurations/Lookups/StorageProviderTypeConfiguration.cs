using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Data.Configurations.Base;
using Storage.Data.Entities.Lookups;

namespace Storage.Data.Configurations.Lookups;

public class StorageProviderTypeConfiguration : BaseLookupConfiguration<StorageProviderType>
{
    public StorageProviderTypeConfiguration() : base()
    {
    }

    protected override void ConfigureEntity(EntityTypeBuilder<StorageProviderType> builder)
    {
        builder.HasKey(x => x.ID);

        builder.HasMany(x => x.FileMetaData).WithOne(x => x.StorageProviderType)
            .HasForeignKey(x => x.StorageProviderTypeID).OnDelete(DeleteBehavior.Restrict);

        //  Seed Data 
        builder.HasData(StorageProviderType.Data);
    }
}
