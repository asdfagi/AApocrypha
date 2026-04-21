using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RunBoolDataComparatorEffect : EffectSO
    {
        public string _data;

        public bool _matchTrue = true;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_data == null) { return !_matchTrue; }

            bool value = CombatManager.Instance._informationHolder.Run.inGameData.GetBoolData(_data);

            return _matchTrue == value;
        }
    }
}
