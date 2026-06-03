using System;
using System.Collections.Generic;
using System.Text;

namespace A_Apocrypha.CustomOther
{
    public class DamageReceivedWasDirectEffectorCondition : EffectorConditionSO
    {
        public bool _passIfTrue = true;
        public override bool MeetCondition(IEffectorChecks effector, object args)
        {
            if (args is DamageReceivedValueChangeException context)
            {
                return context.directDamage == _passIfTrue;
            }
            return false;
        }
    }
}
