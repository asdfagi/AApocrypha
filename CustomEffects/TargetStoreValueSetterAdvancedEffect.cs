using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetStoreValueSetterAdvancedEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public bool _ignoreIfContains;

        public bool _increment = false;

        public string m_unitStoredDataID = "";

        public int _fixedValue = -1;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            } else if (_fixedValue >= 0)
            {
                entryVariable = _fixedValue;
            }
            foreach (TargetSlotInfo targetSlot in targets)
            {
                if (targetSlot.HasUnit)
                {
                    int theValue = targetSlot.Unit.SimpleGetStoredValue(m_unitStoredDataID);
                    bool flag = targetSlot.Unit.SimpleGetStoredValue(m_unitStoredDataID) != 0;
                    if (!_ignoreIfContains || !flag)
                    {
                        targetSlot.Unit.SimpleSetStoredValue(m_unitStoredDataID, (_increment ? theValue + entryVariable : entryVariable));
                        exitAmount += entryVariable;
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
