using Godot;
using System;
using System.Collections.Generic;

public partial class GameStates : Node2D
{
    public State GameState { get; set; }
    public Dictionary<String,State> States = new Dictionary<string, State>{ { "Menu", new MenuState() }, { "Start", new StartState() }, { "Casting", new CastState() }, { "Effect", new EffectState() }, { "Decision", new DecisionState() } , { "TurnEnd", new TurnEndState() }, {"Victory", new VictoryState() }, { "Defeat", new DefeatState() } };

    public override void _Ready()
    {
        setStateChange();
        GameState = States["Start"];
        GameState.EnterState();
        GD.Print("Game state initialized to: Start");
    }

    public void ChangeState(string newState)
    {
        if (States.ContainsKey(newState))
        {
            if (GameState != null)
            {
                GameState.ExitState();
            }
            GameState = States[newState];
            GameState.EnterState();
            GD.Print($"Game state changed to: {newState}");
        }
        else
        {
            GD.Print($"Invalid state: {newState}");
        }
    }
    public void SetTeams(List<Team> teams)
    {
        foreach (var state in States.Values)
        {
            state.SetTeams(teams);
        }
    }
    public void SetContainers(List<PlayerContainer> containers)
    {
        foreach (var state in States.Values)
        {
            state.SetContainers(containers);
        }
    }
    public void SetTextBox(RichTextLabel rtl)
    {
        foreach (var state in States.Values)
        {
            state.SetTextBox(rtl);
        }
    }

    public void SetActions(List<TBAction> actions)
    {
        foreach (var state in States.Values)
        {
            state.Actions = actions;
        }
    }
    public void setStateChange()
    {
        foreach (State state in States.Values)
        {
            state.StateChangedHandler = ChangeState;
        }
    }
    public void setNextCharacter(Func<PlayerContainer> nextCharacterHandler)
    {
        foreach (State state in States.Values)
        {
            state.NextCharacterHandler = nextCharacterHandler;
        }
    }
    public void setGetBattleConditions(Func<BattleConditions> getBCHandler)
    {
        foreach (State state in States.Values)
        {
            state.GetConditionsHandler = getBCHandler;
        }
    }
}