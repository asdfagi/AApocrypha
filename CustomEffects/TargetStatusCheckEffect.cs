using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class TargetStatusCheckEffect : EffectSO
    {
        public StatusEffect_SO _status;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    if (targetSlotInfo.Unit is EnemyCombat targetEN)
                    {
                        foreach (IStatusEffect status in targetEN.StatusEffects)
                        {
                            if (status.StatusID == _status.StatusID)
                            {
                                exitAmount += status.StatusContent;
                            }
                        }
                    }
                    else if (targetSlotInfo.Unit is CharacterCombat targetCH)
                    {
                        foreach (IStatusEffect status in targetCH.StatusEffects)
                        {
                            if (status.StatusID == _status.StatusID)
                            {
                                exitAmount += status.StatusContent;
                            }
                        }
                    }
                }
            }
            return exitAmount > entryVariable;
        }
    }
}
