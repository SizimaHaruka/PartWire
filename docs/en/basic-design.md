# PartWire Basic Design

## 0. Document Operation

- This document records confirmed basic design decisions including entities, status transitions, screen direction, authorization design, CSV design, and core behavioral rules.
- The primary source for unresolved items is [design-decisions-checklist.md](/C:/wk/csherp/PartWire/docs/en/design-decisions-checklist.md).
- When unresolved items are decided, they are reflected into this document by domain.
- Decision rationale and follow-up issues continue to be tracked in the checklist rather than here.

## 1. Purpose

This document presents the initial basic design for `PartWire` based on the requirements definition. The design favors a structure suitable for a small operating team while leaving room for later expansion.

## 2. Design Principles

- Manage quotations, approvals, orders, deliveries, invoices, and circulation in time sequence around a project.
- Separate construction numbers from project numbers.
- Separate part masters from price history so price changes can be accumulated over time.
- Support both purchased parts and manufactured parts in one business flow while managing their attributes separately.
- Treat delivery confirmation as the most critical operation and prioritize list visibility and input efficiency.
- Make CSV import a standard capability for both master maintenance and existing data migration.
- Keep extension room for attachments and notifications.
- Treat quotations as transaction units while accumulating progress and completion at line level.

## 3. Assumed Application Structure

- Client: WPF desktop application
- Persistence: SQLite or SQL Server
- I/O: screen operations, CSV import, CSV export
- Distribution: installer

Notes:

- SQLite is intended for local standalone verification.
- SQL Server is intended for concurrent multi-user operation.
- Authentication is switchable between local authentication and Active Directory authentication.

## 4. Major Business Entities

### 4.1 Construction Number

- Construction Number ID
- Construction Number
- Construction Title
- Status

### 4.2 Project

- Project ID
- Project Number
- Construction Number ID
- Project Name
- Requester ID
- Procurement Operator ID
- Created Date
- Project Status
- Approval Status

Notes:

- Project numbers are auto-generated in the format `Q` + 10 digits with no annual reset.

### 4.3 Project Line

- Project Line ID
- Project ID
- Item Type
- Item ID
- Requested Quantity
- Requested Delivery Date
- Line Status
- Adopted Quotation Line ID

Notes:

- Duplicate parts within the same project are prohibited by system rule.

### 4.4 Purchased Part Master

- Part ID
- Part Name
- Manufacturer ID
- Model
- Unit
- Default Supplier ID

### 4.5 Manufactured Part Master

- Manufactured Part ID
- Drawing Number
- Part Name
- Material ID
- Shape ID
- Process ID
- Size ID
- Surface Treatment ID

Notes:

- Drawing number and part name are mandatory.
- Company-specific extensions use fixed columns rather than a generic attribute table.

### 4.6 Business Partner

- Partner ID
- Partner Name
- Supplier Flag
- Processing Vendor Flag
- Invoice Registration Required Flag

Notes:

- The same partner may act as both supplier and processing vendor.
- No per-project exception is allowed for partner roles or invoice settings.
- Processing-vendor candidates that do not satisfy attribute conditions are not shown.

### 4.7 Quotation

- Quotation ID
- Project ID
- Partner ID
- Revision Number
- Quotation Date
- Total Amount
- Approval Status
- Quotation Status

### 4.8 Quotation Line

- Quotation Line ID
- Quotation ID
- Project Line ID
- Quantity
- Unit Price
- Amount
- Planned Delivery Date
- Delivered Quantity
- Checked Quantity
- Invoiced Quantity

Notes:

- `Delivered Quantity`, `Checked Quantity`, and `Invoiced Quantity` are denormalized counters recalculated from related detail records in the same transaction.

### 4.9 Approval Route Definition

- Approval Route Definition ID
- Department ID
- Amount Rank ID
- Step Number
- Required Approver Rank

Notes:

- The combination of Department, Amount Rank, and Step Number is unique.

### 4.10 Approval Record

- Approval Record ID
- Quotation ID
- Department ID
- Amount Rank ID
- Approval Step
- Requester ID
- Approver ID
- Approval Result

### 4.11 Order

- Order ID
- Quotation ID
- Partner ID
- Order Number
- Order Date
- Order Amount
- Order Status
- Cancelled Date
- Cancel Reason

