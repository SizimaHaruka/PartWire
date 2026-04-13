# PartWire

PartWire is a purchase management application for small teams that cannot easily adopt cloud services due to contract approval, security review, or operational constraints.

It is designed for departments that still need to manage quotation requests, approvals, orders, deliveries, invoices, and document circulation in one place, using a simple setup that can be operated on their own PCs.

## Concept

PartWire aims to provide a practical alternative for teams that:

- cannot introduce SaaS or cloud procurement tools quickly
- need to keep operations simple and understandable
- want to start with a small local setup and expand only if needed
- prioritize fast day-to-day processing on Windows PCs

The product is intentionally oriented toward "simple, controllable, and locally operable" procurement management rather than cloud-first operation.

## Target Users

PartWire is intended for:

- small departments or back-office teams with limited IT budget or approval authority
- teams managing purchasing internally across quotation, ordering, delivery, and invoice processing
- users who primarily work on Windows PCs
- organizations that want an OSS application they can run and customize themselves

## Operation Model

PartWire supports two practical deployment styles:

- Single-PC operation: suitable for small-scale use, verification, or departments that want to start immediately on one operator PC
- Server DB operation: recommended when the organization already has the environment to host a shared database

In other words, PartWire can be operated on a single PC, but if your environment allows it, building the database on a server is the recommended approach.

The intended direction is:

- SQLite for local standalone operation
- SQL Server for environments that need shared or more structured operation

## Project Direction

This project is planned as OSS and is being designed around the following principles:

- Windows desktop-first usability
- simple operation for small teams
- minimal dependence on cloud services
- ability to migrate from a local-first start to a more managed environment later

Technical selection and implementation details will be documented incrementally as the project design is refined.
