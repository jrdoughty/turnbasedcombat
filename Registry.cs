using Godot;
using Godot.Collections;

public static class Registry
{
    //public static Dictionary<string, object> PlayerData = new Dictionary<string, object>();
    //public static Dictionary<string, object> GameData = new Dictionary<string, object>();
    public static Dictionary<string, TBAction> ActionData = new Dictionary<string, TBAction>(){
        { "Dagger", new TBAction() { ActionType = "Attack", ActionName = "Dagger", ActionDescription = "They swing their dagger at the enemy.", EffectNames = new Array() { "Dagger" } } },
        { "Axe", new TBAction() { ActionType = "Attack", ActionName = "Axe", ActionDescription = "They swing their axe at the enemy.", EffectNames = new Array() { "Axe" } } },
        { "Heal", new TBAction() { ActionType = "Heal", ActionName = "Heal", ActionDescription = "They heal themselves.", EffectNames = new Array() { "Heal" } } },
        { "Regen", new TBAction() { ActionType = "Spell", ActionName = "Regeneration", ActionDescription = "They begin to regenerate.", EffectNames = new Array() { "Regeneration" } } },
        { "Burn", new TBAction() { ActionType = "Burn", ActionName = "Burn", ActionDescription = "They burn the enemy.", EffectNames = new Array() { "Burn" } } },
        { "Freeze", new TBAction() { ActionType = "Freeze", ActionName = "Freeze", ActionDescription = "They freeze the enemy.", EffectNames = new Array() { "Freeze" } } }
    };
    public static Dictionary<string, Effect> EffectData = new Dictionary<string, Effect>(){
        { "Dagger", new Effect() { EffectType = "Damage", EffectName = "Damage", EffectValue = 2 } },
        { "Axe", new Effect() { EffectType = "Damage", EffectName = "Damage", EffectValue = 2 } },
        { "Heal", new Effect() { EffectType = "Heal", EffectName = "Heal", EffectValue = 2 } },
        { "Regeneration", new Effect() { EffectType = "Heal Over Time", EffectName = "Regeneration", EffectValue = 1 } },
        { "Burn", new Effect() { EffectType = "Burn", EffectName = "Burn", EffectValue = 1 } },
        { "Freeze", new Effect() { EffectType = "Freeze", EffectName = "Freeze", EffectValue = 1 } }
    };
    //public static Dictionary<string, object> GameStateData = new Dictionary<string, object>();
    //public static Dictionary<string, object> UIData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AnimationData = new Dictionary<string, object>();
    //public static Dictionary<string, object> AudioData = new Dictionary<string, object>();
}