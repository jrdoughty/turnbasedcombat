using Godot;
using System;

public partial class EffectState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        Effect effect = Actions[0].Effects[0];
        Actions[0].Effects.RemoveAt(0);
        PlayerContainer targetPlayer = Actions[0].Target;
        PlayerContainer actorPlayer = Actions[0].Actor;
        switch (effect.EffectType)
        {
            case "Damage":
                targetPlayer.PlayerCurrentHealth -= effect.EffectValue;
                targetPlayer.GetNode<ProgressBar>("PlayerHealth").Value = targetPlayer.PlayerCurrentHealth;
                RTL.Text = targetPlayer.PlayerData.name + " takes " + effect.EffectValue + " damage from " + Players[0].PlayerData.name + "'s " + Actions[0].ActionName + "!";
                break;
            case "Heal":
                actorPlayer.PlayerCurrentHealth += effect.EffectValue;
                actorPlayer.GetNode<ProgressBar>("PlayerHealth").Value = actorPlayer.PlayerCurrentHealth;
                break;
            case "Buff":
                Players[0].GetNode<Control>("PlayerControls").Visible = true;
                break;
            case "Debuff":
                Players[0].GetNode<Control>("PlayerControls").Visible = true;
                break;
            default:
                Players[0].GetNode<Control>("PlayerControls").Visible = false;
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
                StateChangedHandler("Effect");
            }
            else
            {
                Actions.RemoveAt(0);
                StateChangedHandler("Menu");
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