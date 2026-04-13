# PartWire Requirements Definition

## 0. Document Operation

- This document records confirmed business, operational, and screen requirements.
- The primary source for unresolved items is [design-decisions-checklist.md](/C:/wk/csherp/PartWire/docs/en/design-decisions-checklist.md).
- Decisions are reflected into this document by domain rather than one issue at a time.
- Only confirmed decisions are reflected here. Open derivative issues remain in the checklist.

## 1. Purpose

This document defines the requirements for `PartWire`, an application that consistently manages internally handled purchasing parts from quotation request through ordering, delivery, invoice registration, and downstream document circulation.

The system is intended for a small team and must support visibility of progress and amounts at both project and construction-number levels, while especially improving delivery confirmation speed and accuracy.

## 2. Background and Challenges

- Purchasing information is easily fragmented across quotation, order, delivery, and invoicing stages.
- Aggregation by construction number and project-wide traceability are difficult.
- Price history is not accumulated consistently for future procurement decisions.
- Delivery confirmation is labor-intensive and hard to perform quickly.
- Flexible initial loading of master data and existing business data is needed.

## 3. Objectives

- Manage the full flow from project creation to downstream circulation in one application.
- Track required information by construction number, project, and part.
- Accumulate quotation and actual prices for future purchasing decisions.
- Provide an interface that enables fast delivery confirmation.
- Support bulk registration and update through CSV for master maintenance and migration.

## 4. Intended Users

- Requester
- Procurement operator
- Administrator

Notes:

- The expected operating team size is about five users.
- One user may serve multiple roles.

## 5. Scope

- Construction number registration
- Project registration and numbering
- Supplier quotation request management
- Quotation registration
- Purchase approval requests
- Supplier ordering
- Delivery date registration
- Delivery slip registration
- Delivery confirmation
- Invoice registration
- Downstream circulation registration for delivery slips and invoices
- Part master management
- Manufactured part management
- Price history management
- CSV import of masters and business data

## 6. Business Flow

1. The requester creates a project.
2. The operator sends quotation requests.
3. The operator registers quotations.
4. The operator submits approval requests.
5. The operator places supplier orders.
6. The operator registers delivery dates.
7. The operator registers delivery slips.
8. The operator performs delivery confirmation.
9. The operator registers invoices.
10. The operator registers downstream circulation of invoices and delivery slips.

Notes:

- Invoice registration applies only to selected suppliers.
- One project may include multiple lines, parts, and suppliers.

## 7. Functional Requirements

### 7.1 Project Management

- The system must support registration of a parent construction number.
- Project numbers must be auto-generated in the format `Q` + 10 digits.
- Each project must have a requester and a procurement operator.
- Each project belongs to exactly one construction number.
- Project status must follow the business flow.
- Project management is project-centric, while approval and delivery confirmation use dedicated screens.
- Users must be able to review quotation, order, delivery, invoice, and circulation history for each project.
- Project lines must be entered at project creation time and can be added later.
- Duplicate parts within the same project must be prohibited.
- Project status is auto-determined from detail progress and has no manual override.
- `Requesting Quotations` and `Collecting Quotations` are merged into one `Collecting Quotations` state.
- `Delivery In Progress` and `Delivery Confirmation In Progress` are separate states.
- A project becomes `Completed` only when all ordered lines have required delivery confirmation completed, invoice-required items have been invoiced, and all circulation-target documents have been circulated.
- Partial delivery does not allow completion until the full quantity has been confirmed.
- `Circulated` is reached when all target documents have been circulated.
- `Cancelled` can be set by manual cancellation, by loss of all valid quotation targets, or by deciding not to continue the remaining scope after a partial order.

### 7.2 Construction Number Management

- Construction numbers must support create, update, and disable operations.
- Aggregation by construction number must include project count, quotation amount, order amount, delivery amount, and invoice amount.

### 7.3 Part and Partner Management

- Purchased parts must be managed in a part master.
- Manufactured parts must be managed separately from purchased parts.
- Manufactured part extensions must use a fixed-column approach.
- Manufactured-part attributes may be used to filter processing vendors.
- Vendors that do not satisfy the required attribute conditions must not be shown.
- No per-project exception is allowed for partner roles or invoice settings.

### 7.4 Quotation and Approval

- Quotations are managed per quotation document, while progress and completion are tracked at line level.
- Approval is performed per quotation.
- Additional quotations created during project execution are approved independently.
- Approvers are determined by department and amount rank.
- Multi-step approval must be supported.
- Rejected quotations can be resubmitted.
- Project-specific approval route overrides are not allowed.
- Changes to amount ranks or approval rules apply only to newly created or newly submitted cases.

### 7.5 Ordering

- Orders can be created only for approved quotations.
- Order date, partner, order number, amount, quantity, and delivery date must be registered.
- Order numbers use the format `O` + 10 digits.
- Orders are strictly quotation-based.
- Partial early ordering of only some lines is not allowed.
- Split deliveries and additional orders must remain traceable as history.
- Additional-order history is displayed in the same way as multiple quotations under one project.
- Order cancellation is recorded per quotation.
- Cancelled order data is hidden from normal screens and remains available as history.
- Quotation revision is allowed only before delivery.
- If a revision occurs after ordering, the old planned delivery date is invalidated and a new quotation is registered.
- Old revision data remains as history but is removed from active processing.
- Changes after delivery are handled as a separate quotation or an additional order.

