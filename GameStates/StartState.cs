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
            PlayerContainer nextPlayer = NextCharacterHandler();
            if(!nextPlayer.IsPlayerContolled)
            {
                GD.Print("End of turn!");
                StateChangedHandler("Decision");
            }
            else
            {
                GD.Print("Your turn!");
                StateChangedHandler("Menu");
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
    }
}