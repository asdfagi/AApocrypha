using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterChangeNameEnemyEffect : EffectSO
    {
        // code modified from original from Salt Enemies (glass figurine)
        public string namePoolID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;

            string[] namePool = ["someone forgot to set something"];
            if (namePoolID == "simulacrum")
            {
                namePool =
                [
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Simulacrum",
                    "Hello fellow human!",
                    "How about this weather!",
                    "Not bad for an every day?",
                    "A shame to spoil this weather!",
                ];
            }

            EnemyCombat enemyCombat = caster as EnemyCombat;
            bool flag = enemyCombat != null;
            if (flag)
            {
                int newNameIndex = UnityEngine.Random.Range(0, namePool.Length);
                enemyCombat._currentName = namePool[newNameIndex];
                foreach (EnemyCombatUIInfo enemyCombatUIInfo in stats.combatUI._enemiesInCombat.Values)
                {
                    bool flag2 = enemyCombatUIInfo.SlotID == enemyCombat.SlotID;
                    if (flag2)
                    {
                        enemyCombatUIInfo.Name = enemyCombat._currentName;
                    }
                }
            }
            return true;
        }
    }
}
