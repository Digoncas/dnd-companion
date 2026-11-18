// Co-Authored-By: Claude
using DndCompanion.Core;

namespace DndCompanion.Tests;

public class InitiativeTrackerTests
{
    private static InitiativeTracker Sample()
    {
        var t = new InitiativeTracker();
        t.Add("Goblin", 12);
        t.Add("Aria", 18);
        t.Add("Borin", 7);
        return t;
    }

    [Fact]
    public void Orders_highest_initiative_first()
    {
        Assert.Equal(new[] { "Aria", "Goblin", "Borin" }, Sample().Order.Select(c => c.Name));
    }

    [Fact]
    public void Ties_keep_insertion_order()
    {
        var t = new InitiativeTracker();
        t.Add("A", 10);
        t.Add("B", 10);
        Assert.Equal(new[] { "A", "B" }, t.Order.Select(c => c.Name));
    }

    [Fact]
    public void NextTurn_advances_then_wraps_and_increments_round()
    {
        var t = Sample();
        Assert.Equal("Aria", t.Current!.Name);
        t.NextTurn(); t.NextTurn();
        Assert.Equal("Borin", t.Current!.Name);
        Assert.Equal(1, t.Round);
        t.NextTurn();
        Assert.Equal("Aria", t.Current!.Name);
        Assert.Equal(2, t.Round);
    }

    [Fact]
    public void Adding_mid_combat_keeps_the_current_turn()
    {
        var t = Sample();
        t.NextTurn(); // Goblin
        t.Add("Fast", 20);
        Assert.Equal("Goblin", t.Current!.Name);
    }

    [Fact]
    public void Removing_before_current_keeps_the_current_turn()
    {
        var t = Sample();
        t.NextTurn(); // Goblin
        t.Remove("Aria");
        Assert.Equal("Goblin", t.Current!.Name);
    }

    [Fact]
    public void Removing_current_last_combatant_wraps_to_next_round()
    {
        var t = Sample();
        t.NextTurn(); t.NextTurn(); // Borin, last
        t.Remove("Borin");
        Assert.Equal("Aria", t.Current!.Name);
        Assert.Equal(2, t.Round);
    }

    [Fact]
    public void Rejects_blank_or_duplicate_names_and_empty_next_turn()
    {
        var t = new InitiativeTracker();
        Assert.Throws<InvalidOperationException>(() => t.NextTurn());
        Assert.Throws<ArgumentException>(() => t.Add(" ", 5));
        t.Add("A", 5);
        Assert.Throws<InvalidOperationException>(() => t.Add("A", 9));
        Assert.Throws<InvalidOperationException>(() => t.Remove("Nobody"));
    }
}
