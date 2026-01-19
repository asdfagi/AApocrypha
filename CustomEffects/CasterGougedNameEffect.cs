using System;
using System.Collections.Generic;
using System.Text;
using static UnityEngine.UI.CanvasScaler;

namespace A_Apocrypha.CustomEffects
{
    public class CasterGougedNameEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (caster is CharacterCombat character)
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

            if (caster is EnemyCombat enemy)
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

            return true;
        }
    }
}
