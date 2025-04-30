using Godot;
using System;
using System.Collections.Generic;

public partial class Team : Node2D
{
    public List<Player> Players { get; set; }
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
}