using Godot;
using Godot.Collections;
using System;

//using MonoCustomResourceRegistry;
namespace TwoDGame
{
	//[RegisteredType(nameof(Item),"", nameof(Resource))]

	public partial class OverWorldData : Resource
    {    
        public Vector2 PlayerPosition { get; set; }
        public string PlayerAnimation { get; set; }
        public Vector2 PlayerDirection { get; set; }
        public Vector2 PlayerLastDirection { get; set; }
        public Dictionary<string, Vector2> NpcPositions { get; set; }
        public Dictionary<string, string> NpcAnimations { get; set; }
	}
}