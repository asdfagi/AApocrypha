using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class AddEnemyAbilityFromCasterEffect : EffectSO
    {
        public List<string> _abilityBlacklist = new List<string>();
        public bool _removeFromCaster = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            Debug.Log("Ability Adder | ability adder called");
            //int selectedIndex = 100;

            List<string> localBlacklist = new List<string>();
            localBlacklist.AddRange(_abilityBlacklist);

            /*if (!caster.IsUnitCharacter && stats.IsPlayerTurn && _removeFromCaster)
            {
                Debug.Log("Ability Adder | enemy and it is the player's turn - blacklisting selected ability...");
                EnemyCombat casterEnInit = caster as EnemyCombat;
                foreach (var thingy in stats.timeline.RoundTurnUIInfo)
                {
                    if (thingy.enemyID == casterEnInit.ID)
                    {
                        Debug.Log($"Ability Adder | timeline entry with abilitySlotID {thingy.abilitySlotID} and enemyID {thingy.enemyID} belongs to this enemy");
                        localBlacklist.Add(casterEnInit.Abilities[thingy.abilitySlotID].ability.name);
                        localBlacklist.Add(casterEnInit.Abilities[thingy.abilitySlotID].ability._abilityName);
                        Debug.Log($"Ability Adder | ability {casterEnInit.Abilities[thingy.abilitySlotID].ability._abilityName} ({casterEnInit.Abilities[thingy.abilitySlotID].ability.name}) blacklisted");
                        if (thingy.abilitySlotID < selectedIndex) { selectedIndex = thingy.abilitySlotID; }
                    }
                }
            }*/ //this code is all sorts of weird - the main issue is the selected ability not changing when the others get moved around so it switches around or even goes out of range (which waitlocks, naturally)
            exitAmount = 0;
            List<CombatAbility> abilityRawList = new List<CombatAbility>();
            List<CombatAbility> abilityList = new List<CombatAbility>();
            List<string> repeatPreventionBlacklist = new List<string>();
            if (caster.IsUnitCharacter)
            {
                Debug.Log("Ability Adder | this is a character! adding...");
                CharacterCombat ch = caster as CharacterCombat;
                abilityRawList.AddRange(ch.CombatAbilities);
                Debug.Log("Ability Adder | abilities grabbed");
            } else if (!caster.IsUnitCharacter)
            {
                Debug.Log("Ability Adder | this is an enemy! adding...");
                EnemyCombat en = caster as EnemyCombat;
                abilityRawList.AddRange(en.Abilities);
                Debug.Log("Ability Adder | abilities grabbed");
            }
            foreach (CombatAbility abilityRaw in abilityRawList)
            {
                Debug.Log($"Ability Adder | checking ability {abilityRaw.ability.name}...");
                if (!localBlacklist.Contains(abilityRaw.ability.name) && !localBlacklist.Contains(abilityRaw.ability._abilityName))
                {
                    Debug.Log("Ability Adder | not blacklisted! adding...");
                    abilityList.Add(abilityRaw);
                    Debug.Log("Ability Adder | ability added to list!");
                }
            }
            foreach (TargetSlotInfo target in targets)
            {
                if (target.HasUnit)
                {
                    if (target.Unit is EnemyCombat enemy)
                    {
                        Debug.Log($"Ability Adder | target enemy found ({enemy.Name})");
                        if (enemy.Abilities.Count < entryVariable)
                        {
                            Debug.Log($"Ability Adder | choosing ability to add to {enemy.Name}...");
                            List<CombatAbility> abilityListCopy = new List<CombatAbility>();
                            foreach (CombatAbility ability in abilityList)
                            {
                                if (!repeatPreventionBlacklist.Contains(ability.ability.name))
                                {
                                    abilityListCopy.Add(ability);
                                }
                            }
                            while (abilityListCopy.Count > 1)
                            {
                                int randomIndex = UnityEngine.Random.Range(0, abilityListCopy.Count);
                                abilityListCopy.RemoveAt(randomIndex);
                            }
                            if (abilityListCopy.Count <= 0)
                            {
                                Debug.Log("Ability Adder | no abilities left, repeat blacklist exhausted");
                                return exitAmount > 0;
                            }
                            Debug.Log($"Ability Adder | adding ability {abilityListCopy[0].ability.name} to {enemy.Name}...");
                            enemy.Abilities.Add(abilityListCopy[0]);
                            Debug.Log("Ability Adder | success!");
                            if (_removeFromCaster)
                            {
                                repeatPreventionBlacklist.Add(abilityListCopy[0].ability.name);
                                Debug.Log("Ability Adder | removing ability from caster...");
                                List<CombatAbility> newAbilities = new List<CombatAbility>();
                                if (caster.IsUnitCharacter)
                                {
                                    CharacterCombat ch = caster as CharacterCombat;
                                    foreach (CombatAbility casterAbility in ch.CombatAbilities)
                                    {
                                        if (casterAbility.ability.name != abilityListCopy[0].ability.name)
                                        {
                                            newAbilities.Add(casterAbility);
                                        }
                                    }
                                    ch.CombatAbilities = newAbilities;
                                    /*foreach (TurnUIInfo thingy in stats.timeline.RoundTurnUIInfo)
                                    {
                                        Debug.Log($"enemyID: {thingy.enemyID} (caster.ID: {caster.ID}) - abilitySlotID: {thingy.abilitySlotID} (selectedIndex: {selectedIndex})");
                                        if (thingy.enemyID == caster.ID && thingy.abilitySlotID == selectedIndex && thingy.abilitySlotID > caster.AbilityCount - 1)
                                        {
                                            stats.timeline.RoundTurnUIInfo[thingy.timeSlotID].abilitySlotID -= 1;
                                            Debug.Log(stats.timeline.RoundTurnUIInfo[thingy.timeSlotID].abilitySlotID);
                                        }
                                    }*/
                                    CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(ch.ID));
                                }
                                else if (!caster.IsUnitCharacter)
                                {
                                    EnemyCombat en = caster as EnemyCombat;
                                    foreach (CombatAbility casterAbility in en.Abilities)
                                    {
                                        if (casterAbility.ability.name != abilityListCopy[0].ability.name)
                                        {
                                            newAbilities.Add(casterAbility);
                                        }
                                    }
                                    en.Abilities = newAbilities;
                                    /*foreach (TurnUIInfo thingy in stats.timeline.RoundTurnUIInfo)
                                    {
                                        Debug.Log($"enemyID: {thingy.enemyID} (caster.ID: {caster.ID}) - abilitySlotID: {thingy.abilitySlotID} (selectedIndex: {selectedIndex})");
                                        if (thingy.enemyID == caster.ID && thingy.abilitySlotID == selectedIndex && thingy.abilitySlotID > caster.AbilityCount - 1)
                                        {
                                            stats.timeline.RoundTurnUIInfo[thingy.timeSlotID].abilitySlotID -= 1;
                                            Debug.Log("changed! new value: " + stats.timeline.RoundTurnUIInfo[thingy.timeSlotID].abilitySlotID);
                                        }
                                    }*/
                                    CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(en.ID));
                                }
                            }
                            CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(enemy.ID));
                            exitAmount++;
                        }
                    }
                }
            }
            return exitAmount > 0;
        }
    }
}
