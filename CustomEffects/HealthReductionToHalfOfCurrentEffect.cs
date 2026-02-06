using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class HealthReductionToHalfOfCurrentEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    if (targetSlotInfo.Unit.CurrentHealth > 1) { exitAmount += targetSlotInfo.Unit.ReduceHealthTo(Mathf.Max(1, (int)Mathf.Round(targetSlotInfo.Unit.CurrentHealth / 2))); }
                }
            }

            return exitAmount > 0;
        }
    }
}