### 7.6 Delivery and Delivery Confirmation

- Promised delivery dates must be registerable and updatable.
- Delivery slip information must be registerable.
- Delivery confirmation must support quantity checking per part.
- Delivery confirmation is performed one delivery slip at a time.
- PC usage is assumed.
- Cross-project bulk confirmation is not supported.
- Delivery slip registration uses manual input as the base flow.
- Delivery slip registration must support assisted input from quotation lines.
- Remaining quantity by quotation line must be visible.
- Barcode or QR code reading must be addable as an optional feature.

### 7.7 Invoice Management

- Invoice registration requirement must be configurable per partner.
- Invoice number, invoice date, invoice amount, and payment target month must be registerable.
- Invoice management is primarily per quotation.
- Split invoices across months must be supported.
- Only partners with invoice registration enabled are eligible for invoice registration.
- One invoice may link only to deliveries belonging to the same quotation.
- Completion is judged on a quantity basis, while amount differences trigger notification.
- Discounts, miscellaneous costs, administration fees, and shipping fees must be allowed as free-form entries.
- Invoice registration must support assisted input from delivery data.
- Remaining uninvoiced quantity and amount must be visible during input.

### 7.8 Circulation

- Users must be able to register circulation of delivery slips and invoices to downstream processes.
- Quotations are not circulation targets by default.
- A company setting may optionally make quotations circulation targets.

### 7.9 CSV Import and Export

- Master data and business data must support CSV import.
- CSV import must support overwrite update and disable operations using `UserKey`.
- Field mapping and validation must be available before import.
- Import errors must roll back the full batch.
- CSV export must be available for aggregation and external submission.

### 7.10 Search, Lists, and Aggregation

- Search must support project number, construction number, part, supplier, requester, operator, and status.
- Aggregation by construction number must provide amount totals and counts.
- Lists such as unprocessed items, waiting for delivery, and waiting for invoice must be available.

## 8. Screen Direction

- The top screen must show pending counts and attention items by user.
- Attention items are shown to users whose names appear somewhere in the project.
- Other projects are accessed through cross-project list screens such as approval-pending and unfinished-project lists.
- The delivery confirmation screen must allow filtering and confirmation with minimal steps.
- The delivery registration screen must support easy quantity entry by loading quotation lines.
- The invoice registration screen must support easy entry by loading delivery lines.
- The project detail screen must show progress from quotation through circulation in time order.
- The CSV import screen must support template download, preview, and error review.

## 9. Non-Functional Requirements

### 9.1 Operability

- The system must be operable by about five users without strain.
- The UI must avoid flows that only a specific person can handle.

### 9.2 Performance

- Search must be responsive in normal daily use.
- Delivery confirmation screens must especially prioritize responsiveness.
- CSV import should handle hundreds to thousands of rows in practical time.

### 9.3 Technical Configuration

- A local standalone verification edition must support SQLite.
- A multi-user edition must support SQL Server.
- Functional differences between DB products should be minimized.
- Migration from SQLite to SQL Server must be possible.
- Both local authentication and Active Directory authentication must be supported.
- Authentication mode must be switchable by configuration.
- Active Directory authentication must support Windows logon integration.
- Distribution uses an installer.

### 9.4 Auditability

- The system must track who changed what and when.
- CRUD operations on major tables except intermediate tables must be auditable.
- Audit logs use standard granularity with before/after values for major fields.
- Default audit-log retention is five years and configurable.
- Audit-log viewing is limited to system administrators.

### 9.5 Security

- Users must be identifiable.
- View and operation scope are controlled by roles.
- Disable/history retention is preferred over physical deletion.
- User linkage across authentication mode changes must be preserved using UPN.
- Under normal conditions, authorization is resolved using both internal user data and AD group lookup.
- If AD group lookup fails, operation continues using internal user data only.
- No dedicated privilege is provided solely for amount visibility.

### 9.6 Attachment File Management

- Quotations, delivery slips, and invoices may optionally have linked files.
- In v1, attachments are implemented for quotations, delivery slips, and invoices.
- The DB stores only file links or paths.
- Broken links must be detectable and clearly indicated on attachment screens.
- Storage must be switchable by configuration.
- The standard storage is a shared folder, and SharePoint is also supported.
- When the storage limit is exceeded, only attachment addition is stopped after notification.

### 9.7 Availability and Operations

- DB endpoints and attachment storage targets must be changeable.
- It is acceptable for normal operation to stop during connection failure.
- The system must specify backup targets and recommended procedures.
- Sample data loading for initial setup must be available.
- Migration from SQLite to SQL Server is performed through a dedicated migration tool or an administrator command.

## 10. Remaining Areas to Detail

The issue-by-issue primary source is [design-decisions-checklist.md](/C:/wk/csherp/PartWire/docs/en/design-decisions-checklist.md).

- Relationship between projects and construction numbers
- Line-level versus project-level detail balance
- Detailed AD integration method
- Detailed notification retry behavior
- Resubmission UI and input retention rules after rejection
- Dashboard prioritization rules by assignee
