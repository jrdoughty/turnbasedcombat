using Godot;
using System;

public partial class TurnEndState : State
{
    private PlayerContainer Player;
    private int effectCount = 0;
    public override void EnterState()
    {
        base.EnterState();
        foreach (var player in Players)
        {
            if(player.IsActive)
            {
                Player = player;
                ApplyEndTurnEffects();
                break;
            }
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Input.IsActionJustPressed("ui_accept"))
        {
            ApplyEndTurnEffects(); 
        }
        // Handle state updates
    }
    private void ApplyEndTurnEffects()
    {
        if(Player.PlayerEffects.Count > effectCount)
        {
            Effect effect = Player.PlayerEffects[effectCount];
            switch (effect.EffectType)
            {
                case "Heal Over Time":
                    Player.PlayerCurrentHealth += effect.EffectValue;
                    Player.GetNode<ProgressBar>("PlayerHealth").Value = Player.PlayerCurrentHealth;
                    RTL.Text = Player.PlayerData.name + " heals for " + effect.EffectValue + " from " + effect.EffectName + "!";
                    break;
                case "Buff":
                    Player.PlayerEffects.Add(effect);
                    break;
                case "Debuff":
                    Player.PlayerEffects.Add(effect);
                    break;
                default:
                    GD.Print("Unknown effect type: " + effect.EffectType);
                    break;
            }
            effect.EffectDuration--;
            if (effect.EffectDuration <= 0)
            {
                Player.PlayerEffects.Remove(effect);
                effectCount--;//compensate for the removal of the effect
            }
            effectCount++;
        }
        else
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        foreach (var player in Players)
        {
            player.IsActive = !player.IsActive;//this didnt work
        }
        if(!Players[0].IsActive)
        {
            GD.Print("End of turn!");
            StateChangedHandler("Decision");
        }
        else
        {
            GD.Print("Your turn!");
            StateChangedHandler("Menu");
        }
    }


    public override void ExitState()
    {
        base.ExitState();
    }
}