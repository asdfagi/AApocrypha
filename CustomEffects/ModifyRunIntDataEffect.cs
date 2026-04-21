using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ModifyRunIntDataEffect : EffectSO
    {
        public string _data;

        public bool _additive = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_data == null || _data == "") { return false; }

            int valueMod = CombatManager.Instance._informationHolder.Run.inGameData.GetIntData(_data);
            CombatManager.Instance._informationHolder.Run.inGameData.SetIntData(_data, (_additive ? valueMod + entryVariable : entryVariable));
            exitAmount = (_additive ? valueMod + entryVariable : entryVariable);
            Debug.Log($"ModifyRunIntDataEffect | run data {_data} is now {CombatManager.Instance._informationHolder.Run.inGameData.GetIntData(_data)}");
            return true;
        }
    }
}
