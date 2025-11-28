using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterInEdgesCheckEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (caster.SlotID == 0 || caster.SlotID == 4)
            {
                exitAmount++;
            }
            return exitAmount > 0;
        }
    }
}
