using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class LeftOrRightChanceForNextEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            if (caster.SlotID == 0) { exitAmount = 1; }
            else if (caster.SlotID == 4) { exitAmount = 0; }
            else if (UnityEngine.Random.Range(0, 100) < 50) { exitAmount = 1; }
            else { exitAmount = 0; }
            return exitAmount > 0;
            // 0/false is Left, 1/true is Right
        }
    }
}
