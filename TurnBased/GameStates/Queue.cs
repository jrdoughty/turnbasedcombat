using Godot;
using System;
using System.Collections.Generic;

public partial class Queue : Node
{
	public Action<string> StateChangedHandler;
	public Action<TurnBasedCharacter> ActiveCharacterHandler;
	public List<TurnBasedCharacter> CharacterQueue { get; set; } = new List<TurnBasedCharacter>();

	public TurnBasedCharacter CurrentCharacter { get; set; } = null;
	private bool speedCheck = false;
	[Export] public int QueueVal { get; set; } = 10;
	public void Initialize(List<TurnBasedCharacter> characterQueue)
	{
		CharacterQueue = characterQueue;
		foreach (var character in CharacterQueue)
		{
			character.QueueVal = 0;
		}
	}

	public TurnBasedCharacter GetNextCharacter()
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
					character.QueueVal += character.CharacterData.Speed;
				}
				CheckIfCharacterReady();
			}
		}
		CurrentCharacter.QueueVal = CurrentCharacter.QueueVal % QueueVal;
		return CurrentCharacter;
	}
	

	public void CheckIfCharacterReady()
	{
		foreach (var character in CharacterQueue)
		{
			if(character.CharacterData.Speed > 0)
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
