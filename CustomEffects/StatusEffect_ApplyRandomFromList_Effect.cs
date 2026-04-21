using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class StatusEffect_ApplyRandomFromList_Effect : EffectSO
    {
        [Header("Status")]
        public List<StatusEffect_SO> _Statuses;

        [Header("Data")]
        public bool _ApplyToFirstUnit;

        public bool _JustOneRandomTarget;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (_Statuses == null || _Statuses.Count <= 0)
            {
                return false;
            }

            int statusIndex = UnityEngine.Random.Range(0, _Statuses.Count);
            StatusEffect_SO _Status = _Statuses[statusIndex];

            if (_ApplyToFirstUnit || _JustOneRandomTarget)
            {
                List<TargetSlotInfo> list = new List<TargetSlotInfo>();
                foreach (TargetSlotInfo targetSlotInfo in targets)
                {
                    if (targetSlotInfo.HasUnit)
                    {
                        list.Add(targetSlotInfo);
                        if (_ApplyToFirstUnit)
                        {
                            break;
                        }
                    }
                }

                if (list.Count > 0)
                {
                    int index = UnityEngine.Random.Range(0, list.Count);
                    exitAmount += ApplyStatusEffect(_Status, list[index].Unit, entryVariable);
                }
            }
            else
            {
                for (int j = 0; j < targets.Length; j++)
                {
                    if (targets[j].HasUnit)
                    {
                        exitAmount += ApplyStatusEffect(_Status, targets[j].Unit, entryVariable);
                    }
                }
            }

            return exitAmount > 0;
        }

        public int ApplyStatusEffect(StatusEffect_SO status, IUnit unit, int entryVariable)
        {
            int num = (entryVariable);
            if (num < status.MinimumRequiredToApply)
            {
                return 0;
            }

            if (!unit.ApplyStatusEffect(status, num))
            {
                return 0;
            }

            return Mathf.Max(1, num);
        }
    }
}
