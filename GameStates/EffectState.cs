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
                int modifiedDamage = actorPlayer.PlayerData.attack;
                modifiedDamage -= targetPlayer.PlayerData.defense;
                GD.Print("Base Damage: " + effect.EffectValue);
                GD.Print("Modified Damage: " + modifiedDamage);
                GD.Print("Target Defense: " + targetPlayer.PlayerData.defense);
                GD.Print("Actor Attack: " + actorPlayer.PlayerData.attack);
                effect.EffectModifier = modifiedDamage;
                targetPlayer.PlayerData.currentHealth -= effect.EffectValue + effect.EffectModifier;
                targetPlayer.GetNode<ProgressBar>("PlayerHealth").Value = targetPlayer.PlayerData.currentHealth;
                break;
            case "Heal":
                actorPlayer.PlayerData.currentHealth += effect.EffectValue;
                actorPlayer.GetNode<ProgressBar>("PlayerHealth").Value = actorPlayer.PlayerData.currentHealth;
                break;
            case "Heal Over Time":
                if(actorPlayer.PlayerEffects.Count > 0)
                {
                    foreach(var e in actorPlayer.PlayerEffects)
                    {
                        if(e.EffectName == effect.EffectName)
                        {
                            if (e.EffectDuration == -1)
                                RTL.Text = actorPlayer.PlayerData.name + " is already affected by " + effect.EffectName + "!";
                            else
                            {
                                e.EffectDuration = effect.EffectDuration;
                                RTL.Text = actorPlayer.PlayerData.name + "'s " + effect.EffectName + " duration has been renewed!";
                            }
                            actorPlayer.updateConditions();
                            return;
                        }
                    }
                }
                actorPlayer.PlayerEffects.Add(effect.Duplicate() as Effect);
                break;
            case "Damage Over Time":
                if(targetPlayer.PlayerEffects.Count > 0)
                {
                    foreach(var e in targetPlayer.PlayerEffects)
                    {
                        if(e.EffectName == effect.EffectName)
                        {
                            if (e.EffectDuration == -1)
                                RTL.Text = targetPlayer.PlayerData.name + " is already affected by " + effect.EffectName + "!";
                            else
                            {
                                e.EffectDuration = effect.EffectDuration;
                                RTL.Text = targetPlayer.PlayerData.name + "'s " + effect.EffectName + " duration has been refreshed!";
                            }
                            targetPlayer.updateConditions();
                            return;
                        }
                    }
                }
                targetPlayer.PlayerEffects.Add(effect.Duplicate() as Effect);
                break;
            case "Debuff": 
                targetPlayer.PlayerEffects.Add(effect.Duplicate() as Effect);
                break;
            default:
                GD.Print("Unknown effect type: " + effect.EffectType);
                break;
        }
        string effectText = Utils.ReplacePlayerStrings(effect.EffectCastDescription, actorPlayer.PlayerData);
        effectText = Utils.ReplaceOtherPlayerStrings(effectText, targetPlayer.PlayerData);
        effectText = Utils.ReplaceDamageStrings(effectText, effect);
        RTL.Text = effectText;
        targetPlayer.updateConditions();
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
                StateChangedHandler("TurnEnd");
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