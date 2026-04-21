using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class ReRollCasterTimelineAbilityIfBlankEntryEffect : EffectSO
    {
        public bool _useRandomBetween;

        public bool dontRerollIfNoAbilitiesLeft;

        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            bool timelineGaps = false;
            if (stats.timeline.RoundTurnUIInfo.Length <= 0) { return false; }

            foreach (var thingy in stats.timeline.Round)
            {
                if (!thingy.turnUnit.HasAbilityID(thingy.abilitySlot))
                {
                    timelineGaps = true;
                    break;
                }
            }
            if (!caster.IsUnitCharacter && timelineGaps)
            {
                int turnsToReRoll = (_useRandomBetween ? UnityEngine.Random.Range(base.PreviousExitValue, entryVariable + 1) : entryVariable);
                EnemyCombat unit = stats.TryGetEnemyOnField(caster.ID);
                exitAmount += stats.timeline.TryReRollRandomEnemyTurns(unit, turnsToReRoll, dontRerollIfNoAbilitiesLeft);
            }

            return exitAmount > 0;
        }
    }
}
