using Godot;
using System;

public partial class EffectState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        RTL.Text = Actions[0].Effects[0].EffectName;
        // Handle entering the state
    }
    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}