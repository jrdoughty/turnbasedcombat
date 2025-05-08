using Godot;
using Godot.Collections;

public static class Registry
{
    //public static Dictionary<string, object> PlayerData = new Dictionary<string, object>();
    //public static Dictionary<string, object> GameData = new Dictionary<string, object>();
    public static Dictionary<string, TBAction> ActionData = new Dictionary<string, TBAction>(){
        { "Dagger", GD.Load<TBAction>("res://Actions/Dagger.tres")},
        { "Axe", new TBAction() { ActionType = "Attack", ActionName = "Axe", ActionDescription = "They swing their axe at the enemy.", EffectNames = new Array() { "Axe" } } },
        { "Heal", new TBAction() { ActionType = "Heal", ActionName = "Heal", ActionDescription = "They heal themselves.", EffectNames = new Array() { "Heal" } } },
        { "Regen", new TBAction() { ActionType = "Spell", ActionName = "Regeneration", ActionDescription = "They begin to regenerate.", EffectNames = new Array() { "Regeneration" } } },
        { "Burn", new TBAction() { ActionType = "Burn", ActionName = "Burn", ActionDescription = "They burn the enemy.", EffectNames = new Array() { "Burn" } } },
        { "Freeze", new TBAction() { ActionType = "Freeze", ActionName = "Freeze", ActionDescription = "They freeze the enemy.", EffectNames = new Array() { "Freeze" } } }
    };
    public static Dictionary<string, Effect> EffectData = new Dictionary<string, Effect>(){
        { "Dagger", GD.Load<Effect>("res://Effects/DaggerDamage.tres") },
        { "Axe", GD.Load<Effect>("res://Effects/AxeDamage.tres") },
        { "Heal", GD.Load<Effect>("res://Effects/Heal.tres") },
        { "Regeneration", GD.Load<Effect>("res://Effects/Regeneration.tres") }
    };
    //public static Dictionary<string, object> GameStateData = new Dictionary<string, object>();
    //public static Dictionary<string, object> UIData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AnimationData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AudioData = new Dictionary<string, object>();
}