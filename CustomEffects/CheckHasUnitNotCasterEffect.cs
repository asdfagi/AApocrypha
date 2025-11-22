using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CheckHasUnitNotCasterEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (!targets[i].HasUnit)
                {
                    return false;
                }
                else if (targets[i].Unit == caster)
                {
                    return false;
                }

                exitAmount++;
            }

            return exitAmount > 0;
        }
    }
}
