using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class PigmentAmountAnyCheckEffect : EffectSO
    {
        public int _returnPercentage = 100;
        public bool _roundUp = true;
        public int _staticBonus = 0;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (ManaBarSlot manaSlot in stats.MainManaBar.ManaBarSlots)
            {
                if (manaSlot.ManaColor != null)
                {
                    exitAmount++;
                }
            }
            Debug.Log("Pigment Any Check | exitAmount: " + exitAmount);
            if (exitAmount <= 0) { 
                exitAmount = _staticBonus;
                return exitAmount > 0;
            }
            if (_roundUp)
            {
                exitAmount = (int) Mathf.Ceil(exitAmount * ((float) _returnPercentage / 100.0f));
            }
            else
            {
                exitAmount = (int) Mathf.Floor(exitAmount * ((float) _returnPercentage / 100.0f));
            }
            exitAmount += _staticBonus;
            Debug.Log("Pigment Any Check | new exitAmount: " + exitAmount);
            return exitAmount > 0;
        }
    }
}
