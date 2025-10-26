using Godot;
using Godot.Collections;
using System;
using TwoDGame;

public partial class OverWorld : Node2D
{

    public override void _Ready()
    {

    }

    public void loadOverWorldData(OverWorldData data)
    {
        var player = GetNode<OverworldPlayer>("Player");
        player.GlobalPosition = data.PlayerPosition;
        var animationPlayer = GetNode<AnimationPlayer>("Player/CharAnimPlayer");
        player.direction = data.PlayerDirection;
        player.lastDirection = data.PlayerLastDirection;
        animationPlayer.Play(data.PlayerAnimation);
        var npcs = GetTree().GetNodesInGroup("NPC");
        foreach (OverworldNPC npc in npcs)
        {
            if (data.NpcPositions.ContainsKey(npc.Name))
            {
                npc.GlobalPosition = data.NpcPositions[npc.Name];
            }
            if (data.NpcAnimations.ContainsKey(npc.Name))
            {
                var charSprite = npc.GetNode<AnimatedSprite2D>("AnimatedSprite2D");
                charSprite.Play(data.NpcAnimations[npc.Name]);
            }
        }
    }

    public Vector2 GetPlayerPosition()
    {
        var player = GetNode<Node2D>("Player");
        return player.Position;
    }

    public string GetPlayerAnimation()
    {
        var animationPlayer = GetNode<AnimationPlayer>("Player/CharAnimPlayer");
        return animationPlayer.CurrentAnimation;
    }

    public Dictionary<string, Vector2> GetNpcPositions()
    {
        Dictionary<string, Vector2> npcPositions = new Dictionary<string, Vector2>();
        var npcs = GetTree().GetNodesInGroup("NPC");
        foreach (OverworldNPC npc in npcs)
        {
            npcPositions[npc.Name] = npc.Position;
        }
        return npcPositions;
    }

    public Dictionary<string, String> GetNpcAnimations()
    {
        Dictionary<string, String> npcAnimations = new Dictionary<string, String>();
        var npcs = GetTree().GetNodesInGroup("NPC");
        foreach (OverworldNPC npc in npcs)
        {
            npcAnimations[npc.Name] = npc.GetNode<AnimatedSprite2D>("AnimatedSprite2D").Animation;
        }
        return npcAnimations;
    }

    public void SafeFree()
    {// Clean up dynamically added nodes or resources
        foreach (Node child in GetChildren())
        {
            if (IsInstanceValid(child))
            {
                child.QueueFree();
            }
        }
        QueueFree();
    }
    
}
