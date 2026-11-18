// Co-Authored-By: Claude
using DndCompanion.Core;
using Microsoft.AspNetCore.SignalR;

namespace DndCompanion.Web.Hubs;

/// <summary>Clients join a session by code, send commands, and receive "StateChanged" snapshots.</summary>
public sealed class CombatHub(CombatSessions sessions) : Hub
{
    private const string CodeKey = "code";

    public async Task<CombatSnapshot> Join(string code)
    {
        if (string.IsNullOrWhiteSpace(code)) throw new HubException("Session code is required.");
        Context.Items[CodeKey] = code;
        await Groups.AddToGroupAsync(Context.ConnectionId, code);
        return Run(c => { }, snapshotOnly: true);
    }

    public void AddCreature(string name, int maxHp, int initiative) => Run(c => c.Add(new Creature(name, maxHp), initiative));
    public void Damage(string name, int amount, DamageType type) => Run(c => c.Damage(name, amount, type));
    public void Heal(string name, int amount) => Run(c => c.Heal(name, amount));
    public void NextTurn() => Run(c => c.NextTurn());

    private CombatSnapshot Run(Action<Combat> command, bool snapshotOnly = false)
    {
        if (Context.Items[CodeKey] is not string code) throw new HubException("Join a session first.");
        var combat = sessions.GetOrCreate(code);
        // shortcut: one lock per combat is enough for in-memory phase 1; revisit with persistence
        lock (combat)
        {
            try { command(combat); }
            catch (Exception e) when (e is ArgumentException or InvalidOperationException) { throw new HubException(e.Message); }
            return CombatSessions.Snapshot(combat);
        }
    }
}
