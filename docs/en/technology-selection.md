# PartWire Technology Selection Notes

## 0. Purpose

This document organizes the technology choices for `PartWire`.

- Prefer technologies that match the product concept
- Keep the structure understandable for OSS contributors
- Separate what should be decided for v1 from what can be refined later

This is a working decision memo, not a final specification.

## 1. Product Assumptions

PartWire is being designed with the following assumptions:

- It should work for teams that cannot easily adopt cloud services
- It should be able to start on a single PC
- If an organization has the environment, it should be able to use a server-hosted DB
- It is a Windows-PC-first business application
- It should stay operable for a small team
- It is planned to be released as OSS

These assumptions lead to the following selection criteria:

- Easy to operate as a Windows business application
- Able to grow from local-first to more structured operation
- Avoid unnecessary implementation and operational complexity
- Easy to understand for OSS contributors
- Clear separation of responsibilities for future extension

## 2. Current Direction

### 2.1 Application Style

- Direction: Windows desktop application
- UI: WPF

Why:

- The product is explicitly Windows-PC-first
- Single-PC operation is easy to start
- Keyboard-centric operational screens fit well
- It aligns with a local-first, non-cloud-dependent setup

Note:

- WPF may attract a narrower OSS contributor pool than web technologies
- To compensate, UI logic and business logic should stay separated

### 2.2 Runtime

- Adopted direction: .NET 10

Why:

- The team already has practical experience with it
- It aligns development and maintenance choices with existing know-how
- Strong compatibility with WPF, SQLite, SQL Server, and common libraries

Note:

- The current project already targets `net10.0-windows`
- The supported SDK version should be documented clearly for OSS users

### 2.3 Architecture

- Adopted direction: layered architecture with clear UI/business separation

Initial idea:

- `PartWire.Desktop`: WPF views and view models
- `PartWire.Application`: use cases and application services
- `PartWire.Domain`: entities and business rules
- `PartWire.Infrastructure`: DB, files, authentication, notifications

Why:

- It keeps WPF concerns out of business rules
- It gives a clear place to absorb SQLite/SQL Server differences
- It is easier to understand as an OSS project
- It preserves future options for other front ends or tools

### 2.4 UI Pattern

- Adopted direction: MVVM

### 2.5 WPF Framework Direction

- First candidate: `CommunityToolkit.Mvvm` + `Microsoft.Extensions.Hosting`
- Supporting option: `Microsoft.Extensions.DependencyInjection`

Why:

- It adds the MVVM support WPF needs without becoming heavy
- It provides the essential pieces such as `ObservableObject`, `RelayCommand`, and `AsyncRelayCommand`
- It fits naturally with .NET-style DI, configuration, and logging
- It is easier for OSS contributors to understand than a more opinionated large framework
- It keeps the UI foundation lighter than Prism at this stage

Why Prism is not the first choice:

- The project does not yet require advanced modular UI infrastructure
- It is heavier than needed for an early local-first business app
- The main complexity for PartWire is business design and data flow, not UI framework features

Current recommendation:

- ViewModel layer: `CommunityToolkit.Mvvm`
- Startup/DI foundation: Generic Host
- Logging: `Microsoft.Extensions.Logging` + `Serilog`
- Configuration: `Microsoft.Extensions.Configuration`

Why:

- It is one of the most maintainable and familiar WPF patterns
- It communicates structure clearly to contributors
- It helps prevent business logic from drifting into code-behind

## 3. Database and Data Access

### 3.1 Database Strategy

- Base v1 support: SQLite
- Recommended operational environment: SQL Server

Interpretation:

- SQLite should support local single-PC operation
- SQL Server should be the recommended option where the environment exists
- Business behavior should stay as consistent as possible across DB products

### 3.2 ORM / Data Access

- Adopted direction: Entity Framework Core

Why:

- Official support for both SQLite and SQL Server
- Easier migration and setup management
- Reasonable learning curve for OSS contributors
- Good base for optimistic locking, audit, and transaction handling

Suggested approach:

- Use EF Core as the primary update model
- Add dedicated queries for heavier list and aggregation screens as needed

### 3.3 Migrations

- Recommended direction: EF Core Migrations for schema management
- SQLite-to-SQL Server migration remains a separate dedicated tool

## 4. Authentication and Authorization

### 4.1 v1 Priority

- First for v1: local authentication
- Later or phased support: Active Directory authentication

Why:

- Local authentication is easier for OSS users to try immediately
- AD integration is valuable but environment-dependent
- Early value comes from making the application self-contained first

### 4.2 Authorization

- Recommended direction: role-based authorization as the center
- Supplement with department, approver rank, and partner-specific rules in the application layer

## 5. Attachments, Notifications, and Audit

### 5.1 Attachments

- v1 direction: store local or shared-folder paths
- Later option: SharePoint support

### 5.2 Notifications

- v1 direction: prioritize in-app notifications
- Later option: email notifications

### 5.3 Audit

- v1 direction: DB-backed audit for major tables

## 6. Distribution and Operations

### 6.1 Distribution

- First candidate: installer-based distribution
- Candidates to compare later: MSIX or WiX

### 6.2 Logging

- Recommended candidate: `Microsoft.Extensions.Logging` + `Serilog`

## 7. Recommended v1 Stack

At this point, the recommended v1 stack is:

- UI: WPF
- Runtime: .NET 10
- UI pattern: MVVM
- WPF framework: CommunityToolkit.Mvvm + Generic Host
- DB: SQLite for baseline support, SQL Server for recommended managed environments
- Data access: EF Core
- Logging: Microsoft.Extensions.Logging + Serilog
- CSV: CsvHelper
- Distribution: installer

## 8. Decisions to Make Soon

The following should be decided before implementation grows:

1. Whether to proceed with CommunityToolkit.Mvvm + Generic Host as the WPF framework base
2. Whether v1 should prioritize local authentication
3. Whether v1 should prioritize in-app notifications
4. Whether installer evaluation should begin with MSIX or WiX

## 9. Current Recommendation

The least risky path at this stage is:

- WPF
- .NET 10
- Layered architecture
- MVVM + CommunityToolkit.Mvvm + Generic Host
- EF Core
- SQLite-first with SQL Server-ready design
- Local-auth-first
- Attachment links through local/shared-folder paths
- In-app notifications first

This combination supports both single-PC startup and later server-backed operation, while staying understandable as an OSS Windows desktop application.
