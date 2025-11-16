using Godot;
using System;

public partial class DecisionState : State
{

    public override void EnterState()
    {

        base.EnterState();
        //GD.Print("Enemy has " + Players[1].PlayerEffects.Count + " effects.");
    }
    public override void UpdateState()
    {
        float random = GD.Randf();
        Players[1].PerformAction(Players[1].CharacterData.PlayerActions[(int)Math.Floor(random * Players[1].CharacterData.PlayerActions.Count)].ToString().Trim('"'));
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}