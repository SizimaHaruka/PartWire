using Microsoft.EntityFrameworkCore;
using PartWire.Domain.Deliveries;
using PartWire.Domain.Invoices;
using PartWire.Domain.Orders;
using PartWire.Domain.Projects;
using PartWire.Domain.Quotations;

namespace PartWire.Infrastructure.Persistence;

public sealed class PartWireDbContext : DbContext
{
    public PartWireDbContext(DbContextOptions<PartWireDbContext> options)
        : base(options)
    {
    }

    public DbSet<Project> Projects => Set<Project>();

    public DbSet<ProjectItem> ProjectItems => Set<ProjectItem>();

    public DbSet<Quotation> Quotations => Set<Quotation>();

    public DbSet<QuotationLine> QuotationLines => Set<QuotationLine>();

    public DbSet<Order> Orders => Set<Order>();

    public DbSet<OrderLine> OrderLines => Set<OrderLine>();

    public DbSet<Delivery> Deliveries => Set<Delivery>();

    public DbSet<DeliveryLine> DeliveryLines => Set<DeliveryLine>();

    public DbSet<Invoice> Invoices => Set<Invoice>();

    public DbSet<InvoiceTarget> InvoiceTargets => Set<InvoiceTarget>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PartWireDbContext).Assembly);
    }
}
