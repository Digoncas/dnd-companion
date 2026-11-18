# ADR 0002: GitHub OAuth first, Microsoft Entra External ID as the end goal

Status: accepted

## Context
Identity should be delegated, not self-built. Entra External ID is the target but needs more setup.

## Decision
Phase 2 ships GitHub OAuth (OpenID Connect-style flow in ASP.NET Core). Roles (Master, Player) are claims-based and provider-independent. Migrate to Entra External ID in a later version.

## Consequences
- Fast first login; authorization code never depends on the provider.
- Migration is a provider swap, not a redesign, as long as policies use our own role claims.
