using Godot;
using Godot.Collections;
namespace TwoDGame
{
    public partial class Game : Node
    {
        public override void _Ready()
        {
            var d = GetNode<Node>("/root/Dialogic");
            Callable dialogEvent = new Callable(this, "DialogEvent");
            d.Connect("signal_event", dialogEvent);
            Registry.CurrentOverworldData = new OverWorldData();
        }

        public void DialogEvent(string eventName)
        {
            OverWorld w = GetNode<OverWorld>("World");
            createOverWorldData();
            RemoveChild(w);
            w.SafeFree();
            AddChild(GD.Load<PackedScene>("res://TurnBased/TurnBasedBattle.tscn").Instantiate());
            GetNode<Node>("Battle").TreeExiting += TurnBasedBattleFinish;
        }

        public void TurnBasedBattleFinish()
        {
            CallDeferred("AddOverworld");
        }
        public void AddOverworld()
        {
            OverWorld w = GD.Load<PackedScene>("res://Overworld/OverWorld.tscn").Instantiate() as OverWorld;
            AddChild(w);
            w.loadOverWorldData(Registry.CurrentOverworldData);
        }

        public void createOverWorldData()
        {
            OverWorld w = GetNode<OverWorld>("World");
            Registry.CurrentOverworldData.PlayerPosition = w.GetPlayerPosition();
            Registry.CurrentOverworldData.PlayerAnimation = w.GetPlayerAnimation();
            Registry.CurrentOverworldData.NpcPositions = w.GetNpcPositions();
            Registry.CurrentOverworldData.NpcAnimations = w.GetNpcAnimations();
            Registry.CurrentOverworldData.PlayerDirection = ((OverworldPlayer)w.GetNode("Player")).direction;
            Registry.CurrentOverworldData.PlayerLastDirection = ((OverworldPlayer)w.GetNode("Player")).lastDirection;
        }
    }
}