Notes:

- Orders are strictly quotation-based.
- Partial line-only advance ordering is not allowed.
- Order numbers use `O` + 10 digits with no annual reset.
- Order cancellation is recorded per quotation and hidden from normal screens while remaining visible in history.

### 4.12 Order Line

- Order Line ID
- Order ID
- Quotation Line ID
- Ordered Quantity
- Remaining Delivery Quantity

Notes:

- `Remaining Delivery Quantity` is a denormalized counter.

### 4.13 Delivery and Delivery Line

- Delivery ID
- Order ID
- Delivery Slip Number
- Delivery Date
- Delivery Status
- Delivery Line ID
- Quotation Line ID
- Delivered Quantity
- Accepted Quantity
- Check Result

### 4.14 Invoice and Invoice Target Link

- Invoice ID
- Quotation ID
- Partner ID
- Invoice Number
- Invoice Date
- Payment Target Month
- Invoice Amount
- Invoice Target Link ID
- Delivery Line ID
- Invoiced Quantity
- Invoiced Amount

### 4.15 Circulation

- Circulation ID
- Target Type
- Target ID
- Circulation Date
- Destination
- Sender ID

### 4.16 Attachment Link

- Attachment Link ID
- Target Type
- Target ID
- Document Type
- File Name
- Storage Link
- Registered By
- Registered At

Notes:

- File bodies are not stored in the DB.
- In v1, attachments cover quotations, delivery slips, and invoices.
- Shared folders are standard, and SharePoint is also supported.
- When capacity is exceeded, only new attachment registration stops.
- Screens that display attachments indicate broken links.
- Existing links remain unchanged even if the storage backend is switched.

### 4.17 Notification and Notification Recipient

- Notification ID
- Notification Type
- Notification Channel
- Project ID
- Target Record Type
- Target Record ID
- Subject
- Body
- Occurred At
- Sent At
- Status
- Notification Recipient ID
- User ID
- Email Address
- Delivered At
- Read At
- Read Flag

Notes:

- Recipients default to all project-related users.
- Notification items are configurable per type.
- Notification history and read-state retention default to one year and are configurable.

### 4.18 User, Department, AD Group Mapping, and System Setting

- User ID, Name, Login ID, Authentication Type, Password Hash, Department ID, Role Type, Approver Rank ID, AD Link Key
- Department ID, Department Code, Department Name
- AD Group Mapping ID, Group Name, Usage Type, Department Mapping, Role Mapping, Approver Rank Mapping
- System Setting ID, Authentication Mode, AD Domain, AD Group Lookup Enabled Flag, Quotation Circulation Enabled Flag, Password Policy, Updated At

Notes:

- AD link key is UPN.

## 5. Status Design

### 5.1 Project Status

- Created
- Collecting Quotations
- Partially Approved
- All Quotations Approved
- Partially Ordered
- All Ordered
- Delivery In Progress
- Delivery Confirmation In Progress
- Invoice In Progress
- Circulated
- Completed
- Cancelled

Notes:

- Project status is auto-determined and has no manual correction.
- `Requesting Quotations` is merged into `Collecting Quotations`.
- `Delivery In Progress` and `Delivery Confirmation In Progress` are separate.
- `Completed` requires delivery confirmation, invoicing where required, and circulation where required.
- Partial delivery does not allow completion until all quantity confirmation is done.
- `Circulated` is reached when all circulation-target documents have been circulated.
- `Cancelled` can also be used when the remaining scope is intentionally abandoned after a partial order.

### 5.2 Delivery Status

- Not Delivered
- Partially Delivered
- Delivered
- Confirmed
- Discrepancy Found

### 5.3 Approval Status

- Not Requested
- Requested
- In Approval
- Approved
- Rejected

### 5.4 Order Status

- Not Ordered
- Ordered
- Partially Delivered
- Completed
- Cancelled

### 5.5 Approval Route Policy

- Approval is per quotation and multi-step.
- Approvers are derived from department and amount rank.
- Rejected quotations may be resubmitted.
- Project-specific approver replacement and route override are not allowed.
- Changes to amount ranks and route definitions apply only to newly created or newly submitted cases.
- Quotation revision does not inherit old approval results.

