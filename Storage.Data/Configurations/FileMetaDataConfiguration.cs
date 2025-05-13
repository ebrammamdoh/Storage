using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Storage.Data.Entities;

namespace Storage.Data.Configurations;

public class FileMetaDataConfiguration : IEntityTypeConfiguration<FileMetaData>
{
    public void Configure(EntityTypeBuilder<FileMetaData> builder)
    {
        builder.HasKey(s => s.ID);
        builder.Property(x => x.ContentType).HasMaxLength(300);
        builder.Property(x => x.FileName).HasMaxLength(1000);
        builder.Property(x => x.Path).HasMaxLength(1000);

    }
}
