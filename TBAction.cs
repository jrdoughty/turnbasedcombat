using Godot;
using System;
using System.Collections.Generic;

public partial class TBAction : Node
{

    public string Target { get; set; }
    public Guid TargetID { get; set; }
    public string Actor { get; set; }
    public Guid ActorID { get; set; }
    public string ActionType { get; set; }
    public string ActionName { get; set; }
    public string ActionDescription { get; set; }
    public List<Effect> Effects { get; set; } = new List<Effect>();
}