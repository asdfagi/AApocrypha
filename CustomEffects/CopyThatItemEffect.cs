using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class CopyThatItemEffect : EffectSO
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
                if (extraAbility.ability.name == "HumanHeartAbility_A")
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
                    copiedAbility.name = "HumanHeartAbility_A";
                    if (copiedAbility.priority == null)
                    {
                        copiedAbility.priority = Priority.Normal;
                    }
                    copiedAbility.abilitySprite = ResourceLoader.LoadSprite("ItemHumanHeartAbility");
                    ExtraAbilityInfo extraAbility = new()
                    {
                        ability = copiedAbility,
                        rarity = Rarity.Impossible,
                        cost = [Pigments.YellowRed, Pigments.YellowBlue],
                    };
                    casterCH.AddExtraAbility(extraAbility);
                }
            }

            //HEALTH COLOR/COLOUR COPYING
            List<ManaColorSO> newHealthColour = new List<ManaColorSO>();

            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    newHealthColour.Add(target.Unit.HealthColor);
                }
            }
            if (newHealthColour.Count > 1)
            {
                if (!newHealthColour.Distinct().Skip(1).Any()) //if the list is all the same health colour, just use that one
                {
                    caster.ChangeHealthColor(newHealthColour[0]);
                }
                else //yes, pigments like PurplePurpleBluePurpleBlue are still allowed, because I think it is funny
                {
                    caster.ChangeHealthColor(Pigments.SplitPigment(newHealthColour.ToArray()));
                }
            }
            else
            {
                caster.ChangeHealthColor(newHealthColour[0]);
            }
            Debug.Log("A Human Heart - Health Colour: " + caster.HealthColor.name);

            exitAmount = 1;
            return exitAmount > 0;
        }
    }
}
