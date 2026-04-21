using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ModifyRunBoolDataEffect : EffectSO
    {
        public string _data;

        public bool _isTrue = true;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_data == null) { return false; }

            bool valueMod = CombatManager.Instance._informationHolder.Run.inGameData.GetBoolData(_data);
            CombatManager.Instance._informationHolder.Run.inGameData.SetBoolData(_data, _isTrue);
            exitAmount = (_isTrue ? 1 : 0);
            //Debug.Log($"ModifyRunBoolDataEffect | run data {_data} is now {CombatManager.Instance._informationHolder.Run.inGameData.GetBoolData(_data)}");
            return true;
        }
    }
}
