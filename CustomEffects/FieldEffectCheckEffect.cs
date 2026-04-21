using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class FieldEffectCheckEffect : EffectSO
    {
        public List<FieldEffect_SO> _fields = [];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            for (int i = 0; i < targets.Length; i++)
            {
                if (!targets[i].HasUnit)
                {
                    continue;
                }
                foreach (FieldEffect_SO fieldEffect in _fields)
                {
                    if (targets[i].Unit.ContainsFieldEffect(fieldEffect._FieldID))
                    {
                        exitAmount++;
                    }
                }
            }
            Debug.Log(exitAmount > 0);
            return exitAmount > 0;
        }
    }
}
