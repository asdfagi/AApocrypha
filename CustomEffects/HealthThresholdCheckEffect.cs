using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class HealthThresholdCheckEffect : EffectSO
    {
        public bool _aboveThreshold = false;
        public bool _usePreviousAsMult = false;
        public bool _failIfMax = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_usePreviousAsMult) { entryVariable *= base.PreviousExitValue; }
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (!targetSlotInfo.HasUnit)
                {
                    continue;
                }

                int num = targetSlotInfo.Unit.CurrentHealth;
                if (_failIfMax && num >= targetSlotInfo.Unit.MaximumHealth) { continue; }
                if (num >= entryVariable && _aboveThreshold == true)
                {
                    exitAmount += Mathf.Max(num, 1);
                }
                if (num <= entryVariable && _aboveThreshold == false)
                {
                    exitAmount += Mathf.Max(num, 1);
                }
            }

            return exitAmount > 0;
        }
    }
}
