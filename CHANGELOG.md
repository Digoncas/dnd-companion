# Changelog

Newest first. One entry per meaningful change; keep in sync with commits.

## 2026-11-17
- CI: GitHub Actions workflow `.github/workflows/ci.yml` (build + test on push to main and PRs). Not yet seen running. Phase 1.
- Web: Blazor `/combat/{code}` page (initiative list, HP, damage/heal, add creature, next turn) over the SignalR hub; Home takes a session code. Server and negotiate respond; two-browser live check not done yet. Phase 1.
- Web: `CombatHub` (`/hubs/combat`: Join, AddCreature, Damage, Heal, NextTurn) and in-memory `CombatSessions` that broadcast `StateChanged` snapshots to the session group. Not yet runtime-tested end to end; Blazor page planned. Phase 1.
- Core: `Combat` composes tracker and creatures, skips downed creatures on `NextTurn`, raises `Changed` after each mutation; 5 more tests (20 total). Phase 1.
- Core: `Creature` (HP, heal capped at max) with Strategy-based `IDamageRule` (`Resistance`, `Vulnerability`, `Immunity`); 8 more xUnit tests (15 total). Phase 1.
- Docs: `docs/design.md` (SOLID/patterns map), ADR 0003, design rule in CLAUDE.md.
- Core: `InitiativeTracker` (initiative order, turns, rounds; ties keep insertion order; turn stays on the same combatant once combat runs) with 7 xUnit tests. Phase 1.
- Docs: README, architecture, ADR 0001 (mixed persistence), ADR 0002 (GitHub OAuth then Entra External ID), phase 1-3 plans and todos, CLAUDE.md.
- Repo: solution skeleton (Core, Web, Tests), `nuget.config` limited to nuget.org, public repo created.
