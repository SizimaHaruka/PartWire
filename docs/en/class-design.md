# PartWire Initial Class Design

## 0. Purpose

This document organizes the initial class responsibilities for `PartWire` based on the first DDL draft.

Assumptions:

- UI: WPF
- Pattern: MVVM
- Architecture: layered
- ORM: EF Core

## 1. Layer Responsibilities

### 1.1 PartWire.Domain

- Business rules
- State transitions
- Invariant protection
- Aggregate operations

### 1.2 PartWire.Application

- Use case execution
- DTOs
- Transaction boundaries
- Authorization calls
- Coordination between UI intent and domain operations

### 1.3 PartWire.Infrastructure

- EF Core implementation
- DB mapping
- Repository / query implementations
- Authentication, notification, attachment, clock, numbering, and other external integrations

### 1.4 PartWire.Desktop

- Views
- Navigation
- View models
- Dialogs
- Input state handling

## 2. Aggregate Candidates

- `Project` with `ProjectItem`
- `Quotation` with `QuotationLine` and `PurchaseApproval`
- `Order` with `OrderLine`
- `Delivery` with `DeliveryLine`
- `Invoice` with `InvoiceTarget`

## 3. Main Domain Types

### 3.1 Masters

- `ConstructionProject`
- `Department`
- `User`
- `ApprovalRank`
- `AmountRank`
- `BusinessPartner`
- `Manufacturer`
- `PurchasePart`
- `ManufacturedPart`
- `Material`
- `Shape`
- `ProcessingMethod`
- `Size`
- `SurfaceTreatment`
- `ApprovalRouteDefinition`
- `SystemSetting`

### 3.2 Business Types

- `Project`
- `ProjectItem`
- `QuoteRequest`
- `Quotation`
- `QuotationLine`
- `PurchaseApproval`
- `Order`
- `OrderLine`
- `Delivery`
- `DeliveryLine`
- `Invoice`
- `InvoiceTarget`
- `DocumentForwarding`
- `AttachmentLink`
- `Notification`
- `NotificationRecipient`
- `PriceHistory`

### 3.3 Value Objects

- `ProjectNumber`
- `OrderNumber`
- `Money`
- `Quantity`
- `InvoiceTargetMonth`
- `AttachmentLocation`

## 4. Main Application Use Cases

- `LoginUseCase`
- `CreateProjectUseCase`
- `SearchProjectsUseCase`
- `GetProjectDetailUseCase`
- `RegisterQuotationUseCase`
- `SubmitQuotationApprovalUseCase`
- `ApproveQuotationUseCase`
- `CreateOrderUseCase`
- `RegisterDeliveryUseCase`
- `ConfirmDeliveryUseCase`
- `RegisterInvoiceUseCase`
- `RegisterDocumentForwardingUseCase`
- `RegisterAttachmentLinkUseCase`
- `ImportCsvUseCase`
- `ExportCsvUseCase`

## 5. Infrastructure Direction

- `PartWireDbContext`
- repositories only for update-centered aggregates
- query services for list/search/aggregation screens
- cross-cutting services such as numbering, clock, user context, authentication, attachment storage, notification, and audit logging

## 6. Desktop ViewModel Candidates

- `ShellViewModel`
- `LoginViewModel`
- `DashboardViewModel`
- `ProjectListViewModel`
- `ProjectDetailViewModel`
- `ProjectEditViewModel`
- `QuotationEditViewModel`
- `ApprovalInboxViewModel`
- `OrderEditViewModel`
- `DeliveryRegisterViewModel`
- `DeliveryConfirmationViewModel`
- `InvoiceRegisterViewModel`
- `MasterListViewModel`
- `CsvImportViewModel`
- `SettingsViewModel`

## 7. Recommended Direction

- Keep business rules in Domain
- Keep use-case orchestration in Application
- Keep EF Core and external integrations in Infrastructure
- Keep Desktop focused on MVVM and UI behavior
