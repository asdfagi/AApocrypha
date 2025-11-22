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
            if (targets[1].HasUnit && !targets[0].HasUnit)
            {
                if (stats.combatSlots.CanEnemiesSwap(targets[1].SlotID, targets[0].SlotID, out int num, out int num2))
                {
                    if (num >= 5 || num2 >= 5 || num < 0 || num2 < 0)
                    {
                        Debug.LogWarning("Enemy Swapper | Out of Bounds! Skipping...");
                        return false;
                    }
                    bool flag3 = stats.combatSlots.SwapEnemies(targets[1].SlotID, num, targets[0].SlotID, num2, true, "");
                    if (flag3)
                    {
                        exitAmount++;
                        return exitAmount > 0;
                    }
                }
            }
            else
            {
                if (stats.combatSlots.CanEnemiesSwap(targets[0].SlotID, targets[1].SlotID, out int num, out int num2))
                {
                    if (num >= 5 || num2 >= 5 || num < 0 || num2 < 0)
                    {
                        Debug.LogWarning("Enemy Swapper | Out of Bounds! Skipping...");
                        return false;
                    }
                    bool flag3 = stats.combatSlots.SwapEnemies(targets[0].SlotID, num, targets[1].SlotID, num2, true, "");
                    if (flag3)
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
