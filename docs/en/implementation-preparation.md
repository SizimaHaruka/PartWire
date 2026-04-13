# PartWire Implementation Preparation Notes

## 0. Purpose

This document organizes the deliverables and decisions that should be prepared before implementation starts.

Current assumptions:

- UI: WPF
- Runtime: .NET 10
- Architecture: layered
- Data access: EF Core
- Single-PC operation is supported, while SQL Server is recommended when the environment exists

## 1. Deliverables to Prepare Before Implementation

At minimum, the following should be prepared before implementation starts:

1. DDL
2. Class design
3. Screen transition draft

To reduce rework, the following should also be prepared early:

4. Solution structure draft
5. Responsibility split for DbContext / repository / query handling
6. Naming and placement conventions
7. Initial migration policy
8. Minimum authentication/authorization approach
9. Audit log and common-column policy
10. Sample data policy

## 2. What DDL Should Define

DDL should be treated not just as table definitions, but as a document that defines the implementation boundary of business rules.

It should include at least:

- Table list
- Column definitions
- PK / FK
- Unique constraints
- Indexes
- Nullability policy
- Common audit columns
- Optimistic-lock columns
- Soft-delete or disable policy

Important points to decide early:

- Key naming for project, quotation, order, delivery, and invoice tables
- How update timestamps are used for optimistic locking
- How to handle type differences between SQLite and SQL Server
- Type policy for amounts, quantities, dates, and booleans
- Table granularity for attachment links, notifications, and audit logs

## 3. What Class Design Should Define

Class design should cover more than table-mapped classes. At minimum, separate the following concerns:

- Domain entities
- Value objects
- Application services / use cases
- Infrastructure entity mapping
- View models
- Screen DTOs / search condition DTOs / list item DTOs

Items to organize first:

- Responsibility of major entities
- Where status transition logic lives
- How aggregation queries are handled
- Who recalculates denormalized counters
- Who writes audit logs

Note:

- It is often easier if EF Core entities are not treated as the entire domain model
- For v1, avoid excessive class proliferation and prioritize clear responsibilities

## 4. What the Screen Transition Draft Should Define

The screen transition draft should decide navigation before visual details.

Minimum screen set:

- Login
- Dashboard
- Project list
- Project detail
- Quotation entry
- Approval request / approval review
- Order entry
- Delivery slip entry
- Delivery confirmation
- Invoice entry
- Circulation entry
- Master management
- CSV import
- Settings

Topics to draft early:

- Which screens are the main operational route
- Relationship between project-centric flow and cross-project task lists
- Whether create/edit/detail should be separate screens or combined
- Where dialogs should be used instead of full navigation
- Return paths for errors, conflicts, and rejection/resubmission

## 5. Other Missing Pieces

In addition to DDL, class design, and screen transitions, the following are worth preparing before coding starts.

### 5.1 Solution Structure Diagram

At minimum, define the boundaries of:

- `PartWire.Desktop`
- `PartWire.Application`
- `PartWire.Domain`
- `PartWire.Infrastructure`

Possible later additions:

- `PartWire.Contracts`
- `PartWire.Migrations`
- `PartWire.Tests`

### 5.2 Coding Conventions

Even a lightweight first version is useful:

- Folder structure
- Namespace conventions
- File naming
- `async` method naming
- DI registration conventions
- Exception handling conventions

### 5.3 DB Initialization Policy

Decide early:

- Whether DB creation is automatic on startup
- Whether migrations run explicitly
- Whether SQLite and SQL Server use different initialization flows
- When sample data loading is allowed

### 5.4 Test Strategy

At minimum:

- Unit tests should focus on Domain and Application layers
- UI testing can stay limited to critical routes
- Keep a minimum verification strategy for both SQLite and SQL Server behavior

### 5.5 Error Handling Policy

In WPF, this is better decided early:

- User-facing error display
- Separation of expected business errors and unexpected exceptions
- Reload flow for optimistic-lock conflicts
- Logging granularity

## 6. Pre-Implementation Checklist

Implementation is easier to start safely when the following exist:

- Initial DDL
- Initial class design for major entities
- Screen transition draft for major screens
- Solution structure diagram
- Naming conventions
- DbContext responsibility split
- Migration policy
- v1 authentication scope
- Minimum audit-log policy
- Sample data loading policy

## 7. Recommended Order

The following order is recommended:

1. Create the initial DDL
2. Derive Domain and Infrastructure class design from the DDL
3. Draft the main screen transitions
4. Finalize project boundaries in the solution
5. Fix the initial EF Core model and migration policy
6. Start implementation after that

This order keeps the dependencies between DB design, class design, and UI flow easier to manage.
