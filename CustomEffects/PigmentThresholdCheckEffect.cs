using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class PigmentThresholdCheckEffect : EffectSO
    {
        public ManaColorSO _color;
        public bool _contains = false;
        public bool _capByPrevious = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            int counter = 0;
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                if (manaSlot.ManaColor != null)
                {
                    if (_contains == false && manaSlot.ManaColor == _color)
                    {
                        counter++;
                    }
                    if (_contains == true && manaSlot.ManaColor.ContainsPigment([_color.pigmentID]))
                    {
                        counter++;
                    }
                }
            }
            if (counter >= (_capByPrevious ? PreviousExitValue : entryVariable))
            {
                exitAmount = counter;
            }
            return exitAmount > 0;
        }
    }
}
