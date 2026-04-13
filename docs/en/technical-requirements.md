# PartWire Technical Requirements

## 0. Document Operation

- This document records technical decisions related to authentication, DB abstraction, audit, attachments, migration, and update control.
- The primary source for unresolved items is [design-decisions-checklist.md](/C:/wk/csherp/PartWire/docs/en/design-decisions-checklist.md).
- Business-operation-only topics are not duplicated here unless they require technical design decisions.
- Reflection into this document is performed when a related technical domain has been sufficiently finalized.

## 1. Purpose

This document defines the technical requirements for implementing, operating, and distributing `PartWire`.

## 2. Supported Configuration

### 2.1 Application Configuration

- The client application is delivered as a Windows desktop application.
- Distribution uses an installer package.

### 2.2 Database Configuration

- A local standalone verification edition must support SQLite.
- A multi-user edition must support SQL Server.
- Business functionality should remain as consistent as possible across DB products.
- Migration from SQLite to SQL Server must be possible.

## 3. Authentication and Authorization

### 3.1 Authentication

- Both local authentication and Active Directory authentication must be supported.
- Authentication mode must be switchable by configuration.
- Active Directory authentication must support Windows logon integration.
- UPN is used as the user binding key for Active Directory authentication.

### 3.2 Authorization

- Authorization must be finely configurable.
- Control may consider role, department, approver rank, and partner-related operations.
- Control must be available at screen, function, and operation levels.
- View scope and operation scope are role-based.
- Under normal conditions, authorization is resolved using both internal user data and AD group lookup.
- If AD group lookup fails, operation continues using internal user data only.
- No dedicated privilege is provided solely for amount visibility.

## 4. Data Update Control

- Optimistic locking must be used for concurrent update control.
- On update conflict, the user must be prompted to reload or re-enter data.
- The UI must explicitly show that a conflict occurred, refresh the screen, and prompt re-entry.
- Major tables must keep an update timestamp for optimistic lock checks.
- Denormalized counters must be recalculated in the same transaction when related detail rows are added, updated, or cancelled.

## 5. Attachment Files

- Attachment storage must be switchable by configuration.
- The standard storage target is a shared folder.
- SharePoint must also be supported as a storage target.
- The database stores only links or paths, not file binaries.
- In v1, attachments are implemented for quotations, delivery slips, and invoices.
- A storage size limit is enforced, and when exceeded only new attachment registration is stopped after notification.
- Screens that display attachments must indicate broken links.
- Existing links are not migrated when the storage backend is switched.

## 6. Audit Logs

- CRUD operations on major tables except intermediate tables are subject to audit logging.
- At minimum, operations on masters, projects, quotations, approvals, orders, deliveries, invoices, circulation records, users, and settings must be logged.
- Audit logs must keep target table, target ID, operation type, operation time, operator, before values, and after values.
- Audit logs use a standard granularity that keeps before/after values for major fields.
- Default retention is five years and must be configurable.
- Audit-log viewing is limited to system administrators.

Notes:

- Logging every intermediate table may create excessive log volume, so the initial scope focuses on major tables.
- Important configuration tables such as approval-route definitions and partner-condition settings are also audit targets.

## 7. Failure Operation

- Connection targets such as DB endpoints and attachment storage must be changeable.
- It is acceptable that normal business operation cannot continue during connection failure.
- Recovery must be possible after failure through reconnection or setting changes.

## 8. Initial Data Loading

- The system must provide a sample data loading function for initial setup.
- Sample loading is executed through application logic rather than manual CSV loading.
- Master seed data and validation data should be loadable immediately after environment setup.
- CSV import must support overwrite update and disable operations using `UserKey`.
- CSV import errors must roll back the entire batch.

## 9. Backup

- Backup operations are generally the responsibility of the operator.
- The operation guide must specify backup targets and recommended procedures.
- For SQLite, the protected targets include the DB file and attachment storage.
- For SQL Server, the protected targets include DB backup output and attachment storage.

## 10. Remaining Technical Topics

The issue-by-issue primary source is [design-decisions-checklist.md](/C:/wk/csherp/PartWire/docs/en/design-decisions-checklist.md).

- Installer implementation details
- Detailed Windows logon integration method
- Authorization for the connection-settings screen
- Scope and conditions for sample data loading
- Detailed implementation of email and in-app notifications
- Retry control when notification delivery fails
