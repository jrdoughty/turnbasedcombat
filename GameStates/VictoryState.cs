using Godot;
using System;

public partial class VictoryState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        int exp = 0;
        //int gold = 0;
        foreach (var team in Teams)
        {
            if (team.IsDefeated)
            {
                foreach (var player in team.Players)
                {
                    if(player.IsDefeated)
                    {
                        exp += player.PlayerData.getExperience(player.PlayerData.level);
                    }
                    
                }
            }
        }
        foreach (var team in Teams)
        {
            if (!team.IsDefeated)
            {
                RTL.Text = $"{team.Name} wins!";
                foreach (var player in team.Players)
                {
                    player.PlayerData.experience += exp;
                    while(player.PlayerData.experience >= player.PlayerData.getNextLevelExperience())
                    {
                        player.PlayerData.experience -= player.PlayerData.getNextLevelExperience();
                        player.PlayerData.level++;
                    }
                }
            }
        }
        // Handle entering the state
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(Input.IsActionJustPressed("ui_accept"))
        {

        }
        // Handle state updates
    }
    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}