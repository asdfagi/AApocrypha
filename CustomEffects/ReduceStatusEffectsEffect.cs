using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ReduceStatusEffectsEffect : EffectSO
    {
        public bool _decreasePositives = true;

        public bool _decreaseNegatives = true;

        //public bool m_AffectFieldEffects = true;

        //public bool m_AffectStatusEffects = true;

        public List<string> _blacklist = new List<string>();

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (entryVariable <= 0)
            {
                return false;
            }
            entryVariable = Math.Abs(entryVariable) * -1; //clever math trick seen in Salt Enemies (Quest/Unlocks/UnlocksSix.cs - Scalpel Item)

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is CharacterCombat targetCH)
                    {
                        foreach (IStatusEffect status in targetCH.StatusEffects)
                        {
                            if (((status.IsPositive && _decreasePositives) || (!status.IsPositive && _decreaseNegatives)) && !_blacklist.Contains(status.StatusID))
                            {
                                if (status.StatusContent > Math.Abs(entryVariable))
                                {
                                    if (status.TryAddContent(entryVariable, 0))
                                    {
                                        targetCH.StatusEffectValuesChanged(status.StatusID, entryVariable, true);
                                        exitAmount += Math.Abs(entryVariable);
                                    }
                                }
                                else
                                {
                                    exitAmount += targetCH.TryRemoveStatusEffect(status.StatusID);
                                }
                            }
                        }
                    } 
                    else if (target.Unit is EnemyCombat targetEN)
                    {
                        foreach (IStatusEffect status in targetEN.StatusEffects)
                        {
                            if (((status.IsPositive && _decreasePositives) || (!status.IsPositive && _decreaseNegatives)) && !_blacklist.Contains(status.StatusID))
                            {
                                if (status.StatusContent > Math.Abs(entryVariable))
                                {
                                    if (status.TryAddContent(entryVariable, 0))
                                    {
                                        targetEN.StatusEffectValuesChanged(status.StatusID, entryVariable, true);
                                        exitAmount += Math.Abs(entryVariable);
                                    }
                                }
                                else
                                {
                                    exitAmount += targetEN.TryRemoveStatusEffect(status.StatusID);
                                }
                            }
                        }
                    }
                }
            }

            return exitAmount > 0;
        }
    }
}
