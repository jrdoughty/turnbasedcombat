using Godot;
using Godot.Collections;
namespace TwoDGame
{
    public partial class Game : Node
    {
        public override void _Ready()
        {
            GD.Print("Dialogic Initialized");
            var d = GetNode<Node>("/root/Dialogic");
            Callable dialogEvent = new Callable(this, "DialogEvent");
            d.Connect("signal_event", dialogEvent);
        }

        public void DialogEvent(string eventName)
        {
            GD.Print("Dialog Event Triggered: " + eventName);
            Node w = GetNode("World");
            RemoveChild(w);
            w.QueueFree();
            AddChild(GD.Load<PackedScene>("res://TurnBased/TurnBasedBattle.tscn").Instantiate());
            GetNode<Node>("Battle").TreeExiting += TurnBasedBattleFinish;
        }

        public void TurnBasedBattleFinish()
        {
            GD.Print("Turn Based Battle Finished");
            CallDeferred("AddOverworld");
        }
        public void AddOverworld()
        {
            AddChild(GD.Load<PackedScene>("res://Overworld/OverWorld.tscn").Instantiate());
        }
    }
}