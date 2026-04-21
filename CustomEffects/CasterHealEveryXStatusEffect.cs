using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterHealEveryXStatusEffect : EffectSO
    {
        public bool _directHeal = true;

        public int _threshold = 1;

        public StatusEffect_SO _status;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (_status == null) { return false; }

            int amount = 0;
            int statusAmount = 0;
            if (caster.IsUnitCharacter)
            {
                CharacterCombat casterCH = caster as CharacterCombat;
                foreach (IStatusEffect status in casterCH.StatusEffects)
                {
                    if (status.StatusID == _status.StatusID)
                    {
                        statusAmount = status.StatusContent;
                        break;
                    }
                }
            }
            else if (!caster.IsUnitCharacter)
            {
                EnemyCombat casterEN = caster as EnemyCombat;
                foreach (IStatusEffect status in casterEN.StatusEffects)
                {
                    if (status.StatusID == _status.StatusID)
                    {
                        statusAmount = status.StatusContent;
                        break;
                    }
                }
            }
            //Debug.Log("Heal Debug | status amount: " + statusAmount);
            statusAmount -= statusAmount % _threshold;
            statusAmount /= _threshold;
            //Debug.Log("Heal Debug | heal iterations: " + statusAmount);

            for (int i = 0; i < statusAmount; i++)
            {
                amount += entryVariable;
            }

            if (_directHeal)
            {
                amount = caster.WillApplyHeal(amount, caster);
            }
            exitAmount += caster.Heal(amount, caster, _directHeal);

            return exitAmount > 0;
        }
    }
}
