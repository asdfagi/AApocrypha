using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ShufflePositionsAmongTargetsEffect : EffectSO
    {
        public int MassEnemySwapSwapping(SlotsCombat self, List<int> slots)
        {
            int num = 0;
            List<int> foundSlots = new List<int>();
            List<IUnit> foundUnits = new List<IUnit>();
            foreach (int i in slots)
            {
                if (i < 0 || i >= 5) continue;
                if (!self.EnemySlots[i].HasUnit) continue;
                else if (self.EnemySlots[i].Unit.Size >= 2) continue;
                else if (self.EnemySlots[i].Unit.SlotID == i)
                {
                    if (!self.EnemySlots[i].Unit.CanBeSwapped)
                    {
                        //return 0;
                        continue;
                    }

                    foundUnits.Add(self.EnemySlots[i].Unit);
                    foundSlots.Add(i);
                    num++;
                }
            }

            if (num == 0)
            {
                return 0;
            }

            int pointInArray = 0;
            int newSlotID = foundSlots[pointInArray];
            int[] ret_IDs = new int[foundUnits.Count];
            int[] ret_Slots = new int[foundUnits.Count];
            List<int> swappedSlots = new List<int>();
            int index = (foundUnits.Count > 1) ? UnityEngine.Random.Range(0, foundUnits.Count) : 0;
            while (foundUnits.Count > 0)
            {
                IUnit unit = foundUnits[index];
                foundUnits.RemoveAt(index);
                index = UnityEngine.Random.Range(0, foundUnits.Count);
                bool hasUnit = unit != null && !unit.Equals(null);
                int Size = ((!hasUnit) ? 1 : unit.Size);
                for (int j = 0; j < Size; j++)
                {
                    self.EnemySlots[newSlotID + j].SetUnit(unit);
                }

                if (hasUnit)
                {
                    swappedSlots.Add(newSlotID);
                    ret_IDs[pointInArray] = unit.ID;
                }
                else
                {
                    ret_IDs[pointInArray] = -1;
                }

                ret_Slots[pointInArray] = newSlotID;
                pointInArray++;
                if (foundSlots.Count > pointInArray)
                    newSlotID = foundSlots[pointInArray];
            }

            CombatManager.Instance.AddUIAction(new EnemySlotsHaveSwappedUIAction(ret_IDs, ret_Slots, CombatType_GameIDs.Swap_Mass.ToString()));
            foreach (int item in swappedSlots)
            {
                self.EnemySlots[item].Unit.SwappedTo(item);
            }

            return num;
        }
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<int> ret = new List<int>();
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                int num5 = (targetSlotInfo.HasUnit ? targetSlotInfo.Unit.SlotID : targetSlotInfo.SlotID);
                int num6 = ((!targetSlotInfo.HasUnit) ? 1 : targetSlotInfo.Unit.Size);
                if (targetSlotInfo.IsTargetCharacterSlot)
                {
                    continue;
                }
                else
                {
                    ret.Add(targetSlotInfo.SlotID);
                }
            }

            if (ret.Count > 0)
            {
                exitAmount += MassEnemySwapSwapping(stats.combatSlots, ret);
            }

            return exitAmount > 0;
        }
    }
}
