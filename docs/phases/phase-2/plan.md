# Phase 2: Auth and mixed persistence (draft)

**Goal:** users sign in with GitHub OAuth; campaigns in SQL, session event log in Cosmos DB.

**In scope:** OAuth login, Master/Player roles as claims, EF Core + SQL, Cosmos event log, ADR updates.

**Out of scope:** Entra External ID (documented in ADR 0002 as the later target), Azure Functions.

**Done when:** login works, roles gate Master actions, a finished session's log is readable from Cosmos.

Refine this plan when phase 1 is done.
