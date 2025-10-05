using Godot;
using System;

public partial class TurnEndState : State
{
    private TurnBasedCharacter Player;
    private bool firstTime = true;
    private int effectCount = 0;
    public override void EnterState()
    {
        base.EnterState();
        foreach (var player in Players)
        {
            if (player.IsActive)
            {
                Player = player;
                effectCount = 0;
                firstTime = true;
                return;
            }
        }
        GD.Print("No active player found, ending turn.");
        EndTurn();
    }

    public override void UpdateState()
    {
        base.UpdateState();
        if(Player.PlayerEffects.Count == 0)
        {
            EndTurn();
        }
        else if(Input.IsActionJustPressed("ui_accept") || firstTime)
        {
            firstTime = false;
            GD.Print("End Turn pressed");
            // Apply end turn effects
            ApplyEndTurnEffects();
        }
        // Handle state updates
    }
    private void ApplyEndTurnEffects()
    {
        GD.Print(Player.PlayerEffects.Count + " effects to process. We're on effect " + effectCount);
        if (Player.PlayerEffects.Count > effectCount)
        {
            Effect effect = Player.PlayerEffects[effectCount];
            GD.Print("Applying effect: " + effect.EffectName);
            switch (effect.EffectType)
            {
                case "Heal Over Time":
                    Player.CharacterData.CurrentHealth += effect.EffectValue;
                    Player.GetNode<ProgressBar>("PlayerHealth").Value = Player.CharacterData.CurrentHealth;
                    break;
                case "Damage Over Time":
                    Player.CharacterData.CurrentHealth -= effect.EffectValue;
                    Player.GetNode<ProgressBar>("PlayerHealth").Value = Player.CharacterData.CurrentHealth;
                    break;
                case "Buff":
                    Player.PlayerEffects.Add(effect);
                    break;
                case "Debuff":
                    Player.PlayerEffects.Add(effect);
                    break;
                default:
                    GD.Print("Unknown effect type: " + effect.EffectType);
                    break;
            }
            string str = Utils.ReplacePlayerStrings(effect.EffectEndTurnDescription, Player.CharacterData);
            str = Utils.ReplaceOtherPlayerStrings(str, Player.CharacterData);
            str = Utils.ReplaceDamageStrings(str, effect);
            RTL.Text = str;
            if(effect.EffectDuration > 0)
                effect.EffectDuration--;
            GD.Print("Effect duration: " + effect.EffectDuration);
            if (effect.EffectDuration == 0)
            {
                Player.PlayerEffects.Remove(effect);
                effectCount--;//compensate for the removal of the effect
            }
            Player.updateConditions();
            effectCount++;
        }
        else
        {
            EndTurn();
        }
    }

    private void EndTurn()
    {
        if(GetConditionsHandler() != null)
        {
            if(GetConditionsHandler().SessionComplete())
            {
                GD.Print("Victory!");
                StateChangedHandler("Victory");
                return;
            }
        }
        foreach (var player in Players)
        {
            player.IsActive = false;
        }
        TurnBasedCharacter next = NextCharacterHandler();
        next.IsActive = true;
        if(!next.IsPlayerContolled)
        {
            GD.Print("End of turn!");
            StateChangedHandler("Decision");
        }
        else
        {
            GD.Print("Your turn!");
            StateChangedHandler("Menu");
        }
    }


    public override void ExitState()
    {
        base.ExitState();
    }
}