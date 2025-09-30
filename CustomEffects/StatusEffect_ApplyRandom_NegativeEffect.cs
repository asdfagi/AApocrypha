using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class StatusEffect_ApplyRandom_NegativeEffect : EffectSO
    {
        // given to me by MillieAmp
        // Token: 0x06000019 RID: 25 RVA: 0x00003650 File Offset: 0x00001850
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit)
                {
                    List<StatusEffect_SO> list = new List<StatusEffect_SO>(LoadedDBsHandler._StatusFieldDB._StatusEffects.Values);
                    List<StatusEffect_SO> list2 = new List<StatusEffect_SO>();
                    foreach (StatusEffect_SO statusEffect_SO in list)
                    {
                        bool flag = true;
                        bool flag2 = this.ForceNew && targetSlotInfo.Unit.ContainsStatusEffect(statusEffect_SO.StatusID, 0);
                        if (flag2)
                        {
                            flag = false;
                        }
                        else
                        {
                            bool flag3 = this.ForceHasCount && !statusEffect_SO.TryUseNumberOnPopUp;
                            if (flag3)
                            {
                                flag = false;
                            }
                            else
                            {
                                bool flag4 = !this.AllowPositive && statusEffect_SO.IsPositive;
                                if (flag4)
                                {
                                    flag = false;
                                }
                                else
                                {
                                    bool flag5 = !this.AllowNegative && !statusEffect_SO.IsPositive;
                                    if (flag5)
                                    {
                                        flag = false;
                                    }
                                }
                            }
                        }
                        bool flag6 = flag;
                        if (flag6)
                        {
                            list2.Add(statusEffect_SO);
                        }
                    }
                    bool flag7 = list2.Count < 1;
                    if (flag7)
                    {
                        break;
                    }
                    int entryVariable2 = entryVariable;
                    bool applyBetweenPreviousAndEntry = this.ApplyBetweenPreviousAndEntry;
                    if (applyBetweenPreviousAndEntry)
                    {
                        entryVariable2 = UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1);
                    }
                    exitAmount += this.ApplyStatusEffect(targetSlotInfo.Unit, entryVariable2, list2[UnityEngine.Random.Range(0, list2.Count - 1)]);
                }
            }
            return exitAmount > 0;
        }

        // Token: 0x0600001A RID: 26 RVA: 0x00003804 File Offset: 0x00001A04
        public int ApplyStatusEffect(IUnit unit, int entryVariable, StatusEffect_SO status)
        {
            bool flag = !unit.ApplyStatusEffect(status, entryVariable, 0);
            int result;
            if (flag)
            {
                result = 0;
            }
            else
            {
                result = entryVariable;
            }
            return result;
        }

        // Token: 0x04000013 RID: 19
        public bool ForceNew = false;

        // Token: 0x04000014 RID: 20
        public bool ForceHasCount = false;

        // Token: 0x04000015 RID: 21
        public bool AllowPositive = false;

        // Token: 0x04000016 RID: 22
        public bool AllowNegative = true;

        // Token: 0x04000017 RID: 23
        public bool ApplyBetweenPreviousAndEntry = false;
    }
}
