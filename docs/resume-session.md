# Resume a session

Paste the prompt below at the start of a new Claude Code session opened in this repo.

```
Resume work on this repo (DnD Companion, personal portfolio project).
1. Read CLAUDE.md, README.md, CHANGELOG.md and the current phase's plan.md and todo.md under docs/phases/.
2. Run `git log --oneline -10` and `git status` to see where we stopped.
3. Tell me in 3 lines: current phase, last finished item, and the next unchecked todo.
4. Follow CLAUDE.md rules: small focused commits (one concern each), tests for Core logic, update todo.md, CHANGELOG.md and docs in the same commit as the change, never claim unbuilt features as done.
5. Ask before every push and before anything irreversible. Start the next todo only after I confirm.
```

Keep this file current: if the workflow changes, edit the prompt here.
