using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class StoredValueComparatorEffect : EffectSO
    {
        public string m_unitStoredDataID = "";
        public bool usePreviousExitValue;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            }
            int value = caster.SimpleGetStoredValue(m_unitStoredDataID);
            Debug.Log("comparator: value is " + value + " compared to entry " + entryVariable);
            return entryVariable == value;
        }
    }
}
