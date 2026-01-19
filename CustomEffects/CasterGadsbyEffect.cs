using System;
using System.Collections.Generic;
using System.Text;
using BrutalAPI;
using static UnityEngine.UI.CanvasScaler;

namespace A_Apocrypha.CustomEffects
{
    public class CasterGadsbyEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (caster is CharacterCombat character)
            {
                character._currentName = GadsbyFilter(character._currentName);
                List<CombatAbility> newAbilities = new List<CombatAbility>();

                foreach (CombatAbility ability in character.CombatAbilities)
                {
                    AbilitySO gadsbyAbility = ability.ability.Clone<AbilitySO>();
                    gadsbyAbility.name = "Gadsby_" + ability.ability.name;
                    gadsbyAbility._abilityName = GadsbyFilter(ability.ability._abilityName);
                    gadsbyAbility._description = GadsbyFilter(ability.ability._description);
                    newAbilities.Add(new CombatAbility(gadsbyAbility, ability.rarity));
                }

                character.CombatAbilities = newAbilities;

                foreach (CharacterCombatUIInfo characterInfo in stats.combatUI._charactersInCombat.Values)
                {
                    if (characterInfo.SlotID == character.SlotID)
                    {
                        characterInfo.Name = character._currentName;
                    }
                }
            }

            if (caster is EnemyCombat enemy)
            {
                enemy._currentName = GadsbyFilter(enemy._currentName);
                List<CombatAbility> newAbilities = new List<CombatAbility>();

                foreach (CombatAbility ability in enemy.Abilities)
                {
                    AbilitySO gadsbyAbility = ability.ability.Clone<AbilitySO>();
                    gadsbyAbility.name = "Gadsby_" + ability.ability.name;
                    gadsbyAbility._abilityName = GadsbyFilter(ability.ability._abilityName);
                    gadsbyAbility._description = GadsbyFilter(ability.ability._description);
                    newAbilities.Add(new CombatAbility(gadsbyAbility, ability.rarity));
                }

                enemy.Abilities = newAbilities;

                foreach (EnemyCombatUIInfo enemyInfo in stats.combatUI._enemiesInCombat.Values)
                {
                    if (enemyInfo.SlotID == enemy.SlotID)
                    {
                        enemyInfo.Name = enemy._currentName;
                    }
                }
            }

            return true;
        }

        public static string GadsbyFilter(string input)
        {
            string pun = "";
            bool upper = false;

            foreach (char c in input)
            {
                if (c != 'E' && c != 'e')
                {
                    if (upper)
                    {
                        pun += Char.ToUpper(c);
                        upper = false;
                    }
                    else
                    {
                        pun += c;
                    }
                }
                if (c == 'E')
                {
                    upper = true;
                }
            }

            return pun;
        }
    }
}
