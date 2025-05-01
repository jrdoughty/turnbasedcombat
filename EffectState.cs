using Godot;
using System;

public partial class EffectState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        switch (Actions[0].Effects[0].EffectType)
        {
            case "Damage":
                Players[1].PlayerCurrentHealth -= Actions[0].Effects[0].EffectValue;
                Players[1].GetNode<ProgressBar>("PlayerHealth").Value = Players[1].PlayerCurrentHealth;
                RTL.Text = Players[1].PlayerData.name + " takes " + Actions[0].Effects[0].EffectValue + " damage from " + Players[0].PlayerData.name + "'s " + Actions[0].ActionName + "!";
                break;
            case "Heal":
                Players[0].GetNode<Control>("PlayerControls").Visible = true;
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
        //RTL.Text = Actions[0].Effects[0].EffectName;
        // Handle entering the state
    }
    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}