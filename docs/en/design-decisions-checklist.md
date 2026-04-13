# PartWire Design Decision Log and Checklist

## 1. Purpose

This document is the primary source for centrally managing unresolved items in `PartWire` and continuously recording design decisions, their rationale, remaining follow-up items, and reflection status in related documents.

Operational Rules:

- Unresolved items are managed in this document.
- Each issue is managed with a Markdown checkbox.
- The issue checkbox is updated when the policy is fixed.
- `Reflected` is updated when the related documents have been updated.
- `requirements.md`, `basic-design.md`, and `technical-requirements.md` are treated as deliverable documents that reflect this log.
- Reflection is performed by domain rather than one issue at a time.

## 2. Entry Rules

Each issue is recorded in the following fixed format.

- [ ] Issue Name
  - Current Options / Premise:
  - Answer:
  - Rationale:
  - Follow-up Items:
  - Reflection Target:
  - Reflected: [ ] requirements [ ] basic-design [ ] technical-requirements

Notes:

- If an issue is still open, write `Open` in `Answer`.
- `Rationale` briefly states the adoption reason or operational premise.
- `Follow-up Items` should contain only derivative issues that truly require later review.

## 3. Confirmed Domains

The following domains have already been reflected into the Japanese source documents and are summarized here in English:

- Approval and ordering
  - Approval is per quotation and multi-step.
  - Orders are strictly quotation-based.
  - Project-specific approval route overrides are not allowed.
  - Quotation revision is allowed only before delivery.
  - Additional orders are presented in history in the same way as multiple quotations under one project.

- Delivery and invoicing
  - Delivery confirmation is PC-first and one delivery slip at a time.
  - Cross-project bulk confirmation is not supported.
  - Barcode and QR support are optional modules.
  - Invoice registration targets are determined by the partner master flag.
  - One invoice can link only to deliveries under the same quotation.
  - Completion is judged on quantity basis, while amount differences trigger notifications.
  - Discounts, miscellaneous costs, administration fees, and shipping fees can be entered freely.

- Project, line, and numbering
  - The business axis is project-centric.
  - Duplicate parts within the same project are prohibited.
  - Project numbers use `Q` + 10 digits.
  - Status is auto-determined with no manual override.

- Partners and manufactured parts
  - Manufactured part extensions use fixed columns rather than EAV.
  - Processing vendors that do not satisfy required conditions are not shown.
  - No per-project exception exists for partner roles or invoice settings.

- Authorization and authentication
  - View and operation scope are role-based.
  - No dedicated amount-visibility-only privilege exists.
  - UPN is the user binding key across authentication modes.
  - Authorization uses both internal user information and AD group lookup under normal conditions.
  - If AD group lookup fails, operation continues using internal user information only.

- Attachments, CSV, audit, and DB foundation
  - Attachments are in v1 scope for quotations, delivery slips, and invoices.
  - Shared folders are standard attachment storage, with SharePoint also supported.
  - When storage limits are exceeded, only new attachment registration is stopped.
  - CSV import uses `UserKey` and full rollback on error.
  - Optimistic locking uses update timestamps.
  - SQLite-to-SQL Server migration uses a dedicated tool or administrator command.
  - Audit logs use standard granularity, five-year configurable retention, and system-admin-only viewing.

- Notifications and dashboard
  - Notifications are in v1 scope.
  - Both email and in-app notifications are provided.
  - Recipients default to all project-related users.
  - Notification items are configurable.
  - Notification history and read-state retention default to one year and are configurable.

## 4. Ongoing Follow-up Issues

- [ ] Resubmission UI and input retention rules after rejection
  - Current Options / Premise: Rejection and resubmission are allowed, but detailed UI behavior is still open.
  - Answer: Open
  - Rationale: This affects user experience and resubmission efficiency.
  - Follow-up Items: Field-level input retention and screen transition details
  - Reflection Target: basic-design
  - Reflected: [ ] requirements [ ] basic-design [ ] technical-requirements

- [ ] Dashboard prioritization by assignee
  - Current Options / Premise: The dashboard already shows projects related to the current user, but ordering and priority logic remain open.
  - Answer: Adjust after implementation.
  - Rationale: This affects dashboard ordering, counts, and attention extraction.
  - Follow-up Items: Priority logic remains open
  - Reflection Target: requirements, basic-design
  - Reflected: [ ] requirements [ ] basic-design [ ] technical-requirements
