// Co-Authored-By: Claude
using DndCompanion.Core;

namespace DndCompanion.Tests;

public class CreatureTests
{
    [Fact]
    public void Damage_reduces_hp_and_returns_hp_lost()
    {
        var c = new Creature("Orc", 15);
        Assert.Equal(6, c.TakeDamage(6, DamageType.Slashing));
        Assert.Equal(9, c.CurrentHp);
    }

    [Fact]
    public void Hp_never_drops_below_zero_and_lost_is_capped()
    {
        var c = new Creature("Orc", 5);
        Assert.Equal(5, c.TakeDamage(50, DamageType.Fire));
        Assert.True(c.IsDown);
    }

    [Fact]
    public void Resistance_halves_matching_type_rounding_down()
    {
        var c = new Creature("Imp", 20, [new Resistance(DamageType.Fire)]);
        Assert.Equal(3, c.TakeDamage(7, DamageType.Fire));
    }

    [Fact]
    public void Vulnerability_doubles_matching_type()
    {
        var c = new Creature("Mummy", 30, [new Vulnerability(DamageType.Fire)]);
        Assert.Equal(14, c.TakeDamage(7, DamageType.Fire));
    }

    [Fact]
    public void Immunity_blocks_matching_type()
    {
        var c = new Creature("Golem", 30, [new Immunity(DamageType.Poison)]);
        Assert.Equal(0, c.TakeDamage(9, DamageType.Poison));
        Assert.Equal(30, c.CurrentHp);
    }

    [Fact]
    public void Rules_ignore_other_damage_types()
    {
        var c = new Creature("Imp", 20, [new Resistance(DamageType.Fire)]);
        Assert.Equal(7, c.TakeDamage(7, DamageType.Cold));
    }

    [Fact]
    public void Heal_is_capped_at_max_hp()
    {
        var c = new Creature("Aria", 10);
        c.TakeDamage(4, DamageType.Piercing);
        c.Heal(100);
        Assert.Equal(10, c.CurrentHp);
    }

    [Fact]
    public void Rejects_invalid_input()
    {
        Assert.Throws<ArgumentException>(() => new Creature(" ", 5));
        Assert.Throws<ArgumentOutOfRangeException>(() => new Creature("X", 0));
        var c = new Creature("X", 5);
        Assert.Throws<ArgumentOutOfRangeException>(() => c.TakeDamage(-1, DamageType.Fire));
        Assert.Throws<ArgumentOutOfRangeException>(() => c.Heal(-1));
    }
}
