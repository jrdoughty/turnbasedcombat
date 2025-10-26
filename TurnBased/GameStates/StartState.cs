using Godot;
using System;

public partial class StartState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        RTL.Text = "Battle is joined!";
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if (Input.IsActionJustPressed("ui_accept"))
        {
            TurnBasedCharacter nextPlayer = NextCharacterHandler();
            if(!nextPlayer.IsPlayerContolled)
            {
                StateChangedHandler("Decision");
            }
            else
            {
                StateChangedHandler("Menu");
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}