using System;
using Godot;
using Godot.Collections;

public static class Registry
{
    //public static Dictionary<string, object> PlayerData = new Dictionary<string, object>();
    //public static Dictionary<string, object> GameData = new Dictionary<string, object>();
    public static Dictionary<string, TBAction> ActionData = new Dictionary<string, TBAction>(){
        { "Dagger", GD.Load<TBAction>("res://Actions/Dagger.tres")},
        { "Axe", GD.Load<TBAction>("res://Actions/Axe.tres")},
        { "Heal", GD.Load<TBAction>("res://Actions/Heal.tres")},
        { "Regeneration", GD.Load<TBAction>("res://Actions/Regeneration.tres") },
        { "Poison", GD.Load<TBAction>("res://Actions/Poison.tres")}
    };
    public static Dictionary<string, Effect> EffectData = new Dictionary<string, Effect>(){
        { "Dagger", GD.Load<Effect>("res://Effects/DaggerDamage.tres") },
        { "Axe", GD.Load<Effect>("res://Effects/AxeDamage.tres") },
        { "Heal", GD.Load<Effect>("res://Effects/Heal.tres") },
        { "Poison", GD.Load<Effect>("res://Effects/Poison.tres") },
        { "Regeneration", GD.Load<Effect>("res://Effects/Regeneration.tres") }
    };
    //public static Dictionary<string, object> GameStateData = new Dictionary<string, object>();
    //public static Dictionary<string, object> UIData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AnimationData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AudioData = new Dictionary<string, object>();


}