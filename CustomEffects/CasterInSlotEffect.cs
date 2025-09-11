using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterInSlotEffect : EffectSO
    {
        public bool usePreviousExitValue;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (usePreviousExitValue)
            {
                entryVariable = PreviousExitValue;
            }

            exitAmount = caster.SlotID + 1;
            Debug.Log("caster in slot " + exitAmount);
            return entryVariable == caster.SlotID + 1;
        }
    }
}
