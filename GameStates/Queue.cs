using Godot;
using System;
using System.Collections.Generic;

public partial class Queue : Node
{
    public Action<string> StateChangedHandler;
    public Action<PlayerContainer> ActiveCharacterHandler;
    public List<PlayerContainer> CharacterQueue { get; set; } = new List<PlayerContainer>();

    public PlayerContainer CurrentCharacter { get; set; } = null;
    private bool speedCheck = false;
    [Export] public int QueueVal { get; set; } = 10;
    public void Initialize(List<PlayerContainer> characterQueue)
    {
        CharacterQueue = characterQueue;
        foreach (var character in CharacterQueue)
        {
            character.QueueVal = 0;
        }
    }

    public PlayerContainer GetNextCharacter()
    {
        CurrentCharacter = null;
        speedCheck = false;
        CheckIfCharacterReady();
        if(CurrentCharacter == null)
        {
            while (CurrentCharacter == null && speedCheck)
            {
                foreach (var character in CharacterQueue)
                {
                    character.QueueVal += character.PlayerData.speed;
                }
                CheckIfCharacterReady();
            }
        }
        GD.Print($"Current Character: {CurrentCharacter.PlayerData.name}");
        GD.Print($"Current Character QueueVal: {CurrentCharacter.QueueVal}");
        CurrentCharacter.QueueVal = CurrentCharacter.QueueVal % QueueVal;
        return CurrentCharacter;
    }
    

    public void CheckIfCharacterReady()
    {
        foreach (var character in CharacterQueue)
        {
            if(character.PlayerData.speed > 0)
            {
                speedCheck = true;
            }
            if (character.QueueVal > QueueVal)
            {
                if (CurrentCharacter == null || character.QueueVal > CurrentCharacter.QueueVal)
                {
                    CurrentCharacter = character;
                }
            }
        }
    }
}