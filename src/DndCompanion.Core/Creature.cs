// Co-Authored-By: Claude
namespace DndCompanion.Core;

/// <summary>Hit points of one creature. Damage rules are injected; this class only tracks HP.</summary>
public sealed class Creature
{
    private readonly IReadOnlyList<IDamageRule> _rules;

    public Creature(string name, int maxHp, IEnumerable<IDamageRule>? rules = null)
    {
        if (string.IsNullOrWhiteSpace(name)) throw new ArgumentException("Name is required.", nameof(name));
        if (maxHp <= 0) throw new ArgumentOutOfRangeException(nameof(maxHp), "Max HP must be positive.");
        Name = name;
        MaxHp = CurrentHp = maxHp;
        _rules = (rules ?? []).ToList();
    }

    public string Name { get; }
    public int MaxHp { get; }
    public int CurrentHp { get; private set; }
    public bool IsDown => CurrentHp == 0;

    /// <returns>HP actually lost, after rules and the floor at 0.</returns>
    public int TakeDamage(int amount, DamageType type)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        var adjusted = _rules.Aggregate(amount, (a, rule) => rule.Apply(a, type));
        var lost = Math.Min(adjusted, CurrentHp);
        CurrentHp -= lost;
        return lost;
    }

    public void Heal(int amount)
    {
        if (amount < 0) throw new ArgumentOutOfRangeException(nameof(amount));
        CurrentHp = Math.Min(MaxHp, CurrentHp + amount);
    }
}
