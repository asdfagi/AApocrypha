using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CopyCasterPassiveToTargetsByMIDEffect : EffectSO
    {
        public string m_passiveID;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            BasePassiveAbilitySO passiveToApply = null;
            if (caster is CharacterCombat ch)
            {
                foreach (BasePassiveAbilitySO passive in ch.PassiveAbilities)
                {
                    if (passive.m_PassiveID == m_passiveID)
                    {
                        passiveToApply = passive;
                        break;
                    }
                }
            } else if (caster is EnemyCombat en)
            {
                foreach (BasePassiveAbilitySO passive in en.PassiveAbilities)
                {
                    if (passive.m_PassiveID == m_passiveID)
                    {
                        passiveToApply = passive;
                        break;
                    }
                }
            }
            if (passiveToApply == null) { return false; }
            foreach (TargetSlotInfo targetSlotInfo in targets)
            {
                if (targetSlotInfo.HasUnit && targetSlotInfo.Unit.AddPassiveAbility(passiveToApply))
                {
                    exitAmount++;
                }
            }

            return exitAmount > 0;
        }
    }
}
