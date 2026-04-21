using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace A_Apocrypha.CustomEffects
{
    public class RemoveAmountStatusEffectEffect : EffectSO
    {
        public string statusId;

        public bool entryAsPercentage = false;

        public bool randomBetweenPrevious = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            foreach (var t in targets)
            {
                if (t == null || !t.HasUnit) continue;

                if (t.Unit is not IStatusEffector effector || effector.StatusEffects == null) continue;

                foreach (var st in effector.StatusEffects.ToList())
                {
                    if (!st.IsStatus(statusId)) continue;

                    if (st is not StatusEffect_Holder hold || hold.m_ContentMain <= 0 || hold._Status == null) continue;

                    var oldContent = hold.m_ContentMain;
                    if (entryAsPercentage)
                    {
                        hold.m_ContentMain = Mathf.FloorToInt(hold.m_ContentMain * entryVariable / 100f);
                    }
                    else
                    {
                        if (randomBetweenPrevious)
                        {
                            entryVariable = UnityEngine.Random.Range(PreviousExitValue, entryVariable + 1);
                        }
                        hold.m_ContentMain -= entryVariable;
                    }

                    exitAmount += oldContent - hold.m_ContentMain;

                    if (!hold._Status.TryRemoveStatusEffect(hold, effector) && oldContent != hold.m_ContentMain)
                    {
                        effector.StatusEffectValuesChanged(hold.StatusID, hold.m_ContentMain - oldContent, true);
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
