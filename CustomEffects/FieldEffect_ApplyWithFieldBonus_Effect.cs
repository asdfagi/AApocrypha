using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class FieldEffect_ApplyWithFieldBonus_Effect : EffectSO
    {
        public FieldEffect_SO _Field;

        public bool _UseRandomBetweenPrevious;

        public bool _UsePreviousExitValueAsMultiplier;

        public int _PreviousExtraAddition;

        public int _bonusAmount = 1;

        public bool _bonusStacking;

        public FieldEffect_SO _field;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_Field == null)
            {
                return false;
            }

            if (_UsePreviousExitValueAsMultiplier)
            {
                entryVariable = _PreviousExtraAddition + entryVariable * base.PreviousExitValue;
            }

            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                int amount = entryVariable;
                int bonus = 0;
                if (stats.combatSlots.UnitInSlotContainsFieldEffect(targetSlotInfo.SlotID, targetSlotInfo.IsTargetCharacterSlot, _field.FieldID))
                {

                }
                /*foreach (IFieldEffect field in )
                {
                    if (field.FieldID == _field.FieldID)
                    {
                        if (_bonusStacking)
                        {
                            bonus += _bonusAmount * status.StatusContent;
                        }
                        else
                        {
                            bonus += _bonusAmount;
                        }
                    }
                }*/
                amount += bonus;
                exitAmount += ApplyFieldEffect(stats, targetSlotInfo, entryVariable);
            }

            return exitAmount > 0;
        }

        public int ApplyFieldEffect(CombatStats stats, TargetSlotInfo target, int entryVariable)
        {
            int num = (_UseRandomBetweenPrevious ? UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1) : entryVariable);
            if (num < _Field.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!stats.combatSlots.ApplyFieldEffect(target.SlotID, target.IsTargetCharacterSlot, _Field, num))
            {
                return 0;
            }

            return num;
        }
    }
}
