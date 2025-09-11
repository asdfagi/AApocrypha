using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    // code by SpecialAPI from the BOStuffPack github repository
    public class GenerateTargetHealthColorEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach (var t in targets)
            {
                if (t == null || !t.HasUnit)
                    continue;

                t.Unit.GenerateHealthMana(entryVariable);
                exitAmount += entryVariable;
            }

            return exitAmount > 0;
        }
    }
}
