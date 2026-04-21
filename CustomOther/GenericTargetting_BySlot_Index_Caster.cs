using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class GenericTargetting_BySlot_Index_Caster : BaseCombatTargettingSO
    {
        public bool getAllies;

        [Header("0 - 1 - 2 - 3 - 4")]
        [Range(0f, 4f)]
        public int[] slotPointerDirections;

        public override bool AreTargetAllies => getAllies;

        public override bool AreTargetSlots => true;

        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            List<TargetSlotInfo> list = new List<TargetSlotInfo>();
            for (int i = 0; i < slotPointerDirections.Length; i++)
            {
                if (getAllies)
                {
                    TargetSlotInfo genericAllySlotTarget = slots.GetGenericAllySlotTarget(slotPointerDirections[i], isCasterCharacter);
                    if (genericAllySlotTarget != null)
                    {
                        list.Add(genericAllySlotTarget);
                    }
                }
                else
                {
                    TargetSlotInfo genericAllySlotTarget = slots.GetGenericOpponentSlotTarget(slotPointerDirections[i], isCasterCharacter);
                    if (genericAllySlotTarget != null)
                    {
                        list.Add(genericAllySlotTarget);
                    }
                }
            }
            list.Add(slots.GetGenericAllySlotTarget(casterSlotID, isCasterCharacter));

            return list.ToArray();
        }
    }
}
