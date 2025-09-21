using Godot;

[GlobalClass]
public partial class Effect : Resource
{
    public PlayerContainer Target { get; set; }
    public PlayerContainer Actor { get; set; }
    [Export] public string EffectType { get; set; }
    [Export] public int EffectValue { get; set; }
    [Export] public string EffectName { get; set; }
    [Export] public int EffectDuration { get; set; }
    [Export] public string EffectCastDescription { get; set; }
    [Export] public string EffectEndTurnDescription { get; set; }
}