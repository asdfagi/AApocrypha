using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class CasterRemoveRandomAbilityEffect : EffectSO
    {
        public List<string> _abilityBlacklist = new List<string>();
        public int fixedValue = -1;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            //int selectedIndex = 100;

            List<string> localBlacklist = new List<string>();
            localBlacklist.AddRange(_abilityBlacklist);

            /*if (!caster.IsUnitCharacter && stats.IsPlayerTurn && _removeFromCaster)
            {
                Debug.Log("Ability Remover | enemy and it is the player's turn - blacklisting selected ability...");
                EnemyCombat casterEnInit = caster as EnemyCombat;
                foreach (var thingy in stats.timeline.RoundTurnUIInfo)
                {
                    if (thingy.enemyID == casterEnInit.ID)
                    {
                        Debug.Log($"Ability Remover | timeline entry with abilitySlotID {thingy.abilitySlotID} and enemyID {thingy.enemyID} belongs to this enemy");
                        localBlacklist.Add(casterEnInit.Abilities[thingy.abilitySlotID].ability.name);
                        localBlacklist.Add(casterEnInit.Abilities[thingy.abilitySlotID].ability._abilityName);
                        Debug.Log($"Ability Remover | ability {casterEnInit.Abilities[thingy.abilitySlotID].ability._abilityName} ({casterEnInit.Abilities[thingy.abilitySlotID].ability.name}) blacklisted");
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
                Debug.Log("Ability Remover | this is a character! adding...");
                CharacterCombat ch = caster as CharacterCombat;
                abilityRawList.AddRange(ch.CombatAbilities);
                Debug.Log("Ability Remover | abilities grabbed");
            } else if (!caster.IsUnitCharacter)
            {
                Debug.Log("Ability Remover | this is an enemy! adding...");
                EnemyCombat en = caster as EnemyCombat;
                abilityRawList.AddRange(en.Abilities);
                Debug.Log("Ability Remover | abilities grabbed");
            }
            foreach (CombatAbility abilityRaw in abilityRawList)
            {
                Debug.Log($"Ability Remover | checking ability {abilityRaw.ability.name}...");
                if (!localBlacklist.Contains(abilityRaw.ability.name) && !localBlacklist.Contains(abilityRaw.ability._abilityName))
                {
                    Debug.Log("Ability Remover | not blacklisted! adding...");
                    abilityList.Add(abilityRaw);
                    Debug.Log("Ability Remover | ability added to list!");
                }
            }
            Debug.Log($"Ability Remover | choosing ability to remove");
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
                Debug.Log("Ability Remover | no abilities left, repeat blacklist exhausted");
                return exitAmount > 0;
            }
            repeatPreventionBlacklist.Add(abilityListCopy[0].ability.name);
            Debug.Log("Ability Remover | removing ability from caster...");
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
                CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(en.ID));
            }

            exitAmount++;
            return exitAmount > 0;
        }
    }
}
