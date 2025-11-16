using Godot;
using System;

public partial class EffectState : State
{
    
    TurnBasedCharacter targetPlayer;
    TurnBasedCharacter actorPlayer;
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
        //GD.Print("Actor: " + actorPlayer.CharacterData.CharacterName + ", Target: " + targetPlayer.CharacterData.CharacterName);
        Effect effect = Actions[0].Effects[0];
        Actions[0].Effects.RemoveAt(0);
        switch (effect.EffectType)
        {
            case "Damage":
                effect.EffectModifier = actorPlayer.CharacterData.Attack - targetPlayer.CharacterData.Defense;
                if(effect.EffectValue + effect.EffectModifier < 0)
                {
                    effect.EffectModifier = -effect.EffectValue;// Prevent healing from damage
                }
                targetPlayer.CharacterData.CurrentHealth -= effect.EffectValue + effect.EffectModifier;
                targetPlayer.GetNode<ProgressBar>("PlayerHealth").Value = targetPlayer.CharacterData.CurrentHealth;
                break;
            case "Heal":
                targetPlayer.CharacterData.CurrentHealth += effect.EffectValue;
                targetPlayer.GetNode<ProgressBar>("PlayerHealth").Value = targetPlayer.CharacterData.CurrentHealth;
                break;
            case "Heal Over Time":
                if(targetPlayer.PlayerEffects.Count > 0)
                {
                    foreach(var e in targetPlayer.PlayerEffects)
                    {
                        if(e.EffectName == effect.EffectName)
                        {
                            if (e.EffectDuration == -1)
                                RTL.Text = targetPlayer.CharacterData.CharacterName + " is already affected by " + effect.EffectName + "!";
                            else
                            {
                                e.EffectDuration = effect.EffectDuration;
                                RTL.Text = targetPlayer.CharacterData.CharacterName + "'s " + effect.EffectName + " duration has been renewed!";
                            }
                            targetPlayer.updateConditions();
                            return;
                        }
                    }
                }
                targetPlayer.PlayerEffects.Add(effect.Duplicate() as Effect);
                break;
            case "Damage Over Time":
                if(targetPlayer.PlayerEffects.Count > 0)
                {
                    foreach(var e in targetPlayer.PlayerEffects)
                    {
                        if(e.EffectName == effect.EffectName)
                        {
                            if (e.EffectDuration == -1)
                                RTL.Text = targetPlayer.CharacterData.CharacterName + " is already affected by " + effect.EffectName + "!";
                            else
                            {
                                e.EffectDuration = effect.EffectDuration;
                                RTL.Text = targetPlayer.CharacterData.CharacterName + "'s " + effect.EffectName + " duration has been refreshed!";
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
                //GD.Print("Unknown effect type: " + effect.EffectType);
                break;
        }
        string effectText = Utils.ReplacePlayerStrings(effect.EffectCastDescription, actorPlayer.CharacterData);
        effectText = Utils.ReplaceOtherPlayerStrings(effectText, targetPlayer.CharacterData);
        effectText = Utils.ReplaceDamageStrings(effectText, effect);
        RTL.Text += "\n"+effectText;
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