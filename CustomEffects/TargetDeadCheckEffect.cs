using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetDeadCheckEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (!targetSlotInfo.HasUnit)
                {
                    continue;
                }

                if (targetSlotInfo.Unit.CurrentHealth <= 0)
                {
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }
    }
}
