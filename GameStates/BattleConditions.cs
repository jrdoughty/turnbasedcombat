using System.Collections.Generic;
using Godot;

public partial class BattleConditions : Node
{
    public List<Team> Teams { get; set; }
    public bool SessionComplete()
    {
        foreach (var team in Teams)
        {
            if (team.IsDefeated)
            {
                return true; // Session is complete if any team is defeated
            }
        }

        return false; // Placeholder for actual session check
    }
}