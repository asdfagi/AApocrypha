using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class DogmaMusicHandlerEffect : EffectSO
    {
        public static int Amount = 0;
        public static void Reset() => Amount = 0;
        public bool Add = true;
        public bool ResetEffect = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (ResetEffect)
            {
                Amount = 0;
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("Dogma", 0);
                //Debug.Log("Dogma set to 0 because a reset command was issued");
                return true;
            }
            if (Add) Amount++;
            else Amount--;
            Debug.Log("Dogma | Amount: " + Amount);
            if (Amount > 0 && Amount <= 4)
            {
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("Dogma", Amount);
                exitAmount = Amount;
                //Debug.Log($"Dogma set to {Amount} because Amount = {Amount}");
            }
            else if (Amount > 4)
            {
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("Dogma", 4);
                exitAmount = 4;
                //Debug.Log($"Dogma set to 4 because Amount = {Amount}");
            }
            else
            {
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("Dogma", 0);
                exitAmount = 0;
                //Debug.Log($"Dogma set to 0 because Amount = {Amount}");
            }
            return Amount > 0;
        }
    }
}
