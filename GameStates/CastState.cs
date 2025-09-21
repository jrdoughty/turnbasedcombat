using Godot;
using System;

public partial class CastState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        string ActionDescription = Utils.ReplacePlayerStrings(Actions[0].ActionDescription, Actions[0].Actor.PlayerData);
        RTL.Text = ActionDescription;
        // Handle entering the state
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
                StateChangedHandler("TurnEnd");
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