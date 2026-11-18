// Co-Authored-By: Claude
using System.Collections.Concurrent;
using DndCompanion.Core;
using Microsoft.AspNetCore.SignalR;

namespace DndCompanion.Web.Hubs;

/// <summary>What clients see: a read-only copy of a combat.</summary>
public sealed record CombatSnapshot(int Round, string? Current, IReadOnlyList<CreatureView> Creatures);

public sealed record CreatureView(string Name, int CurrentHp, int MaxHp, bool IsDown);

/// <summary>In-memory combats by session code (phase 1; persistence arrives in phase 2). Subscribes to <see cref="Combat.Changed"/> and broadcasts to the SignalR group.</summary>
public sealed class CombatSessions(IHubContext<CombatHub> hub)
{
    private readonly ConcurrentDictionary<string, Combat> _sessions = new(StringComparer.OrdinalIgnoreCase);

    public Combat GetOrCreate(string code) => _sessions.GetOrAdd(code, c =>
    {
        var combat = new Combat();
        // shortcut: fire-and-forget send, a failed broadcast is not retried; upgrade if clients must never miss an update
        combat.Changed += () => _ = hub.Clients.Group(c).SendAsync("StateChanged", Snapshot(combat));
        return combat;
    });

    public static CombatSnapshot Snapshot(Combat combat) => new(
        combat.Round,
        combat.Current?.Name,
        combat.InOrder.Select(c => new CreatureView(c.Name, c.CurrentHp, c.MaxHp, c.IsDown)).ToList());
}
