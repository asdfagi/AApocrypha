using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace A_Apocrypha.Custom_Passives
{
    public class GadsbyPassiveAbility : PerformEffectPassiveAbility
    {
        public override void OnPassiveConnected(IUnit unit)
        {
            CombatStats stats = CombatManager.Instance._stats;

            if (unit is CharacterCombat character)
            {
                character._currentName = GadsbyFilter(character._currentName);

                foreach (CharacterCombatUIInfo characterInfo in stats.combatUI._charactersInCombat.Values)
                {
                    if (characterInfo.SlotID == character.SlotID)
                    {
                        characterInfo.Name = character._currentName;
                    }
                }
            }

            if (unit is EnemyCombat enemy)
            {
                enemy._currentName = GadsbyFilter(enemy._currentName);

                foreach (EnemyCombatUIInfo enemyInfo in stats.combatUI._enemiesInCombat.Values)
                {
                    if (enemyInfo.SlotID == enemy.SlotID)
                    {
                        enemyInfo.Name = enemy._currentName;
                    }
                }
            }
        }

        public override void OnPassiveDisconnected(IUnit unit)
        {
            CombatStats stats = CombatManager.Instance._stats;
            //Debug.Log("GOUGED DEBUG | stats acquired");
            if (unit is CharacterCombat character)
            {
                //Debug.Log($"GOUGED DEBUG | attempting to reset character name ({character.Character._characterName})");
                character._currentName = character.Character._characterName;
                //Debug.Log("GOUGED DEBUG | reset!");

                foreach (CharacterCombatUIInfo characterInfo in stats.combatUI._charactersInCombat.Values)
                {
                    if (characterInfo.SlotID == character.SlotID)
                    {
                        characterInfo.Name = character._currentName;
                    }
                }
            }

            if (unit is EnemyCombat enemy)
            {
                //Debug.Log($"GOUGED DEBUG | attempting to reset enemy name ({enemy.Enemy._enemyName})");
                enemy._currentName = enemy.Enemy._enemyName;
                //Debug.Log("GOUGED DEBUG | reset!");

                foreach (EnemyCombatUIInfo enemyInfo in stats.combatUI._enemiesInCombat.Values)
                {
                    if (enemyInfo.SlotID == enemy.SlotID)
                    {
                        enemyInfo.Name = enemy._currentName;
                    }
                }
            }
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
