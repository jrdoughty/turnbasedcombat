using Godot;
using Godot.Collections;
namespace TwoDGame
{
    public partial class OverworldNPC : CharacterBody2D, IInteractable
    {
        [Export]
        public string NpcName { get; set; } = "NPC";        //[Export]
       // public Array<string> dialogueLines = new Array<string>{};
        private AnimatedSprite2D characterSprite;

        private const string LEFT = "left";
        private const string RIGHT = "right";
        private const string UP = "up";
        private const string DOWN = "down";
        public override void _Ready()
        {
            characterSprite = (AnimatedSprite2D)GetNode("AnimatedSprite2D");
			AddToGroup("interactable");
        }
        
		public void interact(OverworldPlayer initiater)
        {
		    var d = GetNode<Node>("/root/Dialogic");
		    d.Call("start", "res://Dialog/barbarian.dtl");
        }

		private void _on_area_2d_area_entered(Area2D area)
		{
			// Replace with function body.
			if(area.GetParent() != null && area.GetParent() is Player)
			{
				OverworldPlayer p = (OverworldPlayer)area.GetParent();
				p.interactables.Add(this);
				//QueueFree();
			}
		}


		private void _on_area_2d_area_exited(Area2D area)
		{
			// Replace with function body.
			if(area.GetParent() != null && area.GetParent() is Player)
			{
				OverworldPlayer p = (OverworldPlayer)area.GetParent();
				p.interactables.Remove(this);
				//GD.Print(p.items.Count);
				//QueueFree();
			}
		}
    }
}