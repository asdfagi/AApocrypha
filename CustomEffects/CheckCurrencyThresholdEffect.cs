using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CheckCurrencyThresholdEffect : EffectSO
    {
        public bool _atOrAbove = true;

        public int _threshold = 0;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = stats.PlayerCurrency;
            if (_atOrAbove)
            {
                return stats.PlayerCurrency >= _threshold;
            }
            else
            {
                return stats.PlayerCurrency < _threshold;
            }
        }
    }
}
