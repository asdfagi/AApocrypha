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
                    if (chAbilities.Count <= 0) { return false; }
                    //Debug.Log("Ability Copying | current list of abilities has a length of " + chAbilities.Count);
                    foreach (CombatAbility thing in chAbilities)
                    {
                        //Debug.Log("Ability Copying | ability " + thing.ability.name + " / " + thing.ability._abilityName);
                    }
                    int randomIndex = UnityEngine.Random.Range(0, chAbilities.Count);
                    //Debug.Log("Ability Copying | attempting to copy " + chAbilities[randomIndex].ability.name + " / " + chAbilities[randomIndex].ability._abilityName);
                    if (CasterPerformAbility(caster, chAbilities[randomIndex].ability))
                    {
                        //Debug.Log("Ability Copying | success!");
                        exitAmount++;
                    }
                }
            }
            return exitAmount > 0;
        }

        public static bool CasterPerformAbility(IUnit caster, AbilitySO ability)
        {
            CombatManager.Instance.AddSubAction(new ShowAttackInformationUIAction(caster.ID, caster.IsUnitCharacter, ability.GetAbilityLocData().text));
            CombatManager.Instance.AddSubAction(new PlayAbilityAnimationAction(ability.visuals, ability.animationTarget, caster));
            CombatManager.Instance.AddSubAction(new EffectAction(ability.effects, caster));
            return true;
        }
    }
}
