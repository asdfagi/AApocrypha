using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomEffects
{
    public class CopyThatItemPassiveEffect : EffectSO
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
            } else if (caster is EnemyCombat casterEN)
            {
                currentAbilities = casterEN.Abilities;
            }
            else
            {
                return false;
            }

            List<ExtraAbilityInfo> extraAbilities = new List<ExtraAbilityInfo>();
            if (caster is CharacterCombat casterCH2)
            {
                extraAbilities = casterCH2.ExtraAbilities;
            } else if (caster is EnemyCombat casterEN2)
            {
                extraAbilities = casterEN2.ExtraAbilities;
            }
            else
            {
                return false;
            }

            foreach (ExtraAbilityInfo extraAbility in extraAbilities)
            {
                if (extraAbility.ability.name == "HumanHeartAbility_A")
                {
                    //Debug.Log("found ability [" + extraAbility.ability.name + "], deleting...");
                    if (caster is CharacterCombat casterCH3) { casterCH3.TryRemoveExtraAbility(extraAbility); }
                    if (caster is EnemyCombat casterEN3) { casterEN3.TryRemoveExtraAbility(extraAbility); }
                    break;
                }
                else
                {
                    //Debug.Log("ability [" + extraAbility.ability.name + "] is clear");
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
                    else if (target.Unit is EnemyCombat fool)
                    {
                        if (fool.AbilityCount > 0)
                        {
                            List<CombatAbility> targetAbilitiesCopy = new List<CombatAbility>();
                            targetAbilitiesCopy.AddRange(fool.Abilities);
                            foreach (CombatAbility abilityCopy in targetAbilitiesCopy)
                            {
                                if (abilityCopy.ability._abilityName == "Slap")
                                {
                                    targetAbilitiesCopy.Remove(abilityCopy);
                                    break;
                                }
                            }
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
                        rarity = Rarity.Common,
                        cost = [Pigments.YellowRed, Pigments.YellowBlue],
                    };
                    caster.AddExtraAbility(extraAbility);
                }
            }

            if (caster is EnemyCombat casterEN4)
            {
                CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(casterEN4.ID));
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
            //Debug.Log("A Human Heart - Health Colour: " + caster.HealthColor.name);

            exitAmount = 1;
            return exitAmount > 0;
        }
        public class RefreshEnemyInfoUIAction : CombatAction
        {
            public int ID;
            public RefreshEnemyInfoUIAction(int id)
            {
                ID = id;
            }
            public override IEnumerator Execute(CombatStats stats)
            {
                EnemyCombat yeah = null;
                foreach (EnemyCombat enemy in stats.EnemiesOnField.Values) if (enemy.ID == ID) yeah = enemy;
                if (yeah != null)
                {
                    foreach (int enID in stats.combatUI._enemiesInCombat.Keys)
                    {
                        EnemyCombatUIInfo enemyInfo;
                        if (stats.combatUI._enemiesInCombat.TryGetValue(enID, out enemyInfo))
                        {
                            if (enemyInfo.SlotID == yeah.SlotID)
                            {
                                enemyInfo.Abilities = yeah.Abilities;
                                //enemyInfo.UpdateAttacks(enemyInfo.Abilities.ToArray());
                                stats.combatUI.TryUpdateAllEnemyAttacks(yeah.ID, yeah.Abilities.ToArray());
                                stats.combatUI.TryUpdateEnemyIDInformation(enID);
                            }
                        }
                    }
                }
                yield return null;
            }
        }
    }
}
