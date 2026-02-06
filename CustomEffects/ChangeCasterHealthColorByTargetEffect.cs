using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ChangeCasterHealthColorByTargetEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (targets.Length > 0)
            {
                if (caster.HealthColor != targets[0].Unit.HealthColor)
                {
                    if (targets[0].HasUnit && caster.ChangeHealthColor(targets[0].Unit.HealthColor))
                    {
                        exitAmount++;
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
