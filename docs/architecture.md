# Architecture

Target architecture (phases 2-3 are **planned**, not built).

```mermaid
flowchart LR
  Browser -->|Blazor + SignalR| Web[DndCompanion.Web]
  Web --> Core[DndCompanion.Core]
  Web -->|campaigns, users - planned| SQL[(SQL)]
  Web -->|event log - planned| Cosmos[(Cosmos DB)]
  Web -->|HTTP - planned| Fn[Azure Function]
  Fn --> Claude[Claude API]
  Browser -.->|OAuth - planned| IdP[GitHub, later Entra External ID]
```

- **Core** holds pure rules and is the only project unit-tested heavily.
- **Web** owns UI, the SignalR hub and (later) auth and persistence.
- **Function** keeps the Claude API key out of the web app and runs async, occasional work.
