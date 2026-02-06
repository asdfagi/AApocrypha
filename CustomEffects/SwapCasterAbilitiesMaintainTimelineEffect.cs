using System;
using System.Collections.Generic;
using System.Text;
using static A_Apocrypha.CustomEffects.CopyThatEffect;

namespace A_Apocrypha.CustomEffects
{
    public class SwapCasterAbilitiesMaintainTimelineEffect : EffectSO
    {
        [Header("Abilities To Swap Data")]
        public ExtraAbilityInfo[] _abilitiesToSwap;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            bool num = caster.SwapWithExtraAbilities(_abilitiesToSwap);
            if (num && !caster.IsUnitCharacter)
            {
                EnemyCombat casterEN = caster as EnemyCombat;
                stats.timeline.AddConfusionSource();
                stats.timeline.RemoveConfusionSource();
                stats.timeline.TryRemoveNextForgottenAbilityIDs(casterEN);
                CombatManager.Instance.AddUIAction(new RefreshEnemyInfoUIAction(casterEN.ID));
            }

            return num;
        }
    }
}
