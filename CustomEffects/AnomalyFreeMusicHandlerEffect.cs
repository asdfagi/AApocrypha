using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class AnomalyFreeMusicHandlerEffect : EffectSO
    {
        public static int Amount = 0;
        public static void Reset() => Amount = 0;
        public bool Add = true;
        public bool ResetEffect = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            bool GOING = Amount > 0;
            if (ResetEffect)
            {
                Amount = 0;
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("AnomalyFree", 0);
                Debug.Log("AnomalyFree set to 0 because a reset command was issued");
                return true;
            }
            if (Add) Amount++;
            else Amount--;
            if ((Amount > 0) == GOING) return Amount > 0;
            if (Amount > 0)
            {
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("AnomalyFree", 1);
                Debug.Log($"AnomalyFree set to 1 because Amount = {Amount}");
            }
            else
            {
                CombatManager.Instance._stats.audioController.MusicCombatEvent.setParameterByName("AnomalyFree", 0);
                Debug.Log($"AnomalyFree set to 0 because Amount = {Amount}");
            }
            return Amount > 0;
        }
    }
}
