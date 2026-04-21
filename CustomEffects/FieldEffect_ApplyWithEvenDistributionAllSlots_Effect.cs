using System;
using System.Collections.Generic;
using System.Text;
using HarmonyLib;
using static UnityEngine.GraphicsBuffer;
using Random = UnityEngine.Random;

namespace A_Apocrypha.CustomEffects
{
    public class FieldEffect_ApplyWithEvenDistributionAllSlots_Effect : EffectSO
    {
        public FieldEffect_SO field;
        public bool usePrevious;
        public bool previousIsRange = false;
        public bool _includeCaster = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (usePrevious)
            {
                if (previousIsRange) { entryVariable = UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1); }
                else { entryVariable *= base.PreviousExitValue; }
            }

            if (entryVariable <= 0 || targets.Length <= 0) {
                return false; 
            }

            float val1 = entryVariable;
            float val2 = entryVariable / (_includeCaster ? targets.Length + 1 : targets.Length);
            int value = (int) Math.Max(1, Math.Ceiling(val2));

            foreach (TargetSlotInfo target in targets)
            {
                exitAmount += ApplyFieldEffect(stats, target, value);
            }

            if (_includeCaster && caster.IsUnitCharacter)
            {
                foreach (CombatSlot charSlot in stats.combatSlots.CharacterSlots)
                {
                    if (charSlot.HasUnit)
                    {
                        if (charSlot.Unit.ID == caster.ID)
                        {
                            exitAmount += ApplyFieldEffect(stats, charSlot.TargetSlotInformation, value);
                            break;
                        }
                    }
                }
            }

            if (_includeCaster && !caster.IsUnitCharacter)
            {
                foreach (CombatSlot enemSlot in stats.combatSlots.EnemySlots)
                {
                    if (enemSlot.HasUnit)
                    {
                        if (enemSlot.Unit.ID == caster.ID)
                        {
                            exitAmount += ApplyFieldEffect(stats, enemSlot.TargetSlotInformation, value);
                            break;
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
        public int ApplyFieldEffect(CombatStats stats, TargetSlotInfo target, int entryVariable)
        {
            if (entryVariable < field.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!stats.combatSlots.ApplyFieldEffect(target.SlotID, target.IsTargetCharacterSlot, field, entryVariable))
            {
                return 0;
            }

            return entryVariable;
        }
    }
}