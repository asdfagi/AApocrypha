using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class AllTargeting : BaseCombatTargettingSO
    {
        public bool _units = false;
        public override bool AreTargetAllies => true;
        public override bool AreTargetSlots => true;
        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            var res = new List<TargetSlotInfo>();
            
            if (_units)
            {
                res.AddRange(slots.GetAllUnitTargets());
            }
            else
            {
                res.AddRange(slots.GetAllUnitTargetSlots(true, true));
            }
                return [.. res];
        }
    }
}
