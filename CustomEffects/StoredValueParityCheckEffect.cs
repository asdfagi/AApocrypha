using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class StoredValueParityCheckEffect : EffectSO
    {
        public string m_unitStoredDataID = "";
        public bool _evenTrue = true;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            int value = caster.SimpleGetStoredValue(m_unitStoredDataID);
            if (_evenTrue) { return 0 == value % 2; }
            else { return 1 == value % 2; }
        }
    }
}
