using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CheckHasUnitThresholdEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (targets[i].HasUnit)
                {
                    exitAmount++;
                }
            }
            if (exitAmount >= entryVariable) { Debug.Log($"Comparator | success! {exitAmount} is greater than {entryVariable}"); }
            else { Debug.Log($"Comparator | failure... {exitAmount} is not greater than {entryVariable}"); }
            return exitAmount >= entryVariable;
        }
    }
}
