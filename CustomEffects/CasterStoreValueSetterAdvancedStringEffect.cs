using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterStoreValueSetterAdvancedStringEffect : EffectSO
    {
        public bool usePreviousExitValue;

        public bool _ignoreIfContains;

        public bool _increment = false;

        public string m_unitStoredDataID = "";

        public string _stringData = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (usePreviousExitValue)
            {
                entryVariable *= PreviousExitValue;
            }
            Debug.Log("storing entry " + entryVariable + " to storage");
            int theValue = caster.SimpleGetStoredValue(m_unitStoredDataID);
            bool flag = caster.SimpleGetStoredValue(m_unitStoredDataID) != 0;
            if (!_ignoreIfContains || !flag)
            {
                caster.SimpleSetStoredValue(m_unitStoredDataID, (_increment ? theValue + entryVariable : entryVariable));
                if (caster.IsUnitCharacter)
                {
                    CharacterCombat ch = caster as CharacterCombat;
                    foreach (var holder in ch.StoredValues)
                    {
                        holder.Value.m_MainString = _stringData;
                    }
                }
                else if (!caster.IsUnitCharacter)
                {
                    EnemyCombat en = caster as EnemyCombat;
                    foreach (var holder in en.StoredValues)
                    {
                        holder.Value.m_MainString = _stringData;
                    }
                }
                Debug.Log("stored as " + entryVariable);
                return true;
            }

            return false;
        }
    }
}
