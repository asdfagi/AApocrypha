using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    public class RemoveCasterLastTimelineAbilityEffect : EffectSO
    {
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (!caster.IsUnitCharacter)
            {
                EnemyCombat unit = stats.TryGetEnemyOnField(caster.ID);
                int counter = 0;
                int indexCounter = 0;
                int resultID = -1;
                int resultIndex = -1;
                foreach (TurnInfo thingy in stats.timeline.Round)
                {
                    //thingy.turnUnit.HasAbilityID(thingy.abilitySlot);
                    Debug.Log("testing " + thingy.turnUnit.ID + " by comparison to " + unit.ID);
                    if (thingy.turnUnit.ID != unit.ID) {
                        indexCounter++;
                        continue;
                    } 
                    counter++;
                    if (counter == thingy.turnUnit.TurnsInTimeline)
                    {
                        resultID = thingy.abilitySlot;
                        resultIndex = indexCounter;
                        Debug.Log("settled on resultID " + resultID + " and resultIndex " + resultIndex);
                        break;
                    } else
                    {
                        indexCounter++;
                        continue;
                    }
                }

                if (resultID != -1 && resultIndex != -1)
                {
                    stats.timeline.Round.RemoveAt(resultIndex);
                    unit.TurnsInTimeline -= 1;
                    List<int> remIndices = new List<int>();
                    remIndices.Add(resultIndex);
                    CombatManager.Instance.AddUIAction(new RemoveSlotTimelineUIAction(remIndices.ToArray()));
                    CombatManager.Instance.AddUIAction(new UpdateTimelinePointerUIAction(stats.timeline.CurrentTurn));
                    exitAmount = resultID;
                }
            }

            return exitAmount > 0;
        }
    }
}
