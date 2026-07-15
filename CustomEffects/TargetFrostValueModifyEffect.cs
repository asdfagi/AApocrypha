using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetFrostValueModifyEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public int _fixedValue = -1;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            }
            else if (_fixedValue >= 0)
            {
                entryVariable = _fixedValue;
            }
            foreach (TargetSlotInfo targetSlot in targets)
            {
                if (targetSlot.HasUnit)
                {
                    if (!targetSlot.Unit.ContainsStatusEffect("Frostbite_ID")) { continue; }
                    int theValue = targetSlot.Unit.SimpleGetStoredValue("FrostbiteIntensityStoredValue");
                    if (targetSlot.Unit.SimpleGetStoredValue("FrostbiteIntensityStoredValue") != -1)
                    {
                        targetSlot.Unit.SimpleSetStoredValue("FrostbiteIntensityStoredValue", theValue + entryVariable);
                        exitAmount += entryVariable;
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
