# Design principles and patterns

Where SOLID and design patterns are applied, and why. Update with each phase. Only items with a code link exist today; the rest are **planned**.

| Principle / pattern | Where | Why | Status |
|---|---|---|---|
| Single responsibility | [InitiativeTracker](../src/DndCompanion.Core/InitiativeTracker.cs) (order and turns), [Creature](../src/DndCompanion.Core/Creature.cs) (HP) | Each class changes for one reason | done |
| Strategy + Open/closed | [IDamageRule](../src/DndCompanion.Core/Damage.cs): `Resistance`, `Vulnerability`, `Immunity` | A new damage rule is a new class; `Creature` is not edited ([ADR 0003](adr/0003-strategy-for-damage-rules.md)) | done |
| Dependency inversion | `Core` has no UI or I/O dependency; rules are injected into `Creature` | Logic stays testable and reusable | partial |
| Composition (`Combat`) | Will compose tracker and creatures instead of growing one class | Avoid a god class | planned (phase 1) |
| Observer / domain events | Combat state changes broadcast through the SignalR hub | Decouples logic from transport | planned (phase 1) |
| Liskov + Interface segregation + Repository | `IEventWriter` / `IEventReader` with in-memory and Cosmos implementations | Swap stores with the same tests | planned (phase 2) |
| Adapter | `IEncounterGenerator` over the Claude API, fake for tests | Isolate the external service | planned (phase 3) |
| Decorator | Logging/caching around the generator | Add behavior without changing it | planned (phase 3) |

Rule: apply a principle where it reduces coupling, and record the reason. No pattern without a real need.
