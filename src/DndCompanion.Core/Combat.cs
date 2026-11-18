// Co-Authored-By: Claude
namespace DndCompanion.Core;

/// <summary>Composes initiative and creatures. Raises <see cref="Changed"/> after every mutation (Observer) so a transport such as SignalR can broadcast without Core knowing it.</summary>
public sealed class Combat
{
    private readonly InitiativeTracker _tracker = new();
    private readonly Dictionary<string, Creature> _creatures = new();

    public event Action? Changed;

    public int Round => _tracker.Round;
    public Creature? Current => _tracker.Current is null ? null : _creatures[_tracker.Current.Name];
    public IEnumerable<Creature> InOrder => _tracker.Order.Select(c => _creatures[c.Name]);

    public void Add(Creature creature, int initiative)
    {
        _tracker.Add(creature.Name, initiative); // validates name and duplicates first
        _creatures[creature.Name] = creature;
        Changed?.Invoke();
    }

    public int Damage(string name, int amount, DamageType type)
    {
        var lost = Get(name).TakeDamage(amount, type);
        Changed?.Invoke();
        return lost;
    }

    public void Heal(string name, int amount)
    {
        Get(name).Heal(amount);
        Changed?.Invoke();
    }

    /// <summary>Advances to the next creature that is still standing; if all are down the turn still advances once.</summary>
    public void NextTurn()
    {
        for (var i = 0; i < _creatures.Count; i++)
        {
            _tracker.NextTurn();
            if (!Current!.IsDown) break;
        }
        Changed?.Invoke();
    }

    private Creature Get(string name) =>
        _creatures.TryGetValue(name, out var c) ? c : throw new InvalidOperationException($"'{name}' is not in combat.");
}
