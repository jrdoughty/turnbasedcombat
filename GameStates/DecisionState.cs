using Godot;
using System;

public partial class DecisionState : State
{

    public override void EnterState()
    {

        base.EnterState();
        GD.Print("Enemy has " + Players[1].PlayerEffects.Count + " effects.");
    }
    public override void UpdateState()
    {
        float random = GD.Randf();
        if (random < 0.5f)
        {
            Players[1].PerformAction(Players[1].CharacterData.PlayerActions[0].ToString().Trim('"'));
        }
        else
        {
            Players[1].PerformAction(Players[1].CharacterData.PlayerActions[1].ToString().Trim('"'));
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}