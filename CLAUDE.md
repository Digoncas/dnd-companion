# CLAUDE.md

Personal portfolio project (D&D session companion). Not related to any employer code.

## Commands
- Build: `dotnet build`
- Test: `dotnet test`
- Run: `dotnet run --project src/DndCompanion.Web`

## Rules
- Game logic goes in `DndCompanion.Core` (no UI, no I/O) and gets an xUnit test.
- NuGet comes only from nuget.org (see `nuget.config`); never add a private feed.
- Git identity is local to this repo; do not change global git config.
- Before work, read the current phase in `docs/phases/`; tick `todo.md` as items finish.
- A significant decision gets an ADR in `docs/adr/`. Update docs in the same commit as the change.
- Apply SOLID and patterns only where they reduce coupling, and record why in `docs/design.md` (and an ADR when significant).
- Mark unbuilt features as "planned" in docs; never present them as done.
- No secrets in the repo; use user-secrets or environment variables.
- Source edits carry a `Co-Authored-By: Claude` comment on the changed line.
