using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterCapitalizeNameEffect : EffectSO
    {
        // code modified from original from Salt Enemies (glass figurine)

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            if (caster is EnemyCombat en)
            {
                en._currentName = en._currentName.ToUpper();
                foreach (EnemyCombatUIInfo enemyCombatUIInfo in stats.combatUI._enemiesInCombat.Values)
                {
                    if (enemyCombatUIInfo.SlotID == en.SlotID)
                    {
                        enemyCombatUIInfo.Name = en._currentName;
                    }
                }
            }
            return true;
        }
    }
}
