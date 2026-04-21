using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class PigmentAmountCheckEffect : EffectSO
    {
        public ManaColorSO _color;
        public bool _contains = false;
        public bool _useCasterHealthColor = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_useCasterHealthColor) { _color = caster.HealthColor; }
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                if (manaSlot.ManaColor != null)
                {
                    if (_contains == false && manaSlot.ManaColor == _color)
                    {
                        exitAmount++;
                    }
                    if (_contains == true && manaSlot.ManaColor.ContainsPigment([_color.pigmentID]))
                    {
                        exitAmount++;
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
