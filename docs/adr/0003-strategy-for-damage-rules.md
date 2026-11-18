# ADR 0003: Strategy for damage rules

Status: accepted

## Context
Damage is modified by resistance, vulnerability, immunity and, later, other effects. An `if/switch` inside `Creature` would grow with every rule.

## Decision
`IDamageRule` (Strategy) with one class per rule, injected into `Creature` and applied in order.

## Consequences
- New rules need no change to `Creature` (open/closed) and are tested alone.
- Rule order matters when several apply; today they run in the order given. Revisit if combinations need defined precedence.
