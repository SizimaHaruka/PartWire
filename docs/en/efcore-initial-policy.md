# PartWire EF Core Initial Policy

## 0. Purpose

This document summarizes the initial EF Core policy for `PartWire`.

## 1. Assumptions

- both SQLite and SQL Server should be supported
- the initial schema is organized from a SQL Server baseline
- the runtime provider is selected by configuration

## 2. Core Policy

- EF Core is the standard approach for updates
- list/search/aggregation queries may be separated into query services
- schema management uses migrations
- start with a single `DbContext`

## 3. DbContext Policy

Initial name:

- `PartWireDbContext`

Responsibilities:

- persistence
- relational mapping
- common-column configuration
- optimistic-lock configuration

## 4. Entity Mapping Policy

- prefer Fluent API
- use `IEntityTypeConfiguration<T>` per table
- keep data annotations minimal

## 5. Migration Policy

- create the first migration from the SQL Server baseline
- use the same model for SQLite where provider differences are acceptable
- keep SQLite-to-SQL Server migration as a separate tool

## 6. Type Policy

- amounts: `decimal(18,2)`
- quantities: `decimal(18,4)`
- datetimes: `datetime2` equivalent
- dates: `date`
- booleans: `bit` with SQLite conversion
- enums: store as strings

## 7. Recommended Next Work

1. create `PartWireDbContext`
2. add entity configurations
3. create the initial migration
4. add provider switching by configuration
5. define sample-data loading policy
