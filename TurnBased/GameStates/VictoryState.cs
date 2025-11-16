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
                        exp += player.CharacterData.getExperience(player.CharacterData.Level);
                    }
                    
                }
            }
        }
        foreach (var team in Teams)
        {
            if (!team.IsDefeated)
            {
                if (team.Players.Count > 1)
                    RTL.Text = $"{team.Name} wins!";
                else
                    RTL.Text = $"{team.Players[0].CharacterData.CharacterName} wins!";
                foreach (var player in team.Players)
                {
                    if (player.CharacterData.Level > 0)
                    {
                        player.CharacterData.Experience += exp;
                        while (player.CharacterData.Experience >= player.CharacterData.getNextLevelExperience())
                        {
                            player.CharacterData.Experience -= player.CharacterData.getNextLevelExperience();
                            Level oldLevel = ResourceLoader.Load<Level>($"res://TurnBased/Characters/Levels/{player.CharacterData.CharacterName}{player.CharacterData.Level}.tres");
                            player.CharacterData.Level++;
                            Level newLevel = ResourceLoader.Load<Level>($"res://TurnBased/Characters/Levels/{player.CharacterData.CharacterName}{player.CharacterData.Level}.tres");
                            if (newLevel != null && oldLevel != null)
                            {
                                //GD.Print($"Leveling up {player.CharacterData.CharacterName} to level {player.CharacterData.Level}");
                                player.CharacterData.Attack += newLevel.attack - oldLevel.attack;
                                player.CharacterData.Defense += newLevel.defense - oldLevel.defense;
                                player.CharacterData.Speed += newLevel.speed - oldLevel.speed;
                                player.CharacterData.Health += newLevel.health - oldLevel.health;
                                player.CharacterData.Mana += newLevel.mana - oldLevel.mana;
                                player.CharacterData.MagicAttack += newLevel.magicAttack - oldLevel.magicAttack;
                                player.CharacterData.MagicDefense += newLevel.magicDefense - oldLevel.magicDefense;
                                player.CharacterLevel.Text = player.CharacterData.Level.ToString();
                                RTL.Text += $"\n{player.CharacterData.CharacterName} is now level {player.CharacterData.Level}!";
                            }
                            else if (newLevel != null && oldLevel == null)
                            {
                                player.CharacterData.Attack += newLevel.attack;
                                player.CharacterData.Defense += newLevel.defense;
                                player.CharacterData.Speed += newLevel.speed;
                                player.CharacterData.Health += newLevel.health;
                                player.CharacterData.Mana += newLevel.mana;
                                player.CharacterData.MagicAttack += newLevel.magicAttack;
                                player.CharacterData.MagicDefense += newLevel.magicDefense;
                                player.CharacterLevel.Text = player.CharacterData.Level.ToString();
                                RTL.Text += $"\n{player.CharacterData.CharacterName} is now level {player.CharacterData.Level}!";
                            }

                        }
                    }
                }
                Utils.SavePartyData(team.GetPartyData());
            }
            else
            {
                //GD.Print($"{team.Players[0].CharacterData.CharacterName} has been killed.");
                EventManager.CompleteEvent($"Kill_{team.Players[0].CharacterData.CharacterName}");
            }
        }
        // Handle entering the state
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(Input.IsActionJustPressed("ui_accept"))
        {
            var p = GetParent();
            TurnBasedBattle battle = GetNode<TurnBasedBattle>("/root/Game/Battle");
            battle.GetParent<Node>().RemoveChild(battle);
            battle.QueueFree();
        }
        // Handle state updates
    }
    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}