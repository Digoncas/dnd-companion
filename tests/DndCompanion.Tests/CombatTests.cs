// Co-Authored-By: Claude
using DndCompanion.Core;

namespace DndCompanion.Tests;

public class CombatTests
{
    private static Combat Sample()
    {
        var c = new Combat();
        c.Add(new Creature("Goblin", 7), 12);
        c.Add(new Creature("Aria", 20), 18);
        c.Add(new Creature("Borin", 25), 7);
        return c;
    }

    [Fact]
    public void Lists_creatures_in_initiative_order()
    {
        Assert.Equal(new[] { "Aria", "Goblin", "Borin" }, Sample().InOrder.Select(x => x.Name));
    }

    [Fact]
    public void Damage_and_heal_apply_to_the_named_creature()
    {
        var c = Sample();
        Assert.Equal(5, c.Damage("Goblin", 5, DamageType.Slashing));
        c.Heal("Goblin", 1);
        Assert.Equal(3, c.InOrder.Single(x => x.Name == "Goblin").CurrentHp);
    }

    [Fact]
    public void NextTurn_skips_downed_creatures()
    {
        var c = Sample();
        c.Damage("Goblin", 99, DamageType.Fire);
        c.NextTurn(); // Aria -> Goblin is down -> Borin
        Assert.Equal("Borin", c.Current!.Name);
    }

    [Fact]
    public void Raises_changed_after_each_mutation()
    {
        var c = new Combat();
        var n = 0;
        c.Changed += () => n++;
        c.Add(new Creature("A", 5), 10);
        c.Damage("A", 1, DamageType.Fire);
        c.Heal("A", 1);
        c.NextTurn();
        Assert.Equal(4, n);
    }

    [Fact]
    public void Unknown_name_and_duplicate_are_rejected()
    {
        var c = Sample();
        Assert.Throws<InvalidOperationException>(() => c.Damage("Nobody", 1, DamageType.Fire));
        Assert.Throws<InvalidOperationException>(() => c.Add(new Creature("Aria", 5), 3));
    }
}
