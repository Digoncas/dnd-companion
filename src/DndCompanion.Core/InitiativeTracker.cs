// Co-Authored-By: Claude
namespace DndCompanion.Core;

public sealed record Combatant(string Name, int Initiative);

/// <summary>Initiative order for one combat: highest first, ties keep insertion order.</summary>
public sealed class InitiativeTracker
{
    private readonly List<Combatant> _order = new();
    private int _index;
    private bool _started;

    public int Round { get; private set; } = 1;
    public IReadOnlyList<Combatant> Order => _order;
    public Combatant? Current => _order.Count == 0 ? null : _order[_index];

    public void Add(string name, int initiative)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
        if (_order.Any(c => c.Name == name)) throw new InvalidOperationException($"'{name}' is already in combat.");

        var current = Current;
        _order.Insert(_order.FindLastIndex(c => c.Initiative >= initiative) + 1, new Combatant(name, initiative));
        if (_started && current is not null) _index = _order.IndexOf(current); // once combat runs, keep the turn on the same combatant; before that the top of the order acts first
    }

    public void NextTurn()
    {
        if (_order.Count == 0) throw new InvalidOperationException("No combatants.");
        _started = true;
        if (++_index >= _order.Count) { _index = 0; Round++; }
    }

    public void Remove(string name)
    {
        var i = _order.FindIndex(c => c.Name == name);
        if (i < 0) throw new InvalidOperationException($"'{name}' is not in combat.");

        _order.RemoveAt(i);
        if (i < _index) _index--;
        else if (i == _index && _index >= _order.Count) { _index = 0; if (_order.Count > 0) Round++; } // removed the last in order: the turn wraps
    }
}
