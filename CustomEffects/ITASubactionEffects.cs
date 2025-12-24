using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    
    public class PerformEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            CombatManager.Instance.AddSubAction(new EffectAction(this.effects, caster, 0));
            return exitAmount > 0;
        }

        public EffectInfo[] effects;
    }

    public class TargetPerformEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit)
                {
                    CombatManager.Instance.AddSubAction(new EffectAction(this.effects, targetSlotInfo.Unit, 0));
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }

        public EffectInfo[] effects;
    }

    public class RandomTargetPerformEffectViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            List<TargetSlotInfo> victims = [];
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                bool hasUnit = targetSlotInfo.HasUnit;
                if (hasUnit && !victims.Contains(targetSlotInfo))
                {
                    victims.Add(targetSlotInfo);
                }
            }
            if (victims.Count > 0)
            {
                CombatManager.Instance.AddSubAction(new EffectAction(this.effects, victims[UnityEngine.Random.Range(0, victims.Count)].Unit, 0));
                exitAmount++;
            }


            return exitAmount > 0;
        }

        public EffectInfo[] effects;
    }

    public class PerformEffectXTimesViaSubaction : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 1;
            if (this.usePreviousExit)
            {
                entryVariable *= base.PreviousExitValue;
            }
            List<EffectInfo> loopedEffects = [];
            for (int i = 0; i < entryVariable; i++)
            {
                foreach (EffectInfo effect in this.effects)
                {
                    loopedEffects.Add(effect);
                }
            }
            CombatManager.Instance.AddSubAction(new EffectAction(loopedEffects.ToArray(), caster, 0));
            return exitAmount > 0;
        }

        public EffectInfo[] effects;
        public bool usePreviousExit;
    }
}
