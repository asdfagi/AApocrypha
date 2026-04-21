using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class SpecificHealthColorAllTargetsCheckEffect : EffectSO
    {
        public ManaColorSO _color;

        public bool _ignorePure = false;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    ManaColorSO healthColor = target.Unit.HealthColor;
                    if (healthColor == _color)
                    {
                        exitAmount++;
                    }
                    else if (_ignorePure && target.Unit.ContainsPassiveAbility(Passives.Pure.m_PassiveID)) 
                    {
                        exitAmount++;
                    } 
                    else { return false; }
                }
            }
            return exitAmount > 0;
        }
    }
}
