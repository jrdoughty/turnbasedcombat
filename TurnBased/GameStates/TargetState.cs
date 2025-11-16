using Godot;
using System;
using System.Collections.Generic;

public partial class TargetState : State
{
    
    public override void EnterState()
    {
        base.EnterState();
        foreach(var p in Players)
        {
            p.Selected = false;
            p.Picking = true;
        }

        TurnBasedCharacter target = null;
        List<TurnBasedCharacter> validTargets = new List<TurnBasedCharacter>();
        
        if(Actions[0].ActionType == "Attack" || Actions[0].ActionType == "Spell")
        {
            foreach(var p in Players)
            {
                if(p.team != characterQueue.CurrentCharacter.team && p.CharacterData.CurrentHealth > 0)
                {
                    validTargets.Add(p);
                }
            }
        }
        else if(Actions[0].ActionType == "Heal")
        {
            foreach(var p in Players)
            {
                if(p.team == characterQueue.CurrentCharacter.team && p.CharacterData.CurrentHealth > 0)
                {
                    validTargets.Add(p);
                }
            }
        }
        if(validTargets.Count == 0)
        {
            if( !characterQueue.CurrentCharacter.IsPlayerContolled)
            {
                GD.Print("No valid targets found, ending turn. Finding new action...");
                StateChangedHandler("Decision");
                return;
            }
            else
            {
                RTL.Text = "No valid targets available for this action.";
                StateChangedHandler("Menu");
                return;
            }
        }
        if(!characterQueue.CurrentCharacter.IsPlayerContolled)
        {
            GD.Print("Auto selecting target...");
            if(Actions[0].ActionType == "Attack" || Actions[0].ActionType == "Spell")
            {
                if(validTargets.Count > 0)
                {
                    target = validTargets[(int)Math.Floor(GD.Randf() * validTargets.Count)];
                }
            }
            else if(Actions[0].ActionType == "Heal")
            {
                target = characterQueue.CurrentCharacter;
            }
            Actions[0].Target = target;
            StateChangedHandler("Casting");
        }
        else if(Actions[0].ActionType == "Attack")
        {
            RTL.Text = "Select a target to attack.";
        }
        else if(Actions[0].ActionType == "Heal")
        {
            RTL.Text = "Select a target to heal.";
        }
        else if(Actions[0].ActionType == "Spell")
        {
            RTL.Text = "Select a target for the spell.";
        }
        
    }

    public override void UpdateState()
    {
        base.UpdateState();
        // Handle state updates

        for(int i = 0; i < Players.Count; i++)
        {
            TurnBasedCharacter p = Players[i];
            if(p.Selected)
            {
                Actions[0].Target = p;
                StateChangedHandler("Casting");
            }
        }
    }
    public override void ExitState()
    {
        base.ExitState();
        foreach(var p in Players)
        {
            p.Selected = false;
            p.Picking = false;
        }
    }
}