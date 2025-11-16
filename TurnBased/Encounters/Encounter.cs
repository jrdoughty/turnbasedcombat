using Godot;
using System;

[GlobalClass]
public partial class Encounter : Resource
{
    [Export] public string EncounterName { get; set; }
    [Export] public Godot.Collections.Array<CharacterData> EnemyScenes { get; set; } = new Godot.Collections.Array<CharacterData>();
}
