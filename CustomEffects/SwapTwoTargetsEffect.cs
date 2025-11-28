using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class SwapTwoTargetsEffect : EffectSO
    {
        public override bool PerformEffect( CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            Debug.Log(targets.Count());
            if (targets.Count() != 2) 
            { 
                return false;
            }
            bool areTargetsEN = false;
            bool areTargetsCH = false;
            if (targets[0].IsTargetCharacterSlot && targets[1].IsTargetCharacterSlot) { areTargetsCH = true; }
            else if (!targets[0].IsTargetCharacterSlot && !targets[1].IsTargetCharacterSlot) { areTargetsEN = true; }
            if (targets[1].HasUnit && !targets[0].HasUnit)
            {
                if (areTargetsEN)
                {
                    if (stats.combatSlots.CanEnemiesSwap(targets[1].SlotID, targets[0].SlotID, out int num, out int num2))
                    {
                        if (num >= 5 || num2 >= 5 || num < 0 || num2 < 0)
                        {
                            Debug.LogWarning("Enemy Swapper | Out of Bounds! Skipping...");
                            return false;
                        }
                        if (stats.combatSlots.SwapEnemies(targets[1].SlotID, num, targets[0].SlotID, num2, true, ""))
                        {
                            exitAmount++;
                            return exitAmount > 0;
                        }
                    }
                }
                else if (areTargetsCH)
                {
                    if (stats.combatSlots.SwapCharacters(targets[1].SlotID, targets[0].SlotID, true, ""))
                    {
                        exitAmount++;
                        return exitAmount > 0;
                    }
                }
            }
            else
            {
                if (areTargetsEN)
                {
                    if (stats.combatSlots.CanEnemiesSwap(targets[0].SlotID, targets[1].SlotID, out int num, out int num2))
                    {
                        if (num >= 5 || num2 >= 5 || num < 0 || num2 < 0)
                        {
                            Debug.LogWarning("Enemy Swapper | Out of Bounds! Skipping...");
                            return false;
                        }
                        if (stats.combatSlots.SwapEnemies(targets[0].SlotID, num, targets[1].SlotID, num2, true, ""))
                        {
                            exitAmount++;
                            return exitAmount > 0;
                        }
                    }
                }
                else if (areTargetsCH)
                {
                    if (stats.combatSlots.SwapCharacters(targets[0].SlotID, targets[1].SlotID, true, ""))
                    {
                        exitAmount++;
                        return exitAmount > 0;
                    }
                }
            }
            return false;
        }
    }
}
