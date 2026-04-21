using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterStoredValueChangeWithMaxEffect : EffectSO
    {
        public bool _increase = true;

        public int _minimumValue;

        public int _maximumValue;

        public bool _exitValueIsChange;

        [UnitStoreValueNamesIDsEnumRef]
        public string m_unitStoredDataID = "";

        public bool _usePreviousExitValue;

        public bool _randomBetweenPrevious;

        public int _fixedValue = 0;

        public bool _useFixedValue = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            caster.TryGetStoredData(m_unitStoredDataID, out var holder);
            int mainData = holder.m_MainData;
            if (_usePreviousExitValue)
            {
                entryVariable *= base.PreviousExitValue;
            }
            else if (_randomBetweenPrevious)
            {
                entryVariable = UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1);
            }
            else if (_useFixedValue)
            {
                entryVariable = _fixedValue;
            }

            mainData += (_increase ? entryVariable : (-entryVariable));
            mainData = Mathf.Max(_minimumValue, mainData);
            mainData = Mathf.Min(_maximumValue, mainData);
            if (_exitValueIsChange)
            {
                exitAmount = Mathf.Abs(holder.m_MainData - mainData);
            }
            else
            {
                exitAmount = mainData;
            }

            holder.m_MainData = mainData;
            return true;
        }
    }
}
