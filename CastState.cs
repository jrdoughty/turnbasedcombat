using Godot;
using System;

public partial class CastState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        RTL.Text = Actions[0].ActionDescription;
        // Handle entering the state
    }
    public override void UpdateState()
    {
        base.UpdateState();
        if(Input.IsActionJustPressed("ui_accept") || Input.IsMouseButtonPressed(MouseButton.Left))
        {
            if (Actions[0].Effects.Count > 0)
            {
                StateChangedHandler("Effect");
            }
            //else
            //{
            //    GameState.ChangeState("Decision");
            //}
        }
        // Handle state updates
    }
    public override void ExitState()
    {
        base.ExitState();
        // Handle exiting the state
    }
}