### 5.6 Quotation Revision Policy

- Quotation revision is allowed only before delivery.
- Old revisions are treated as cancelled revisions.
- If ordered data exists, the old planned delivery date is invalidated, cancellation is recorded, and history is retained.
- Old downstream data remains as history but is excluded from active processing.
- The new revision requires new approval before ordering.
- Changes after delivery are handled as a separate quotation or additional order.

### 5.7 Authentication Policy

- Authentication mode is switched between AD and local authentication through system settings.
- A common business user record is used regardless of authentication mode.
- In AD mode, the user is identified by UPN.
- Under normal conditions, authorization uses both internal user data and AD group lookup.
- If AD group lookup fails, operation continues with internal user data only.

## 6. Main Screen Design

### 6.1 Dashboard

- Shows attention-required projects related to the current user.
- Other projects are accessed through cross-project list screens.

### 6.2 Project List and Project Detail

- Project list is the main operational hub.
- Project detail shows quotations, approvals, orders, deliveries, invoices, attachments, and circulation history in one place.

### 6.3 Delivery Confirmation Screen

- Prioritizes unconfirmed data.
- Supports filtering by supplier, delivery date, project number, and delivery slip number.
- Optimized for keyboard operation.
- Assumes PC usage.
- Does not support cross-project bulk confirmation.
- Barcode/QR support is an optional module.

### 6.4 Delivery Registration Screen

- Starts from a quotation and shows remaining quantity by quotation line.
- Allows input of delivery quantity and registration of delivery slip number, date, and attachment link.

### 6.5 Invoice Registration Screen

- Starts from delivery data and shows uninvoiced quantity and amount.
- Only invoice-enabled partners are eligible.
- One invoice can link only to deliveries under the same quotation.
- Completion is quantity-based, while amount differences trigger notifications.
- Free-form entries are allowed for discounts, miscellaneous costs, administration fees, and shipping fees.
- Even for month-crossing partial deliveries, the standard project-based invoice flow is kept.

### 6.6 Master Management and CSV Import

- Master screens cover construction numbers, parts, partners, and users.
- CSV import supports `UserKey`, full rollback on error, and same-transaction denormalized counter recalculation.

## 7. Authorization Design

### 7.1 Requester

- Create project
- View own projects
- Review progress

### 7.2 Procurement Operator

- Register quotation requests
- Register quotations
- Submit approval requests
- Register orders
- Register delivery dates
- Register delivery slips
- Perform delivery confirmation
- Register invoices
- Register circulation

### 7.3 Administrator

- View all records
- Manage masters
- Manage users
- Manage approval routes
- Review aggregates

Notes:

- View and operation scopes are controlled by role.
- No dedicated privilege is provided solely for amount visibility.

## 8. CSV Import Design

- Import targets include masters, projects, project lines, quotations, orders, deliveries, invoices, and attachment links.
- `UserKey` is used for overwrite update and disable.
- Errors roll back the full import.

## 9. Aggregation Design

- Construction-number aggregation includes quotation, order, delivery, and invoice totals as well as unfinished counts.
- Assignee aggregation includes assigned projects, approval waiting counts, and delivery-confirmation waiting counts.

## 10. Audit Log Design

- Stores created/updated user and time, before/after values, and change reason.
- Covers major tables plus important configuration tables.
- Uses standard granularity and five-year configurable retention.
- Audit-log viewing is limited to system administrators.

## 11. Additional Technical Policies

- DB product differences are absorbed in the data access layer.
- Migration from SQLite to SQL Server uses a dedicated migration tool or administrator command.
- The migration tool preserves attachment links as-is.
- Major tables use update timestamps for optimistic locking.
- On optimistic-lock conflict, the UI shows a conflict, refreshes, and asks the user to re-enter data.
- Attachment storage is configurable, with a shared folder as standard and SharePoint as an additional target.
- Sample loading supports initial data seeding.

## 12. Areas for Further Detail

The issue-by-issue primary source is [design-decisions-checklist.md](/C:/wk/csherp/PartWire/docs/en/design-decisions-checklist.md).

- Resubmission UI and input retention rules after rejection
- Dashboard prioritization rules by assignee
- Detailed AD integration implementation
- Detailed notification retry behavior
