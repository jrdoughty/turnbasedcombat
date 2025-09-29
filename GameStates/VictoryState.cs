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
                    if (player.PlayerData.level > 0)
                    {
                        player.PlayerData.experience += exp;
                        while (player.PlayerData.experience >= player.PlayerData.getNextLevelExperience())
                        {
                            player.PlayerData.experience -= player.PlayerData.getNextLevelExperience();
                            Level oldLevel = ResourceLoader.Load<Level>($"res://Characters/Levels/{player.PlayerData.name}{player.PlayerData.level}.tres");
                            player.PlayerData.level++;
                            Level newLevel = ResourceLoader.Load<Level>($"res://Characters/Levels/{player.PlayerData.name}{player.PlayerData.level}.tres");
                            if (newLevel != null && oldLevel != null)
                            {
                                GD.Print($"Leveling up {player.PlayerData.name} to level {player.PlayerData.level}");
                                player.PlayerData.attack += newLevel.attack - oldLevel.attack;
                                player.PlayerData.defense += newLevel.defense - oldLevel.defense;
                                player.PlayerData.speed += newLevel.speed - oldLevel.speed;
                                player.PlayerData.health += newLevel.health - oldLevel.health;
                                player.PlayerData.mana += newLevel.mana - oldLevel.mana;
                                player.PlayerData.magicAttack += newLevel.magicAttack - oldLevel.magicAttack;
                                player.PlayerData.magicDefense += newLevel.magicDefense - oldLevel.magicDefense;
                                player.PlayerLevel.Text = player.PlayerData.level.ToString();
                                RTL.Text += $"\n{player.PlayerData.name} is now level {player.PlayerData.level}!";
                            }
                            else if (newLevel != null && oldLevel == null)
                            {
                                player.PlayerData.attack += newLevel.attack;
                                player.PlayerData.defense += newLevel.defense;
                                player.PlayerData.speed += newLevel.speed;
                                player.PlayerData.health += newLevel.health;
                                player.PlayerData.mana += newLevel.mana;
                                player.PlayerData.magicAttack += newLevel.magicAttack;
                                player.PlayerData.magicDefense += newLevel.magicDefense;
                                player.PlayerLevel.Text = player.PlayerData.level.ToString();
                                RTL.Text += $"\n{player.PlayerData.name} is now level {player.PlayerData.level}!";
                            }

                        }
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