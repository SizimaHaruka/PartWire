using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PartWire.Domain.Projects;

namespace PartWire.Infrastructure.Persistence.Configurations;

public sealed class ProjectItemConfiguration : IEntityTypeConfiguration<ProjectItem>
{
    public void Configure(EntityTypeBuilder<ProjectItem> builder)
    {
        builder.ToTable("project_items");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Id)
            .HasColumnName("id");

        builder.Property(x => x.ProjectId)
            .HasColumnName("project_id")
            .IsRequired();

        builder.Property(x => x.ItemType)
            .HasColumnName("item_type")
            .HasConversion<string>()
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.ItemId)
            .HasColumnName("item_id")
            .IsRequired();

        builder.Property(x => x.RequestedQuantity)
            .HasColumnName("requested_qty")
            .HasPrecision(18, 4)
            .IsRequired();

        builder.Property(x => x.Unit)
            .HasColumnName("unit")
            .HasMaxLength(50);

        builder.Property(x => x.UsageText)
            .HasColumnName("usage_text")
            .HasMaxLength(200);

        builder.Property(x => x.RequestedDueDate)
            .HasColumnName("requested_due_date");

        builder.Property(x => x.LineStatus)
            .HasColumnName("line_status")
            .HasMaxLength(30)
            .IsRequired();

        builder.Property(x => x.Note)
            .HasColumnName("note")
            .HasMaxLength(1000);

        builder.Property(x => x.CreatedAt)
            .HasColumnName("created_at")
            .IsRequired();

        builder.Property(x => x.UpdatedAt)
            .HasColumnName("updated_at")
            .IsRequired();

        builder.HasIndex(x => new { x.ProjectId, x.ItemType, x.ItemId })
            .IsUnique();
    }
}
