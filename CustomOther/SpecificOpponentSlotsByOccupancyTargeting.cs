using System;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using System.Text;
using BrutalAPI;

namespace A_Apocrypha.CustomOther
{
    public class SpecificOpponentSlotsByOccupancyTargeting : BaseCombatTargettingSO
    {
        public bool targetUnitAllySlots;
        public bool getAllUnitSelfSlots;
        public bool _inverted = false;

        public override bool AreTargetAllies => targetUnitAllySlots;
        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            var res = new List<TargetSlotInfo>();

            if (isCasterCharacter)
            {
                foreach (CombatSlot enemySlot in slots.EnemySlots)
                {
                    if (!enemySlot.HasUnit && !_inverted) { continue; }
                    if (enemySlot.HasUnit && _inverted) { continue; }
                    if (!targetUnitAllySlots) { res.Add(enemySlot.TargetSlotInformation); }
                    else if (getAllUnitSelfSlots) { res.Add(slots.CharacterSlots[enemySlot.SlotID].TargetSlotInformation); }
                }
            }
            else
            {
                foreach (CombatSlot charSlot in slots.CharacterSlots)
                {
                    if (charSlot.HasUnit)
                    {
                        if (!charSlot.HasUnit && !_inverted) { continue; }
                        if (charSlot.HasUnit && _inverted) { continue; }
                        if (!targetUnitAllySlots) { res.Add(charSlot.TargetSlotInformation); }
                        else if (getAllUnitSelfSlots) { res.Add(slots.EnemySlots[charSlot.SlotID].TargetSlotInformation); }
                    }
                }
            }

            return [.. res];
        }
    }
}
