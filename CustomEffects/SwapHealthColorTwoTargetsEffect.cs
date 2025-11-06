using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class SwapHealthColorTwoTargetsEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (targets.Length == 2)
            {
                if (targets[0].HasUnit && targets[1].HasUnit)
                {
                    ManaColorSO color1 = targets[0].Unit.HealthColor;
                    ManaColorSO color2 = targets[1].Unit.HealthColor;
                    if (color1 == color2)
                    {
                        return false;
                    }
                    else
                    {
                        targets[0].Unit.ChangeHealthColor(color2);
                        targets[1].Unit.ChangeHealthColor(color1);
                        exitAmount = 1;
                    }
                }
            }
            else
            {
                return false;
            }

            return exitAmount > 0;
        }
    }
}
