using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomEffects
{
    // from the Salt Enemies github repo (EvilDog.cs, ButterflyEffects.cs)
    public class RerollBonusAbilityLastEffect : EffectSO
    {
        public bool _bonusSuite = false;
        public override bool PerformEffect(CombatStats stats, IUnit caster, TargetSlotInfo[] targets, bool areTargetSlots, int entryVariable, out int exitAmount)
        {
            exitAmount = 0;
            if (caster is EnemyCombat unit)
            {
                int counter = 0;
                int indexCounter = 0;
                int resultID = -1;
                int resultIndex = -1;
                foreach (TurnInfo thingy in stats.timeline.Round)
                {
                    //thingy.turnUnit.HasAbilityID(thingy.abilitySlot);
                    Debug.Log("testing " + thingy.turnUnit.ID + " by comparison to " + unit.ID);
                    if (thingy.turnUnit.ID != unit.ID)
                    {
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
                    }
                    else
                    {
                        indexCounter++;
                        continue;
                    }
                }
                if (resultID == -1 || resultIndex == -1) { return false; }

                int extraCount = unit.ExtraAbilities.Count;
                int totalCount = unit.Abilities.Count;
                Debug.Log("total count: " + totalCount + " | extra count: " + extraCount + " | result range: " + (totalCount - extraCount) + " to " + (totalCount - 1));
                int newID = 0;
                newID = (extraCount > 1 ? (totalCount - UnityEngine.Random.Range(0, extraCount) - 1) : totalCount - 1);
                while (_bonusSuite)
                {
                    Debug.Log("new: " + newID + " | old: " + resultID);
                    if (newID != resultID) { break; }
                    newID = totalCount - UnityEngine.Random.Range(0, extraCount) - 1;
                    Debug.Log("rerolled to " + newID);
                }

                TurnInfo rerollAbility = stats.timeline.Round[resultIndex];
                rerollAbility.abilitySlot = newID;
                stats.timeline.Round[resultIndex] = rerollAbility;
                CombatManager.Instance.AddUIAction(new UpdateReRolledSlotTimelineUIAction(unit.ID, [resultIndex], [resultID], [newID]));
            }
            return true;
        }
    }
}
