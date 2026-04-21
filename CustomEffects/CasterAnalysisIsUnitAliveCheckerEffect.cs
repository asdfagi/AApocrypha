using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterAnalysisIsUnitAliveCheckerEffect : EffectSO
    {
        public string m_unitStoredDataID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            caster.TryGetStoredData(m_unitStoredDataID, out var holder);
            if (holder.m_MainString == null || holder.m_MainString == "None")
            {
                return false;
            }
            else
            {
                foreach (TargetSlotInfo target in targets)
                {
                    if (target.HasUnit)
                    {
                        if (target.Unit.ID == holder.m_MainData)
                        {
                            if (target.Unit.CurrentHealth <= 0)
                            {
                                return false;
                            }
                            else
                            {
                                exitAmount = target.Unit.ID;
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
