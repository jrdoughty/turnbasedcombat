using Godot;
using System;


namespace TwoDGame
{
	public partial class WorldItem : Node2D, IInteractable
	{
		

		// Declare the item variable as an export so it can be edited from the editor
		[Export] public Item item;

		public override void _Ready()
		{
			// Print the item's name when the scene is ready
			AddToGroup("interactable");
			if (item != null)
			{
				////GD.Print("Item name: " + item.name);
			}
			else
			{
				//GD.Print("Item not set");
			}
		}

		// Called every frame. 'delta' is the elapsed time since the previous frame.
		public override void _Process(double delta)
		{
		}

		public void interact(OverworldPlayer initiater)
		{
			initiater.AddItem(item);
			QueueFree();
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
				////GD.Print(p.items.Count);
				//QueueFree();
			}
		}
	}
}