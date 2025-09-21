using Godot;
using Godot.Collections;
using System.Collections.Generic;

[GlobalClass]
public partial class TBAction : Resource
{
	public PlayerContainer Target { get; set; }
	public PlayerContainer Actor { get; set; }
	[Export] public string ActionType { get; set; }
	[Export] public string ActionName { get; set; }
	[Export] public string ActionDescription { get; set; }
	public List<Effect> Effects { get; set; } = new List<Effect>();
	[Export] public Array EffectNames { get; set; } = new Array();

	public void Initialize()
	{
		// Initialize the action
		//GD.Print("TBAction initialized!");
		foreach (var effect in EffectNames)
		{
			Effect e = Registry.EffectData[(string)effect];
			Effects.Add(e);
		}
		ActionDescription = ActionDescription.Replace("<character>", Actor.PlayerData.name);
		ActionDescription = ActionDescription.Replace("<selves>", Actor.PlayerData.selves);
		ActionDescription = ActionDescription.Replace("<they>", Actor.PlayerData.they);
		ActionDescription = ActionDescription.Replace("<them>", Actor.PlayerData.them);
		ActionDescription = ActionDescription.Replace("<theirs>", Actor.PlayerData.theirs);
		ActionDescription = ActionDescription.Replace("<their>", Actor.PlayerData.their);
	}
}
