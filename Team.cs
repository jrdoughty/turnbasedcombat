using Godot;
using System;
using System.Collections.Generic;
using Godot.Collections;

public partial class Team : Node2D
{
    public List<TurnBasedCharacter> Players { get; set; } = new List<TurnBasedCharacter>();
    public string TeamName { get; set; }
    public int TeamId { get; set; }
    public int TeamScore { get; set; }
    public int TeamHealth { get; set; }
    public int TeamMana { get; set; }
    public int TeamMaxMana { get; set; }
    public bool IsActive { get; set; } = false;
    public bool IsDefeated { get; set; } = false;
    public bool IsTurnEnded { get; set; } = false;
    public bool IsTurnStarted { get; set; } = false;
    public bool IsTurnSkipped { get; set; } = false;
    public bool IsTurnPassed { get; set; } = false;
    public bool IsTurnFinished { get; set; } = false;

    public void AddPlayer(TurnBasedCharacter player)
    {
        Players.Add(player);
        player.team = this;
    }
    public void RemovePlayer(TurnBasedCharacter player)
    {
        Players.Remove(player);
    }

    public Array<Player> GetPartyData()
    {
        Array<Player> partyData = new Array<Player>();
        foreach (var player in Players)
        {
            partyData.Add(player.CharacterData);
        }
        return partyData;
    }

    public void CheckStatus()
    {
        foreach (var player in Players)
        {
            if (!player.IsDefeated)
            {
                return;
            }
        }
        IsDefeated = true;
    }
}