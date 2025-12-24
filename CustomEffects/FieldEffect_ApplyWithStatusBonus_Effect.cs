using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class FieldEffect_ApplyWithStatusBonus_Effect : EffectSO
    {
        public FieldEffect_SO _Field;

        public StatusEffect_SO _Status;

        public bool _UseRandomBetweenPrevious;

        public int _PreviousExtraAddition;

        public int _bonusAmount = 1;

        public bool _bonusStacking;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_Field == null)
            {
                return false;
            }

            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit)
                {
                    int amount = entryVariable;
                    if (targetSlotInfo.Unit is EnemyCombat targetEN)
                    {
                        int bonus = 0;
                        foreach (IStatusEffect status in targetEN.StatusEffects)
                        {
                            if (status.StatusID == _Status.StatusID)
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
                        }
                        amount += bonus;
                    }
                    else if (targetSlotInfo.Unit is CharacterCombat targetCH)
                    {
                        int bonus = 0;
                        foreach (IStatusEffect status in targetCH.StatusEffects)
                        {
                            if (status.StatusID == _Status.StatusID)
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
                        }
                        amount += bonus;
                    }
                    exitAmount += ApplyFieldEffect(stats, targetSlotInfo, amount);
                }
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
