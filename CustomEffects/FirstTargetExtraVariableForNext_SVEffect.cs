using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class FirstTargetExtraVariableForNext_SVEffect : EffectSO
    {
        public UnitStoreData_BasicSO unitStoredData;

        [UnitStoreValueNamesIDsEnumRef]
        public string m_unitStoredDataID = "";
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            bool didit = false;
            if (targets.Length < 1) { return false; }
            if (targets[0].HasUnit)
            {
                Debug.Log("DEBUG | unit detected");
                didit = targets[0].Unit.TryGetStoredData(m_unitStoredDataID, out var holder);
                Debug.Log("DEBUG | stored value found and retrieved");
                exitAmount = holder.m_MainData;
                Debug.Log("DEBUG | stored value data saved");
            }
            return didit;
        }
    }
}
