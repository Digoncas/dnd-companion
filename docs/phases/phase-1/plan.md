# Phase 1: Real-time combat, tests, CI

**Goal:** several browsers share one live combat (initiative order, HP, turns).

**In scope:** Core logic (initiative, damage) with xUnit tests; SignalR hub; Blazor combat page; GitHub Actions build+test; README demo.

**Out of scope:** auth, databases, Azure, Claude API.

**Done when:** two browsers see the same combat update live, `dotnet test` is green in CI, README has a demo GIF.

**Decisions to make:** in-memory state per session (fine for phase 1; persistence arrives in phase 2).
