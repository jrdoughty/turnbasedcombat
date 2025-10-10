using Godot;
using System;

public partial class CastState : State
{

    public override void EnterState()
    {
        base.EnterState();
        string ActionDescription = Utils.ReplacePlayerStrings(Actions[0].ActionDescription, Actions[0].Actor.CharacterData);
        ActionDescription = Utils.ReplaceOtherPlayerStrings(ActionDescription, Actions[0].Target.CharacterData);
        RTL.Text = ActionDescription;
        if(Actions[0].ActionType == "Attack")
        {
            Actions[0].Actor.CharacterSprite.Play("Attack");
        }
        else if(Actions[0].ActionType == "Heal")
        {
            Actions[0].Actor.CharacterSprite.Play("Cast");
        }
        else if(Actions[0].ActionType == "Spell")
        {
            Actions[0].Actor.CharacterSprite.Play("Cast");
        }
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