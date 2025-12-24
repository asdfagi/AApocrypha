using System;
using System.Collections.Generic;
using System.Text;

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
                foreach (char c in character._currentName)
                {
                    if (c != 'I' && c != 'i')
                    {
                        pun += c;
                    }
                }
                character._currentName = pun;

                foreach (CharacterCombatUIInfo enemyInfo in stats.combatUI._charactersInCombat.Values)
                {
                    if (enemyInfo.SlotID == character.SlotID)
                    {
                        enemyInfo.Name = character._currentName;
                    }
                }
            }


            return true;
        }
    }
}
