using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class CasterMasochismEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            IUnit unit = caster;
            CharacterCombat characterCombat = caster as CharacterCombat;
            EnemyCombat enemyCombat = caster as EnemyCombat;
            if (characterCombat != null)
            {
                characterCombat.RefreshAbilityUse();
                characterCombat.RestoreSwapUse();
                exitAmount++;
            }
            if (enemyCombat != null)
            {
                CombatManager.Instance._stats.timeline.TryAddNewExtraEnemyTurns(enemyCombat, 1);
                exitAmount++;
            }
            return exitAmount > 0;
        }
    }
}
