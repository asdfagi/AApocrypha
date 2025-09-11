using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterStoreValueSetterAdvancedEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public bool _ignoreIfContains;

        public string m_unitStoredDataID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            }
            Debug.Log("storing entry " + entryVariable + " to storage");
            bool flag = caster.SimpleGetStoredValue(m_unitStoredDataID) != 0;
            if (!_ignoreIfContains || !flag)
            {
                caster.SimpleSetStoredValue(m_unitStoredDataID, entryVariable);
                Debug.Log("stored as " + entryVariable);
                return true;
            }

            return false;
        }
    }
}
