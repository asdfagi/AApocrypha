using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CheckIsAliveMultiplyByEntryOrPreviousEffect : EffectSO
    {
        public bool _checkByHealth = true;

        public bool _usePreviousExitValue = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (!targetSlotInfo.HasUnit)
                {
                    continue;
                }

                if (_checkByHealth)
                {
                    if (targetSlotInfo.Unit.CurrentHealth > 0)
                    {
                        exitAmount++;
                    }
                }
                else if (targetSlotInfo.Unit.IsAlive)
                {
                    exitAmount++;
                }
            }

            if (_usePreviousExitValue)
            {
                exitAmount *= PreviousExitValue;
                Debug.Log(exitAmount + ", " + PreviousExitValue);
            } 
            else
            {
                exitAmount *= entryVariable;
            }

            return exitAmount > 0;
        }
    }
}
