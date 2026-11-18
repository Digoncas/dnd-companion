# Phase 3: Azure Function + Claude API (draft)

**Goal:** an Azure Function generates encounters/NPCs via the Claude API, called from the web app.

**In scope:** HTTP-triggered Function, Claude API client with error handling and cost limits, tests with a fake client, deploy to Azure free tier with a spending cap.

**Out of scope:** Azure SignalR Service, Entra External ID migration.

**Done when:** the web app requests an encounter and shows the result; API key lives only in the Function's settings.

Re-check current free-tier limits (Azure, Cosmos) before starting. Refine when phase 2 is done.
