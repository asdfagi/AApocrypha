using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class FiiFItemEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            // SELECT TARGETS
            List<TargetSlotInfo> targetsList = targets.ToList();
            while (targetsList.Count > entryVariable)
            {
                int randomIndex = UnityEngine.Random.Range(0, targetsList.Count);
                targetsList.RemoveAt(randomIndex);
            }
            targets = targetsList.ToArray();

            // ABILITY CLEARING
            List<CombatAbility> currentAbilities = new List<CombatAbility>();
            if (caster is CharacterCombat casterCH)
            {
                currentAbilities = casterCH.CombatAbilities;
            } else {
                return false;
            }

            List<ExtraAbilityInfo> extraAbilities = new List<ExtraAbilityInfo>();
            if (caster is CharacterCombat)
            {
                extraAbilities = casterCH.ExtraAbilities;
            }
            else
            {
                return false;
            }
            foreach (ExtraAbilityInfo extraAbility in extraAbilities)
            {
                if (extraAbility.ability.name == "FiiFAbility_A")
                {
                    Debug.Log("found ability [" + extraAbility.ability.name + "], deleting...");
                    casterCH.TryRemoveExtraAbility(extraAbility);
                    break;
                }
                else
                {
                    Debug.Log("ability [" + extraAbility.ability.name + "] is clear");
                }
            }

            // ABILITY COPYING
            List<CombatAbility> abilitiesToProcess = new List<CombatAbility>();

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is EnemyCombat enemy)
                    {
                        if (enemy.AbilityCount > 0)
                        {
                            List<CombatAbility> targetAbilitiesCopy = new List<CombatAbility>();
                            targetAbilitiesCopy.AddRange(enemy.Abilities);
                            while (targetAbilitiesCopy.Count > 1)
                            {
                                int randomIndex = UnityEngine.Random.Range(0, targetAbilitiesCopy.Count);
                                targetAbilitiesCopy.RemoveAt(randomIndex);
                            }
                            foreach (CombatAbility abilityCopy in targetAbilitiesCopy)
                            {
                                abilitiesToProcess.Add(abilityCopy);
                            }
                        }
                    }
                }
            }

            List<CombatAbility> abilitiesToAdd = new List<CombatAbility>();
            if (abilitiesToProcess.Count > 0)
            {
                foreach (CombatAbility abilityAdd in abilitiesToProcess)
                {
                    AbilitySO copiedAbility = abilityAdd.ability.Clone();
                    copiedAbility.name = "FiiFAbility_A";
                    if (copiedAbility.priority == null)
                    {
                        copiedAbility.priority = Priority.Normal;
                    }
                    copiedAbility.abilitySprite = ResourceLoader.LoadSprite("ItemFiiFAbility");
                    ExtraAbilityInfo extraAbility = new()
                    {
                        ability = copiedAbility,
                        rarity = Rarity.Impossible,
                        cost = [Pigments.YellowRed, Pigments.YellowBlue],
                    };
                    casterCH.AddExtraAbility(extraAbility);
                }
            }

            exitAmount = 1;
            return exitAmount > 0;
        }
    }
}
