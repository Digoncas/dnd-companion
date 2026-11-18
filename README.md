# DnD Companion

A real-time D&D session companion: shared initiative order, HP tracking and an event log that every player sees live.

> **Status:** Phase 1 in progress. Nothing below is claimed as done until its phase is checked off in [docs/phases](docs/phases).

## Stack

| Area | Choice | Phase |
|---|---|---|
| UI | Blazor Web App (.NET 10) | 1 |
| Real time | ASP.NET Core SignalR | 1 |
| Tests / CI | xUnit, GitHub Actions | 1 |
| Persistence | SQL (campaigns, users) + Azure Cosmos DB (session event log) | 2 |
| Auth | GitHub OAuth, then Microsoft Entra External ID | 2 |
| Async work | Azure Functions calling the Claude API (encounter generator) | 3 |

Why these choices: see [docs/adr](docs/adr).

## Run locally

```
dotnet build
dotnet test
dotnet run --project src/DndCompanion.Web
```

## Layout

- `src/DndCompanion.Core` - pure game logic (initiative, damage), no UI or I/O
- `src/DndCompanion.Web` - Blazor UI and SignalR hub
- `tests/DndCompanion.Tests` - xUnit tests for Core
- `docs/` - [architecture](docs/architecture.md), [ADRs](docs/adr), [phase plans](docs/phases)

## Built with Claude Code

Developed with Claude Code. Repo rules live in [CLAUDE.md](CLAUDE.md); each phase has a plan and todo so work is reviewable and resumable.
