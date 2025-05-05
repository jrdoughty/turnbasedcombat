using Godot;
using System;

public partial class DecisionState : State
{
    
    public override void EnterState()
    {

        base.EnterState();
    }
    public override void UpdateState()
    {
        float random = GD.Randf();
        if (random < 0.5f)
        {
            Players[1].Spell1();
        }
        else
        {
            Players[1].Spell2();
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}