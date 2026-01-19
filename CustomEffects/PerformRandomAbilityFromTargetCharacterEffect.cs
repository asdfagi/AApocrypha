using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class PerformRandomAbilityFromTargetCharacterEffect : EffectSO
    {
        public List<string> _abilityBlacklist = new List<string>();
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (targets.Length <= 0) { return false; }

            if (targets[0].HasUnit)
            {
                if (targets[0].Unit.IsUnitCharacter)
                {
                    CharacterCombat ch = targets[0].Unit as CharacterCombat;
                    List<CombatAbility> chAbilities = [];
                    foreach (CombatAbility filterAbility in ch.CombatAbilities)
                    {
                        if (!_abilityBlacklist.Contains(filterAbility.ability.name) && !_abilityBlacklist.Contains(filterAbility.ability._abilityName))
                        {
                            chAbilities.Add(filterAbility);
                        }
                    }
                    int randomIndex = UnityEngine.Random.Range(0, chAbilities.Count);
                    if (caster.TryPerformRandomAbility(chAbilities[randomIndex].ability))
                    {
                        exitAmount++;
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
