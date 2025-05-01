using Godot;
using System;
using System.Collections.Generic;

public partial class Effect : Resource
{
    public string Target { get; set; }
    public Guid TargetID { get; set; }
    public string Actor { get; set; }
    public Guid ActorID { get; set; }
    public string EffectType { get; set; }
    public int EffectValue { get; set; }
    public string EffectName { get; set; }
}