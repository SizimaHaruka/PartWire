# PartWire

PartWire is an application concept for managing procurement work in manufacturing environments, including quotation handling, approvals, ordering, delivery tracking, invoice handling, and document forwarding.

The requirements and design materials in this repository were written from the perspective of someone who understands real manufacturing purchasing work and is trying to translate that work into a usable system. The underlying ideas come from actual operations, but the contents here have been anonymized and generalized to make them easier to share and discuss.

This repository is not intended to present a textbook-perfect answer. The goal is to make the thought process visible: how complex operational requirements are organized, how exceptions are handled, what should be treated as a management unit, and how those ideas can be turned into a system design.

The target style is something close to cleaned-up internal design material: practical documents that feel grounded in actual work, but are rewritten in a form that can be read and reviewed more clearly from the outside.

Note:
The author is not a professional software developer and does not claim to know the standard or ideal way such design documentation is usually produced. Because of that, this repository should be read less as a polished final methodology and more as a careful attempt to structure practical business knowledge into a coherent system design.

## Documentation

English documents are the primary reference set for this repository.

- [Requirements](C:/wk/c#/PartWire/docs/en/requirements.md)
- [Basic Design](C:/wk/c#/PartWire/docs/en/basic-design.md)
- [Technical Requirements](C:/wk/c#/PartWire/docs/en/technical-requirements.md)
- [ER Diagram (DBML)](C:/wk/c#/PartWire/docs/en/partwire-er.dbml)

## Concept

PartWire aims to provide a practical alternative for teams that:

- cannot introduce SaaS or cloud procurement tools quickly
- need to keep operations simple and understandable
- want to start with a small local setup and expand only if needed
- prioritize fast day-to-day processing on Windows PCs

The product is intentionally oriented toward simple, controllable, locally operable procurement management rather than cloud-first operation.

## Target Users

PartWire is intended for:

- small departments or back-office teams with limited IT budget or approval authority
- teams managing purchasing internally across quotation, ordering, delivery, and invoice processing
- users who primarily work on Windows PCs
- organizations that want an OSS application they can run and customize themselves

## Operation Model

PartWire supports two practical deployment styles:

- single-PC operation for small-scale use, verification, or fast local rollout
- shared database operation for environments that support multi-user use

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
