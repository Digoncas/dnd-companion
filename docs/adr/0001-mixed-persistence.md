# ADR 0001: Mixed persistence (SQL + Cosmos DB)

Status: accepted (implemented in phase 2)

## Context
Campaigns and users are relational with integrity needs. The session event log (rolls, damage, turns) is append-heavy, schema-flexible JSON.

## Decision
SQL for users and campaigns; Azure Cosmos DB (NoSQL) for the event log.

## Consequences
- Each store fits its data; shows choosing storage by data shape.
- Two stores add operational and testing complexity (Cosmos emulator locally).
- Cosmos stays within the free tier; revisit if costs appear.
