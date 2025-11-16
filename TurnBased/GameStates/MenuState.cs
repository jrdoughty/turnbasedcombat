using Godot;
using System;

public partial class MenuState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        characterQueue.CurrentCharacter.GetNode<Control>("PlayerControls").Visible = true;
        RTL.Visible = false;
        // Handle entering the state
    }
    public override void ExitState()
    {
        base.ExitState();
        characterQueue.CurrentCharacter.GetNode<Control>("PlayerControls").Visible = false;
        RTL.Visible = true;
        // Handle exiting the state
    }
}