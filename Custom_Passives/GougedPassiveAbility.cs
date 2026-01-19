using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.Custom_Passives
{
    public class GougedPassiveAbility : PercDmgModPassiveAbility
    {
        public override void OnPassiveConnected(IUnit unit)
        {
            CombatStats stats = CombatManager.Instance._stats;

            if (unit is CharacterCombat character)
            {
                string pun = "";
                bool upper = false;

                foreach (char c in character._currentName)
                {
                    if (c != 'I' && c != 'i')
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
                    if (c == 'I')
                    {
                        upper = true;
                    }
                }
                character._currentName = pun;

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
                string pun = "";
                bool upper = false;

                foreach (char c in enemy._currentName)
                {
                    if (c != 'I' && c != 'i')
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
                    if (c == 'I')
                    {
                        upper = true;
                    }
                }
                enemy._currentName = pun;

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
    }
}
