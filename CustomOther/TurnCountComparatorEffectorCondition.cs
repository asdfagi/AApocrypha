using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class TurnCountComparatorEffectorCondition : EffectorConditionSO
    {
        public int _comparator;
        public bool _lessThan = false;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            int value = CombatManager.Instance._stats.TurnsPassed;
            if (_lessThan)
            {
                return value < _comparator;
            }
            else
            {
                return value >= _comparator;
            }
        }
    }
}
