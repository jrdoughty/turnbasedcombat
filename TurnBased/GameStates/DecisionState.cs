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
        characterQueue.CurrentCharacter.PerformAction(characterQueue.CurrentCharacter.CharacterData.PlayerActions[(int)Math.Floor(random * characterQueue.CurrentCharacter.CharacterData.PlayerActions.Count)].ToString().Trim('"'));
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}