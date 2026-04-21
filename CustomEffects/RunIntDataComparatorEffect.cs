using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RunIntDataComparatorEffect : EffectSO
    {
        public string _data;

        public bool _lessThan = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_data == null || _data == "") { return false; }

            int value = CombatManager.Instance._informationHolder.Run.inGameData.GetIntData(_data);
            
            if (_lessThan)
            {
                return value < entryVariable;
            }
            else
            {
                return value >= entryVariable;
            }
        }
    }
}
