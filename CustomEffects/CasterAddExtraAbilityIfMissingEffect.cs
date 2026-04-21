using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterAddExtraAbilityIfMissingEffect : EffectSO
    {
        public ExtraAbility_Wearable_SMS _extraAbility;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (!caster.IsUnitCharacter) { return false; }
            CharacterCombat casterCH = caster as CharacterCombat;
            ExtraAbilityInfo extra = new ExtraAbilityInfo(_extraAbility.ExtraAbility);
            bool pass = true;
            foreach (ExtraAbilityInfo ability in casterCH.ExtraAbilities)
            {
                if (ability.ability == _extraAbility.ExtraAbility.ability) { pass = false; }
            }
            if (pass)
            {
                caster.AddExtraAbility(extra);
            }

            return true;
        }
    }
}
