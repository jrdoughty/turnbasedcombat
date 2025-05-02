using Godot;
using System;
using System.Collections.Generic;

public partial class TBAction : Node
{
    public PlayerContainer Target { get; set; }
    public PlayerContainer Actor { get; set; }
    public string ActionType { get; set; }
    public string ActionName { get; set; }
    public string ActionDescription { get; set; }
    public List<Effect> Effects { get; set; } = new List<Effect>();
}