using System;
using Godot;
using Godot.Collections;
using TwoDGame;

public static class Registry
{
    //public static Dictionary<string, object> PlayerData = new Dictionary<string, object>();
    //public static Dictionary<string, object> GameData = new Dictionary<string, object>();
    public static Dictionary<string, TBAction> ActionData = new Dictionary<string, TBAction>(){
        { "Dagger", GD.Load<TBAction>("res://TurnBased/Actions/Dagger.tres")},
        { "Axe", GD.Load<TBAction>("res://TurnBased/Actions/Axe.tres")},
        { "Heal", GD.Load<TBAction>("res://TurnBased/Actions/Heal.tres")},
        { "Regeneration", GD.Load<TBAction>("res://TurnBased/Actions/Regeneration.tres") },
        { "Poison", GD.Load<TBAction>("res://TurnBased/Actions/Poison.tres")}
    };
    public static Dictionary<string, Effect> EffectData = new Dictionary<string, Effect>(){
        { "Dagger", GD.Load<Effect>("res://TurnBased/Effects/DaggerDamage.tres") },
        { "Axe", GD.Load<Effect>("res://TurnBased/Effects/AxeDamage.tres") },
        { "Heal", GD.Load<Effect>("res://TurnBased/Effects/Heal.tres") },
        { "Poison", GD.Load<Effect>("res://TurnBased/Effects/Poison.tres") },
        { "Regeneration", GD.Load<Effect>("res://TurnBased/Effects/Regeneration.tres") }
    };
    public static OverWorldData CurrentOverworldData = new OverWorldData();
    //public static Dictionary<string, object> GameStateData = new Dictionary<string, object>();
    //public static Dictionary<string, object> UIData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AnimationData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AudioData = new Dictionary<string, object>();
    
}