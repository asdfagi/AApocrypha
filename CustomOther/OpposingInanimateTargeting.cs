using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class OpposingInanimateTargeting : BaseCombatTargettingSO
    {
        public override bool AreTargetAllies
        {
            get
            {
                return false;
            }
        }
        public override bool AreTargetSlots
        {
            get
            {
                return false;
            }
        }
        public override TargetSlotInfo[] GetTargets(SlotsCombat slots, int casterSlotID, bool isCasterCharacter)
        {
            List<TargetSlotInfo> list = new List<TargetSlotInfo>();
            foreach (EnemyCombat enemyCombat in CombatManager._instance._stats.EnemiesOnField.Values)
            {
                bool flag = enemyCombat.ContainsPassiveAbility("Inanimate");
                if (flag)
                {
                    for (int i = 0; i < enemyCombat.Size; i++)
                    {
                        list.Add(slots.GetGenericOpponentSlotTarget(enemyCombat.SlotID + i, false));
                    }
                }
            }
            return list.ToArray();
        }

    }
}
