using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class FieldEffectCheckAllEffect : EffectSO
    {
        public List<FieldEffect_SO> _fields = [];
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (target.IsTargetCharacterSlot)
                {
                    IFieldSlotEffector emptyFieldEffector = stats.combatSlots.CharacterSlots[target.SlotID];
                    foreach (IFieldEffect field in emptyFieldEffector.FieldEffects)
                    {
                        foreach (FieldEffect_SO checkField in _fields)
                        {
                            if (field.FieldID == checkField.FieldID)
                            {
                                exitAmount += field.FieldContent;
                            }
                        }
                    }
                } else
                {
                    IFieldSlotEffector emptyFieldEffector = stats.combatSlots.EnemySlots[target.SlotID];
                    foreach (IFieldEffect field in emptyFieldEffector.FieldEffects)
                    {
                        foreach (FieldEffect_SO checkField in _fields)
                        {
                            if (field.FieldID == checkField.FieldID)
                            {
                                exitAmount += field.FieldContent;
                            }
                        }
                    }
                }
            }
            Debug.Log(exitAmount > 0);
            return exitAmount > 0;
        }
    }
}
