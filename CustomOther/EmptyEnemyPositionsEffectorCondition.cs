using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class EmptyEnemyPositionsEffectorCondition : EffectorConditionSO
    {
        public int _spaces = 1;
        public bool _consecutive;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            CombatSlot[] slots = CombatManager.Instance._stats.combatSlots.EnemySlots;
            int counter = 0;
            foreach (CombatSlot slot in slots)
            {
                if (slot.HasUnit)
                {
                    if (_consecutive) { counter = 0; }
                } else
                {
                    counter++;
                }
                if (counter >= _spaces) { return true; }
            }
            return false;
        }
    }
}
