using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class PercentageChanceForNextEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (UnityEngine.Random.Range(0, 100) < entryVariable) { exitAmount = 1; }
            else { exitAmount = 0; }
            return exitAmount > 0;
        }
    }
}
