using Godot;
using System;

public partial class EffectState : State
{
    
    PlayerContainer targetPlayer;
    PlayerContainer actorPlayer;
    public override void EnterState()
    {
        base.EnterState();   
        ApplyEffect();
    }

    public void ApplyEffect()
    {
        // Apply the effect to the target player
        // This method can be called from the EnterState method or elsewhere as needed 
        targetPlayer = Actions[0].Target;
        actorPlayer = Actions[0].Actor;
        GD.Print("Actor: " + actorPlayer.PlayerData.name + ", Target: " + targetPlayer.PlayerData.name);
        Effect effect = Actions[0].Effects[0];
        Actions[0].Effects.RemoveAt(0);
        switch (effect.EffectType)
        {
            case "Damage":
                targetPlayer.PlayerCurrentHealth -= effect.EffectValue;
                targetPlayer.GetNode<ProgressBar>("PlayerHealth").Value = targetPlayer.PlayerCurrentHealth;
                RTL.Text = targetPlayer.PlayerData.name + " takes " + effect.EffectValue + " damage from " + actorPlayer.PlayerData.name + "'s " + Actions[0].ActionName + "!";
                break;
            case "Heal":
                actorPlayer.PlayerCurrentHealth += effect.EffectValue;
                actorPlayer.GetNode<ProgressBar>("PlayerHealth").Value = actorPlayer.PlayerCurrentHealth;
                RTL.Text = actorPlayer.PlayerData.name + " heals for " + effect.EffectValue + " from " + actorPlayer.PlayerData.name + "'s " + Actions[0].ActionName + "!";
                break;
            case "Buff": case "Heal Over Time":
                Effect duplicateEffect = effect.Duplicate() as Effect;
                actorPlayer.PlayerEffects.Add(duplicateEffect);
                break;
            case "Debuff": 
                targetPlayer.PlayerEffects.Add(effect.Duplicate() as Effect);
                break;
            default:
                GD.Print("Unknown effect type: " + effect.EffectType);
                break;
        }
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Input.IsActionJustPressed("ui_accept"))
        {
            if (Actions[0].Effects.Count > 0)
            {
                ApplyEffect();
            }
            else
            {
                Actions.RemoveAt(0);
                if(actorPlayer.PlayerEffects.Count > 0)
                {
                    StateChangedHandler("TurnEnd");
                }
                else
                {
                    PlayerContainer nextPlayer = NextCharacterHandler();
                    if(!nextPlayer.IsPlayerContolled)
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
            }
        }
        // Handle state updates
    }

    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}