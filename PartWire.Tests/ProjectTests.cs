using PartWire.Domain.Projects;
using PartWire.Domain.Quotations;
using PartWire.Domain.Orders;
using PartWire.Domain.Deliveries;
using PartWire.Domain.Invoices;

namespace PartWire.Tests;

public sealed class ProjectTests
{
    [Fact]
    public void Rename_UpdatesProjectName()
    {
        var project = new Project("Q0000000001", "Before");

        project.Rename("After");

        Assert.Equal("After", project.ProjectName);
    }

    [Fact]
    public void Constructor_SetsProjectNumber()
    {
        var project = new Project("Q0000000001", "Project");

        Assert.Equal("Q0000000001", project.ProjectNo);
    }

    [Fact]
    public void ProjectItem_StoresCoreFields()
    {
        var item = new ProjectItem(1, ItemType.PurchasePart, 10, 5.5m, "Created");

        Assert.Equal(1, item.ProjectId);
        Assert.Equal(ItemType.PurchasePart, item.ItemType);
        Assert.Equal(10, item.ItemId);
        Assert.Equal(5.5m, item.RequestedQuantity);
        Assert.Equal("Created", item.LineStatus);
    }

    [Fact]
    public void Quotation_StartsAsDraftAndNotRequested()
    {
        var quotation = new Quotation(1, 2, "QT-0001", new DateOnly(2026, 4, 14), 1000m);

        Assert.Equal(1, quotation.ProjectId);
        Assert.Equal(2, quotation.BusinessPartnerId);
        Assert.Equal("QT-0001", quotation.InternalQuoteNo);
        Assert.Equal(ApprovalStatus.NotRequested, quotation.ApprovalStatus);
        Assert.Equal(QuoteAdoptionStatus.Draft, quotation.AdoptionStatus);
        Assert.Equal(1, quotation.RevisionNo);
    }

    [Fact]
    public void QuotationLine_StoresQuotedValues()
    {
        var line = new QuotationLine(1, 2, ItemType.PurchasePart, 3, 4.5m, 100m, 450m);

        Assert.Equal(1, line.QuotationId);
        Assert.Equal(2, line.ProjectItemId);
        Assert.Equal(ItemType.PurchasePart, line.ItemType);
        Assert.Equal(3, line.ItemId);
        Assert.Equal(4.5m, line.QuotedQuantity);
        Assert.Equal(100m, line.QuotedUnitPrice);
        Assert.Equal(450m, line.QuotedAmount);
    }

    [Fact]
    public void Order_StartsAsOrdered()
    {
        var order = new Order(1, 2, "O0000000001", new DateOnly(2026, 4, 14), 1200m);

        Assert.Equal(OrderStatus.Ordered, order.OrderStatus);
        Assert.Equal("O0000000001", order.OrderNo);
    }

    [Fact]
    public void DeliveryLine_CalculatesDifference()
    {
        var deliveryLine = new DeliveryLine(1, 2, 3, ItemType.PurchasePart, 4, 10m, 8m, "Difference");

        Assert.Equal(2m, deliveryLine.DifferenceQuantity);
        Assert.Equal("Difference", deliveryLine.CheckResult);
    }

    [Fact]
    public void Invoice_StartsAsRegistered()
    {
        var invoice = new Invoice(1, 2, "INV-0001", new DateOnly(2026, 4, 14), 3000m);

        Assert.Equal(InvoiceStatus.Registered, invoice.InvoiceStatus);
        Assert.Equal("INV-0001", invoice.InvoiceNo);
    }
}
