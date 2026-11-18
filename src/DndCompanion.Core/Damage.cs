// Co-Authored-By: Claude
namespace DndCompanion.Core;

public enum DamageType { Slashing, Piercing, Bludgeoning, Fire, Cold, Poison }

/// <summary>Strategy: adjusts incoming damage. New rules are new classes; Creature never changes (open/closed).</summary>
public interface IDamageRule
{
    int Apply(int amount, DamageType type);
}

public sealed class Resistance(DamageType type) : IDamageRule
{
    public int Apply(int amount, DamageType t) => t == type ? amount / 2 : amount; // rounds down, as in 5e
}

public sealed class Vulnerability(DamageType type) : IDamageRule
{
    public int Apply(int amount, DamageType t) => t == type ? amount * 2 : amount;
}

public sealed class Immunity(DamageType type) : IDamageRule
{
    public int Apply(int amount, DamageType t) => t == type ? 0 : amount;
}
