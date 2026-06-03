using System;
using System.Collections.Generic;
using System.Text;
using Yarn;

namespace A_Apocrypha.CustomEffects
{
    public class CasterGetNameLengthPercentageEffect : EffectSO
    {
        public int _percentage = 50;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            int nameLength = 0;
            if (caster is CharacterCombat ch)
            {
                nameLength = ch._currentName.Length;
            }
            else if (caster is EnemyCombat en)
            {
                nameLength = en._currentName.Length;
            }
            if (nameLength <= 0) { return false; }

            float f = _percentage * (float)nameLength / 100f;
            exitAmount = Mathf.Max(0, Mathf.FloorToInt(f));
            return exitAmount > 0;
        }
    }
}
