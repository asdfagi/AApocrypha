using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class SpawnEnemyAnywhereWithHealthByPreviousEffect : EffectSO
    {
        public EnemySO enemy;

        public bool givesExperience;

        public string _spawnTypeID = "";

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            CombatManager.Instance.AddSubAction(new SpawnEnemyAction(enemy, -1, givesExperience, trySpawnAnyways: false, _spawnTypeID, PreviousExitValue * entryVariable));

            exitAmount = entryVariable;
            return true;
        }
    }
}
