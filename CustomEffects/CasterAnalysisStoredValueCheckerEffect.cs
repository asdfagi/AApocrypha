using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{//TryGetUnitStoreDataToolTip(UnitStoreDataHolder holder, out string result)
    public class CasterAnalysisStoredValueCheckerEffect : EffectSO
    {
        public string m_unitStoredDataID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            bool didit = caster.TryGetStoredData(m_unitStoredDataID, out var holder);
            if (holder.m_MainString == null || holder.m_MainString == "None")
            {
                exitAmount = 0;
                didit = false;
            }
            else
            {
                exitAmount = holder.m_MainData;
            }
            return didit;
        }
    }
}
